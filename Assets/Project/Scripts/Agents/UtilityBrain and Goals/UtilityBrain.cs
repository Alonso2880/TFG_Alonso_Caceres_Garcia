using UnityEngine;
/*
Terminos:
-U_g: utilidad del objetivo. Es la puntuacion final que obtiene una accion. El agente elegira que hacer en base a estas puntuaciones
-c_i: consideracion individal. Es cada uno de los factores que influyen en la decision. Por ejemplo comer puede tener, hambre, confianza en el recurso, cercania...
-w_i: peso de cada consideracion. Indica cuanto importa cada consideracion (con el ejemplo de arriba el hambre tendra mas peso)
Estos tres son conceptos que forman la formula.

-c_dir: consideracion de la directiva del lider. Indica cuanto favorece una accion la orden/prioridad transmitida por el lider
-w_dir: peso de la directiva. Indica cuanto influye esa directiva en la puntuacion final. 
--La directiva nunca puede imponerse a una necesidad critica del agente--

La arquitectura es:
AgentContext
     |
     |
UtilityBrain (conectado a los cinco diferentes goals. Los goals consultaran AgentContext para saber las necesidades)
     |
     |
ActionRequest
     |
     |
ActionController
     |
     |
Agente en Unity
*/



public class UtilityBrain : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
