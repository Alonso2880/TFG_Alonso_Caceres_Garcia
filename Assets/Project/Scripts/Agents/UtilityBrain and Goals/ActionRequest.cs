using UnityEngine;

/*
 Es una acción que indica que accion debe ejecutarse cuando el cerebro ha elegido un objetivo
 */

public class ActionRequest : MonoBehaviour
{
    public GoalType Goal;
    public int ResourceID;
    public Vector3 TargetPosition;
}
