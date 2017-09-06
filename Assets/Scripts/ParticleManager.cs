using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ParticlePlayground;

public class ParticleManager : MonoBehaviour
{

    public WebCamManager WebCamManager;
    public PlaygroundParticlesC Particle;
    //public GameObject ParticlePrefab;
    private int Width;
    private int Height;
    public int BetweenSpace = 10;

    // Use this for initialization
    void Start()
    {
        Width = WebCamManager.Width;
        Height = WebCamManager.Height;
    }

    // Update is called once per frame
    void Update()
    {
        for (int y = 0; y < Height; y += BetweenSpace)
        {
            for (int x = 0; x < Width; x += BetweenSpace)
            {
                if (WebCamManager.checkActivePixel(y * Width + x))
                {
                    //GameObject particle = (GameObject)Instantiate(ParticlePrefab, new Vector3((x - Width / 2) / BetweenSpace, (y - Height / 2) / BetweenSpace, 0) + transform.position, ParticlePrefab.transform.rotation);
                    //particle.transform.parent = transform;
                    //Destroy(particle, particle.GetComponent<ParticleSystem>().startLifetime + 0.1f);

                    Vector3 pos = new Vector3((x - Width / 2) / BetweenSpace, (y - Height / 2) / BetweenSpace, 0) + transform.position;
                    Particle.Emit(pos);
                }
            }
        }
    }
}
