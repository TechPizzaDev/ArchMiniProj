using UnityEngine;

public class VisualBlender : MonoBehaviour
{
    public delegate void ParticleTick(float progress);

    private ParticleSystem particleSystem;

    private ParticleTick tickAction;
    private float openTime;

    public GameObject cover;
    public float openDuration = 2;

    void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();

        cover.SetActive(false);
    }

    public void PlayBlending(ParticleTick finishAction)
    {
        tickAction?.Invoke(0);
        tickAction = finishAction;

        particleSystem.Play();
        cover.SetActive(true);
        openTime = 0;
    }

    void Update()
    {
        if (tickAction != null)
        {
            float progress = particleSystem.totalTime / particleSystem.main.duration;
            tickAction.Invoke(progress);

            if (progress >= 1)
            {
                tickAction = null;
            }
        }
        else if (openTime >= 0)
        {
            openTime += Time.deltaTime;
            if (openTime >= openDuration)
            {
                cover.SetActive(false);
                openTime = -1;
            }
        }
    }
}
