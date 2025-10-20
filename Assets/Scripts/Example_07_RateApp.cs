using Rubix.Unity.Android.Content;
using Rubix.Unity.Android.Net;
using UnityEngine.UIElements;

public class Example_07_RateApp: Example_Base
{
    public override string ButtonName => "Rate App";

    private string m_ThisPackageName;
    private string m_GoogleChromePackageName;

    private void RateApp(string packageName)
    {
        using var intent = new Intent(Intent.ACTION_VIEW, Uri.Parse("market://details?id=" + packageName));
        Context.CurrentContext.StartActivity(intent);
    }

    public override void Initialize(VisualElement content)
    {
        m_ThisPackageName = Context.CurrentContext != null ? Context.CurrentContext.GetPackageName() : "<None>";
        m_GoogleChromePackageName = "com.android.chrome";
        content.Add(CreateButton($"Rate This App\n({m_ThisPackageName})", () => RateApp(m_ThisPackageName)));
        content.Add(CreateButton($"Rate Google Chrome\n({m_GoogleChromePackageName})", () => RateApp(m_GoogleChromePackageName)));
    }
}
