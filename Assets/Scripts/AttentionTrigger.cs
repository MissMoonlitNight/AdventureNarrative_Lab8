using UnityEngine;
public class AttentionTrigger : MonoBehaviour
{
    public Transform cameraPoint; // точка, куда смотреть
    public float cameraDuration = 2f;
    public string message; // текст подсказки
    public bool destroyAfterTrigger = true;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CameraController camCtrl = FindObjectOfType<CameraController>();
            if (camCtrl != null && cameraPoint != null)
                camCtrl.SetFixedCamera(cameraPoint, cameraDuration);
            if (!string.IsNullOrEmpty(message))
            {
                HintManager.Instance?.ShowHint(message, cameraDuration);
            }
            if (destroyAfterTrigger)
                Destroy(gameObject);
        }
    }
}
