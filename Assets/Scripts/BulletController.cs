using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float timeToLive = 3.0f;
    [SerializeField] private GameObject bulletImpactPrefab;
    [SerializeField] private AudioClip bulletImpactSound;

    void Start()
    {
        Destroy(gameObject, timeToLive);
    }

    void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Target"))
        {
            Instantiate(bulletImpactPrefab, other.contacts[0].point, Quaternion.LookRotation(other.contacts[0].normal));
            AudioSource.PlayClipAtPoint(bulletImpactSound, other.contacts[0].point);
        }
        Destroy(gameObject);
    }
}
