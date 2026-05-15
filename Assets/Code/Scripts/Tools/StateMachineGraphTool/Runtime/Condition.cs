using UnityEngine;

/// <summary>
/// Condiciones que puede evaluar la IA.
/// </summary>
public enum Conditions
{
   hasHP0,
   getsHit,
   idleChangeState,
   attackChangeState,
   vulnerableChangeState
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
    public bool idleChangeState = false;
    public bool attackChangeState = false;
    public bool vulnerableChangeState = false;
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
        context.idleChangeState = false;
        context.attackChangeState = false;
        context.vulnerableChangeState = false;
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
            case Conditions.idleChangeState:
                return IdleChangeState();
            case Conditions.attackChangeState:
                return AttackChangeState();
            case Conditions.vulnerableChangeState:
                return VulnerableChangeState();
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

    public bool IdleChangeState()
    {
        if (Context.idleChangeState)
        {
            Context.idleChangeState = false;
            return true;
        }
        else
            return false;

    }

    public bool AttackChangeState()
    {
        if (Context.attackChangeState)
        {
            Context.attackChangeState = false;
            return true;
        }
        else
            return false;

    }

    public bool VulnerableChangeState()
    {
        if (Context.vulnerableChangeState)
        {
            Context.vulnerableChangeState = false;
            return true;
        }
        else
            return false;

    }
    //TODO: Make functions for checking conditions booleans
}
