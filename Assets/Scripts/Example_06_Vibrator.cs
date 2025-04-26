using Rubix.Unity.Android.Content;
using Rubix.Unity.Android.Net;
using Rubix.Unity.Android.OS;
using Rubix.Unity.Android.Provider;
using Rubix.Unity.Java.Net;
using UnityEngine;
using UnityEngine.UIElements;

public class Example_05_Vibrator: Example_Base
{
    public override string ButtonName => "Vibrator";
    private Vibrator m_Vibrator;

    // Note: Requires <uses-permission android:name="android.permission.VIBRATE" /> 
    //       The permission is added by Permissions.androidlib\src\main\AndroidManifest.xml
    private void Vibrate(int milliseconds)
    {
        if (m_Vibrator == null || !m_Vibrator.HasVibrator())
        {
            UnityEngine.Debug.LogError("Device does not have vibrator.");
            return;
        }

        using var vibratorEffect = VibrationEffect.CreateOneShot(milliseconds, VibrationEffect.DEFAULT_AMPLITUDE);
        m_Vibrator.Vibrate(vibratorEffect);
    }

    public override void Initialize(VisualElement content)
    {
        if (Context.CurrentContext == null)
        {
            content.Add(new HelpBox("No active context", HelpBoxMessageType.Error));
            return;
        }
        var o = Context.CurrentContext.GetSystemService(Context.VIBRATOR_SERVICE);
        m_Vibrator = o != null ? new Vibrator(o) : null;
        content.Add(CreateButton("Vibrate 200 ms", () => Vibrate(200)));
        content.Add(CreateButton("Vibrate 1000 ms", () => Vibrate(1000)));
    }
}
