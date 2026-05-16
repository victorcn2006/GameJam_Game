using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class Dice : MonoBehaviour, IInteractive
{

    private DiceSystem _diceSystem;
    bool state;

    public void Interact()
    {
        _diceSystem.RotateDice();
    }

    public void InteractB()
    {
        throw new System.NotImplementedException();
    }
    private void Awake()
    {
        _diceSystem = GetComponentInParent<DiceSystem>();
    }


    public bool GetState() => GetComponentInChildren<CorrectFaceTrigger>().GetState();

}
