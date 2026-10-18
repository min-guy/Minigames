using Godot;
using System;
using System.Dynamic;

// System for handling speed and Escalation mechanic
public partial class SpeedEscalation : Node
{
	public static SpeedEscalation Instance { get; private set; }

	public int Intensity {get; private set;}
	
	// Increment the Intensity Value
	public void IncrementIntensity(int value = 1){
		Intensity += value;
	}

	/// <summary>
	/// For calculating the initial speed, aa range will be calculated.
	/// Where inputs will be a float min scale and max scale value
	/// Then, (maxRange - minRange) * Randfloat(0, 1)
	/// Initial speed = minValue + variance
	/// </summary>
	public void CalculateInitialSpeed()
	{
		
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
	}
}
