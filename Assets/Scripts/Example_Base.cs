using UnityEngine;
using UnityEngine.UIElements;

public abstract class Example_Base : MonoBehaviour
{
    public VisualTreeAsset visualTreeAsset;
    public abstract string ButtonName { get; }

    public abstract void Initialize(VisualElement content);

    protected virtual void Update()
    {

    }

    protected Button CreateButton(string name, System.Action action)
    {
        var btn = new Button(action);
        btn.text = name;
        btn.style.flexGrow = 1;
        btn.style.width = new StyleLength(new Length(100, LengthUnit.Percent));
        return btn;
    }

    protected TextField CreateTextField(string name)
    {
        var textField = new TextField();
        textField.value = name;
        textField.style.flexGrow = 1;
        textField.style.width = new StyleLength(new Length(100, LengthUnit.Percent));
        return textField;
    }

    protected Label CreateLabel(string name)
    {
        var label = new Label(name);
        label.text = name;
        label.style.width = new StyleLength(new Length(100, LengthUnit.Percent));
        label.style.color = Color.white;
        return label;
    }
}
