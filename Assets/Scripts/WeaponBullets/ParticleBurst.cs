using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ParticleBurst : MonoBehaviour
{
    //public Transform target;
    private ParticleSystem particleBurstSystem;

    private void Start()
    {
        particleBurstSystem = GetComponent<ParticleSystem>();
    }

    public void Burst(int amount) // Call from other places to emit particles
    {
        particleBurstSystem.Emit(amount);
    }
}
