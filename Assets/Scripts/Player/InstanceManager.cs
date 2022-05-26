using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InstanceManager : MonoBehaviour
{
    public string StoreString;

    void Update()
    {
        AddText();
    }

    void OnMouseDown()
    {
        Debug.Log("Focus");
        print("Focus");
        PlayerController.Instance.TextWindows.SetActive(true);

    }
    void AddText()
    {
        if (PlayerController.Instance.TextWindows.activeSelf)
        {
            print("Add");
            Debug.Log("add");

            PlayerController.Instance.storeText.text = StoreString.ToString();
        }
        else
        {
            //Debug.Log(PlayerController.Instance.TextWindows + "IS Disable");
        }
    }
}
