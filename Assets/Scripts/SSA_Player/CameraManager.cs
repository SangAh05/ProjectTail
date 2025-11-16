using ProjectTail;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CameraManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] InputReader input;
    //[SerializeField] CinemachineVirtualCameraBase freeLookCam;
    [SerializeField] CinemachineFreeLook freeLookCam;
    //[SerializeField] CinemachineCamera freeLookCam;

    [Header("Settings")]
    [SerializeField, Range(0.5f, 3.0f)] float speedMultiplier = 1f;

    bool isRMBPressed;
    bool cameraMovementLock;

    private void OnEnable()
    {
        input.Look += OnLook;
        input.EnableMouseControlCamera += OnEnableMouseControlCamera;
        input.DisableMouseControlCamera += OnDisableMouseControlCamera;
    }
    void OnDisable()
    {
        input.Look -= OnLook;
        input.EnableMouseControlCamera -= OnEnableMouseControlCamera;
        input.DisableMouseControlCamera -= OnDisableMouseControlCamera;
    }

    void OnLook(Vector2 cameraMovement, bool isDeviceMouse)
    {
        if (cameraMovementLock) return;

        if (isDeviceMouse && !isRMBPressed) return;

        float deviceMultiplier = isDeviceMouse ? Time.fixedDeltaTime : Time.deltaTime;

        //freeLookCam.m_XAxis.m_InputAxisValue = cameraMovement.x * speedMultiplier * deviceMultiplier;
        //freeLookCam.m_XAxis.m_InputAxisValue = cameraMovement.y * speedMultiplier * deviceMultiplier;

        //freeLookCam.m_XAxis.m_InputAxisValue = value.x * speedMultiplier * deviceMultiplier;
        //freeLookCam.m_YAxis.m_InputAxisValue = value.y * speedMultiplier * deviceMultiplier;
    }
    void OnEnableMouseControlCamera()
    {
        isRMBPressed = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        StartCoroutine(DisableMouseForFrame());
    }
    void OnDisableMouseControlCamera()
    {
        isRMBPressed = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        freeLookCam.m_XAxis.m_InputAxisValue = 0f;
        freeLookCam.m_YAxis.m_InputAxisValue = 0f;
    }

    IEnumerator DisableMouseForFrame()
    {
        cameraMovementLock = true;
        yield return new WaitForEndOfFrame();
        cameraMovementLock = false;
    }


}
