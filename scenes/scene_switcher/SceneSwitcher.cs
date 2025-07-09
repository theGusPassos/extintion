using System.Security.Cryptography;
using Godot;

public partial class SceneSwitcher : Node
{
	public static SceneSwitcher Instance { get; private set;}

	[Export] AnimationPlayer fadeAnimation;
	[Export] Node currentScene;
	Node nextScene;

    public override void _EnterTree()
    {
		if (Instance != null && Instance != this)
		{
			QueueFree();
			return;
		}

		Instance = this;
    }

    public override void _Ready()
    {
		fadeAnimation.Play("fade_out");
		fadeAnimation.AnimationFinished += OnFadeFinished;
    }

	void OnFadeFinished(StringName animation)
	{
		if (animation == "fade_in")
		{
			currentScene.QueueFree();
			currentScene = nextScene;
			AddChild(nextScene);
			nextScene = null;

			fadeAnimation.Play("fade_out");
		}
	}

    public override void _Process(double delta)
    {
		if (Input.IsKeyPressed(Key.A))
		{
			RestartGame();
		}
    }


	public void LoadGameScene()
	{
		nextScene = GD.Load<PackedScene>("res://scenes/game/game_scene.tscn").Instantiate();
		fadeAnimation.Play("fade_in");
	}

	public void RestartGame()
	{
		LoadGameScene();
	}
}
