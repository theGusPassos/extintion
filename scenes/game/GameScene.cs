using System.Threading.Tasks;
using Godot;

public partial class GameScene : Node3D
{
	[Export] CameraMovement cameraMovement;
	[Export] PackedScene textShowerScene;
	[Export] float timeBeforeShowingText;
	[Export] float timeToShowBlackout;
	[Export] ColorRect blackoutEndgame;

	public override void _Ready()
	{
		EventBus.Instance.ObservePlanetEvent += OnObservePlanetClicked;
		EventBus.Instance.DialogsFinishedEvent += OnLastDialogRead;
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

	void OnLastDialogRead()
	{
		var _ = ShowBlackoutAfterTimeAsync();
	}

	async Task ShowBlackoutAfterTimeAsync()
	{
		await ToSignal(GetTree().CreateTimer(timeToShowBlackout), SceneTreeTimer.SignalName.Timeout);
		blackoutEndgame.Visible = true;

		EventBus.Instance.OnGameEnded();
	}
}
