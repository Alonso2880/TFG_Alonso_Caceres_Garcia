using UnityEngine;
using System.Collections.Generic;

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

    //Se desuscribe del evento
    private void OnDisable()
    {
        if (simulationClock != null)
            simulationClock.OnSimulationTick -= HandleSimulationTick;
    }

    //Funcion llamada cada vez que el reloj genera un tick de simulacion. Llama cada una en un orden especifico.
    private void HandleSimulationTick(float deltaTime)
    {
        TickSystems(enviromentsStystems, deltaTime);
        TickSystems(needsAndPerceptionSystems, deltaTime);
        TickSystems(decisionAndActionSystems, deltaTime);
        TickSystems(metricsSystems, deltaTime);
    }

    private void TickSystems(List<MonoBehaviour> systems, float deltaTime)
    {
        foreach (MonoBehaviour system in systems)
        {
            if (system == null) //Si el sistema es nulo, se salta al siguiente
                continue;

            if(systems is ISimulationTickable tickable) //Comprueba si systems implementa la interfaz y si lo hace lo guarda en la variable tickable
            {
                tickable.SimulationTick(deltaTime); //Cumple con la interfaz y llama al método SimulationTick
            }
            else
            {
                Debug.LogWarning($"Sistema {system.name} no implementa ISimulationTickable.");
            }
        }
    }
}
