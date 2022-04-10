using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using TMPro;
using UnityEngine;

public class PlayerAvatar : MonoBehaviour
{
    public FirebaseAuth auth;
    public FirebaseUser user;

    [SerializeField] private TMP_Text displayNameText = null;

    public void Start()
    {
        user = FirebaseManager.instance.user;
        auth = FirebaseManager.instance.auth;

        displayNameText.text = FirebaseManager.instance.user.DisplayName;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
