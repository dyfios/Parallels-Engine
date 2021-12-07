using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FiveSQD.Parallels.Runtime;

public class UIController : MonoBehaviour
{
    public TabsController tabsController;

    public GameObject menu;

    public TMP_InputField urlField;

    void Start()
    {
        tabsController.Initialize();
        tabsController.AddTab();
    }

    public void OnMenu()
    {
        if (menu.activeSelf)
        {
            menu.SetActive(false);
        }
        else
        {
            menu.SetActive(true);
        }
    }

    public void OnBack()
    {
        TabController activeTab = tabsController.GetActiveTab();
        if (activeTab == null)
        {
            Debug.LogError("[UIController->OnBack] No active tabs.");
            return;
        }

        SetURLField(activeTab.GoBack());
    }

    public void OnForward()
    {
        TabController activeTab = tabsController.GetActiveTab();
        if (activeTab == null)
        {
            Debug.LogError("[UIController->OnForward] No active tabs.");
            return;
        }

        SetURLField(activeTab.GoForward());
    }

    public void OnEnter()
    {
        TabController activeTab = tabsController.GetActiveTab();
        if (activeTab == null)
        {
            Debug.LogError("[UIController->OnEnter] No active tabs.");
            return;
        }

        tabsController.LoadPage(urlField.text, activeTab);
    }

    public void OnLoaded(FiveSQD.Parallels.Runtime.Engine.Scene.Scene.SceneInfo sceneInfo)
    {

    }

    public void SetURLField(string url)
    {
        urlField.text = url;
    }

    public void OnURLFieldChanged()
    {
        TabController activeTab = tabsController.GetActiveTab();
        if (activeTab == null)
        {
            Debug.LogError("[UIController->OnURLFieldChanged] No active tabs.");
            return;
        }

        activeTab.UpdateURL(urlField.text);
    }
}