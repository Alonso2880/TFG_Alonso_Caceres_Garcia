using Mono.Cecil;
using UnityEngine;
using UnityEngine.UIElements;

/*
 Componente que define la puntuacion que resultara en beber 
 */

public class DrinkGoal : IUtilityGoal
{
    [Header("Peso de las consideraciones")]
    private float weightThrist = 0.6f;
    private float weightConfidence = 0.25f;
    private float weightCloseness = 0.2f;

    //Mejor opcion
    private float bestScore = 0;
    private int bestResourceId;
    private Vector3 bestResourcePosition;

    public GoalType Type => GoalType.Drink;

    //Funcion para calcular la puntuacion
    public float Score (AgentContext context)
    {
        //Reiniciamos el candidato en cada evaluacion
        bestScore = 0f;
        bestResourceId = -1;
        bestResourcePosition = Vector3.zero;

        //Sed comun para cada evaluacion
        float scoreThrist = context.thirst * weightThrist;

        //Recorremos cada recurso para evaluar la distancia y la confianza
        foreach (ResourceKnowledge resource in context.GetKnownResources())
        {
            //Si es diferente a water salta a la siguiente intetaccion
            if (resource.type != "Water")
                continue;

            //Creamos las variables que almacenaran informacion del recurso
            int ID = resource.resourceID;
            Vector3 position;
            float confidence;

            //Asigna el valor de posicion y confianza al ID
            if (context.TryGetResourceLocation(ID, out position, out confidence)) { }

            //Calculamos cual es la mejor score
            float scoreCloseness = CalcularDistancia(position, context.agentPosition) * weightCloseness;
            float scoreConfidence = confidence * weightConfidence;

            float TotalScoreResource = scoreThrist + scoreCloseness + scoreConfidence;
            TotalScoreResource = Mathf.Clamp01(TotalScoreResource);

            if (TotalScoreResource > bestScore)
            {
                bestScore = TotalScoreResource;
                bestResourceId = ID;
                bestResourcePosition = position;
            }
        }

        return bestScore;
    }

    //Funcion que comprueba si tiene sentido que el agente se plante beber ahora mismo
    public bool IsValid(AgentContext context)
    {
        return context.thirst >= 0.5;
    }

    //Funcion para calcular la distancia
    private float CalcularDistancia(Vector3 positionResource, Vector3 positionAgent)
    {
        float distancia = Vector3.Distance(positionResource, positionAgent);

        float maxUsefulDistance = 100f; //Distancia maxima util

        return 1f - Mathf.Clamp01(distancia / maxUsefulDistance); //Devuelve un valor normalizado de manera que esta mas cerca de 1 los recursos mas cercanos. Mas alla de 100 metros el valor es 0
    }
}
