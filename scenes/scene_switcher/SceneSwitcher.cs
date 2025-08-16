public partial class SceneSwitcher : Node
{
	public static SceneSwitcher Instance { get; private set; }

	[Export] AnimationPlayer fadeAnimation;
	[Export] Node currentScene;
	Node nextScene;

	public override void _EnterTree()
	{
		if (IsInstanceValid(Instance) && !Instance.IsQueuedForDeletion() && Instance != this)
		{
			QueueFree();
			return;
		}

		Instance = this;
	}

	public override void _Ready()
	{
		fadeAnimation.Play(AnimationName.FadeOut);
		fadeAnimation.AnimationFinished += OnFadeFinished;
	}

	void OnFadeFinished(StringName animation)
	{
		if (animation == AnimationName.FadeIn)
		{
			if (!IsInstanceValid(nextScene)) return;
			currentScene.QueueFree();
			currentScene = nextScene;
			AddChild(nextScene);
			nextScene = null;
			
			fadeAnimation.Play(AnimationName.FadeOut);
		}
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey { Pressed: true, Keycode: Key.A })
		{
			RestartGame();
		}
	}


	public void LoadGameScene()
	{
		nextScene = GD.Load<PackedScene>("res://scenes/game/game_scene.tscn").Instantiate();
		fadeAnimation.Play(AnimationName.FadeIn);
	}

	public void RestartGame() => LoadGameScene();
}
