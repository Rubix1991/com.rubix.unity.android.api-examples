using Rubix.Unity.Android.Content;
using Rubix.Unity.Android.Net;
using Rubix.Unity.Android.Provider;
using Rubix.Unity.Java.Net;
using UnityEngine.UIElements;

public class Example_05_Browser : Example_Base
{
    public override string ButtonName => "Browser";

    public override void Initialize(VisualElement content)
    {
        content.Add(CreateButton("Open Browser", () =>
        {
            Uri webpage = Uri.Parse("http://www.google.com");
            Intent intent = new Intent(Intent.ACTION_VIEW, webpage);
            Context.CurrentContext.StartActivity(intent);
        }));
    }
}
