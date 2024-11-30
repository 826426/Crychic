using UnityEngine;

public class JumpingPlatform : MonoBehaviour
{
    [Label("速度加成")] 
    public Vector2 vel;
    [Label("最大横向速度")]
    public float maxVelX;

    private bool isPlayerOnPlatform = false;

    private void Update()
    {
        if (isPlayerOnPlatform)
        {
            TriggerPlayerJump();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnPlatform = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnPlatform = false;
        }
    }

    private void TriggerPlayerJump()
    {
        var player = FindObjectOfType<Player>();

        if (player != null)
        {
            Debug.Log("player jump");
            player.Jump(vel,new Vector2(maxVelX,0));
            isPlayerOnPlatform = false;
        }
    }
}
