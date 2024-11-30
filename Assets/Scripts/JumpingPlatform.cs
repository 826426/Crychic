using UnityEngine;

public class JumpingPlatform : MonoBehaviour
{
    [Label("速度加成")] 
    public Vector2 vel;
    [Label("最大横向速度")]
    public float maxVelX;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D playerRigidbody = other.gameObject.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                var player = FindObjectOfType<Player>();
                player.Jump(vel, new Vector2(maxVelX, 0));
                //player.Jump();
            }
        }
    }
}
