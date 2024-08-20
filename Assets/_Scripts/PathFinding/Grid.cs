using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private int m_gridSizeX;
    [SerializeField]  private int m_gridSizeY;
    private Node[,] m_grid;
    public List<Node> Path;
    
    public void AddObstacle(Vector2 pos)
    {
        var node = GetNodeFromWorldPos(pos);
        node.WalkAble = false;
    }
    
    public Node GetNodeFromWorldPos(Vector2 worldPos)
    {
        float percentX = (worldPos.x + m_gridSizeX / 2) / m_gridSizeX;
        float percentY = (worldPos.y + m_gridSizeY / 2) / m_gridSizeY;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((m_gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((m_gridSizeY - 1) * percentY);
        return m_grid[x, y];
    }

    public List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        //Relative coord
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                //Node itself so we skip it
                if(x == 0 && y == 0) continue;
                int checkX = node.GridX + x;
                int checkY = node.GridY + y;

                if (checkX >= 0 && checkX < m_gridSizeX && checkY >= 0 && checkY < m_gridSizeY)
                {
                    neighbors.Add(m_grid[checkX,checkY]);
                }
            }
        }
        
        return neighbors;
    }
    
    private void Start()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        var gridPos = transform.position;
        Vector2 worldBottomLeft = new Vector2
            (gridPos.x - m_gridSizeX/2,
            gridPos.y - m_gridSizeY/2);
        
        m_grid = new Node[m_gridSizeX, m_gridSizeY];
        for (int x = 0; x < m_gridSizeX; x++)
        {
            for (int y = 0; y < m_gridSizeY; y++)
            {
                m_grid[x, y] = new Node(true, worldBottomLeft + Vector2.right * x + Vector2.up * y, x, y);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (m_grid == null) return;
        foreach (var node in m_grid)
        {
            if (Path != null && Path.Contains(node))
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawCube(node.WorldPosition,Vector3.one);
            }
            else
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(node.WorldPosition,Vector3.one);
            }
        }
    }
}
