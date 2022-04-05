using System.Collections;
using System.Collections.Generic;
using Firebase.Extensions;
using Firebase.Firestore;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class GetCharacterData : MonoBehaviour
{
    [SerializeField] private string _characterPath = "character_sheets/one_cool_dude";

    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _attackText;
    [SerializeField] private TMP_Text _defenseText;

    private ListenerRegistration _listenerRegistration;
    void Start()
    {
        var firestore = FirebaseFirestore.DefaultInstance;

        _listenerRegistration = firestore.Document(_characterPath).Listen(snapshot =>
         {
             var characterData = snapshot.ConvertTo<CharacterData>();

             _nameText.text = $"Name: {characterData.Name}";
             _descriptionText.text = $"Name: {characterData.Description}";
             _attackText.text = $"Name: {characterData.Attack}";
             _defenseText.text = $"Name: {characterData.Defense}";
         });

    }

    void OnDestroy()
    {
        _listenerRegistration.Stop();
    }

}
