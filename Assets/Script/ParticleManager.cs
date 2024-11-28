using UnityEngine;
using UnityEngine.UIElements;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance;
    public ParticleSystem dustFX;
    public ParticleSystem dashFX;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public void DashPlay(Transform target)
    {
        if (GameObject.Find("DashFX(Clone)"))
        {
            GameObject.Find("DashFX(Clone)").GetComponent<ParticleSystem>().Play();
        }
        else
        {
            Instantiate(dashFX, target);
            dashFX.Play();
        }
    }
    public void DustPlay(Transform target)
    {
        if (GameObject.Find("DustFX(Clone)"))
        {
            GameObject.Find("DustFX(Clone)").GetComponentInParent<ParticleSystem>().Play();
        }
        else
        {
            Instantiate(dustFX, target);
            dustFX.Play();
        }
    }
}
