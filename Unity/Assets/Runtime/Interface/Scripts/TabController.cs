using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FiveSQD.Parallels.Runtime;
using FiveSQD.Parallels.Runtime.Engine.Scene;

public class TabController : MonoBehaviour
{
    private class TabConfig
    {
        public string url;

        public Stack<string> previous;

        public Stack<string> next;
    }

    public ColorBlock activeColors;

    public ColorBlock inactiveColors;

    public Color activeTextColor;

    public Color inactiveTextColor;

    public Image icon;

    public TextMeshProUGUI text;

    public Button close;

    public Button tab;

    public bool active { get; private set; }

    private TabsController tabsController;

    private TabConfig tabConfig;

    public Scene scene { get; private set; }

    public void Initialize(TabsController controller, Scene tabScene)
    {
        tabsController = controller;

        tabConfig = new TabConfig()
        {
            url = "",
            previous = new Stack<string>(),
            next = new Stack<string>()
        };

        scene = tabScene;
    }

    public void OnPress()
    {
        if (tabsController == null)
        {
            Debug.LogError("[TabController->OnPress] Tabs controller reference not set.");
            return;
        }
        tabsController.SetActiveTab(this);
    }

    public void OnClose()
    {
        if (tabsController == null)
        {
            Debug.LogError("[TabController->OnClose] Tabs controller reference not set.");
            return;
        }
        tabsController.RemoveTab(this);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void Resize(float size)
    {
        RectTransform rt = GetComponent<RectTransform>();
        if (rt == null)
        {
            Debug.LogError("[TabController->Resize] No Rect Transform");
            return;
        }

        rt.sizeDelta = new Vector2(size, 100);
    }

    public void SetInactive()
    {
        active = false;
        tab.colors = inactiveColors;
        text.color = inactiveTextColor;
    }

    public void SetActive()
    {
        active = true;
        tab.colors = activeColors;
        text.color = activeTextColor;
    }

    public void SetName(string name)
    {
        text.text = name;
    }

    public string GetName()
    {
        return text.text;
    }

    public string GoBack()
    {
        if (tabConfig.previous.Count == 0)
        {
            Debug.LogError("[TabController->GoBack] No back history.");
            return null;
        }
        tabConfig.next.Push(tabConfig.url);
        tabConfig.url = tabConfig.previous.Pop();
        return tabConfig.url;
    }

    public string GoForward()
    {
        if (tabConfig.previous.Count == 0)
        {
            Debug.LogError("[TabController->GoForward] No forward history.");
            return null;
        }
        tabConfig.previous.Push(tabConfig.url);
        tabConfig.url = tabConfig.next.Pop();
        return tabConfig.url;
    }

    public string GetURL()
    {
        return tabConfig.url;
    }

    public void UpdateURL(string url)
    {
        tabConfig.url = url;
    }

    public void LoadPage(string url)
    {
        StartCoroutine(Parallels.VEMLManager.StartVEMLRequest(url, PageLoaded));
    }

    public void PageLoaded(Scene.SceneInfo sceneInfo)
    {
        foreach (string script in sceneInfo.scripts)
        {
            Parallels.JavascriptManager.RunScript(script);
        }

        SetName(sceneInfo.title);
    }
}