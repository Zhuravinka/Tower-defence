using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };
    Queue<Waypoint> queue = new Queue<Waypoint>();
    List<Waypoint> path = new List<Waypoint>();
    Waypoint searchCenter;

    [SerializeField] Waypoint startWaypoint, endWaypoint;
    [SerializeField] bool isRunning = true;

    public List<Waypoint> GetPath()
    {
        startWaypoint.SetColor(Color.black);
        endWaypoint.SetColor(Color.green);
        LoadBlocks();
        BreadthSearch();
        CreatePath();
        return path;
    }

    private void CreatePath()
    {
        path.Add(endWaypoint);
        Waypoint previous = endWaypoint.exploredFrom;
        while (previous != startWaypoint)
        {
            path.Add(previous);
            previous = previous.exploredFrom;
        }
        path.Add(startWaypoint);
        path.Reverse();
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();

        foreach (Waypoint waypoint in waypoints)
        {
            var gridPos = waypoint.GetGridPos();
            if (!grid.ContainsKey(gridPos))
                grid.Add(gridPos, waypoint);
        }
    }
    private void BreadthSearch()
    {
        queue.Enqueue(startWaypoint);
        while (queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue();
            searchCenter.isExplored = true;
            HaltIfEndFound();
            ExploreNeighbours();
        }
    }

    private void HaltIfEndFound()
    {
        if (searchCenter == endWaypoint)
        {
            isRunning = false;
        }
    }

    

    private void ExploreNeighbours()
    {
        if (!isRunning) { return; }
        foreach(Vector2Int direction in directions)
        {
            Vector2Int neighbourCoordinates = searchCenter.GetGridPos() + direction;
            if (grid.ContainsKey(neighbourCoordinates))
            {
                EnqueNeighbour(neighbourCoordinates);

            }

            
        }
    }

    private void EnqueNeighbour(Vector2Int neighbourCoordinates)
    {
        Waypoint neighbour = grid[neighbourCoordinates];
        if(!neighbour.isExplored && !queue.Contains(neighbour))
        {
            neighbour.exploredFrom = searchCenter;
            queue.Enqueue(neighbour);
            
        }
        
    }
}
