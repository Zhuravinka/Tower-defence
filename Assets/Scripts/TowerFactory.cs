using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] int towerLimit = 5;
    [SerializeField] Tower towerPrefab;
    [SerializeField] Transform parent;
    Queue<Tower> towerQueue = new Queue<Tower>();
    public void AddTower(Waypoint baseWaypoint)
    {
        int numTowers = towerQueue.Count;
        if (numTowers < towerLimit)
        {
            InstantiateTower(baseWaypoint);
        }
        else
        {
            MoveTower(baseWaypoint);
        }    
    }
    private void InstantiateTower(Waypoint baseWaypoint)
    {
        var newTower = Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity);
        newTower.transform.parent = parent;
        baseWaypoint.isPlaceable = false;
        newTower.baseWaypoint = baseWaypoint;
        towerQueue.Enqueue(newTower);

    }
    private void MoveTower(Waypoint newBase)
    {
        var oldTower = towerQueue.Dequeue();

        oldTower.baseWaypoint.isPlaceable = true;
        newBase.isPlaceable = false;

        oldTower.baseWaypoint = newBase;
        oldTower.transform.position = newBase.transform.position;

        towerQueue.Enqueue(oldTower);
    }

}
