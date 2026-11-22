using System;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public ResourceDefinition Definition;
    public string type;
    public float amountLeft;
    public bool IsDepleted = false;

    public int ID;

    public event Action<ResourceNode> ResourceConsumed;

    private void Awake()
    {
        type = Definition.kind.ToString();
    }
    public void Consume()
    {
        amountLeft -= 1;

        if(amountLeft <= 0)
        {
            ResourceConsumed?.Invoke(this);
            IsDepleted = true;
        }
    }
}
