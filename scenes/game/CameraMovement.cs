using Godot;
using System;

public partial class CameraMovement : Camera3D
{
	[Export] float animationTime;
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
			movementT += (float)delta;
			if (movementT >= animationTime)
			{
				movementT = animationTime;
				isMovingToInGamePos = false;
			}

			Position = mainMenuPosition.Lerp(positionInGame, EaseOutCubic(movementT / animationTime));
		}
	}

	static float EaseOutCubic(float x) {
		return 1 - (float)Math.Pow(1 - x, 3);
	}

	public void GotToGamePosition()
	{
		Tween tween = CreateTween();
		tween.TweenProperty(this, "position", positionInGame, 3.5f);
	}
}
