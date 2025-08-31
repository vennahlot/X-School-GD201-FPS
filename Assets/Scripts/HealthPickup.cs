using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int healthToAdd = 50;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().AddHealth(healthToAdd);
            Destroy(gameObject);
        }
    }
    
}
