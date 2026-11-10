using UnityEngine;
using System.Collections.Generic;

/*
 Este script controla la memoria del agente, guardando beliefs y resourceKnowledge
 */

public sealed class AgentMemory : MonoBehaviour
{
    private Character character;

    [Header("Observation confidence")]
    [SerializeField, Range(0f, 1f)]
    private float initialObservationConfidence = 0.5f; //Confianza cuando descubre algo por primera vez

    [SerializeField, Range(0f, 1f)]
    private float directObervationGain = 0.1f; //Confianza que gana al obervar algo de forma directa

    [SerializeField, Range(0f, 1f)]
    private float indirectObservationGain = 0.05f; //Confianza que gana cuando la informacion que le llega no es de forma directa

    [SerializeField, Range(0f, 1f)]
    private float leaderObservationGain = 0.3f; //Confianza que gana cuando la informacion que le llega es de un lider

    //Diccionario que se encarga de guardar las creencias del agente
    private readonly Dictionary<BeliefKind, Dictionary<int, Belief>> beliefs = new Dictionary<BeliefKind, Dictionary<int, Belief>>(); //Diccionario que contiede dos niveles de creencias, el tipo de creencia (BeliefKind) y el identificador del objeto (int)

    //Diccionario que guarda informacion sobre los recursos que conoce el agente
    private readonly Dictionary<int, ResourceKnowledge> resourceKnowledge = new Dictionary<int, ResourceKnowledge>(); //Diccionar que guarda informacion sobre los recursos que el agente conoce

    private void Start()
    {
        character = this.gameObject.GetComponent<Character>();
    }


    //Funcion que procesa la observacion que recibe el agente y actualiza su memoria

    public bool ProcessObservation(Observation observation)
    {
        
        if (observation.entityID == null) //Comprueba que tenga un ID valido, si no lo tiene devuelve false
            return false;
        
        //Recuperar o crear recuerdo del recurso
        if(!resourceKnowledge.TryGetValue(observation.entityID, out ResourceKnowledge resource)) //El agente comprueba si ya conoce este recurso. Si lo hace, recupera la informacion guardada, si no, crea uno nuevo
        {
            resource = new ResourceKnowledge();
            resource.resourceID = observation.entityID;

            resourceKnowledge.Add(observation.entityID, resource);
        }

        //No permite que una observacion antigua sobreescriba a una mas reciente
        if (resource.lastObersvationTime > observation.simulationTime)
            return false;

        //Actualizamos la informacion dada en resource
        resource.resourceID = observation.entityID; //ID del recurso
        resource.estimaedQuantity = observation.percivedQuantity; //Cantidad del recurso
        resource.lastObersvationTime = observation.simulationTime; //Cuando se vio el recurso

        //Recuperar o crear la creencia
        Belief resourceBelief = GetorCreateBelief(observation.entityID, BeliefKind.Location);

        resourceBelief.estimatedPosition = observation.position; //Posicion donde se encuentra el recurso
        resourceBelief.lastUpdateTime = observation.simulationTime; //Cuando se vio el recurso
        resourceBelief.source = EvidenceSource.Observation; //Indicamos que se ha percibido el recurso por observacion
        resourceBelief.sourceAgentId = character.ID; //Agente que da esta info. Como en este caso es por observacion, la ID es la del mismo agente

        //Actualizar la confianza
        if(resourceBelief.evidenceCount == 0)
        {
            resourceBelief.confidence = initialObservationConfidence; //Se le asigna la confianza inicial ya que es la primera vez que ve este recurso
        }
        else
        {
            resourceBelief.confidence += directObervationGain; //Aumenta segun el valor de observarlo directamente
        }

        resourceBelief.confidence = Mathf.Clamp01(resourceBelief.confidence); //Normalizamos el valor y hacemos que nunca sea ni 0 ni superior a 1
        resourceBelief.evidenceCount++; //Aumentamos en uno el numero de evidencias

        return true;
    }


    //Crear nuevas creencias
    private Belief GetorCreateBelief(int subjectID, BeliefKind kind)
    {
        //Comprueba que existe un diccionario para esta creencia, si no es asi, lo crea
        if(!beliefs.TryGetValue(kind, out var beliefsKind))
        {
            beliefsKind = new Dictionary<int, Belief>();
            beliefs.Add(kind, beliefsKind);
        }

        //Comprueba si ya existe una creencia para este elemento, si no es asi, crea una nueva
        if(!beliefsKind.TryGetValue(subjectID, out Belief belief))
        {
            belief = new Belief
            {
                subjectId = subjectID,
                kind = kind,
                estimatedValue = 0f,
                confidence = 0f,
                evidenceCount = 0
            };

            beliefsKind.Add(subjectID, belief); //Guarda la creencia
        }

        return belief;
    }


    //Permite consultar a la memoria del agente 
    public bool TryGetBelief(int subjectId, BeliefKind kind, out Belief belief)
    {
        belief = null; //Inicializa el resultado como vacio
        if (!beliefs.TryGetValue(kind, out var beliefsKind)) //Comprueba si existe la creencia
            return false;

        return beliefsKind.TryGetValue(subjectId, out belief); //Devuelve true si la encuentra y false si no existe
    }


    //Funcion que permite actualizar creencias
    public void UpdateBelief(int subjectId, BeliefKind kind, float estimatedValue, float confidence, double simulationTime, EvidenceSource source, int sourceAgentId)
    {
        if (subjectId == null)
            return;

        if (kind == BeliefKind.Location) //Las ubicaciones no se actualizan con esta funcion
            return;

        Belief belief = GetorCreateBelief(subjectId, kind); //Recupera la creencia

        belief.estimatedValue = Mathf.Clamp(estimatedValue, -1f, 1f); //Actualiza su valor
        belief.confidence = Mathf.Clamp01(confidence); //Actualiza su confianza
        belief.lastUpdateTime = simulationTime; //Guarda cuando se actualizo
        belief.source = source; //Guarda de donde viene 
        belief.sourceAgentId = sourceAgentId; //Guarda el agente que dio la informacion

        belief.evidenceCount++; //Aumenta la evidencia
    }


    //Funcion que permite consultar si el agente conoce un recurso en concreto
    public bool TryGetResourceKnowledge(int entityId, out ResourceKnowledge knowledge)
    {
        return resourceKnowledge.TryGetValue(entityId, out knowledge); //Si lo conoce devuelve true, si no false
    }


    //Funcion que permite recorrer todas las creencias del agente
    public IEnumerable<Belief> GetAllBeliefs()
    {
        foreach(var beliefKind in beliefs.Values)
        {
            foreach(var belief in beliefKind.Values)
            {
                yield return belief;
            }
        }
    }

    //Devuelve todos los recursos que conoce el agente
    public IEnumerable<ResourceKnowledge> GetKnownResources()
    {
        return resourceKnowledge.Values;
    }
}
