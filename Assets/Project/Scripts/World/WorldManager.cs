using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [Header("Spaces for instantiation")]
    [Header("Lakes")]
    public List<GameObject> Lakes = new List<GameObject>();

    [Header("BlueBerries")]
    public List<GameObject> BlueBerries = new List<GameObject>();

    [Header("RedBerries")]
    public List<GameObject> RedBerries = new List<GameObject>();

    [Header("Prefabs")]
    [Header("Bodies of water")]
    public List<GameObject> LakesPrefabs = new List<GameObject>();

    [Header("Good food")]
    public List<GameObject> BlueBerriesPrefabs = new List<GameObject>();

    [Header("Dangerous food")]
    public List<GameObject> RedBerriesPrefabs = new List<GameObject>();
    void Start()
    {
        ShuffleList(LakesPrefabs);
        ShuffleList(BlueBerriesPrefabs);
        ShuffleList(RedBerriesPrefabs);
        InstantiationElements();
    }

    private void InstantiationElements()
    {
        int index = 0;

        //Lagos
        for (int i = 0; i < Lakes.Count; i++)
        {

            Instantiate(LakesPrefabs[index], Lakes[i].transform.position, Lakes[i].transform.rotation, Lakes[i].transform);

            if(index+1 >= LakesPrefabs.Count)
            {
                index = 0;
            }
            else
            {
                index++;
            }
        }

        //Debug.Log("index lake " + index);

        //Bayas Azules
        for (int i = 0; i < BlueBerries.Count; i++)
        {

            Instantiate(BlueBerriesPrefabs[index], BlueBerries[i].transform.position, BlueBerries[i].transform.rotation, BlueBerries[i].transform);

            if (index + 1 >= BlueBerriesPrefabs.Count)
            {
                index = 0;
            }
            else
            {
                index++;
            }
        }

        //Debug.Log("index blue " + index);

        //Bayas Rojas
        for (int i = 0; i < RedBerries.Count; i++)
        {

            Instantiate(RedBerriesPrefabs[index], RedBerries[i].transform.position, RedBerries[i].transform.rotation, RedBerries[i].transform);

            if (index + 1 >= RedBerriesPrefabs.Count)
            {
                index = 0;
            }
            else
            {
                index++;
            }
        }

        //Debug.Log("index red " + index);
    }


    private void ShuffleList(List<GameObject> lista)
    {
        for(int i = lista.Count-1; i>0; i--)
        {
            int random = Random.Range(0, i + 1);

            GameObject temp = lista[i];
            lista[i] = lista[random];
            lista[random] = temp;
        }
    }
}
