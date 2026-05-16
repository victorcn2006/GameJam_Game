using DG.Tweening;
using UnityEngine;

public class PuzzleTwoManager : MonoBehaviour
{

    private bool TurtleOneState;
    private bool TurtleTwoState;

    [SerializeField] private GameObject _door;
    private bool doorOpen = false;

    public void TurtleState(int turtleID, bool state) 
    {
        if (turtleID == 1) TurtleOneState = state;
        else TurtleTwoState = state;

        Debug.Log(TurtleOneState + " " + TurtleTwoState);

        if (TurtleOneState && TurtleTwoState) OpenDoor();

    }

    private void OpenDoor()
    {

        if (!doorOpen)
        {
            doorOpen = true;
            _door.transform.DORotate(_door.transform.eulerAngles + new Vector3(0, 90f, 0), 5f).SetEase(Ease.InOutCubic);
        }
    }

}



