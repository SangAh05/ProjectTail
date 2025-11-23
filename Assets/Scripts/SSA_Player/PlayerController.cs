//using KBCore.Refs;
//using Cinemachine;
using Unity.Burst.Intrinsics;
using Unity.Cinemachine;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace ProjectTail
{
    public class PlayerController : MonoBehaviour /*ValidatedMonoBehaviour*/
    {

        //[Header("References")]
        //[SerializeField, Self] CharacterController controller;
        //[SerializeField, Self] Animator animator;
        //[SerializeField, Anywhere] CinemachineFreeLook freeLookCam; 
        //[SerializeField, Anywhere] InputReader inputReader; 

        [Header("References")]
        [SerializeField] CharacterController controller;
        [SerializeField] Animator animator;
        //[SerializeField] CinemachineFreeLook freeLookCam;
        //[SerializeField] CinemachineVirtualCameraBase freeLookCam;
        [SerializeField] CinemachineCamera freeLookCam;
        [SerializeField] InputReader input;
     


        [Header("Settings")]
        [SerializeField] float moveSpeed = 6f;
        [SerializeField] float rotationSpeed = 15.0f;
        [SerializeField] float smoothTime = 0.2f;

        Transform mainCam;
        float currentSpeed;
        float velocity;

        // animator parameter 
        static readonly int speed = Animator.StringToHash("Speed");

        void Awake()
        {
            mainCam = Camera.main.transform; 
            freeLookCam.Follow = transform;
            freeLookCam.LookAt = transform;
            freeLookCam.OnTargetObjectWarped(transform, transform.position - freeLookCam.transform.position - Vector3.forward);
        }

        void Start() => input.EnablePlayerActions();
   

        void Update()
        {
            HandleMovement();
            UpdateAnimator();
        }

        void UpdateAnimator()
        {
            animator.SetFloat(speed, currentSpeed);
        }

        void HandleMovement()
        {
            var movementDirection = new Vector3(input.Direction.x, 0f, input.Direction.y).normalized;

            var adjustedDirection = Quaternion.AngleAxis(mainCam.eulerAngles.y, Vector3.up) * movementDirection;

            if (adjustedDirection.magnitude > 0f)
            {
                HandleRotation(adjustedDirection);

                HandleCharacterController(adjustedDirection);

                SmoothSpeed(adjustedDirection.magnitude);
            }
            else
            {
                SmoothSpeed(0f);
            }
        }

        void HandleCharacterController(Vector3 adjustedDirection)
        {
            var adjustedMovement = adjustedDirection * (moveSpeed * Time.deltaTime);

            controller.Move(adjustedMovement);

        }

        void HandleRotation(Vector3 adjustedDirection)
        {
            var targetRotation = Quaternion.LookRotation(adjustedDirection);

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );

            transform.LookAt(transform.position + adjustedDirection);
        }

        void SmoothSpeed(float value)
        {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, value, ref velocity, smoothTime);
        }
    }

    
    
}
