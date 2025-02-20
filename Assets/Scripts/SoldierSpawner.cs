using UnityEngine;

public class SoldierSpawner : MonoBehaviour
{
    public GameObject attackerPrefab;
    public GameObject defenderPrefab;
    public Transform attackerSpawnPoint;
    public Transform defenderSpawnPoint;

    private int attackerCost = 2;
    private int defenderCost = 3;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Tap to spawn
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("AttackLand"))
                {
                    SpawnAttacker();
                }
                else if (hit.collider.CompareTag("DefenseLand"))
                {
                    SpawnDefender();
                }
            }
        }
    }

    void SpawnAttacker()
    {
        if (EnergyBar.Instance.CanSpawn(attackerCost))
        {
            Instantiate(attackerPrefab, attackerSpawnPoint.position, Quaternion.identity);
            EnergyBar.Instance.UseEnergy(attackerCost);
        }
    }

    void SpawnDefender()
    {
        if (EnergyBar.Instance.CanSpawn(defenderCost))
        {
            Instantiate(defenderPrefab, defenderSpawnPoint.position, Quaternion.identity);
            EnergyBar.Instance.UseEnergy(defenderCost);
        }
    }
}
