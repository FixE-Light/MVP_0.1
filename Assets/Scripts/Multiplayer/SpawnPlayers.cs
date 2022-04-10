using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject player;
    public FirebaseAuth auth;
    public FirebaseUser user;

    public float minX, minY, maxX, maxY;
    public void Start()
    {
        user = FirebaseManager.instance.user;
        auth = FirebaseManager.instance.auth;

        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        PhotonNetwork.Instantiate(player.name, randomPosition, Quaternion.identity);
    }

    public void SignOut()
    {
        if (auth != null)
        {
            auth.SignOut();
        }
    }
}
