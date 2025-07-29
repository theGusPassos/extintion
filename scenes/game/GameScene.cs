public partial class GameScene : Node3D
{
    [Export] CameraMovement cameraMovement;
    [Export] PackedScene textShowerScene;
    [Export] float timeBeforeShowingText;
    [Export] float timeToShowBlackout;
    [Export] ColorRect blackoutEndgame;

    public override void _Ready()
    {
        EventBus.Instance.ObservePlanet += OnObservePlanetClicked;
        EventBus.Instance.DialogsFinished += OnLastDialogRead;
    }

    void OnObservePlanetClicked()
    {
        cameraMovement.GotToGamePosition();
        WaitAndShowGameText();
    }

    void WaitAndShowGameText()
    {
        var timer = GetTree().CreateTimer(timeBeforeShowingText);
        timer.Timeout += () => AddChild(textShowerScene.Instantiate());
    }

    void OnLastDialogRead()
    {
        var timer = GetTree().CreateTimer(timeToShowBlackout);
        timer.Timeout += () =>
        {
            blackoutEndgame.Visible = true;
            EventBus.Instance.OnGameEnded();
        };
    }
}