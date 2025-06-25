using Godot;
using System;
using System.Diagnostics;

public partial class RotationY : Node3D
{
	[Export] float RotationSpeed;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		RotateY(RotationSpeed * (float)delta);
	}
}
