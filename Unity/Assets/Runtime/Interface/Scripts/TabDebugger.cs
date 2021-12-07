using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabDebugger : MonoBehaviour
{
    public TabsController tabsController;

    void Start()
    {
        tabsController.Initialize();
        tabsController.AddTab();
        tabsController.AddTab();
    }
}