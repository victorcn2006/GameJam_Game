using UnityEngine;

public class PuzzleOneManager : MonoBehaviour
{
    private bool Dice1;
    private bool Dice2;

    [SerializeField] private GameObject Door;

    public void DiceState(int diceID, bool state)
    {
        if (diceID == 1) Dice1 = state;
        else Dice2 = state;

        if (Dice1 && Dice2) OpenDoor();

    }

    private void OpenDoor()
    {
        Debug.Log("Puerta abierta");
        //Animacion para abrir puerta
    }

}
