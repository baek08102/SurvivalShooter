using UnityEngine;
using UnityEngine.Events;

public class Entities : MonoBehaviour, IDamgable
{
    public float startingHealth = 100f;
    public float Health { get; private set; }
    public bool IsDead { get; private set; }

    public UnityEvent OnDead;

    protected virtual void OnEnable()
    {
        IsDead = false;
        Health = startingHealth;
    }
    public void onDamage(float dagmage, Vector3 hitPosition, Vector3 hitNormal)
    {
        if (IsDead)
        {
            return;
        }

        Health -= dagmage;

        if (Health <= 0f)
        {
            Health = 0;
            OnDead.Invoke();
        }
    }
}
