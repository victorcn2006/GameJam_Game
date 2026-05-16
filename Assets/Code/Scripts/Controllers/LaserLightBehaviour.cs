using Unity.VisualScripting;
using UnityEngine;

public class LaserLightBehaviour : MonoBehaviour
{
    [Header("Laser Settings")]
    [SerializeField] float speed = 25f;
    [SerializeField] float lifeTime = 5f;

    [SerializeField] public Transform origin;

    Collider laserCol;

    void Start()
    {
        laserCol = GetComponent<Collider>();
        // Auto-destrucción para limpiar jerarquía
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Movimiento constante hacia delante
        transform.position += transform.forward * speed * Time.deltaTime;
    }

}
