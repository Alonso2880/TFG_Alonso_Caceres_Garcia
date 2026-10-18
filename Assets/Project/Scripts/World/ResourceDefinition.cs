using UnityEngine;

public enum ResourceKind {Water, Food, Material, Mineral }
public enum DisciplineC1 {None, Collector, Hunt}
public enum DisciplineC2 {None, Agriculture, AnimalBreeding, Construction, Miner, Agriculture_Leader, AnimalBreeding_Leader, Miner_Leader}

[CreateAssetMenu(fileName = "ResourceDefinition", menuName = "TFG/ResourceDefinition")]
public class ResourceDefinition : ScriptableObject
{
    public string resourceTypeID;  //Nombre como "red_berry"
    public ResourceKind kind;
    public float useDuration;
    public float hungerEffect; //Cuanta hambre quita
    public float thirstEffect; //Cuanta sed quita
    public float healthEffect; //Efecto que provoca en la salud del agente
    public float maxCapacity;
    public float regenerationPerMinute;
}
