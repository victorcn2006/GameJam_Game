using UnityEngine;

public class PuzzlePosition : MonoBehaviour
{
    [SerializeField] private bool _skyLight;
    bool imOccupied;
    /*
    les he puesto interactable para detectar a los deflectores ya que no hay otro objeto interactable que pueda colisionar con ellos
    y los deflectores necesitan tener ese tag para funcionar con el player
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            imOccupied = true;
            Debug.Log(imOccupied);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            imOccupied = false;
            Debug.Log(imOccupied);
        }
    }

    public bool IsOccupied() => imOccupied;
    public bool IsIluminated() => _skyLight;
}