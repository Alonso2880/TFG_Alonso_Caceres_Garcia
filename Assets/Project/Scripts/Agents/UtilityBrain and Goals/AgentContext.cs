using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;

/*
 AgentContext es una clase que contiene la informacion que necesita el objetivo para tomar decisiones sobre el agente
 conocera cosas como el hambre, sed (conectarlo con agent needs), posicion del agente, recursos cercanos, memoria, directivas del lider...
 */
public class AgentContext : MonoBehaviour, ISimulationTickable
{
    [Header("Referencias componentes agente")]
    [SerializeField] private AgentsNeeds agentNeeds;
    [SerializeField] private AgentTraits agentTraits;
    [SerializeField] private AgentPerception agentPerception;
    [SerializeField] private AgentMemory agentMemory;
    [SerializeField] private CompetenceSystem competenceSystem;
    private SimulationManager simulationManager;

    [Header("Informacion que almacena")]
    [HideInInspector] public float hunger;
    [HideInInspector] public float thirst;
    [HideInInspector] public float energy;
    [HideInInspector] public float health;

    [HideInInspector] public Vector3 agentPosition;


    //Subscribimos AgentContext a los ticks
    public void SimulationTick(float dt)
    {
        //Sincronizo la informacion de las necesidades
        hunger = agentNeeds.Hunger;
        thirst = agentNeeds.Thirst;
        energy = agentNeeds.Energy;
        health = agentNeeds.Health;
        agentPosition = this.gameObject.transform.position;
    }


    private void OnEnable()
    {
        simulationManager = FindAnyObjectByType<SimulationManager>();

        if (simulationManager != null)
        {
            simulationManager.RegisterAgentContext(this);
        }
        else
        {
            Debug.LogError("No se ha encontrado SimulationContext");
        }
    }

    //Conectamos varios metodos con los de AgentMemory

    //Devuelve todos los recursos que conoce el agente
    public IEnumerable<ResourceKnowledge> GetKnownResources()
    {
        foreach(ResourceKnowledge resource in agentMemory.GetKnownResources())
        {
            yield return resource;
        }
    }

    //Consulta la informacion de un recurso concreto
    public bool TryGetResourceKnowledge(int resourceId, out ResourceKnowledge knowledge)
    {
        return agentMemory.TryGetResourceKnowledge(resourceId, out knowledge);
    }

    //Consulta la posicion conocida y la confianza de esa informacion
    public bool TryGetResourceLocation(int resourceId, out Vector3 position, out float confidence)
    {
        position = Vector3.zero;
        confidence = 0;

        if (!agentMemory.TryGetBelief(resourceId, BeliefKind.Location, out Belief belief))
            return false;

        position = belief.estimatedPosition;
        confidence = belief.confidence;

        return true;
    }
    
}
