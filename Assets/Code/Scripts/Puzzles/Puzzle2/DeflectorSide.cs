using UnityEngine;

public class DeflectorSide : MonoBehaviour
{
    public enum Side { Left, Right, Front, Back }

    [SerializeField] private Side side;
    private Deflector _deflector;

    private void Awake()
    {
        _deflector = GetComponentInParent<Deflector>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _deflector.OnSidePlayerDetected(side);
        }
    }
}