using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private float health = 100.0f;

    private void OnCollisionEnter(Collision other)
    {
        print("Hit");
        if (other.gameObject.CompareTag("Bullet"))
        {
            health -= 10.0f;
            if (health <= 0)
            {
                Destroy(gameObject);
                ScoreManager.Instance.UpdateScore(100);
            }
        }
    }

}
