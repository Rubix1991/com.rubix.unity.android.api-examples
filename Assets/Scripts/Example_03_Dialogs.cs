using Rubix.Unity.Android.App;
using Rubix.Unity.Android.Content;
using Rubix.Unity.Android.Hardware.Input;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UIElements;

public class Example_03_Dialogs : Example_Base
{
    public override string ButtonName => "Dialogs";

    private readonly string[] Options = new[] { "Option A", "Option B", "Option C" };

    class DialogListener : DialogInterface.OnClickListenerProxy
    {
        string[] Messages { get; }

        public DialogListener(string message)
        {
            this.Messages = new[] { message };
        }

        public DialogListener(string[] messages)
        {
            this.Messages = messages;
        }

        protected override void OnClick(DialogInterface arg0, int arg1)
        {
            var message = arg1 >= 0 && arg1 < Messages.Length ? Messages[arg1] : Messages[0];

            Utilities.Log(message);
            Utilities.ShowToast(message);
        }
    }

    public override void Initialize(VisualElement content)
    {
        content.Add(CreateButton("Show Dialog with One Button", () =>
        {
            var builder = new AlertDialog.Builder(Context.CurrentContext);
            builder.SetTitle("Native dialog");
            builder.SetMessage("Hello from Unity");
            builder.SetPositiveButton("Ok", new DialogListener("Clicked Ok"));
            builder.Show();
        }));

        content.Add(CreateButton("Show Dialog with Two Buttons", () =>
        {
            var builder = new AlertDialog.Builder(Context.CurrentContext);
            builder.SetTitle("Native dialog");
            builder.SetMessage("Hello from Unity. Are you sure?");
            builder.SetPositiveButton("Yes", new DialogListener("Clicked Yes"));
            builder.SetNegativeButton("No", new DialogListener("Clicked No"));
            builder.Show();
        }));

        content.Add(CreateButton("Show Dialog with Options", () =>
        {
            var builder = new AlertDialog.Builder(Context.CurrentContext);
            builder.SetTitle("Choose an Option");
            builder.SetItems(Options, new DialogListener(Options));
            builder.Show();
        }));
    }
}

