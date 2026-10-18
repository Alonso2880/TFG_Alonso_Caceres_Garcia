using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

//Sistema que se encarga de crear, gestionar y destruir las civilzaciones
public class CivilizationManager : MonoBehaviour
{
    private List<CivilizationContext> civilizations = new List<CivilizationContext>();

    //Funcion que se encarga de crear un acivilizacion
    public CivilizationContext CreateCivilization(int id)
    {
        CivilizationContext civilization = new CivilizationContext(id);
        civilizations.Add(civilization);

        Debug.Log("Civilizacion creada con ID : " + id);

        return civilization;
    }

    //Funcion que se encarga de destruir una civilizacion
    public void DestroyCivilization(int id)
    {
        CivilizationContext civilization = civilizations.Find(c => c.Civid == id); //Busca y encuentra la civilizacion con ese id

        if(civilization != null)
        {
            civilizations.Remove(civilization);
            Debug.Log("Civilizacion destruida con ID : " + id);
        }
    }
}
