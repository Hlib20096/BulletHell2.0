using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float lifetime = 6f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
}