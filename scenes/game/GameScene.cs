using Godot;

public partial class GameScene : Node3D
{
	[Export] CameraMovement cameraMovement;

	public override void _Ready()
	{
		EventBus.Instance.ObservePlanetEvent += OnObservePlanetClicked;
	}

	void OnObservePlanetClicked()
	{
		// move camera,
		cameraMovement.GotToGamePosition();
		// wait and spawn text shower
		// load text from .d and send to text shower
		// once all texts are done, calls GameController so it can reset the game
	}
}
