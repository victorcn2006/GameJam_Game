using UnityEngine;
using StateMachine.Runtime;

public class BossController : MonoBehaviour
{
    StateMachineComponent sm;
    public Context bossContext;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sm = GetComponent<StateMachineComponent>();
        bossContext = sm.GetStateContext();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
