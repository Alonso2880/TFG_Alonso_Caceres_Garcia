using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

//Objeto que tiene la informacion principal de cada civilizacion
public class CivilizationContext
{
    public int Civid; //Identificador de la poblacion
    public List<GameObject> poblacion = new List<GameObject>(); //Lista de la poblacion
    public GameObject lider; //Lider de la civilizacion
    public Storage almacen; //Almacen de la civilizacion
    public Knowledge conocimiento; //Conocimiento de la civilizacion
    public Settlement asentamiento; //Asentamiento de la civilizacion que contiene ademas, informacion de los edificios

    public CivilizationContext(int id)
    {
        Civid = id;
    }

}
