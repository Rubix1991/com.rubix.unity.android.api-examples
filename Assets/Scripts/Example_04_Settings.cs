using Rubix.Unity.Android.Content;
using Rubix.Unity.Android.Net;
using Rubix.Unity.Android.Provider;
using Rubix.Unity.Java.Net;
using UnityEngine.UIElements;

public class Example_04_Settings : Example_Base
{
    public override string ButtonName => "Settings";

    private void OpenSettings(string intentName)
    {
        using var intent = new Intent(intentName);
        Context.CurrentContext.StartActivity(intent);
    }

    public override void Initialize(VisualElement content)
    {
        content.Add(CreateButton("Open App Settings", () =>
        {
            using var intent = new Intent(Settings.ACTION_APPLICATION_DETAILS_SETTINGS);
            using var uri = Uri.FromParts("package", Context.CurrentContext.GetPackageName(), null);
            intent.SetData(uri);
            Context.CurrentContext.StartActivity(intent);
        }));

        content.Add(CreateButton("Open Settings", () => OpenSettings(Settings.ACTION_SETTINGS)));
        content.Add(CreateButton("Open Wifi Settings", () => OpenSettings(Settings.ACTION_WIFI_SETTINGS)));
        content.Add(CreateButton("Open Bluetooth Settings", () => OpenSettings(Settings.ACTION_BLUETOOTH_SETTINGS)));
        content.Add(CreateButton("Open Sound Settings", () => OpenSettings(Settings.ACTION_SOUND_SETTINGS)));
        content.Add(CreateButton("Open Locale Settings", () => OpenSettings(Settings.ACTION_LOCALE_SETTINGS)));
        content.Add(CreateButton("Open Display Settings", () => OpenSettings(Settings.ACTION_DISPLAY_SETTINGS)));
    }
}
