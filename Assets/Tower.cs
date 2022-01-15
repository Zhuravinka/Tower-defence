using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    [SerializeField] Transform targetEnemy;
    [SerializeField] float shootingRange = 10f;
    [SerializeField] ParticleSystem projectileParticle;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (targetEnemy)
        {
            objectToPan.LookAt(targetEnemy);
            ShootAtEnemy();
        }
        else
        {
            Shoot(false);
        }
    }

    private void ShootAtEnemy()
    {
        float distanceToEnemy = Vector3.Distance(targetEnemy.transform.position, objectToPan.transform.position);
        if (distanceToEnemy <= shootingRange )
        {
            Shoot(true);
        }
        else
        {
            Shoot(false);
        }
    }

    private void Shoot(bool isActive)
    {
        var emissionModule = projectileParticle.emission;
        emissionModule.enabled = isActive;
    }
}
