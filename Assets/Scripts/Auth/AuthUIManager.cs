using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase;

public class AuthUIManager : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField]
    private GameObject checkingForAcccount;
    [SerializeField]
    private GameObject loginUI;
    [SerializeField]
    private GameObject registerUI;
    [SerializeField]
    private GameObject verifyEmailUI;

    [SerializeField]
    private TMP_Text verifyEmailTextUI;


    public static AuthUIManager instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void ClearUI()
    {
        loginUI.SetActive(false);
        registerUI.SetActive(false);
        verifyEmailUI.SetActive(false);
        checkingForAcccount.SetActive(false);
        FirebaseManager.instance.ClearOutputs();
    }

    public void LoginScreen()
    {
        ClearUI();
        loginUI.SetActive(true);

    }

    public void RegisterScreen()
    {
        ClearUI();
        registerUI.SetActive(true);

    }

}