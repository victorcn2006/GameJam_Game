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
            GetComponentInParent<PuzzleTwoManager>().TurtleState(_lightIdNeeded, true);
        }
        //_material. Activar EMISSION/ Iluminar algo para dar fedback
    }

    public void DeactivateTurtle()
    {
        Debug.Log("Tortuga Desactivada");
        GetComponentInParent<PuzzleTwoManager>().TurtleState(_lightIdNeeded, false);
        //_material. Desactivar EMISSION/ dejar de Iluminar algo para dar fedback
    }
}
