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
    private float CompressionRatio;

    // Use this for initialization
    void Start()
    {
        CompressionRatio = WebCamManager.CompressionRatio;
        Width = (int)(WebCamManager.Width * CompressionRatio);
        Height = (int)(WebCamManager.Height * CompressionRatio);
    }

    // Update is called once per frame
    void Update()
    {
        for (int y = 0; y < Height; y ++)
        {
            for (int x = 0; x < Width; x ++)
            {
                if (WebCamManager.checkActivePixel(x, y))
                {
                    Vector3 pos = new Vector3((x - Width / 2), (y - Height / 2), 0) + transform.position;
                    Particle.Emit(pos);
                }
            }
        }
    }
}
