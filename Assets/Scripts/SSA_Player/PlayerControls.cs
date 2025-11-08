using System.Collections;
using UnityEngine;


public class PlayerControls : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] Vector3 playerVelocity;
    [SerializeField] bool groundedPlayer;
    [SerializeField] float playerSpeed;
    [SerializeField] float gravityValue;
    [SerializeField] GameObject activeChar;
    [SerializeField] float moveHorizontal;
    [SerializeField] float moveVertical;
    [SerializeField] float speed = 4.0f;
    [SerializeField] float rotateSpeed = 4.0f;
    [SerializeField] float jumpHeight = 1.2f;
    [SerializeField] bool isJumping; 

    void Start()
    {
        playerSpeed = 4;
        gravityValue = -20;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded; // 플레이어가 땅에 있을 때 
        if (groundedPlayer && playerVelocity.y < 0 )
        {
            playerVelocity.y = 0.0f;
        }

        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

        Vector3 forwar = transform.TransformDirection(Vector3.forward);

        float curSpeed = playerSpeed * Input.GetAxis("Vertical");  //AD  입력값
        controller.SimpleMove(forwar * curSpeed);

        // 점프 
        if(Input.GetKey(KeyCode.Space) && groundedPlayer)
        {
            isJumping = true;
            // 점프애니메이션 재생
            activeChar.GetComponent<Animator>().Play("JumpFull_Normal_InPlace_SwordAndShield"); // 애니메이션 이름에 따라 바꿔줘야하나??????
            playerVelocity.y += jumpHeight;
            StartCoroutine(RestJump()); // 리셋 구간 
        }
       
        playerVelocity.y += gravityValue * Time.deltaTime; // 땅에 내려올 때 
        controller.Move(playerVelocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))  // WASD
        {
            this.gameObject.GetComponent<CharacterController>().minMoveDistance = 0.001f;
            if(isJumping == false)
            {
                activeChar.GetComponent<Animator>().Play("MoveFWD_Battle_InPlace_SwordAndShield"); // 내가 설정한 애니메이션 이름으로 바꿔줘야 동작 
            }
        }

        else
        {
            this.gameObject.GetComponent<CharacterController>().minMoveDistance = 0.901f; // 아이들포즈 
            if (isJumping == false)
            {
                activeChar.GetComponent<Animator>().Play("Idle_Battle_SwordAndShield"); 
            }
        }
    }

    IEnumerator RestJump() 
    {
        yield return new WaitForSeconds(0.8f); // ~ 초 후에 점프 리셋 
        isJumping = false;
    }
}
