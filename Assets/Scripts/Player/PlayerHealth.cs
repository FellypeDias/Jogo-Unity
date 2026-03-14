using UnityEngine;
using TMPro;


public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;
    public TextMeshProUGUI vidaText;


    void Start()
    {
        currentHealth = maxHealth;
        currentHealth = maxHealth;
        UpdateVidaText();
    }

   public void TakeDamage(int amount)
{
    currentHealth -= amount;
    UpdateVidaText();

    if (currentHealth <= 0)
    {
        Die();
    }
}

    void Die()
    {
        Debug.Log("Você morreu!");
        // Aqui você pode desativar o jogador, mostrar UI, etc.
        gameObject.SetActive(false);
    }
    void UpdateVidaText()
{
    if (vidaText != null)
    {
        vidaText.text = "Vida: " + currentHealth;
    }
}

}
