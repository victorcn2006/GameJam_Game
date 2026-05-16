using UnityEngine;

public class LightRay : MonoBehaviour
{
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
            other.GetComponent<Turtle>().ActivateTurtle();
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
