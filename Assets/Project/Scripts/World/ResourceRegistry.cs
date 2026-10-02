using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceRegistry : MonoBehaviour
{
    public List<GameObject> Nodes = new List<GameObject>();
    void Start()
    {
        StartCoroutine("Delay");
    }

    void Update()
    {
        
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.1f);
        SearchNodesInGame();
    }

    private void SearchNodesInGame()
    {
        ResourceNode[] founded = FindObjectsByType<ResourceNode>(FindObjectsSortMode.None); //Busca todos los objetos que contienen ResourceNode y no los ordena en un orden en especifico

        for(int i =0; i < founded.Length; i++)
        {
            Nodes.Add(founded[i].gameObject);
        }
    }
}
