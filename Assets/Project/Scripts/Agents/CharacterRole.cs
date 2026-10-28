using UnityEngine;

public abstract class CharacterRole : MonoBehaviour
{
    public abstract void PerformAction(); //Definir accion en las clases que hereden de esta, LiderRole, SubLieaderRole y CitizenRole
}
