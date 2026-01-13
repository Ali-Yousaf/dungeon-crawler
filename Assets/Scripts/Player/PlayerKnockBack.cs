//using System.Collections;
//using UnityEngine;

//public class PlayerKnockback : MonoBehaviour
//{
//    private Rigidbody2D rb;
//    private bool isKnockedBack = false;

//    void Awake()
//    {
//        rb = GetComponent<Rigidbody2D>();
//    }

//    public void Knockback(Vector2 direction, float force, float duration)
//    {
//        if (!isKnockedBack)
//            StartCoroutine(DoKnockback(direction, force, duration));
//    }

//    private IEnumerator DoKnockback(Vector2 direction, float force, float duration)
//    {
//        isKnockedBack = true;
//        rb.linearVelocity = Vector2.zero;
//        rb.AddForce(direction.normalized * force, ForceMode2D.Impulse);

//        yield return new WaitForSeconds(duration);

//        rb.linearVelocity = Vector2.zero;
//        isKnockedBack = false;
//    }
//}
