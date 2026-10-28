using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 Sistema que se encarga de gestionar el flujo de la simulacion, llamando a los sistemas en un orden especifico cada vez que el reloj genera un tick
 */

public class SimulationManager : MonoBehaviour
{
    [Header("Simulation Clock")]
    [SerializeField]
    private SimulationClock simulationClock;

    [Header("Systems")]
    [SerializeField]
    private List<MonoBehaviour> enviromentsStystems = new(); //Lista de mundo y eventos ambientales

    [SerializeField]
    private List<MonoBehaviour> needsAndPerceptionSystems = new(); //Lista de poblacion, necesidades y percepcion

    [SerializeField]
    private List<MonoBehaviour> decisionAndActionSystems = new(); //Lista de liderazgo, directivas, decisiones y acciones

    [SerializeField]
    private List<MonoBehaviour> metricsSystems = new(); //Lista de metricas

    [Header("Agents Systems")]
    [SerializeField]
    private List<AgentsNeeds> agentsNeedsSystem = new();

    [SerializeField]
    private List<AgentLife> agentsLifeSystem = new();

    [SerializeField]
    private List<AgentPerception> agentPerceptionsSystem = new();




    private void Awake()
    {
        if(simulationClock == null)
            simulationClock = GetComponent<SimulationClock>(); //Obtiene el componente SimulationClock

        if (simulationClock == null)
        {
            Debug.LogError("No se ha asignado un SimulationClock");
        }
    }

    //Se subscribe al evento
    private void OnEnable()
    {
        if (simulationClock != null)
            simulationClock.OnSimulationTick += HandleSimulationTick;
    }

    //Se desusbcribe del evento
    private void OnDisable()
    {
        if (simulationClock != null)
            simulationClock.OnSimulationTick -= HandleSimulationTick;
    }

    //Registra agente con AgentNeed de manera automatica
    public void RegisterAgentNeeds(AgentsNeeds needs)
    {
        if(needs != null && !agentsNeedsSystem.Contains(needs))
        {
            agentsNeedsSystem.Add(needs);
        }
    }

    //Registra agente con AgentLife de manera automatica
    public void RegisterAgentLife(AgentLife life)
    {
        if (life != null && !agentsLifeSystem.Contains(life))
        {
            agentsLifeSystem.Add(life);
        }
    }

    //Registra agente con AgentPerceptions de manera automatica
    public void RegisterAgentPerception(AgentPerception perception)
    {
        if (perception != null && !agentPerceptionsSystem.Contains(perception))
        {
            agentPerceptionsSystem.Add(perception);
        }
    }

    //Desregistra el agente
    public void UnregisterAgentNeed(AgentsNeeds needs)
    {
        agentsNeedsSystem.Remove(needs);
    }



    //Funcion llamada cada vez que el reloj genera un tick de simulacion. Llama cada una en un orden especifico.
    private void HandleSimulationTick(float deltaTime)
    {
        TickSystems(enviromentsStystems, deltaTime);
        TickSystems(needsAndPerceptionSystems, deltaTime);
        TickSystems(decisionAndActionSystems, deltaTime);
        TickSystems(metricsSystems, deltaTime);

        //Ticks de los agentes
        TickAgentsNeed(agentsNeedsSystem, deltaTime);
        TickAgentsLife(agentsLifeSystem, deltaTime);
        TickAgentPerceptions(agentPerceptionsSystem, deltaTime);

    }

    private void TickSystems(List<MonoBehaviour> systems, float deltaTime)
    {
        foreach (MonoBehaviour system in systems)
        {
            if (system == null) //Si el sistema es nulo, se salta al siguiente
                continue;

            if(system is ISimulationTickable tickable) //Comprueba si systems implementa la interfaz y si lo hace lo guarda en la variable tickable
            {
                tickable.SimulationTick(deltaTime); //Cumple con la interfaz y llama al método SimulationTick
            }
            else
            {
                Debug.LogWarning($"Sistema {system.name} no implementa ISimulationTickable.");
            }
        }
    }

    private void TickAgentsNeed(List<AgentsNeeds> needs, float deltaTime)
    {
        foreach (AgentsNeeds system in needs)
        {
            if (system == null)
                continue;

            if(system is ISimulationTickable tickable)
            {
                tickable.SimulationTick(deltaTime);
            }
            else
            {
                Debug.LogWarning($"Sistema {system.name} no implementa ISimulationTickable.");
            }
        }
    }

    private void TickAgentsLife(List<AgentLife> life, float deltaTime)
    {
        foreach (AgentLife system in life)
        {
            if (system == null)
                continue;

            if (system is ISimulationTickable tickable)
            {
                tickable.SimulationTick(deltaTime);
            }
            else
            {
                Debug.LogWarning($"Sistema {system.name} no implementa ISimulationTickable.");
            }
        }
    }

    private void TickAgentPerceptions(List<AgentPerception> perception, float deltaTime)
    {
        foreach (AgentPerception system in perception)
        {
            if (system == null)
                continue;

            if (system is ISimulationTickable tickable)
            {
                tickable.SimulationTick(deltaTime);
            }
            else
            {
                Debug.LogWarning($"Sistema {system.name} no implementa ISimulationTickable.");
            }
        }
    }
}
