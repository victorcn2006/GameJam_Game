using UnityEngine;

public class CorrectFaceTrigger : MonoBehaviour
{
    bool state;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FaceDetector")) state = true;

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FaceDetector")) state = false;
    }

    public bool GetState() => state;

}
