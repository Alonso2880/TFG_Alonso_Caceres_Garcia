using System;
using UnityEngine;

/*
 Clase que va a guardar la cantidad que el agente cree que qeuda en el recurso
 */

[Serializable]
public sealed class ResourceKnowledge
{
    public int resourceID; //ID del recurso
    public float estimaedQuantity; //Cantidad del recurso que el agente observo que quedaba la ultima vez
    public double lastObersvationTime; //Cuando se observo por ultima vez
}
