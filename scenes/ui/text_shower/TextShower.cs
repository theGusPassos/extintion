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
		nextDialogButton.Visible = false;
		nextDialogButton.Pressed += OnNextDialogPressed;

		textLabel.Text = string.Empty;
		currentDialog = currentLetter = 0;
		timeToWait = timeToShowFirstDialog;
		timeToShowNextChar = timePerCharacter;

		LoadDialogsToShow();
		isTypingDialog = true;
	}

	void LoadDialogsToShow()
	{
		var locale = TranslationServer.GetLocale();
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
		if (timeToWait > 0)
		{
			timeToWait -= (float)delta;
			return;
		}

		if (!isTypingDialog)
		{
			// finished current dialog
			return;
		}

		if (timeToShowNextChar > 0)
		{
			timeToShowNextChar -= (float)delta;
			return;
		}

		var letter = dialogsToShow[currentDialog][currentLetter];
		textLabel.Text += letter;
		timeToShowNextChar = letter == ' '
			? 0
			: timePerCharacter;
		currentLetter++;

		if (FinishedTypingDialog)
		{
			isTypingDialog = false;
			nextDialogButton.Visible = true;
		}
	}

	void OnNextDialogPressed()
	{
		if (FinishedAllDialogs && FinishedTypingDialog)
		{
			EventBus.Instance.OnDialogsFinished();
			controlAlphaAnimation.Play("fade_out");
			GD.PushWarning("finished");
			return;
		}

		if (FinishedTypingDialog)
		{
			EventBus.Instance.OnDialogRead();

			textLabel.Text = string.Empty;
			currentDialog++;
			currentLetter = 0;
			isTypingDialog = true;
		}
		else
		{
			// skip typing and just show the full dialog
			isTypingDialog = false;
			textLabel.Text = dialogsToShow[currentDialog];
			currentLetter = dialogsToShow[currentDialog].Length;
		}
	}
}
