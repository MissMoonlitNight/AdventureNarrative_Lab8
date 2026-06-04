using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Настройки камеры")]
    public Transform playerCamera;    
    public Transform followTarget;     
    public Vector3 followOffset = new Vector3(0, 0.8f, 0); 

    [Header("Настройки переключения")]
    public float transitionSpeed = 4f;  

    private bool isFixedMode = false;
    private Transform fixedPoint;
    public bool IsInFixedMode => isFixedMode;

    void LateUpdate()
    {
        if (isFixedMode && fixedPoint != null)
        {
            playerCamera.position = Vector3.Lerp(playerCamera.position, fixedPoint.position, transitionSpeed * Time.deltaTime);
            playerCamera.rotation = Quaternion.Slerp(playerCamera.rotation, fixedPoint.rotation, transitionSpeed * Time.deltaTime);
        }
        else
        {
            // Обычный режим: камера следует за игроком
            Vector3 targetPos = followTarget.position + followOffset;
            playerCamera.position = Vector3.Lerp(playerCamera.position, targetPos, transitionSpeed * Time.deltaTime);

            // Камера смотрит туда же, куда и игрок (или просто вперёд)
            playerCamera.LookAt(followTarget.position + followTarget.forward * 10f);
        }
    }
    public void SetFixedCamera(Transform point, float duration)
    {
        isFixedMode = true;
        fixedPoint = point;

        Invoke(nameof(DisableFixedMode), duration);
    }

    private void DisableFixedMode()
    {
        isFixedMode = false;
        fixedPoint = null;
    }
}
