using Unity.VisualScripting;
using UnityEngine;

public class PopulationManager : MonoBehaviour
{
    public GameObject AgentPrefab;
    public GameObject Spawner;

    [SerializeField] private CivilizationManager civManager;
    private int AgenteID = 1; //Variable donde se guarda el ID de los agentes

    //Metodo que crea un agente
    public void CreateAgent()
    {
        if (civManager.civilizations.Count == 0)
        {
            Debug.LogError("No hay ninguna civilizacion creada");
            return;
        }

        int CivID = civManager.civilizations[0].Civid; //Almaceno el ID de la civilizacion

        GameObject NewAgent = Instantiate(AgentPrefab, Spawner.transform.position, Spawner.transform.rotation); //Instancio el nuevo agente

        civManager.civilizations[0].poblacion.Add(NewAgent); //Guardo al agente en el CivilizationContext

        Character newCharacter = NewAgent.GetComponent<Character>(); //Obtengo referencia al componente character

        newCharacter.ID = AgenteID; //Asigno el ID personal
        newCharacter.CivilizationID = CivID; //Asigno el ID de la civilizacion
        AgenteID++; //Aumentamos el ID para el siguiente agente que se cree

        Debug.Log("Creado agente de ID " + newCharacter.ID + " y civilization id " + newCharacter.CivilizationID);

        //Obtenemos la referencia de AgentLife para subscribirnos al evento de muerto
        AgentLife agentlife = NewAgent.GetComponent<AgentLife>();

        if (agentlife != null)
        {
            agentlife.AgentDied += DeathAgent; //Nos subscribimos al evento
        }
        else
            Debug.LogError("El agente no tiene AgentLife");

        /*
         La siguiente parte es para asignar los traits, actualmente para probar el juego los traits se generaran de forma aleatoria. Mas adelante sera una mezcla entre
        herencia e influencia del juegador
         */

        AgentTraits agentTrait = NewAgent.GetComponent<AgentTraits>();

        agentTrait.Curiosity = Random.Range(0, 101) / 100f;
        agentTrait.Sociability = Random.Range(0, 101) / 100f;
        agentTrait.Caution = Random.Range(0, 101) / 100f;
        agentTrait.Lazyness = Random.Range(0, 101) / 100f;
    }


    //Metodo que elimina al agente y lo saca de las listas y los ticks
    public void DeathAgent(GameObject Agente)
    {
        //Recorremos la lista en busca del agente a eliminiar y lo eliminamos
        foreach(var civilization in civManager.civilizations)
        {
            civilization.poblacion.Remove(Agente);
        }

        Destroy(Agente); //Destruimos el agente
    }
}
