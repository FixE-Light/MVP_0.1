using System.Collections;
using System.Collections.Generic;
using Firebase.Firestore;
using UnityEngine;
using UnityEngine.UI;

public class SetCharacterData : MonoBehaviour
{
    [SerializeField] private string _characterPath = "character_sheets/one_cool_dude";

    private string _nameField = "Dido";
    private string _descriptionField = "Test data";
    private int _attackField = 5;
    private int _defenseField = 7;
    [SerializeField] private Button _submitButton;
    void Start()
    {
        _submitButton.onClick.AddListener(() =>
        {
            var characterData = new CharacterData
            {
                Name = _nameField,
                Description = _descriptionField,
                Attack = _attackField,
                Defense = _defenseField,
            };

            var firestore = FirebaseFirestore.DefaultInstance;
            firestore.Document(_characterPath).SetAsync(characterData);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
