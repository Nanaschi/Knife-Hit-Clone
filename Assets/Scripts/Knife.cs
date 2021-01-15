using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{



   public  Rigidbody2D myRigidbody;

    [SerializeField] float knifeSpeed;

    public bool isReleased { get; set; }

    public bool hit { get; set; }

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
   
    }

    public void FireKnife()
    {
        if (!isReleased   && !GameManager.Instance.IsGameOver)
        {
            isReleased = true;
            myRigidbody.AddForce(new Vector2 (0, knifeSpeed), ForceMode2D.Impulse);
            SoundManager.Instance.PlayeKnifeFire();
        }
      
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wheel") && !hit && !GameManager.Instance.IsGameOver && isReleased)
        {

            other.gameObject.GetComponent<Wheel>().KnifeHit(this);

            SoundManager.Instance.Vibrate();
            GameManager.Instance.Score++;

            SoundManager.Instance.PlayWheelHit();

        } else if (other.gameObject.CompareTag("Knife") && !hit && isReleased && !GameManager.Instance.IsGameOver && other.gameObject.GetComponent<Knife>().isReleased)
        {
            transform.SetParent(other.transform);
            myRigidbody.velocity = Vector2.zero;
            myRigidbody.isKinematic = true;
            SoundManager.Instance.PlayGameOverClip();
            SoundManager.Instance.Vibrate();
            GameManager.Instance.IsGameOver = true;
            Invoke(nameof(GameOver), .5f);
        }
    }
    private void GameOver()
    {
        UIManager.Instance.GameOver();
    }
}
