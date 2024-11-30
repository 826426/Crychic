using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Player player;
    private bool isNeedCheck = false;
    [Label("下降速度")]
    public float fallSpeed = 1f;
    [Label("射线长度")]
    public float rayLength = 1f;

    private RaycastHit2D hit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<Player>();
            isNeedCheck = true;
            StartCoroutine(CheckPlayerIsOnPlatform());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNeedCheck = false;
        }
    }

    private IEnumerator CheckPlayerIsOnPlatform()
    {
        while (isNeedCheck)
        {
            if (player.downBox.collider && player.downBox.collider.CompareTag("FallPlatform"))
            {
                StartCoroutine(Fall());
                isNeedCheck = false;
                yield break;
            }
            yield return null;
        }
    }

    private IEnumerator Fall()
    {
        while (true)
        {
            this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - Time.deltaTime * fallSpeed);
            
            hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength,~LayerMask.GetMask("Platform"));
            if (hit.collider != null)
            {
                yield break;
            }

            yield return null;
        }
    }
}
