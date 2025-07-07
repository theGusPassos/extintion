using Godot;
using System;

public partial class CameraMovement : Camera3D
{
	[Export] float movementSpeed;
	[Export] Vector3 mainMenuPosition;
	[Export] Vector3 positionInGame;

	bool isMovingToInGamePos;
	float movementT;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Position = mainMenuPosition;
		movementT = 0;
	}

	public override void _Process(double delta)
	{
		if (isMovingToInGamePos)
		{
			movementT += (float)delta * movementSpeed;
			Position = mainMenuPosition.Lerp(positionInGame, movementT);
		}
	}

	public void GotToGamePosition()
	{
		Tween tween = CreateTween();
		tween.TweenProperty(this, "position", positionInGame, 3.5f);
	}
}
