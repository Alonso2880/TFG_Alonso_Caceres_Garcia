using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class CheatManager : MonoBehaviour
{
    [Header("Simulation Clock")]
    [SerializeField]
    private SimulationClock simulationClock;

    [Header("Cheat Canvas")]
    [SerializeField]
    private GameObject cheatCanvas;

    [Header("Input Field")]
    public TMP_InputField inputField;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            cheatCanvas.SetActive(!cheatCanvas.activeSelf);
        }
    }

    public void UpdateSimulationSpeed()
    {
        if (float.TryParse(inputField.text, out float speed))
        {
            simulationClock.simulationSpeed = speed;
        }
    }
}
