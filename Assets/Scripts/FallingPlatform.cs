using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Player player;
    private bool isNeedCheck = false;
    private bool isNeedContinueFall= false;
    [Label("下降速度")]
    public float fallSpeed = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<Player>();
            isNeedCheck = true;
            StartCoroutine(CheckPlayerIsOnPlatform());
        }
        else
        {
            isNeedContinueFall = false;
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
                isNeedContinueFall = true;
                StartCoroutine(Fall());
                isNeedCheck = false;
                yield break;
            }
            yield return null;
        }
    }

    private IEnumerator Fall()
    {
        while (isNeedContinueFall)
        {
            this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - Time.deltaTime * fallSpeed);

            yield return null;
        }
    }
}
