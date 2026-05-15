using UnityEngine;

/// <summary>
/// Condiciones que puede evaluar la IA.
/// </summary>
public enum Conditions
{
   hasHP0,
   getsHit,

   hardChangeState
}

/// <summary>
/// Contiene todos los datos relevantes para evaluar las condiciones.
/// </summary>
[System.Serializable]
public class Context
{
    //TODO: Make context booleans for conditions
    public int HP = 3;
    public bool getsHit = false;
    public bool hardChangeState = false;
}

/// <summary>
/// La clase Condition eval�a las condiciones de IA.
/// </summary>
public class Condition
{
    public Context Context;

    public Condition(Context context)
    {
        context.HP = 3;
        context.getsHit = false;
        context.hardChangeState = false;
        Context = context;
    }

    public bool GetConditionValue(Conditions condition)
    {
        switch (condition)
        {
            case Conditions.hasHP0:
                return IsHP0();
            case Conditions.getsHit:
                return GetsHit();
            case Conditions.hardChangeState:
                return HardChangeState();
            default:
                Debug.LogError($"Unhandled Condition: {condition}");
                return false;
        }
    }

    public bool IsHP0()
    {
        if (Context.HP <= 0)
        {
            return true;
        }
        else return false;
    }

    public bool GetsHit()
    {
        return Context.getsHit;
    }

    public bool HardChangeState()
    {
        return Context.hardChangeState;
    }
    //TODO: Make functions for checking conditions booleans
}
