using UnityEngine;

public class StegeOut : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Players"))
        {
            Debug.Log("ïâÇØ");
        }

        if(other.CompareTag("Enemy"))
        {
            Debug.Log("èüÇø");
        }
    }
}
