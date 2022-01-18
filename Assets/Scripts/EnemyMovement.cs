using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 0.5f;
    [SerializeField] ParticleSystem gateParticles;
    void Start()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        var path = pathfinder.GetPath();
        StartCoroutine(FollowPath(path));
    }

    IEnumerator FollowPath(List<Waypoint> path)
    {
        print("Starting patrol");
        foreach(Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(movementSpeed);
        }
        SelfDestruct();
    }

    private void SelfDestruct()
    {
        var VFX = Instantiate(gateParticles, transform.position, Quaternion.identity);
        VFX.Play();
       
        Destroy(VFX.gameObject, VFX.main.duration);
        Destroy(gameObject);
    }

}
