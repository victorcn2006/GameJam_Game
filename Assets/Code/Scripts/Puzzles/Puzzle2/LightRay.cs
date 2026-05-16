using UnityEngine;

public class LightRay : MonoBehaviour
{

    [SerializeField] private int _lightId;
    private void OnDisable()
    {
        
    }
    private void OnEnable()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Turtle"))
        {
            other.GetComponent<Turtle>().ActivateTurtle(_lightId);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Turtle"))
        {
            other.GetComponent<Turtle>().DeactivateTurtle();
        }
    }


}
