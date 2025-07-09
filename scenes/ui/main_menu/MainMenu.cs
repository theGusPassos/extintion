using Godot;

public partial class MainMenu : Node
{
	[Export] Button button;
	[Export] AnimationPlayer controlAlphaAnimation;

    public override void _Ready()
    {
		button.Pressed += OnObservePlanetButtonPressed;
    }

	void OnObservePlanetButtonPressed()
	{
		button.Disabled = true;
		controlAlphaAnimation.Play("fade_out");
		EventBus.Instance.OnObservePlanetButtonPressed();
	}
}
