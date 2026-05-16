using UnityEngine;

public class Turtle : MonoBehaviour
{

    Material _material;

    private void Start()
    {
        _material = GetComponent<Material>();
    }

    public void ActivateTurtle()
    {
        Debug.Log("Tortuga Activada");
        //_material. Activar EMISSION/ Iluminar algo para dar fedback
    }

    public void DeactivateTurtle()
    {
        Debug.Log("Tortuga Desactivada");
        //_material. Desactivar EMISSION/ dejar de Iluminar algo para dar fedback
    }
}
