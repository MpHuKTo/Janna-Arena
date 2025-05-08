using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour
{
    public PlayerController player; // Ссылка на PlayerController
    public Image HPLine;
    public static HPbar instance;



    private void Start()
    {
        if (player != null)
        {
            player.OnHealthChanged += UpdateHealthBar; // Подписываемся на событие
            UpdateHealthBar(player.health, player.health); // Инициализируем панель здоровья
        }
        else
        {
            Debug.LogError("PlayerController не привязан к HPbar.");
        }
    }

    private void Awake()
    {
        instance = this;
    } 

    public void UpdateHealthBar(float currentHP, float maxHP)
    {
        if (HPLine != null)
        {
            HPLine.fillAmount = currentHP / maxHP;
        }
    }

    private void OnDestroy()
    {
        if (player != null)
        {
            player.OnHealthChanged -= UpdateHealthBar; // Отписываемся от события
        }
    }
}
