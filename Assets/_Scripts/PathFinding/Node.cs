using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Node
{
    public bool WalkAble;
    public Vector2 WorldPosition;
    public int GCost;
    public int HCost;
    public int GridX;
    public int GridY;
    public Node Parent;
    
    public Node(bool walkAble, Vector2 worldPosition, int gridX, int gridY)
    {
        WalkAble = walkAble;
        WorldPosition = worldPosition;
        GridX = gridX;
        GridY = gridY;
    }
    
    public int FCos => GCost + HCost;
}

