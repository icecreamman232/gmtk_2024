using System;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    //public Transform Start;
    //public Transform Target;
    [SerializeField] private Grid m_grid;

    private void Update()
    {
        //FindPath(Start.position, Target.position);
    }

    public void FindPath(Vector2 start, Vector2 target)
    {
        Node startNode = m_grid.GetNodeFromWorldPos(start);
        Node targetNode = m_grid.GetNodeFromWorldPos(target);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closeSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node curNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FCos < curNode.FCos ||
                    (openSet[i].FCos == curNode.FCos) && openSet[i].HCost < curNode.HCost)
                {
                    curNode = openSet[i];
                }
            }

            openSet.Remove(curNode);
            closeSet.Add(curNode);

            if (curNode == targetNode)
            {
                RetractPath(startNode,targetNode);
                return;
            }

            foreach (var neighbor in m_grid.GetNeighbors(curNode))
            {
                if (!neighbor.WalkAble || closeSet.Contains(neighbor))
                {
                    continue;
                }

                int newMovementCostToNeighbor = curNode.GCost + GetDistance(curNode, neighbor);
                if (newMovementCostToNeighbor < neighbor.GCost || !openSet.Contains(neighbor))
                {
                    neighbor.GCost = newMovementCostToNeighbor;
                    neighbor.HCost = GetDistance(neighbor, targetNode);
                    neighbor.Parent = curNode;
                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }
    }

    private void RetractPath(Node start, Node end)
    {
        List<Node> path = new List<Node>();
        Node curNode = end;
        while (curNode != start)
        {
            path.Add(curNode);
            curNode = curNode.Parent;
        }
        path.Reverse();
        m_grid.Path = path;
    }

    private int GetDistance(Node a, Node b)
    {
        int distanceX = Mathf.Abs(a.GridX - b.GridX);
        int distanceY = Mathf.Abs(a.GridY - b.GridY);

        if (distanceX > distanceY)
        {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }

        return 14 * distanceX + 10 * (distanceY - distanceX);
    }
}
