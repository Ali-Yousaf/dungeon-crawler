using UnityEngine;

public abstract class TrapBase : MonoBehaviour
{
    protected Collider2D trapCollider;
    [SerializeField] protected int damage = 1;

    protected virtual void Awake()
    {
        trapCollider = GetComponent<Collider2D>();
        trapCollider.enabled = false;
    }

    public virtual void Activate()
    {
        trapCollider.enabled = true;
    }

    public virtual void Deactivate()
    {
        trapCollider.enabled = false;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth.Instance.TakeDamage(damage);
            OnPlayerHit();
        }
    }
    protected virtual void OnPlayerHit() { }
}
