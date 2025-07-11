using System.Collections.Generic;
using Godot;

public partial class TextShower : CanvasLayer
{
	[Export] AnimationPlayer controlAlphaAnimation;
	[Export] float timeToShowFirstDialog;
	[Export] RichTextLabel textLabel;
	[Export] float timePerCharacter;
	[Export] Button nextDialogButton;

	bool isTypingDialog;
	float timeToWait;
	string[] dialogsToShow;
	int currentDialog;
	int currentLetter;
	float timeToShowNextChar;

	bool FinishedTypingDialog => currentLetter == dialogsToShow[currentDialog].Length;
	bool FinishedAllDialogs => currentDialog == dialogsToShow.Length - 1;

	public override void _Ready()
	{
		controlAlphaAnimation.Play("fade_in");
		nextDialogButton.Visible = true;
		nextDialogButton.Pressed += OnNextDialogPressed;

		LoadDialogsToShow();

		currentDialog = currentLetter = 0;
		textLabel.Text = dialogsToShow[currentDialog];
	}

	void LoadDialogsToShow()
	{
		var locale = TranslationServer.GetLocale();
		GD.Print($"loading locale {locale}");
		using var file = FileAccess.Open($"res://data/{locale}.txt", FileAccess.ModeFlags.Read);

		var allLines = new List<string>();
		while (!file.EofReached())
		{
			allLines.Add(file.GetLine());
		}
		dialogsToShow = allLines.ToArray();
	}

	public override void _Process(double delta)
	{
	}

	void OnNextDialogPressed()
	{
		if (FinishedAllDialogs && FinishedTypingDialog)
		{
			EventBus.Instance.OnDialogsFinished();
			controlAlphaAnimation.Play("fade_out");
			return;
		}
		else
		{
			EventBus.Instance.OnDialogRead();

			currentDialog++;
			textLabel.Text = dialogsToShow[currentDialog];
		}
	}
}
