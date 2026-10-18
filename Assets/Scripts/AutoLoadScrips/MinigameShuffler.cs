using Godot;
using System.Collections.Generic;

// Global System to handle Shuffling and drawing a random minigame
public partial class MinigameShuffler : Node
{
	/* Thinking Space:
		Need a speed Escalation System
		Need to somehow use a speed variable through all the minigames?
		Need to think about how to get all the child nodes to be able to run random number gen to get the next minigame
	
	
	
	*/

	public static MinigameShuffler Instance { get; private set; }
	private string MinigamesFolderPath = "res://Assets/Scenes/Minigames/";
	public PackedScene CurrMinigame {get; private set;}
	private List<PackedScene> minigames = new();  
	public float MinigameTime { get; private set; }

	// Helper function to get the next minigame (Called externally)
	public bool GetNextMinigame()
	{
		// Just in case, should never be the case
		if (minigames.Count == 0) {
			return false;
		}	

		int randNum = (int)(GD.Randi() % minigames.Count);
		
		PackedScene prevMinigame = null;

		// stores the current minigame into a variable called prev minigame to add back onto the minigames list
		if (CurrMinigame != null) prevMinigame = CurrMinigame;

		// assigns the new minigame to currMinigame
		CurrMinigame = minigames[randNum];

		// adds back the old minigame into the drawing pool
		if (prevMinigame != null) minigames.Add(prevMinigame);

		// returns true if the current Minigane is not null
		return CurrMinigame != null;	
	}

	// Helper function to setup minigmes list
	public void LoadAllMinigames()
	{
		// just to make sure that nothing is in here
		minigames.Clear();
		var dir = DirAccess.Open(MinigamesFolderPath);

		// Safety check
		if (dir == null)
		{
			GD.PrintErr("Could not open directory");
			return;
		}

		string[] files = DirAccess.GetFilesAt(MinigamesFolderPath);

		foreach (string filename in files)
		{
			// making an assumption that all the files in that folder are .tscn, but just in case
			if (filename.EndsWith(".tscn"))
			{
				string fullpath = MinigamesFolderPath.PathJoin(filename);
				PackedScene minigame = GD.Load<PackedScene>(fullpath);
				if (minigame != null)
				{
					minigames.Add(minigame);
					GD.Print($"Loaded scene: {fullpath}");
				}
			}
		}

	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
	}
}
