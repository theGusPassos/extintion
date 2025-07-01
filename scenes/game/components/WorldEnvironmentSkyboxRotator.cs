using Godot;
using System;

public partial class WorldEnvironmentSkyboxRotator : WorldEnvironment
{
	[Export] float rotationSpeed;

	public override void _Process(double delta)
	{
		var currRotation = Environment.SkyRotation;
		currRotation.Y += (float)delta * rotationSpeed;
		currRotation.Y = Mathf.Wrap(currRotation.Y, 0, Mathf.Tau);
		Environment.SkyRotation = currRotation;
	}
}
