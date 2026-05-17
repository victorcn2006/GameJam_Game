using Unity.Multiplayer.PlayMode;
using UnityEngine;
using StateMachine.Runtime;
using System.Collections.Generic;
using Unity.Cinemachine;
public class ShieldCollect : MonoBehaviour
{
    public GameObject shieldModel;
    public Collider shieldCollider;
    public StateMachineComponent BossStateMachine;

    [SerializeField] AudioClip getItemSFX;

    bool setStaticRotation;
    SimplePlayerMovementInput playerController;

    private AudioSource playerControllerAudioSource;

    [SerializeField] private List<GameObject> collidersBoss;
    [SerializeField] private CinemachineCamera battleCam;
    [SerializeField] private CinemachineCamera playerCam;
    [SerializeField] private Canvas heartsCanvas;

    private void Start()
    {
        playerControllerAudioSource = GetComponent<AudioSource>();
        playerControllerAudioSource.clip = getItemSFX;
    }

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
            shieldCollider.enabled = false;
            shieldModel.SetActive(false);
            playerController.DisableInput();
            setStaticRotation = true;
            playerController.obtainCamera.gameObject.SetActive(true);
            playerController.animator.SetTrigger("ChaChaChaChan");
            playerController.shieldGetReference.SetActive(true);
            playerControllerAudioSource.Play();
            heartsCanvas.gameObject.SetActive(true);
            TimeManager.Instance.OneShotTimer(2f, () => 
            {
                playerController.animator.SetTrigger("TerminarChan");
                playerController.obtainCamera.gameObject.SetActive(false);
                battleCam.gameObject.SetActive(true);
                setStaticRotation = false;
                foreach(GameObject gameObject in collidersBoss)
                    gameObject.SetActive(true);

                TimeManager.Instance.OneShotTimer(0.8f, () => 
                { 
                    playerController.shieldGetReference.SetActive(false);
                    playerController.hasShield = true;
                    playerController.EnableInput();
                }
                );
                gameObject.SetActive(false);
                
                
                BossStateMachine.StartStateMachineExecution();
            });
        }
    }
}
