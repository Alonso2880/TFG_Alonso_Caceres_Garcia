using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AgentPerception : MonoBehaviour, ISimulationTickable
{
    private float perceptionInterval = 2f; //Relacionarlo con los ticks
    private float accumulator = 0;
    private int perceptionRadius = 50;

    [Header("Observations")]
    [SerializeField] private Observation observation;
    [SerializeField] private List<Observation> observationsList;

    [Header("List of Gameobjects that the agent sees")]
    [SerializeField] private List<GameObject> objectsObserved = new();

    [Header("Reference to SimulationClock")]
    private SimulationClock simulationClock;

    private ResourceRegistry resourceRegistry;
    private SimulationManager simulationManager;

    void Start()
    {
        resourceRegistry = FindAnyObjectByType<ResourceRegistry>(); //Encuentro ResourceRegistry y lo asigno
        simulationClock = FindAnyObjectByType<SimulationClock>(); //Encuentro SimulationClock y lo asigno
    }

    //Se registra este componente en SimulationManager
    private void OnEnable()
    {
        simulationManager = FindAnyObjectByType<SimulationManager>();

        if (simulationManager != null)
        {
            simulationManager.RegisterAgentPerception(this);
        }
        else
        {
            Debug.LogError("No se ha encontrado SimulationManager");
        }
    }

    //Me subscribo a los ticks
    public void SimulationTick(float dt)
    {
        accumulator += dt;
        
        if(accumulator >= perceptionInterval)
        {
            accumulator -= perceptionInterval;
            DetectResources();
        }
        
    }

    public void DetectResources()
    {
        foreach(GameObject nodes in resourceRegistry.Nodes)
        {
            float distancia = Vector3.Distance(nodes.gameObject.transform.position, this.gameObject.transform.position); //Calculo la distancia entre el agente y el gameobject detectado

            //Debug.Log("Nodo encontrado " + nodes + " distancia " + distancia);

            //Si la distancia es menor, se annade el objeto a la lista de objestos observados
            if(distancia <= perceptionRadius)
            {
                if (!objectsObserved.Contains(nodes))
                {
                    objectsObserved.Add(nodes);
                    observationsList.Add(CreateObservation(nodes));
                }

                //CreateObservation(nodes);
            }
            else
            {
                objectsObserved.Remove(nodes);
            }

        }

    }

    private Observation CreateObservation(GameObject nodes)
    {
        Observation newObservation = new Observation(); //Creo un nuevo objeto Observation
        ResourceNode resourceNode = nodes.GetComponent<ResourceNode>(); //Obtengo la referencia de ResourceNode

        newObservation.entityID = resourceNode.ID; //Asigno el ID del recurso
        newObservation.resourceTypeID = resourceNode.Definition.name; //Asigno el nombre del recurso
        newObservation.position = nodes.transform.position; //Asigno la posicion donde se encuentra el nodo
        //No podemos annadir la cantidad debido a que no esta tocando el objeto
        newObservation.percivedQuality = PercivedQuality.Unkown; //Como esta observacion es desde lejos ponemos Unkown ya que el agente desconoce ahora mismo si es tocixo o no
        newObservation.simulationTime = simulationClock.TotalSimulatedTime; //Asigno cuando se ha encontrado con este recurso

        return newObservation;
    }
}
