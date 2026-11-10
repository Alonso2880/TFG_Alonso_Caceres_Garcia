using UnityEngine;

public sealed class Belief
{
    public int subjectId; //ID del lugar
    public BeliefKind kind; 
    public float estimatedValue; //Valor que le da al recurso. Por ejemplo si tiene un valor de  -0.8 significara que el recurso es perjucial
    public float confidence; //Cuanta confianza tiene en la creencia
    public double lastUpdateTime; //Cuando se actualizo la creencia por ultima vez
    public EvidenceSource source; //De donde proviene la informacion
    public int sourceAgentId; //Que agente le dio la infomacion
    public int evidenceCount; //Cuantas evidencias directas se han acumulado
    public Vector3 estimatedPosition; //Posicion del recurso
}
