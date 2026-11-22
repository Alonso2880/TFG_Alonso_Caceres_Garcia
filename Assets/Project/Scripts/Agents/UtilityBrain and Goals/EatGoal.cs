using Mono.Cecil;
using UnityEngine;
using UnityEngine.UIElements;

/*
 Componente que define la puntuacion que resultara en comer 
 */
public class EatGoal : IUtilityGoal
{
    [Header("Peso de las consideraciones")]
    private float weightHunger = 0.6f;
    private float weightConfidence = 0.25f;
    private float weightCloseness = 0.2f;

    //Mejor opcion
    private float bestScore = 0;
    private int bestResourceId;
    private Vector3 bestResourcePosition;

    public GoalType Type => GoalType.Eat;

    public float Score (AgentContext context)
    {
        //Reiniciamos el candidato en cada evaluacion
        bestScore = 0f;
        bestResourceId = -1;
        bestResourcePosition = Vector3.zero;

        //Hambre comun para cada evaluacion
        float scoreHunger = context.hunger * weightHunger;


        //Recorremos cada recurso para evaluar la distancia y la confianza
        foreach (ResourceKnowledge resource in context.GetKnownResources())
        {
            //Si es diferente a food salta a la siguiente interaccion
            if (resource.type != "Food") 
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

            float TotalScoreResource = scoreHunger + scoreCloseness + scoreConfidence;
            TotalScoreResource = Mathf.Clamp01(TotalScoreResource);

            if(TotalScoreResource > bestScore)
            {
                bestScore = TotalScoreResource;
                bestResourceId = ID;
                bestResourcePosition = position;
            }

        }

        return bestScore;
    }

    public bool IsValid(AgentContext context)
    {
        return true;
    }

    //Funcion para calcular la distancia
    private float CalcularDistancia(Vector3 positionResource, Vector3 positionAgent)
    {
        float distancia = Vector3.Distance(positionResource, positionAgent);

        float maxUsefulDistance = 100f; //Distancia maxima util

        return 1f - Mathf.Clamp01(distancia / maxUsefulDistance); //Devuelve un valor normalizado de manera que esta mas cerca de 1 los recursos mas cercanos. Mas alla de 100 metros el valor es 0
    }
}
