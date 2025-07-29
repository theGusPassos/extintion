using System.Collections.Generic;

public partial class LocalizationMenu : CanvasLayer
{
    [Export] PackedScene languageButton;
    [Export] Container container;

    readonly Dictionary<string, string> languages = new()
    {
        ["en"] = "English",
        ["pt-br"] = "Português",
    };

    public override void _Ready()
    {
        foreach (var (key, language) in languages)
        {
            var button = languageButton.Instantiate<Button>();
            button.Text = language;
            button.Name = $"{language}_button";

            button.Pressed += () => OnLanguageSelected(key);

            container.AddChild(button);
        }
    }

    void OnLanguageSelected(string languageCode)
    {
        TranslationServer.SetLocale(languageCode);
        EventBus.Instance.OnLanguageSelected(languageCode);
    }
}