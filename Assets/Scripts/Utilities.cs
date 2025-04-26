using UnityEngine;

public static class Utilities
{
    public static void Log(string message)
    {
        Debug.LogFormat(LogType.Log, LogOption.NoStacktrace, null, message);
    }

    public static void ShowToast(string message)
    {
        using var toast = Rubix.Unity.Android.Widget.Toast.MakeText(Rubix.Unity.Android.App.Activity.CurrentActivity,
                message,
                Rubix.Unity.Android.Widget.Toast.LENGTH_SHORT);
        toast.Show();
    }
}