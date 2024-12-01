using System.Collections;
using UnityEngine;

public class DashBubble : MonoBehaviour
{
    private Player player;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    [Label("复原时间")]
    public float recoverSeconds = 2.0f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.isTrigger = true;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<Player>();
            
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
            boxCollider2D.enabled = false;
            
            player.ready2DashState.playerStayPos = this.transform.position;
            player.stateMachine.ChangeState(player.ready2DashState);
            StartCoroutine(Discover());
        }
    }

    private IEnumerator Discover()
    {
        while (player.curState == CurState.Ready2Dash)
        {
            yield return null;
        }

        float time2Recover = recoverSeconds;
        while (time2Recover >= 0)
        {
            time2Recover -= Time.deltaTime;
            yield return null;
        }
        
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
        boxCollider2D.enabled = true;
        yield return null;
    }

}
