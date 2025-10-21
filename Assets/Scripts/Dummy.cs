using UnityEngine;

public class Dummy : MonoBehaviour, IDamageable
{
    [SerializeField] float health;

    public void Damage(GameObject damageInflicter, float amount)
    {
        TakeDamage(amount);
    }

    void TakeDamage(float damage)
    {
        health -= Mathf.Abs(damage);
        if(health < 0)
        {
            gameObject.SetActive(false);
        }
    }
}
