using UnityEngine;

public class ShieldBonus : MonoBehaviour
{
    public float shieldDuration = 5f;
    public float lifetime = 8f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ActivateShield(shieldDuration);
            }
            Destroy(gameObject);
        }
    }
}