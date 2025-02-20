using UnityEngine;
using System.Collections.Generic;

public class Defender : MonoBehaviour
{
    public float speed = 1.0f;
    public float inactivationTime = 4f;
    private Transform target;
    private bool isActive = true;

    void Update()
    {
        if (!isActive) return;
        FindTarget();
    }

    void FindTarget()
    {
        List<Attacker> attackers = new List<Attacker>(FindObjectsOfType<Attacker>());
        attackers.RemoveAll(a => !a.gameObject.activeSelf || !a.enabled);

        if (attackers.Count > 0)
        {
            attackers.Sort((a, b) => Vector3.Distance(transform.position, a.transform.position)
                        .CompareTo(Vector3.Distance(transform.position, b.transform.position)));
            target = attackers[0].transform;
            MoveToTarget(target.position);
        }
    }

    void MoveToTarget(Vector3 targetPos)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attacker"))
        {
            Attacker caughtAttacker = other.GetComponent<Attacker>();
            if (caughtAttacker != null)
            {
                if (caughtAttacker.hasBall)
                {
                    PassBallToAnotherAttacker(caughtAttacker);
                }
                caughtAttacker.BecomeInactive();
                BecomeInactive();
            }
        }
    }

    void PassBallToAnotherAttacker(Attacker caughtAttacker)
    {
        List<Attacker> activeAttackers = new List<Attacker>(FindObjectsOfType<Attacker>());
        activeAttackers.RemoveAll(a => !a.gameObject.activeSelf || !a.enabled || a == caughtAttacker);

        if (activeAttackers.Count > 0)
        {
            Attacker nearestAttacker = activeAttackers[0];
            float minDist = Vector3.Distance(caughtAttacker.transform.position, nearestAttacker.transform.position);

            foreach (var attacker in activeAttackers)
            {
                float dist = Vector3.Distance(caughtAttacker.transform.position, attacker.transform.position);
                if (dist < minDist)
                {
                    nearestAttacker = attacker;
                    minDist = dist;
                }
            }

            nearestAttacker.PickUpBall();
        }
        else
        {
            GameManager.Instance.EndMatch(false); // Defender Wins
        }
    }

    public void BecomeInactive()
    {
        isActive = false;
        GetComponent<Renderer>().material.color = Color.gray;
        Invoke(nameof(Reactivate), inactivationTime);
    }

    void Reactivate()
    {
        isActive = true;
        GetComponent<Renderer>().material.color = Color.white;
    }
}
