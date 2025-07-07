using Godot;
using System;

public partial class TextShower : CanvasLayer
{
	[Export] AnimationPlayer controlAlphaAnimation;

	public override void _Ready()
	{
		controlAlphaAnimation.Play("fade_in");
	}
}
