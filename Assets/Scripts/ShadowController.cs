using UnityEngine;

public class ShadowController : MonoBehaviour
{
    public void DestroyShadow()
    {
        Destroy(transform.parent.gameObject);
    }
}
