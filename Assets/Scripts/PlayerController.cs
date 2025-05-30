using System.Collections;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private MyController myControl;
    private Rigidbody rb;

    public float speed = 3f;
    public float turnSpeed = 100f;
    public float tackleForce = 8f;
    private Vector2 moveInput;
    private bool attack = true;
    


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        myControl = new MyController();
        myControl.Player.Jump.performed += OnJumpPerformed;
        myControl.Player.Move.performed += OnMovePerformed;
        myControl.Player.Move.canceled += OnMoveCanceled;
        myControl.Player.Attack.performed += OnAttackPerformed;
    }


    void Update()
    {
        Vector3 move = transform.forward * moveInput.y * speed * Time.deltaTime;
        rb.MovePosition(rb.position + move);


        float turn = moveInput.x * turnSpeed * Time.deltaTime;
        transform.Rotate(0, turn, 0); // Y軸で回転


    }

    private void OnEnable()
    {   
        myControl.Enable();
    }

    private void OnDisable()
    {
        myControl.Disable();
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        
    }
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        if (attack)
        {
            attack = false;
            rb.AddForce(transform.forward * tackleForce, ForceMode.Impulse);
            StartCoroutine(AttackStop());
            StartCoroutine(AttackCoolDown());
        }
    }
    
    
    
    private IEnumerator AttackStop()
    {
        
        yield return new WaitForSeconds(0.2f);
        rb.linearVelocity= Vector3.zero;
        
    }

    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        attack = true;

    }

   

    public void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Ground"))
        {
            rb.constraints &= ~RigidbodyConstraints.FreezeRotation;  // 回転制限を解除
            GameManager.Instance.LoseText();
        }
    }
}