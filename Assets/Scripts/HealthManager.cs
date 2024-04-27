using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public float maxHP = 100.0f;
    public float currentHP = 100.0f;
    public Slider healthBar;
    public Image fillImage;
    public AudioSource audioSource;
    public AudioClip damageClip;
    public float damageCooldown = 1f; // Cooldown duration in seconds

    private bool canTakeDamage = true;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        healthBar.maxValue = maxHP;
        healthBar.value = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = currentHP;
        if (currentHP <= 0)
        {
            Death();
        }
    }

    public void TakeDamage(int damage)
    {
        if (canTakeDamage)
        {
            currentHP = currentHP - damage;
            healthBar.value = currentHP;
            canTakeDamage = false;
            audioSource.PlayOneShot(damageClip);
            StartCoroutine(DamageCooldown());
        }
    }

    void Death()
    {
        if (currentHP <= 0)
        {
            SceneManager.LoadScene("GameOver");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Death Barrier"))
        {
            currentHP = 0;
        }
    }

    private IEnumerator DamageCooldown()
    {
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }
}