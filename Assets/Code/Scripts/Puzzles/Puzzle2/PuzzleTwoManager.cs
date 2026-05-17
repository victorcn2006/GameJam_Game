using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

public class PuzzleTwoManager : MonoBehaviour
{

    private bool TurtleOneState;
    private bool TurtleTwoState;

    [SerializeField] private GameObject _door;
    private bool doorOpen = false;

    [SerializeField] private CinemachineCamera doorCam;
    [SerializeField] private CinemachineCamera playerCam;
    [SerializeField] private SimplePlayerMovementInput playerInput;

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
            playerInput.DisableInput();
            doorCam.gameObject.SetActive(true);
            TimeManager.Instance.OneShotTimer(5f, () =>
            {
                doorCam.gameObject.SetActive(false);
                TimeManager.Instance.OneShotTimer(1f, () => playerInput.EnableInput());
            });
        }
    }

}



