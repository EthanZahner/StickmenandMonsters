using UnityEngine;
using TMPro;

public class StatsUpdater : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] TMP_Text attackText;
    [SerializeField] TMP_Text spellAttackText;
    [SerializeField] TMP_Text manaText;
    [SerializeField] TMP_Text defenseText;
    [SerializeField] TMP_Text speedText;
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text goldText;
    [SerializeField] TMP_Text armorClassText;
    [SerializeField] TMP_Text maxHealthText;
    [SerializeField] TMP_Text maxManaText;

    private FighterStats playerStats;
    private bool statsChanged = true; // Track if stats have changed

    private void Awake()
    {
        playerStats = FindObjectOfType<FighterStats>();
    }

    private void Update()
    {
        if (statsChanged)
        {
            UpdateStatsDisplay();
            statsChanged = false; // Reset flag after updating
        }
    }

    // This method can be called whenever stats change (e.g., when equipping or unequipping items)
    public void OnStatsChanged()
    {
        statsChanged = true; // Mark stats as changed to trigger an update
    }

    // This function will update the UI with the current stats
    private void UpdateStatsDisplay()
    {
        if (playerStats == null) return;

        attackText.text = playerStats.attack.ToString();
        spellAttackText.text = playerStats.spellAttack.ToString();
        manaText.text = playerStats.mana.ToString();
        defenseText.text = playerStats.defense.ToString();
        speedText.text = playerStats.speed.ToString();
        healthText.text = playerStats.health.ToString();
        maxHealthText.text = "/ " + playerStats.startHealth.ToString();
        maxManaText.text = "/ " + playerStats.startMana.ToString();

        // Display the new stats
        goldText.text = playerStats.gold.ToString();
        armorClassText.text = playerStats.armorClass.ToString();
    }
}
