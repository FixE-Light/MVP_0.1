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

    void Start()
    {

    }
    public void GetData()
    {
        var firestore = FirebaseFirestore.DefaultInstance;

        firestore.Document(_characterPath).GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            Assert.IsNull(task.Exception);

            var characterData = task.Result.ConvertTo<CharacterData>();

            _nameText.text = $"Name: {characterData.Name}";
            _descriptionText.text = $"Name: {characterData.Description}";
            _attackText.text = $"Name: {characterData.Attack}";
            _defenseText.text = $"Name: {characterData.Defense}";

        });
    }

}
