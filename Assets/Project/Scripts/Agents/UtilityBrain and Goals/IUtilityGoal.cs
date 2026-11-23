using UnityEngine;

public interface IUtilityGoal
{
    GoalType Type { get; } //Identifica que objetivo representa (por ejemplo eat, drink, rest...)
    bool IsValid(AgentContext context); //Comprueba si el agente puede realizar esa accion
    float Score(AgentContext context); //Calcula la utilidad de ese objetivo utilizando la formula
    ActionRequest BuildRequest(AgentContext context); //Prepara la petición para ejecutar la acción elegida
}

public enum GoalType
{
    Eat,
    Drink,
    Rest,
    Explore,
    Gather
}