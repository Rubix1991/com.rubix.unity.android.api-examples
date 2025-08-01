using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System;
using static MainUIController;

public class MainUIController : MonoBehaviour
{
    public VisualTreeAsset mainUIAsset;

    public class Example
    {
        private bool m_Initialized;
        public Example_Base Controller { get; }
        public VisualElement ContentView { get; }

        public Example(Example_Base controller, VisualElement contentView)
        {
            m_Initialized = false;
            Controller = controller;
            ContentView = contentView;
            Disable();
        }

        public void Disable()
        {
            ContentView.style.display = DisplayStyle.None;
            Controller.enabled = false;
        }

        public void Enable()
        {
            if (m_Initialized == false)
            {
                Controller.Initialize(ContentView);
                m_Initialized = true;
            }

            ContentView.style.display = DisplayStyle.Flex;
            Controller.enabled = true;
        }
    }

    private VisualElement root;
    private VisualElement rightView;
    private ScrollView buttonList;
    private Dictionary<string, Example> contentViews;

    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        mainUIAsset.CloneTree(root);

        rightView = root.Q<VisualElement>("right-view");
        buttonList = root.Q<ScrollView>("button-list");

        if (Application.platform != RuntimePlatform.Android)
            root.Add(new HelpBox($"Examples only work on Android, current platform is {Application.platform}.", HelpBoxMessageType.Warning));

        contentViews = new Dictionary<string, Example>();
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

        contentViews.Add(key, new Example(example, content));
    }

    private void OnButtonClicked(string buttonName)
    {
        foreach (var example in contentViews.Values)
            example.Disable();

        if (contentViews.TryGetValue(buttonName, out var selectedExample))
            selectedExample.Enable();
    }
}