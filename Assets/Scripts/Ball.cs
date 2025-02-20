using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyGate"))
        {
            GameManager.Instance.BallReachedGate();
        }
    }
}
