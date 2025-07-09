using Godot;
using System;
using System.Threading.Tasks;

public partial class GameController : Node3D
{
	const float timeToResetGameAfterEnd = 2f;

	public override void _EnterTree()
	{
		EventBus.Instance.LanguageSelectedEvent += OnLanguageSelected;
		EventBus.Instance.GameEndedEvent += OnGameEnded;
	}

	void OnLanguageSelected(string _)
	{
		SceneSwitcher.Instance.LoadGameScene();
	}

	void OnGameEnded()
	{
		var _ = ResetGameAfterTimeAsync();
	}

	async Task ResetGameAfterTimeAsync()
	{
		await ToSignal(GetTree().CreateTimer(timeToResetGameAfterEnd), SceneTreeTimer.SignalName.Timeout);
		SceneSwitcher.Instance.RestartGame();
	}
}

public enum GameState
{
	IN_MAIN_MENU,
	IN_GAME,
	GAME_FINISHED,
}
