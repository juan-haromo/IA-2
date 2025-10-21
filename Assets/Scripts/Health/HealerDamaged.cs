using UnityEngine;

public class Healer : MonoBehaviour, IDamageable, IHealable
{
    [SerializeField] float health;
    [SerializeField] float minHealth;
    [SerializeField] StateMachine owner;

    public void Damage(GameObject damageInflicter, float amount)
    {
        health -= Mathf.Abs(amount);
        if (health <= minHealth)
        {
            owner.blackBoard.SetValue<Transform>("Attacker", damageInflicter.transform);
            owner.blackBoard.GetValue<StateMachine>("Ally").blackBoard.SetValue<Transform>("Attacker", damageInflicter.transform);
            owner.blackBoard.SetValue<bool>("InDanger", true);
        }
    }

    public void Heal(float amount)
    {
        health += Mathf.Abs(amount);
        if(health > minHealth)
        {
            owner.blackBoard.SetValue<bool>("InDanger", false);
        }
    }
}