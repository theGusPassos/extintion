public partial class GameController : Node3D
{
    const float TimeToResetGameAfterEnd = 2f;

    public override void _EnterTree()
    {
        EventBus.Instance.LanguageSelected += OnLanguageSelected;
        EventBus.Instance.GameEnded += OnGameEnded;
    }

    void OnLanguageSelected(string _) => SceneSwitcher.Instance.LoadGameScene();

    void OnGameEnded()
    {
        var timer = GetTree().CreateTimer(TimeToResetGameAfterEnd);
        timer.Timeout += () => SceneSwitcher.Instance.RestartGame();
    }
}

public enum GameState
{
    InMainMenu,
    InGame,
    GameFinished,
}