using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class Dice : MonoBehaviour, IInteractive
{

    private DiceSystem _diceSystem;

    public void Interact()
    {

    }

    private void Awake()
    {
        _diceSystem = GetComponentInParent<DiceSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FaceDetector")) _diceSystem.SetDiceState(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FaceDetector")) _diceSystem.SetDiceState(false);
    }
}
