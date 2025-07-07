using System.Threading.Tasks;
using Godot;

public partial class GameScene : Node3D
{
	[Export] CameraMovement cameraMovement;
	[Export] PackedScene textShowerScene;
	[Export] float timeBeforeShowingText;

	public override void _Ready()
	{
		EventBus.Instance.ObservePlanetEvent += OnObservePlanetClicked;
	}

	void OnObservePlanetClicked()
	{
		cameraMovement.GotToGamePosition();
		_ = WaitAndShowGameTextAsync();
	}

	async Task WaitAndShowGameTextAsync()
	{
		await ToSignal(GetTree().CreateTimer(timeBeforeShowingText), SceneTreeTimer.SignalName.Timeout);
		AddChild(textShowerScene.Instantiate());
	}
}
