using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    private Transform player;
    private Rigidbody rb;

    public float speed = 3f;
    public float tackleForce = 8f;
    public float attackCooldown = 2f; // タックルの間隔
    private float lastAttackTime = -999f;
    public bool canControl = false;
    private bool hasTrigger = false;
   

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameObject playerObj = GameObject.FindWithTag("Players");
        player = playerObj.transform;
        Collider collider = GetComponent<Collider>();
    }

   
    void Update()
    {
        if (!canControl)
            return;


        if (player == null) return;

        Vector3 direction = (player.position - transform.position);
        direction.y = 0; 
        float distance = direction.magnitude;

        if (distance > 1f)
        {
            Vector3 moveDir = direction.normalized;
            transform.rotation = Quaternion.LookRotation(moveDir);
            rb.MovePosition(rb.position + moveDir * speed * Time.deltaTime);
        }
        else if (Time.time - lastAttackTime > attackCooldown)
        {
            rb.AddForce(transform.forward * tackleForce, ForceMode.Impulse);
            lastAttackTime = Time.time;
        }
    }

    

    public void OnTriggerEnter(Collider other)
    {
        if (hasTrigger) return;

        if (other.CompareTag("Ground"))
        {
            hasTrigger = true;

            rb.constraints = RigidbodyConstraints.None; // 回転制限を解除
            canControl = false;
            
            GameManager.Instance.WinText();
            GameManager.Instance.OnEnemyDefeated();

           
        }
    }
}
