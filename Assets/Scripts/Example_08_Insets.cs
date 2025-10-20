using Rubix.Unity.Android.Content;

using Rubix.Unity.Android.View;
using UnityEngine.Android;
using UnityEngine.UIElements;

public class Example_08_Insets: Example_Base
{
    public override string ButtonName => "Insets";

    Label m_InsetsStatusBar;
    Label m_InsetsNavBar;

    public override void Initialize(VisualElement content)
    {
        m_InsetsStatusBar = CreateLabel("");
        m_InsetsNavBar = CreateLabel("");

        content.Add(CreateLabel("Status Bar Insets:"));
        content.Add(CreateButton("Show", () => ToggleInsetsVisibility(WindowInsets.Type.StatusBars(), true)));
        content.Add(m_InsetsStatusBar);
        content.Add(CreateButton("Hide", () => ToggleInsetsVisibility(WindowInsets.Type.StatusBars(), false)));


        content.Add(CreateLabel("Nav Bar Insets: "));
        content.Add(CreateButton("Show", () => ToggleInsetsVisibility(WindowInsets.Type.NavigationBars(), true)));
        content.Add(m_InsetsNavBar);
        content.Add(CreateButton("Hide", () => ToggleInsetsVisibility(WindowInsets.Type.NavigationBars(), false)));   
    }

    private void ToggleInsetsVisibility(int insetType, bool show)
    {
        var activity = Rubix.Unity.Android.App.Activity.CurrentActivity;
        if (activity == null)
            return;

        AndroidApplication.InvokeOnUIThread(() =>
        {
            using var controller = (WindowInsetsController)activity.GetWindow().GetInsetsController();
            if (show)
                controller.Show(insetType);
            else
                controller.Hide(insetType);
        });
    }

    private string GetInsets(WindowInsets insets, int insetType)
    {
        if (insets != null)
        {
            var result = insets.GetInsets(insetType);
            return result.Call<string>("toString");
        }
        else
        {
            return "Not available";
        }
    }

    protected override void Update()
    {
        var activity = Rubix.Unity.Android.App.Activity.CurrentActivity;
        if (activity == null)
            return;
        var decorView = activity.GetWindow().GetDecorView();
        var insets = decorView.GetRootWindowInsets();
        m_InsetsStatusBar.text = GetInsets(insets, WindowInsets.Type.StatusBars());
        m_InsetsNavBar.text = GetInsets(insets, WindowInsets.Type.NavigationBars());
    }
}
