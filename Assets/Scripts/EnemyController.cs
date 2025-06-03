using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    private Transform player;
    private Rigidbody rb;

    public float speed = 3f;
    public float tackleForce = 8f;
    public float attackCooldown = 2f; // ƒ^ƒbƒNƒ‹‚ÌŠÔŠu
    private float lastAttackTime = -999f;
    public bool canControl = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameObject playerObj = GameObject.FindWithTag("Players");
        player = playerObj.transform;

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
        if (other.CompareTag("Ground"))
        {
            rb.constraints = RigidbodyConstraints.None; // ‰ñ“]§ŒÀ‚ğ‰ğœ
            canControl = false;
            GameManager.Instance.WinText();
            GameManager.Instance.OnEnemyDefeated();
        }
    }
}
