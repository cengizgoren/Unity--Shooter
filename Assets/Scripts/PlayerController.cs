using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform target;
    private Ammo ammo;

    private float fireCountdown = 0f;
    private bool isReloading = false;
    private readonly float lookSpeed = 10f;

    [Header("")]
    public Transform gunRotate;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject reloadText;


    [Header("Bullet")]
    public float reloadTime = 1f;
    public float fireRate = 1f;

    private void Start()
    {
        InvokeRepeating("FindNearestTarget", 0f, 0.5f);
        ammo = GetComponent<Ammo>();
    }
    private void Update()
    {
        if (target == null) return;
        if (isReloading) return;

        LookAtTarget();

        if (fireCountdown <= 0f)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                transform.position = new Vector3(0f, 1f, -21f);      /////
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);  /////
                
                Shoot();
                fireCountdown = 1 / fireRate;
            }
            else
            {
                transform.position = new Vector3(0f, 0.15f, -21f);   ////
                transform.rotation = Quaternion.Euler(0f, 0f, 90f);  ////
                
                target = null;
            }
        }

        if (ammo.CurrentAmmo <= 0)
        {
            reloadText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.R))
                StartCoroutine(Reload());
            return;
        }

        fireCountdown -= Time.deltaTime;
    }
    void FindNearestTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
    void LookAtTarget()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotatiom = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(gunRotate.rotation, lookRotatiom, Time.deltaTime * lookSpeed).eulerAngles;
        gunRotate.rotation = Quaternion.Euler(0f, rotation.y, rotation.z);
    }
    public void Shoot()
    {
        GameObject bullet = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        BulletController bulletController = bullet.GetComponent<BulletController>();

        if(bulletController != null)
        {
            bulletController.Bullet(target);
        }

        ammo.Fire(1);
    }
    IEnumerator Reload()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        reloadText.SetActive(false);
        ammo.CurrentAmmo = ammo.MaxAmmo;
        isReloading = false;
    }
}
