using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Transform target;

    public float bulletSpeed = 70f;
    public GameObject hitEffect;

    public void Bullet(Transform targetObject)
    {
        target = targetObject;
    }
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distance = bulletSpeed * Time.deltaTime;

        if(direction.magnitude <= distance)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction * distance, Space.World);
    }
    void HitTarget()
    {
        GameObject damageEffect =(GameObject)Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(damageEffect, 2f);
        Destroy(gameObject);
    }
}
