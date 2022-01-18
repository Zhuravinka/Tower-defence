using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    //[SerializeField] Collider collisionMesh;
    [SerializeField] int hitpoints;
    [SerializeField] ParticleSystem hitParticles;
    [SerializeField] ParticleSystem deathParticles;
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hitpoints < 1)
        {
            KillEnemy();
        }
    }

    void ProcessHit()
    {
        hitpoints -= 1;
        hitParticles.Play();
    }
    void KillEnemy()
    {
        var VFX = Instantiate(deathParticles, transform.position, Quaternion.identity);
        VFX.Play();
        float destructionDelay = VFX.main.duration;
        Destroy(VFX.gameObject, destructionDelay);
        Destroy(gameObject);
    }
}
