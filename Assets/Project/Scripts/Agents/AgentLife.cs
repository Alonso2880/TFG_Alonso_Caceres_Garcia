using System;
using UnityEngine;

public class AgentLife : MonoBehaviour, ISimulationTickable
{
    [SerializeField] private AgentsNeeds needs;
    [SerializeField] private int edad = 1;
    [SerializeField] private int lifeSpan = 85;

    //Referencias
    private SimulationManager simulationManager;

    private float accumulator = 0;
    private bool isDead = false;

    public event Action<GameObject> AgentDied;

    void Awake()
    {
        needs = this.gameObject.GetComponent<AgentsNeeds>();
    }

    private void OnEnable()
    {
        simulationManager = FindAnyObjectByType<SimulationManager>();

        if (simulationManager != null)
        {
            simulationManager.RegisterAgentLife(this);
        }
        else
        {
            Debug.LogError("No se ha encontrado SimulationManager");
        }
    }


    //Funcion para aumentar la edad. Cumplira un año cada 20 segundos
    public void SimulationTick(float dt)
    {
        if (needs == null)
            Debug.LogError("AgentNeed no añadido");

        accumulator += 1;

        if(accumulator >= 40)
        {
            accumulator = 0;
            edad += 1;
        }

        if(edad >= lifeSpan || needs.Health <= 0)
        {
            isDead = true;
            AgentDied?.Invoke(this.gameObject); //Avisa si un agente se ha muerto por edad o por salud
        }
    }

}
