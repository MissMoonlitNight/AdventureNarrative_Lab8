using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraController : MonoBehaviour
{
    [Header("Настройки камеры")]
    public Transform playerCamera;
    public Transform followTarget;
    public Vector3 followOffset = new Vector3(0, 0.8f, 0);

    [Header("Настройки переключения")]
    public float transitionSpeed = 4f;

    [Header("Настройки DOF ")]
    public PostProcessVolume postProcessVolume;

    private bool isFixedMode = false;
    private Transform fixedPoint;
    private DepthOfField dof;

    public bool IsInFixedMode => isFixedMode;

    void Start()
    {
        
        if (postProcessVolume != null && postProcessVolume.profile.TryGetSettings(out DepthOfField tempDof))
        {
            dof = tempDof;
            dof.enabled.value = false; 
        }
    }

    void LateUpdate()
    {
        if (isFixedMode && fixedPoint != null)
        {
            // Фиксированная камера 
            playerCamera.position = Vector3.Lerp(playerCamera.position, fixedPoint.position, transitionSpeed * Time.deltaTime);
            playerCamera.rotation = Quaternion.Slerp(playerCamera.rotation, fixedPoint.rotation, transitionSpeed * Time.deltaTime);
        }
        else
        {
            // Обычный режим: камера следует за игроком
            Vector3 targetPos = followTarget.position + followOffset;
            playerCamera.position = Vector3.Lerp(playerCamera.position, targetPos, transitionSpeed * Time.deltaTime);

            // Камера смотрит вперёд от игрока (на уровне глаз)
            playerCamera.LookAt(followTarget.position + followTarget.forward * 10f + Vector3.up * 1.5f);
        }
    }

    public void SetFixedCamera(Transform point, float duration)
    {
        isFixedMode = true;
        fixedPoint = point;

        
        if (dof != null)
        {
            dof.enabled.value = true;
        }

        Invoke(nameof(DisableFixedMode), duration);
    }

    private void DisableFixedMode()
    {
        isFixedMode = false;
        fixedPoint = null;

        if (dof != null)
        {
            dof.enabled.value = false;
        }
    }
}