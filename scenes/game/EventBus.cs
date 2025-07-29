public partial class EventBus : Node
{
    public static EventBus Instance { get; private set; }

    [Signal]
    public delegate void LanguageSelectedEventHandler(string languageCode);

    [Signal]
    public delegate void ObservePlanetEventHandler();

    [Signal]
    public delegate void DialogReadEventHandler();

    [Signal]
    public delegate void DialogsFinishedEventHandler();

    [Signal]
    public delegate void GameEndedEventHandler();

    public override void _EnterTree()
    {
        if (IsInstanceValid(Instance) && !Instance.IsQueuedForDeletion() && Instance != this)
        {
            QueueFree();
            return;
        }

        Instance = this;
    }

    public void OnLanguageSelected(string languageCode) => EmitSignalLanguageSelected(languageCode);
    public void OnObservePlanetButtonPressed() => EmitSignalObservePlanet();
    public void OnDialogRead() => EmitSignalDialogRead();
    public void OnDialogsFinished() => EmitSignalDialogsFinished();
    public void OnGameEnded() => EmitSignalGameEnded();
}