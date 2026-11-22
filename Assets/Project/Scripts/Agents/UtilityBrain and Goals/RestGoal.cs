using Unity.VisualScripting;
using UnityEngine;

/*
 Componente que define la puntuacion que resultara en descansar
 */

public class RestGoal : IUtilityGoal
{
    [Header("Peso de las consideraciones")]
    private float weightEnergy = 0.6f;
    private float weightLazyness;

    //Mejor opcion
    private float bestScore = 0;

    public GoalType Type => GoalType.Rest;

    //Funcion para calcular la puntuacion
    public float Score (AgentContext context)
    {
        //Reiniciamos el candidato
        bestScore = 0;
        weightLazyness = 0;

        //Energia comun
        float scoreEnergy = (1 - context.energy) * weightEnergy;

        //Calculamos el score de vagueza
        float scoreLazyness = context.agentTraits.Lazyness * GetweightLazyness(context.agentTraits.Lazyness);

        bestScore = scoreEnergy + scoreLazyness;

        return bestScore;
    }

    //Funcion que comprueba si tiene sentido que el agente se plante descansar ahora mismo
    public bool IsValid(AgentContext context)
    {
        return context.energy <= 0.4 || (context.energy <= 0.6 && context.agentTraits.Lazyness > 0.6);
    }

    //Funcion para obtener el peso de la vagueza
    private float GetweightLazyness(float lazyness)
    {
        if (lazyness >= 0 && lazyness <= 0.3f)
            weightLazyness = 0;
        if (lazyness > 0.3 && lazyness <= 0.6f)
            weightLazyness = 0.3f;
        if (lazyness > 0.6 && lazyness <= 1f)
            weightLazyness = 0.5f;

        return weightLazyness;
    }
}
