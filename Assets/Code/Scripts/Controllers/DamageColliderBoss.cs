using UnityEngine;

public class DamageColliderBoss : MonoBehaviour
{

    Collider coll;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coll = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Laser"))
        {
            GetComponent<BossController>().TakeDamage(1);
            Destroy(other.gameObject);
        }
    }
}
