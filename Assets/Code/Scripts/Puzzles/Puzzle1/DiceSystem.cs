using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.AdaptivePerformance;

public class DiceSystem : MonoBehaviour
{

    [SerializeField] private GameObject dice;
    [SerializeField] private int diceID;
    private bool _resolved = false;
    private PuzzleOneManager _puzzleOneManager;

    private void Awake()
    {
        _puzzleOneManager = GetComponentInParent<PuzzleOneManager>();
    }

    public void RotateDice()
    {
        dice.transform
            .DOLocalRotate(new Vector3(0f, 0f, -450f), 1.2f, RotateMode.FastBeyond360)
            .SetRelative()
            .SetEase(Ease.InQuad)
            .OnComplete(Check);
    }

    public void SetDiceState(bool state) => _resolved = state;

    private void Check()
    {
        if (_resolved) _puzzleOneManager.DiceState(diceID, _resolved);
    }

}
