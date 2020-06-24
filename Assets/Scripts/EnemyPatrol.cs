using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;

    private Transform target;
    private int waypointIndex = 0;
    private float speed = 30f;

    void Start()
    {
        target = patrolPoints[0];
    }
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetLoop();
        }
    }
    void GetLoop()
    {
        waypointIndex++;
        speed = 10;

        if (waypointIndex == 2)
        {
            waypointIndex = 0;
            speed = 30;
        }

        target = patrolPoints[waypointIndex];
    }
}
