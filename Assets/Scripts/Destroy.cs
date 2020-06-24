using UnityEngine;

public class Destroy : MonoBehaviour
{
    private EnemyController hp;
    private void Start()
    {
        hp = GetComponentInChildren<EnemyController>();
    }
    private void Update()
    {
        if (hp.Health == 0)
        {
            Destroy(gameObject, 0.4f); ;
        }
    }
}
