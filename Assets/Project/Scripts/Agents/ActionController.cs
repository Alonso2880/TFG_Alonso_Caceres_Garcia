using UnityEngine;

public class ActionController : MonoBehaviour
{
    //Bool para las interacciones
    private bool HasInteracted = false, IsOut = true;
    private GameObject Resource;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        ResourceNode node = collision.gameObject.GetComponent<ResourceNode>();

        if(node != null)
        {
            Resource = collision.gameObject;
            HasInteracted = true;
            IsOut = false;

            GetResource(Resource, node);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        ResourceNode node = collision.gameObject.GetComponent<ResourceNode>();

        if(node != null)
        {
            Resource = null;
            HasInteracted = false;
            IsOut = false;
        }
    }

    private void GetResource(GameObject resource, ResourceNode node)
    {
        node.amountLeft--; //Reducimos en uno la cantidad

        //Accedemos al scriptable object y al AgentNeeds para aplicar los efectos
        AgentsNeeds needs = this.gameObject.GetComponent<AgentsNeeds>();

        needs.Hunger += node.Definition.hungerEffect; //Aplicamos el efecto de hambre
        needs.Thirst += node.Definition.thirstEffect; //Aplicamos el efecto de sed
        needs.Health += node.Definition.healthEffect; //Aplicamos el efecto de salud
    }


}
