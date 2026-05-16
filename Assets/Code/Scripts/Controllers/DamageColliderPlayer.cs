using Unity.VisualScripting;
using UnityEngine;

public class DamageColliderPlayer : MonoBehaviour
{
    Collider coll;

    private void Start()
    {
        coll = GetComponent<Collider>();
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Laser"))
        {
            SimplePlayerMovementInput player = GetComponentInParent<SimplePlayerMovementInput>();
            if (!player.hasParry)
            {
                GetComponentInParent<CharacterStats>().TakeDamage(1);
                Destroy(other.gameObject);
                Debug.Log("Daño");

            }
                
        }
    }
}
