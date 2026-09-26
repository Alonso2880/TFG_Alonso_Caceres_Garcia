using UnityEngine;
using UnityEngine.AI;

/*
 Este script es temporal para mover el cubo de una punta a otra de acuerdo lo que pone en la semana 3
 */

public class ClickToMove : MonoBehaviour
{
    private NavMeshAgent agent;
    public Camera camara;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //Obtiene el boton izquierdo del raton
        {
            Ray ray = camara.ScreenPointToRay(Input.mousePosition); //Creamos un ray que hace que obtenga las coordenadas donde ha pulsado el raton. Con camara.ScreenPointToRay se lanza un rayo en 3D para averiguar esa posicion

            if (Physics.Raycast(ray, out RaycastHit hit, 200f)) //Comprueba si el raycast ha chocado con algo de la escena. RaycastHit guarda informacion en hit sobre lo que ha golpeado y 200 es la distancia maxima del raycast
            {
                if(NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, 2f , NavMesh.AllAreas)) //A partir del punto donde se hizo clic, busca un punto del NavMesh a una distancia maxima de 2 unidades, si lo hace lo aguarda en navHit
                {
                    Debug.Log("Navegando al destiono");
                    agent.SetDestination(navHit.position); //le da un destino al agente
                }
                else
                {
                    Debug.Log("Destino inalcanzable");
                }
            }
        }
    }
}
