using UnityEngine;
using System.Collections.Generic;

public class Attacker : MonoBehaviour
{
    public float normalSpeed = 1.5f;
    public float carryingSpeed = 0.75f;
    public float inactivationTime = 2.5f;

    public bool hasBall = false;
    private Transform ball;
    private Vector3 targetGate;
    private bool isActive = true;

    void Start()
    {
        targetGate = GameObject.FindGameObjectWithTag("EnemyGate").transform.position;
    }

    void Update()
    {
        if (!isActive) return;

        if (hasBall)
        {
            MoveToTarget(targetGate, carryingSpeed);
        }
        else
        {
            FindBall();
        }
    }

    void FindBall()
    {
        if (ball == null)
        {
            ball = GameObject.FindGameObjectWithTag("Ball")?.transform;
        }
        if (ball != null)
        {
            MoveToTarget(ball.position, normalSpeed);
        }
    }

    void MoveToTarget(Vector3 target, float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") && !hasBall)
        {
            PickUpBall();
        }
    }

    public void PickUpBall()
    {
        hasBall = true;
        ball.SetParent(transform);
        ball.localPosition = Vector3.zero;
    }

    public void DropBall()
    {
        hasBall = false;
        ball.SetParent(null);
    }

    public void BecomeInactive()
    {
        isActive = false;
        GetComponent<Renderer>().material.color = Color.gray;
        DropBall();
        Invoke(nameof(Reactivate), inactivationTime);
    }

    void Reactivate()
    {
        isActive = true;
        GetComponent<Renderer>().material.color = Color.white;
    }
}
