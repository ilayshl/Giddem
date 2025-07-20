using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHP = 10;
    [SerializeField] private float healthRateInterval = 4f;
    [SerializeField] private int healthRegenAmount = 1;

    [Header("UI")]
    [SerializeField] private GameObject hpBarCanvas;
    private Image hpBarImage;

    private int currentHP;
    private float healTimer;

    private void Start()
    {
        currentHP = maxHP;
        healTimer = 0f;
        hpBarImage = hpBarCanvas.GetComponent<Image>();
        UpdateHPBar();
        hpBarCanvas.SetActive(false);
    }

    private void Update()
    {
        //HealOverTime();
    }

    private void HealOverTime()
    {
        if (currentHP <= 0) return;  // Don't heal if dead

        healTimer += Time.deltaTime;
        if (healTimer >= healthRateInterval)
        {
            healTimer = 0f;
            Heal(healthRegenAmount);
        }
    }

    public void TakeDamage(int amount)
    {
        if (currentHP <= 0) return;  // Already dead

        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        UpdateHPBar();

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Heal(int amount)
    {
        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UpdateHPBar();
    }

    private void UpdateHPBar()
    {
        if (hpBarImage != null)
        {
            hpBarImage.fillAmount = (float)currentHP / maxHP;
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} died!");
        hpBarCanvas.SetActive(false);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CancelInvoke(nameof(DisableHpBar));
            Debug.Log("player hit");
            hpBarCanvas.SetActive(true);
            TakeDamage(1);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke(nameof(DisableHpBar), 5);
        }
    }

    private void DisableHpBar()
    {
        Debug.Log("canvas down");
        hpBarCanvas.SetActive(false);
    }
}
