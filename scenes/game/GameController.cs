using Godot;
using System;

public partial class GameController : Node3D
{
	public override void _EnterTree()
	{
		EventBus.Instance.LanguageSelectedEvent += OnLanguageSelected;
	}

	void OnLanguageSelected(string _)
	{
		GD.Print("testsrA");
	}
}

public enum GameState
{
	IN_MAIN_MENU,
	IN_GAME,
	GAME_FINISHED,
}
