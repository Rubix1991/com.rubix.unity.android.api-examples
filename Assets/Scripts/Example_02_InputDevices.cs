using Rubix.Unity.Android.Content;
using Rubix.Unity.Android.Hardware.Input;
using Rubix.Unity.Android.View;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Example_02_InputDevices : Example_Base
{
    public override string ButtonName => "Input Devices";

    IReadOnlyList<string> GetInputDevices()
    {
        if (Application.platform != RuntimePlatform.Android)
            return new List<string>(new[] {"Dummy Device 1", "Dummy Device 2"});
        var context = Rubix.Unity.Android.App.Activity.CurrentActivity;
        using var inputManager = (InputManager)context.GetSystemService(Context.INPUT_SERVICE);
        var deviceIds = inputManager.GetInputDeviceIds();

        var info = new List<string>();
        foreach (var deviceId in deviceIds)
        {
            var device = inputManager.GetInputDevice(deviceId);
            info.Add($"Id: {device.GetId()}, Name: {device.GetName()}");
            info.Add($"- Sources: {SourceToString(device.GetSources())}");
        }

        return info;
    }

    private string SourceToString(int sources)
    {
        var values = new List<string>();
        if ((InputDevice.SOURCE_TOUCHSCREEN & sources) != 0) 
            values.Add("Touchscreen");
        if ((InputDevice.SOURCE_KEYBOARD & sources) != 0)
            values.Add("Keyboard");
        if ((InputDevice.SOURCE_DPAD & sources) != 0)
            values.Add("Dpad");
        if ((InputDevice.SOURCE_GAMEPAD & sources) != 0)
            values.Add("Gamepad");
        if ((InputDevice.SOURCE_JOYSTICK & sources) != 0)
            values.Add("Joystick");
        if ((InputDevice.SOURCE_MOUSE & sources) != 0)
            values.Add("Mouse");
        if ((InputDevice.SOURCE_MOUSE_RELATIVE & sources) != 0)
            values.Add("MouseRelative");
        if ((InputDevice.SOURCE_STYLUS & sources) != 0)
            values.Add("Stylus");

        return "(" + string.Join(", ", values) + ")";
    }

    public override void Initialize(VisualElement content)
    {
        var listView = content.Q<ListView>("listViewInputDevices");
        listView.makeItem = () => CreateLabel("");
        listView.bindItem = (element, index) =>
        {
            ((Label)element).text = ((List<string>)listView.itemsSource)[index];
        };
        listView.itemsSource = GetInputDevices().ToList();
        listView.Rebuild();
    }
}
