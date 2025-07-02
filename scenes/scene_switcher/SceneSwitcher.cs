using Godot;

public partial class SceneSwitcher : Node
{
	public static SceneSwitcher Instance { get; private set;}

	Node currentScene;

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
		// Select Language scene
		currentScene = GetChild(0);
    }

	public void LoadGameScene()
	{
		var gameScene = GD.Load<PackedScene>("res://scenes/game/game_scene.tscn").Instantiate();
		AddChild(gameScene);
		currentScene.QueueFree();
	}
}
