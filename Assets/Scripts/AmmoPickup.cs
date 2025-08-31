using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] private int ammoToAdd = 30;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().weaponController.AddAmmo(ammoToAdd);
            Destroy(gameObject);
        }
    }
    
}
