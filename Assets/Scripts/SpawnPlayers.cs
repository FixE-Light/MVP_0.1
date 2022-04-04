using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject player;

    [SerializeField]
    public FirebaseManager firebaseManager = null;

    [SerializeField]
    private TMP_Text Username;

    public float minX, minY, maxX, maxY;
    public void Start()
    {
        Username.text = firebaseManager.user.DisplayName;
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        PhotonNetwork.Instantiate(player.name, randomPosition, Quaternion.identity);
    }
}
