using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public static EnergyBar Instance;

    public Slider energySlider;
    //public Text energyText;

    private float currentEnergy = 0;
    private float maxEnergy = 6f;
    private float regenRate = 0.5f; // 0.5 per second

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        currentEnergy = 0;
        UpdateEnergyUI();
        InvokeRepeating(nameof(RegenerateEnergy), 1f, 1f); // Regenerate every second
    }

    void RegenerateEnergy()
    {
        if (currentEnergy < maxEnergy)
        {
            currentEnergy += regenRate;
            if (currentEnergy > maxEnergy) currentEnergy = maxEnergy;
            UpdateEnergyUI();
        }
    }

    public bool CanSpawn(int cost)
    {
        return currentEnergy >= cost;
    }

    public void UseEnergy(int cost)
    {
        if (currentEnergy >= cost)
        {
            currentEnergy -= cost;
            UpdateEnergyUI();
        }
    }

    void UpdateEnergyUI()
    {
        energySlider.value = currentEnergy;
        //energyText.text = Mathf.FloorToInt(currentEnergy) + " / " + Mathf.FloorToInt(maxEnergy);
    }
}
