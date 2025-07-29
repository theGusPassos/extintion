using System.Collections.Generic;
using System.Linq;

public partial class TextShower : CanvasLayer
{
    [Export] AnimationPlayer controlAlphaAnimation;
    [Export] float timeToShowFirstDialog;
    [Export] RichTextLabel textLabel;
    [Export] Button nextDialogButton;

    bool isTypingDialog;
    float timeToWait;
    string[] dialogsToShow;
    int currentDialogIndex;
    int currentLetter;
    float timeToShowNextChar;

    bool FinishedAllDialogs => currentDialogIndex == dialogsToShow.Length - 1;

    string CurrentDialog => dialogsToShow is [] ? string.Empty : dialogsToShow[currentDialogIndex];

    public override void _Ready()
    {
        controlAlphaAnimation.Play(AnimationName.FadeIn);
        nextDialogButton.Visible = true;
        nextDialogButton.Pressed += OnNextDialogPressed;

        dialogsToShow = LoadDialogsToShow().ToArray();
        currentDialogIndex = currentLetter = 0;
        textLabel.Text = CurrentDialog;
    }

    static IEnumerable<string> LoadDialogsToShow()
    {
        var locale = TranslationServer.GetLocale();
        GD.Print($"loading locale {locale}");
        using var file = FileAccess.Open($"res://data/{locale}.txt", FileAccess.ModeFlags.Read);

        while (!file.EofReached())
        {
            var line = file.GetLine();
            if (!string.IsNullOrEmpty(line))
                yield return file.GetLine();
        }
    }

    void OnNextDialogPressed()
    {
        if (FinishedAllDialogs)
        {
            EventBus.Instance.OnDialogsFinished();
            controlAlphaAnimation.Play(AnimationName.FadeOut);
            return;
        }

        EventBus.Instance.OnDialogRead();

        currentDialogIndex = Mathf.Clamp(currentDialogIndex + 1, 0, dialogsToShow.Length - 1);
        textLabel.Text = CurrentDialog;
    }
}