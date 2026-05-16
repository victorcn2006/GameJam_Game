using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;


public class PuzzleOneManager : MonoBehaviour
{
    private bool Dice1;
    private bool Dice2;

    [SerializeField] private GameObject _door;

    [SerializeField] private CinemachineCamera doorCam;
    [SerializeField] private CinemachineCamera playerCam;
    [SerializeField] private SimplePlayerMovementInput playerInput;

    private bool doorOpen = false;

    public void DiceState(int diceID, bool state)
    {
        if (diceID == 1) Dice1 = state;
        else Dice2 = state;
        
        Debug.Log(Dice1 + " " + Dice2);

        if (Dice1 && Dice2) OpenDoor();

    }

    private void OpenDoor()
    {
        if (!doorOpen)
        {
            doorOpen = true;
            _door.transform.DORotate(_door.transform.eulerAngles + new Vector3(0, 80f, 0), 5f).SetEase(Ease.InOutCubic);
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
