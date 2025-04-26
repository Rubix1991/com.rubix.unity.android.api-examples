using UnityEngine.UIElements;
using Rubix.Unity.Android.Widget;
using Rubix.Unity.Android.App;

public class Example_01_Toasts : Example_Base
{
    public override string ButtonName => "Toasts";

    public override void Initialize(VisualElement content)
    {
        content.Add(CreateButton("Short Toast", () =>
        {
            using var toast = Toast.MakeText(Activity.CurrentActivity, 
                "This is a short toast",
                Toast.LENGTH_SHORT);
            toast.Show();
        }));


        content.Add(CreateButton("Long Toast", () =>
        {
            using var toast = Toast.MakeText(Rubix.Unity.Android.App.Activity.CurrentActivity,
                "This is a long toast",
                Toast.LENGTH_LONG);
            toast.Show();
        }));

    }
}
