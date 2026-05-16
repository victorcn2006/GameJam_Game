using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IATextNarrator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _iAText;
    [SerializeField] private float _timeBetweenCharacters = 0.05f;

    private IANarratorManager _iANarratorManager;
    private IASoundNarrator _iASoundNarrator;

    void Awake()
    {
        _iANarratorManager = GetComponent<IANarratorManager>();
        _iASoundNarrator = GetComponent<IASoundNarrator>();
    }

    public IEnumerator NarrateLine(string textToNarrate)
    {
        _iAText.text = "";
        foreach(char character in textToNarrate)
        {
            WriteLetter(character);
            yield return new WaitForSeconds(_timeBetweenCharacters);
        }
        _iANarratorManager.DialogueLineFinished();
    }

    private void WriteLetter(char character)
    {
        _iAText.text += character.ToString();
        if (_iASoundNarrator != null) _iASoundNarrator.Narrate(character);
    }
}
