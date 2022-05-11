using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstanceManager : MonoBehaviour
{
    public string StoreString;

    void Update(){
        AddText();
    }

    void OnMouseDown(){
        PlayerController.Instance.TextWindows.SetActive(true);
    }
    void AddText(){
        if(PlayerController.Instance.TextWindows.activeSelf){
            PlayerController.Instance.storeText.text = StoreString.ToString();
        }else{
            //Debug.Log(PlayerController.Instance.TextWindows + "IS Disable");
        }
    }
}
