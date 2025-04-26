using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System;

public class MainUIController : MonoBehaviour
{
    public VisualTreeAsset mainUIAsset;

    private VisualElement root;
    private VisualElement leftView;
    private VisualElement rightView;
    private ScrollView buttonList;
    private Dictionary<string, VisualElement> contentViews;

    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        mainUIAsset.CloneTree(root);

        leftView = root.Q<VisualElement>("left-view");
        rightView = root.Q<VisualElement>("right-view");
        buttonList = root.Q<ScrollView>("button-list");

        if (Application.platform != RuntimePlatform.Android)
            root.Add(new HelpBox($"Examples only work on Android, current platform is {Application.platform}.", HelpBoxMessageType.Warning));

        contentViews = new Dictionary<string, VisualElement>();
        var examples = GetComponentsInChildren<Example_Base>();
        foreach (var example in examples)
        {
            try
            {
                LoadContentView(example);
            }
            catch (Exception ex)
            {
                Debug.LogException(new Exception($"Error while initializing {example.GetType().Name}", ex));
            }
        }
    }

    private void LoadContentView(Example_Base example)
    {
        var contentAsset = example.visualTreeAsset;

        if (contentAsset == null)
        {
            Debug.LogError($"Failed to load content view from: {example.GetType().FullName}");
            return;
        }

        var key = example.GetType().Name;
        var button = new Button();
        button.text = example.ButtonName;
        button.clicked += () => OnButtonClicked(key);
        buttonList.Add(button);

        var contentInstance = contentAsset.Instantiate();
        var content = contentInstance.Q<VisualElement>("content");
        if (content == null)
            throw new Exception($"Failed to find content element in {contentAsset.name}");
        rightView.Add(content);
        contentViews.Add(key, content);
        content.style.display = DisplayStyle.None;

        example.Initialize(content);
    }

    private void OnButtonClicked(string buttonName)
    {
        // Hide all content views
        foreach (var contentView in contentViews.Values)
        {
            contentView.style.display = DisplayStyle.None;
        }

        // Show the selected content view
        if (contentViews.TryGetValue(buttonName, out var selectedContentView))
        {
            selectedContentView.style.display = DisplayStyle.Flex;
        }
    }
}