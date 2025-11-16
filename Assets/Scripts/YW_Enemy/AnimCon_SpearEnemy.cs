using UnityEngine;

public class AnimCon_SpearEnemy : MonoBehaviour
{
    // Settings
    [SerializeField] private string enemyName;
    [SerializeField] private int hp;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float attackDamage;
    [SerializeField] private float waitTime; // 대기타임

    // State
    private bool isWalking;
    private bool isAction;
    private float currentTime;
    private Vector3 direction;

    // Component
    private Animator anim;
    private Rigidbody rigid;
    private CapsuleCollider col;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        currentTime = waitTime;
        isAction = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotation();
        ElapseTime();
    }

    private void Move()
    {
        if(isWalking)
        {
            rigid.MovePosition(transform.position + (transform.forward * walkSpeed * Time.deltaTime)); 
        }
    }
    private void Rotation()
    {
        if (isWalking)
        {
            Vector3 _rotation = Vector3.Lerp(transform.eulerAngles, direction, 0.01f);
            rigid.MoveRotation(Quaternion.Euler(_rotation));
        }
    }
    private void ElapseTime()
    {
        if (isAction)
        {
            currentTime -= Time.deltaTime;
            if(currentTime < 0)
            {
                Reset();
            }
        }
    }

    private void RandomAction()
    {
        isAction = true;
        //idle, walk, attack, die
        int _random = Random.Range(0, 4); // 최소 포함, 최대 포함 x
        
        if(_random == 0) Idle();
        else if(_random == 1) Walk();
        else if(_random == 2) Attack();
        else if(_random == 3) Die();
    }
    private void Reset()
    {
        isWalking = false;
        isAction = true;
        anim.SetBool("Walk", isWalking);
        //anim.SetTrigger("Idle");
        RandomAction();
        direction.Set(0f, Random.Range(0f, 360f), 0f);
    }
    private void Idle()
    {
        currentTime = waitTime;
        anim.SetTrigger("Idle");
        Debug.Log("대기");
    }
    private void Walk()
    {
        isWalking = true;
        currentTime = waitTime;
        Debug.Log("걷기");
        anim.SetBool("Walk", isWalking);
    }
    private void Attack()
    {
        currentTime = waitTime;
        Debug.Log("공격");
        //anim.SetTrigger("Idle");
    }
    private void Die()
    {
        currentTime = waitTime;
        Debug.Log("피격");
        anim.SetTrigger("Damaged");
    }
}
