using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private CharacterController controller;
    private CameraController camCtrl;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        // Находим скрипт управления камерой на сцене
        camCtrl = FindObjectOfType<CameraController>();
    }

    void Update()
    {
        if (camCtrl != null && camCtrl.IsInFixedMode)
        {
            return;
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = transform.right * h + transform.forward * v;

        controller.Move(move * speed * Time.deltaTime);
    }
}