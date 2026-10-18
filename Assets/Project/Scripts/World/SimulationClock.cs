using UnityEngine;
using System;

/*
  Sistema que se encarga de controlar el tiempo simulado y generar los ticks
 */
public class SimulationClock : MonoBehaviour
{

    [Header("Simulation Time")]
    [SerializeField, Min(0.01f)]
    private float tickInterval = 0.5f; //Intervalo de tiempo entre cada tick de simulacion

    public float simulationSpeed = 1f; //Velocidad de la simulacion

    public float TickInterval => tickInterval; 
    public float SimulationSpeed => simulationSpeed;
    public float SimulatedTime { get; private set; } //Tiempo simulado

    //Evento que se ejecuta cada vez que el reloj genera un tick
    public event Action<float> OnSimulationTick;

    private float accumulatedTime; //Tiempo acumulado desde el ultimo tick

    private void Update()
    {
        if (SimulationSpeed <= 0f)
            return;

        //Tiempo real transcurrido, convertido a tiempo simulado
        accumulatedTime += Time.deltaTime * SimulationSpeed;

        while(accumulatedTime >= tickInterval)
        {
            accumulatedTime -= tickInterval;
            SimulatedTime += tickInterval;

            OnSimulationTick?.Invoke(tickInterval); //Invocamos el evento de tick de simulacion, pasando el intervalo de tiempo del tick a los suscriptores
        }

    }

    //Funcion para cambiar la velocidad de la simulacion
    public void SetSimulationSpeed(float newSpeed)
    {
        simulationSpeed = Mathf.Max(0f, newSpeed);
    }

    //Funcion para resetear el reloj
    public void ResetClock()
    {
        SimulatedTime = 0f;
        accumulatedTime = 0f;
    }
}
