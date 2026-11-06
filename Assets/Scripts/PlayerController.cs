using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement & Shooting")]
    public float moveSpeed = 7f;
    public GameObject playerBulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.1f;

    [Header("Shield Visual")]
    public GameObject shieldVisual;

    private float nextFireTime = 0f;
    private bool isShielded = false;
    private Coroutine shieldCoroutine;

    private void Start()
    {
        if (shieldVisual != null)
            shieldVisual.SetActive(false);
    }

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector3 moveDir = new Vector3(moveX, moveY, 0f).normalized;
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
        else if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        if (playerBulletPrefab != null && firePoint != null)
        {
            Instantiate(playerBulletPrefab, firePoint.position, Quaternion.identity);

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.playerShootSound);
            }
        }
    }

    public void ActivateShield(float duration)
    {
        if (shieldCoroutine != null)
            StopCoroutine(shieldCoroutine);

        shieldCoroutine = StartCoroutine(ShieldRoutine(duration));
    }

    private IEnumerator ShieldRoutine(float duration)
    {
        isShielded = true;
        if (shieldVisual != null) shieldVisual.SetActive(true);

        yield return new WaitForSeconds(duration);

        isShielded = false;
        if (shieldVisual != null) shieldVisual.SetActive(false);
    }

    public void TakeDamage()
    {
        if (isShielded) return;

        GameManager.Instance.GameOver();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            Destroy(other.gameObject);
            TakeDamage();
        }
    }
}