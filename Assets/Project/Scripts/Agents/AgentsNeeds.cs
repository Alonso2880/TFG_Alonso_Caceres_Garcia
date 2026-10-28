using UnityEngine;

/*
 Script que controla las necesidades de cada agente y las actualiza
 */

public class AgentsNeeds : MonoBehaviour, ISimulationTickable
{
    [Header("Agents Needs")]
    public float Hunger = 0;
    public float Thirst = 0;
    public float Health = 1;
    public float Energy = 1;

    //Cantidad de cuanto disminuye
    private float hungerRate = 0.01f;
    private float thristRate = 0.015f;
    private float energyRate = 0.01f;

    //Valores criticos
    private float criticalHunger = 0.95f;
    private float criticalThirst = 0.95f;

    //Referencias
    private SimulationManager simulationManager;

    public void SimulationTick(float dt)
    {

        Hunger = Mathf.Clamp01(Hunger + hungerRate * dt); //Aumenta 0.005 por tick
        Thirst = Mathf.Clamp01(Thirst + thristRate * dt); //Aumenta 0.0075 por tick
        Energy = Mathf.Clamp01(Energy - energyRate * dt); //Disminuye 0.005 por tick

        if (Hunger >= criticalHunger || Thirst >= criticalThirst)
        {
            Health = Mathf.Clamp01(Health - 0.04f * dt); //Disminuye 0.02 si hambre o sed alcanzan valores criticos
        }
    }

    private void OnEnable()
    {
        simulationManager = FindAnyObjectByType<SimulationManager>();

        if(simulationManager != null)
        {
            simulationManager.RegisterAgentNeeds(this);
        }
        else
        {
            Debug.LogError("No se ha encontrado SimulationManager");
        }
    }

}
