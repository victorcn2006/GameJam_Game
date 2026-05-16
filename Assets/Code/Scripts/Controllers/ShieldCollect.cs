using Unity.Multiplayer.PlayMode;
using UnityEngine;
using StateMachine.Runtime;
public class ShieldCollect : MonoBehaviour
{
    public GameObject shieldModel;
    public Collider shieldCollider;
    public StateMachineComponent BossStateMachine;

    bool setStaticRotation;
    SimplePlayerMovementInput playerController;

    private void Update()
    {
        if(setStaticRotation)
            playerController.transform.rotation = Quaternion.Euler(0f, 180f,0f);    
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Cojer");
            playerController = other.GetComponentInParent<SimplePlayerMovementInput>();
            BossStateMachine.StartStateMachineExecution();
            shieldModel.SetActive(false);
            //playerController.DisableInput();
            //setStaticRotation = true;
            //playerController.obtainCamera.gameObject.SetActive(true);
            playerController.hasShield = true;


        }
    }
}
