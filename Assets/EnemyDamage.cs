using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    //[SerializeField] Collider collisionMesh;
    [SerializeField] int hitpoints;
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
    }
    void KillEnemy()
    {
        Destroy(gameObject);
    }
}
