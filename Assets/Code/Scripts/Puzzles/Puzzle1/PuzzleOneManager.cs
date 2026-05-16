using DG.Tweening;
using UnityEngine;

public class PuzzleOneManager : MonoBehaviour
{
    private bool Dice1;
    private bool Dice2;

    [SerializeField] private GameObject _door;

    public void DiceState(int diceID, bool state)
    {
        if (diceID == 1) Dice1 = state;
        else Dice2 = state;
        
        Debug.Log(Dice1 + " " + Dice2);

        if (Dice1 && Dice2) OpenDoor();

    }

    private void OpenDoor()
    {

        _door.transform.DORotate(_door.transform.eulerAngles + new Vector3(0, 90f, 0), 5f);
    }

}
