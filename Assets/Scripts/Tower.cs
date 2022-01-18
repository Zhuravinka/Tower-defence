using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    [SerializeField] float shootingRange = 10f;
    [SerializeField] ParticleSystem projectileParticle;

    public Waypoint baseWaypoint;

    Transform targetEnemy;

    void Update()
    {
        SetTargetEnemy();
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

    private void SetTargetEnemy()
    {
        var enemies = FindObjectsOfType<EnemyDamage>();
        if(enemies.Length == 0) { return; }
        Transform closestEnemy = enemies[0].transform;
        foreach (EnemyDamage enemy in enemies)
        {
            closestEnemy = GetClosest(closestEnemy, enemy.transform);
        }
        targetEnemy = closestEnemy;
    }

    private Transform GetClosest(Transform transformA, Transform transformB)
    {
        float distanceToA = Vector3.Distance(transformA.position, objectToPan.transform.position);
        float distanceToB= Vector3.Distance(transformB.position, objectToPan.transform.position);
        if (distanceToB < distanceToA)
            return transformB;
        else
            return transformA;
    }

    private void ShootAtEnemy()
    {
        float distanceToEnemy = Vector3.Distance(targetEnemy.position, objectToPan.transform.position);
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
