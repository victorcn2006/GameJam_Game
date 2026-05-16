using UnityEngine;

public class PuzzlePosition : MonoBehaviour
{
    bool imOccupied;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LightDeflector")) imOccupied = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LightDeflector")) imOccupied = false;
    }

    public bool isOccupied() => imOccupied;
}