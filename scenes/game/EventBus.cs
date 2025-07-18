using Godot;
using System;

public partial class EventBus : Node
{
	public static EventBus Instance { get; private set;}

    public event Action<string> LanguageSelectedEvent;
    public event Action ObservePlanetEvent;
    public event Action DialogReadEvent;
    public event Action DialogsFinishedEvent;
    public event Action GameEndedEvent;

    public override void _EnterTree()
    {
		if (Instance != null && Instance != this)
		{
			QueueFree();
			return;
		}

		Instance = this;
    }

	public void OnLanguageSelected(string languageCode)
	{
		LanguageSelectedEvent?.Invoke(languageCode);
	}

	public void OnObservePlanetButtonPressed()
	{
		ObservePlanetEvent?.Invoke();
	}

	public void OnDialogRead()
	{
		DialogReadEvent?.Invoke();
	}

	public void OnDialogsFinished()
	{
		DialogsFinishedEvent?.Invoke();
	}

	public void OnGameEnded()
	{
		GameEndedEvent?.Invoke();
	}
}
