using UnityEngine;

/*
 Componente que define la puntuacion que resultara en explorar
 */

public class ExploreGoal : IUtilityGoal
{
    [Header("Peso de las consideraciones")]
    private float weightExplore = 0.4f;
    private float weightCuriosity;

    //Mejor opcion
    private float scoreExplore = 0;

    public GoalType Type => GoalType.Explore;

    //Funcion para calcular la puntuacion
    public float Score (AgentContext context)
    {
        //Reiniciamos el candidato
        scoreExplore = 0;
        weightCuriosity = 0;

        //Puntuacion para explorar
        scoreExplore = GetweightCuriosity(context.agentTraits.Curiosity) * weightExplore;


        return scoreExplore;
    }

    //Funcion que comprueba si tiene sentido que el agente se plantee explorar ahora mismo
    public bool IsValid(AgentContext context)
    {
        return true;
    }

    private float GetweightCuriosity(float curiosity)
    {
        if (curiosity >= 0 && curiosity <= 0.3f)
            weightCuriosity = 0.15f;
        if (curiosity > 0.3 && curiosity <= 0.6f)
            weightCuriosity = 0.35f;
        if (curiosity > 0.6 && curiosity <= 1f)
            weightCuriosity = 0.5f;

        return weightCuriosity;
    }

}
