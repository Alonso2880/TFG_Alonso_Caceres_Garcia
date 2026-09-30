using System;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public ResourceDefinition Definition;
    public float amountLeft;
    public bool IsDepleted = false;

    public event Action<ResourceNode> ResourceConsumed;
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
