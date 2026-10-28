using UnityEngine;
public enum PercivedQuality
{
    Unkown,
    Good,
    Toxic
}

[System.Serializable]
public struct Observation
{
    public int entityID; //Nodo observado
    public string resourceTypeID; //Tipo del nodo
    public Vector3 position; //Posicion del nodo
    public float percivedQuantity; //Cantidad que percibe 
    public PercivedQuality percivedQuality; //Calidad, true para una buena calidad, false para las venenosas
    public double simulationTime; //Cuando se ha observado en el tiempo de la simulacion
}
