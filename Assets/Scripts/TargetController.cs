using UnityEngine;

public class TargetController : MonoBehaviour
{
    public Transform playerTransform;
    [SerializeField] private float baseScore = 100.0f;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private AudioClip explosionSound;
    private float timeToLive = 0.0f;

    void Update()
    {
        Quaternion rotation = Quaternion.LookRotation(playerTransform.position - transform.position);
        rotation.x = 0;
        rotation.z = 0;
        transform.rotation = rotation;
        timeToLive += Time.deltaTime;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            ScoreManager.Instance.UpdateScore(baseScore, timeToLive);
            TargetSpawn.Instance.TargetDestroyed();
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
            Destroy(gameObject);
        }
    }
}
