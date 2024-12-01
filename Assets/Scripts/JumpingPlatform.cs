using UnityEngine;

public class JumpingPlatform : MonoBehaviour
{
    [Label("跳跃高度")] 
    public float high;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D playerRigidbody = other.gameObject.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                var player = FindObjectOfType<Player>();
                float temp = player.jumpSpeed;
                player.jumpSpeed = high;
                player.Jump();
                player.jumpSpeed = temp;
            }
        }
    }
}
