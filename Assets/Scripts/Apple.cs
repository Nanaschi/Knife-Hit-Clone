using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{

    [SerializeField] private ParticleSystem particleSystem;

    private SpriteRenderer spriteRenderer;

    private BoxCollider2D boxCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Knife"))
        {
            spriteRenderer.enabled = false;
            boxCollider2D.enabled = false;
            transform.parent = null;
            SoundManager.Instance.PlayAppleHit();
            GameManager.Instance.TotalApple += 1;


            particleSystem.Play();
            Destroy(gameObject, 2f);
            
        }
    }
}
