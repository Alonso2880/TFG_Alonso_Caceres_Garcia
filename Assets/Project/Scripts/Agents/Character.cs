using UnityEngine;

//Clase base para todos los personajes.

public class Character: MonoBehaviour
{
    public int ID { get; set; }
    public int CivilizationID { get; set; } //Puede que se necesite un AgentSpawner o el CivilizationManager para asignar esto

    //Referencias
    public AgentsNeeds needs;
    public AgentTraits traits;
    public AgentLife life;

    public CharacterRole currentRole; //Puede que se necesite un RoleManager para assignar esto
}
