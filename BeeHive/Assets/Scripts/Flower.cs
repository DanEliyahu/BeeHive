using UnityEngine;
using UnityEngine.UI;

public class Flower : MonoBehaviour
{
    [SerializeField] private float health = 10;
    [SerializeField] private Slider healthSlider;

    private float currentHealth;

    private void Start()
    {
        currentHealth = health;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthSlider.value = currentHealth / health;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}