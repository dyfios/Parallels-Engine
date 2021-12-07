using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiveSQD.Parallels.Runtime;

public class TabsController : MonoBehaviour
{    public float maximumTabSize = 790;

    public uint maximumTabs = 16;

    public GameObject tabContainer;

    public GameObject newTabButton;

    public GameObject tabPrefab;

    public UIController uiController;

    private List<TabController> tabObjects;

    public void Initialize()
    {
        if (tabObjects != null)
        {
            foreach (TabController tabObject in tabObjects)
            {
                tabObject.Destroy();
            }
        }

        tabObjects = new List<TabController>();
    }

    public void Resize(float size)
    {
        if (tabObjects != null && tabObjects.Count > 0)
        {
            float tabSize = (size - 200) / tabObjects.Count;
            if (tabSize > maximumTabSize)
            {
                tabSize = maximumTabSize;
            }

            foreach (TabController tabObject in tabObjects)
            {
                tabObject.Resize(tabSize);
            }
            newTabButton.transform.SetAsLastSibling();
        }
    }

    public void AddTab_NoReturn()
    {
        AddTab();
    }

    public TabController AddTab()
    {
        if (tabObjects == null)
        {
            Debug.LogError("[TabsController->AddTab] Null tabObjects reference.");
            return null;
        }

        if (tabObjects.Count >= maximumTabs)
        {
            Debug.Log(tabObjects.Count + " tabs allowed.");
            return null;
        }

        GameObject newTabObject = Instantiate(tabPrefab);
        newTabObject.transform.SetParent(tabContainer.transform);
        newTabObject.transform.localPosition = new Vector3(0, 0, 0);
        newTabObject.transform.localRotation = Quaternion.identity;
        newTabObject.transform.localScale = Vector3.one;
        TabController newTabController = newTabObject.GetComponent<TabController>();
        if (newTabController == null)
        {
            Debug.LogError("[TabsController->AddTab] New tab missing controller.");
            return null;
        }
        tabObjects.Add(newTabController);

        RectTransform rt = GetComponent<RectTransform>();
        if (rt == null)
        {
            Debug.LogError("[TabsController->AddTab] No Rect Transform");
        }
        else
        {
            Resize(rt.rect.width);
        }
        newTabController.Initialize(this, Parallels.SceneManager.CreateScene());
        SetActiveTab(tabObjects[tabObjects.Count - 1]);

        return newTabController;
    }

    public void RemoveTab(TabController tab)
    {
        if (tabObjects == null)
        {
            Debug.LogError("[TabsController->RemoveTab] Null tabObjects reference.");
            return;
        }

        if (!tabObjects.Contains(tab))
        {
            Debug.LogError("[TabsController->RemoveTab] Bad tab reference.");
        }

        if (tab == tabObjects[ tabObjects.Count - 1] && tabObjects.Count > 1)
        {
            SetActiveTab(tabObjects[tabObjects.Count - 2]);
        }

        Parallels.SceneManager.DestroyScene(tab.scene);

        tabObjects.Remove(tab);
        tab.Destroy();

        if (tabObjects.Count == 0)
        {
            Application.Quit();
        }

        RectTransform rt = GetComponent<RectTransform>();
        if (rt == null)
        {
            Debug.LogError("[TabsController->RemoveTab] No Rect Transform");
        }
        else
        {
            Resize(rt.rect.width);
        }
    }

    public void SetActiveTab(TabController tab)
    {
        if (tabObjects == null)
        {
            Debug.LogError("[TabsController->SetActiveTab] Null tabObjects reference.");
            return;
        }

        if (!tabObjects.Contains(tab))
        {
            Debug.LogError("[TabsController->SetActiveTab] Bad tab reference.");
        }

        foreach (TabController tabObject in tabObjects)
        {
            if (tabObject == tab)
            {
                tabObject.SetActive();
                uiController.SetURLField(tabObject.GetURL());
            }
            else
            {
                tabObject.SetInactive();
            }
        }
    }

    public TabController GetActiveTab()
    {
        if (tabObjects == null)
        {
            Debug.LogError("[TabsController->GetActiveTab] Null tabObjects reference.");
            return null;
        }

        foreach (TabController tabObject in tabObjects)
        {
            if (tabObject.active)
            {
                return tabObject;
            }
        }
        return null;
    }

    public void LoadPage(string uri, TabController tab)
    {
        if (tabObjects == null)
        {
            Debug.LogError("[TabsController->LoadPage] Null tabObjects reference.");
            return;
        }

        if (!tabObjects.Contains(tab))
        {
            Debug.LogError("[TabsController->LoadPage] Bad tab reference.");
            return;
        }

        tab.LoadPage(uri);
    }
}