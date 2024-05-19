using UnityEngine;

public class VisualBlender : MonoBehaviour
{
    public delegate void ParticleTick(float progress);

    private ParticleSystem particles;

    private ParticleTick tickAction;
    private float openTime;

    public GameObject cover;
    public float openDuration = 2;

    void Start()
    {
        particles = GetComponentInChildren<ParticleSystem>();

        cover.SetActive(false);
    }

    public void PlayBlending(ParticleTick finishAction)
    {
        tickAction?.Invoke(0);
        tickAction = finishAction;

        particles.Play();
        cover.SetActive(true);
        openTime = 0;
    }

    void Update()
    {
        if (tickAction != null)
        {
            float progress = particles.totalTime / particles.main.duration;
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
