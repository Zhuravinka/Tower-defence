using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint, endWaypoint;

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

     bool isRunning = true;

    public List<Waypoint> GetPath()
    {
        if (path.Count != 0)
            return path;
      
        LoadBlocks();
        BreadthSearch();
        CreatePath();
        return path;
       
       
    }

    private void CreatePath()
    {
        AddToPath(endWaypoint);

        Waypoint previous = endWaypoint.exploredFrom;
        while (previous != startWaypoint)
        {
            AddToPath(previous);
            previous = previous.exploredFrom;
        }
        AddToPath(startWaypoint); 
        path.Reverse();
    }

    private void AddToPath(Waypoint waypoint)
    {
        path.Add(waypoint);
        waypoint.isPlaceable = false;

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
