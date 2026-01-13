using UnityEngine;

public class PushTrap : TrapBase
{
    [SerializeField] private float kbForce = 10f;
    [SerializeField] private float kbDuration = 0.15f;
    [SerializeField] private Vector2 kbDirection = new Vector2(-2, 0);

    protected override void OnPlayerHit()
    {
        Debug.Log("KB function called");
        
        PlayerMovement.Instance.ApplyKnockback(kbDirection, kbForce, kbDuration);
    }
}
