using UnityEngine;

public class CamaraController : MonoBehaviour
{
    [Header("Movimiento WASD")]
    [SerializeField] float vel = 20f;

    [Header("Zoom")]
    [SerializeField] float Zomvel = 10f;
    [SerializeField] float minOrthoSize = 5f;
    [SerializeField] float maxOrthoSize = 60f;
    [SerializeField] float minFov = 20f;
    [SerializeField] float maxFov = 90f;

    public Camera camera;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    void Update()
    {
        MovementCamera();
        ZoomCamera();
    }

    private void MovementCamera()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //Mover la camara
        Vector3 move = new Vector3(horizontal, 0f, vertical) * vel * Time.deltaTime;
        transform.position += move;
    }

    private void ZoomCamera()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Approximately(scroll, 0f)) return;

        if (camera.orthographic)
        {
            camera.orthographicSize = Mathf.Clamp(
                camera.orthographicSize - scroll * Zomvel,
                minOrthoSize, maxOrthoSize);
        }
        else
        {
            camera.fieldOfView = Mathf.Clamp(
                camera.fieldOfView - scroll * Zomvel,
                minFov, maxFov);
        }
    }
}
