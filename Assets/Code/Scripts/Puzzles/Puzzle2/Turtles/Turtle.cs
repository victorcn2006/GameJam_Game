using UnityEngine;

public class Turtle : MonoBehaviour
{

    [SerializeField] private int _lightIdNeeded;
    Material _material;

    private void Start()
    {
        _material = GetComponent<Material>();
    }

    public void ActivateTurtle(int lightId)
    {
        if (lightId == _lightIdNeeded){
            Debug.Log("Tortuga Activada");
        }
        //_material. Activar EMISSION/ Iluminar algo para dar fedback
    }

    public void DeactivateTurtle()
    {
        Debug.Log("Tortuga Desactivada");
        //_material. Desactivar EMISSION/ dejar de Iluminar algo para dar fedback
    }
}
