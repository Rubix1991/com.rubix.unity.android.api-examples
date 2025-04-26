using Rubix.Unity.Android.Content;
using Rubix.Unity.Android.Hardware.Input;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UIElements;

public class Example_02_InputDevices : Example_Base
{
    public override string ButtonName => "Input Devices";

    IReadOnlyList<string> GetInputDevices()
    {
        if (Application.platform != RuntimePlatform.Android)
            return new List<string>();
        var context = Rubix.Unity.Android.App.Activity.CurrentActivity;
        using var inputManager = (InputManager)context.GetSystemService(Context.INPUT_SERVICE);
        var deviceIds = inputManager.GetInputDeviceIds();

        var devices = new List<string>();
        foreach (var deviceId in deviceIds)
        {
            var device = inputManager.GetInputDevice(deviceId);
            devices.Add($"Device: {device.GetName()}, Id: {device.GetId()} Sources: {device.GetSources()}");
        }

        return devices;
    }

    public override void Initialize(VisualElement content)
    {
        var listView = content.Q<ListView>("listViewInputDevices");
        listView.itemsSource = GetInputDevices().ToList();
        listView.Rebuild();
    }
}
