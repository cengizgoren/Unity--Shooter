using System;
using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float maxHealth = 200f;
    public GameObject destroyEffect;
    private float health;
    private int damageRange;

    public GameObject damageTextPrefab;

    public event EventHandler<HealthChangedEventArgs> OnHealthChanged;
    public float MaxHealth => maxHealth;
    public float Health
    {
        get => health;
        private set
        {
            health = Mathf.Clamp(value, 0, maxHealth);

            OnHealthChanged?.Invoke(this, new HealthChangedEventArgs
            {
                Health = health,
                MaxHealth = maxHealth
            });
        }
    }
    private void Start() => Health = maxHealth;
    private void Update()
    {
        if(health <= 0)
        {
            DeadEffect();
        }
    }
    private void TakeDamage(float value)
    {
        value = Mathf.Max(value, 0f);
        damageRange = UnityEngine.Random.Range(5, 30);
        Health -= value;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet") && damageTextPrefab)
        {
            TakeDamage(damageRange);
            ShowDamageText();
        }
    }
    void ShowDamageText()
    {
        GameObject damageText = Instantiate(damageTextPrefab, transform.position, Quaternion.identity, transform);
        damageText.GetComponent<TextMesh>().text = damageRange.ToString();

        if(damageRange >= 29)
        {
            damageText.GetComponent<TextMesh>().color = Color.red;
            damageText.GetComponent<TextMesh>().fontSize = 35;
            StartCoroutine("SlowMo");
        }
    }
    void DeadEffect()
    {
        GameObject deadEffect = (GameObject)Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(deadEffect, 1f);
    }
    IEnumerator SlowMo()
    {
        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        yield return new WaitForSeconds(0.3f);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
    public class HealthChangedEventArgs : EventArgs
    {
        public float Health { get; set; }
        public float MaxHealth { get; set; }
    }
}
