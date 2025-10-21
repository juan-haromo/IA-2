using UnityEngine;

public class DPSHealth : MonoBehaviour, IDamageable, IHealable
{
    [SerializeField] float health;
    [SerializeField] float dangerHp;
    [SerializeField] StateMachine owner;
    public void Damage(GameObject damageInflicter, float amount)
    {
        health -= Mathf.Abs(amount);
        if (health <= dangerHp)
        {
            owner.blackBoard.SetValue<bool>("InDanger", true);
        }
    }

    public void Heal(float amount)
    {
        health += Mathf.Abs(amount);
        if (health > dangerHp)
        {
            owner.blackBoard.SetValue<bool>("InDanger", false);
        }
    }
}