using UnityEngine;
using ZTH.Unity.Tool;

public class GridMapper : MonoBehaviour
{
    public Vector2Int WorldPosition2Index(Vector3 position)
    {
        var objectPosition = (Vector2)(position - transform.position) - offset;
        return (objectPosition / cellSize).RoundToInt();
    }

    public Vector3 Index2WorldPosition(Vector2Int index)
    {
        var objectPosition = index * cellSize + offset;
        return transform.position + new Vector3(objectPosition.x, objectPosition.y);
    }

    public bool IsIn(Vector3 position)
    {
        return IsIn(WorldPosition2Index(position));
    }

    public bool IsIn(Vector2Int index)
    {
        if (index.x < 0) return false;
        if (index.x >= gridSize.x) return false;
        if (index.y < 0) return false;
        if (index.y >= gridSize.y) return false;

        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        for (var y = 0; y < gridSize.y; y++)
        {
            for (var x = 0; x < gridSize.x; x++)
            {
                var index = new Vector2Int(x, y);
                var position = Index2WorldPosition(index);
                Gizmos.DrawWireCube(position, cellSize);
            }
        }
    }

    public Vector2 CellSize => cellSize;
    public Vector2Int GridSize => gridSize;

    [SerializeField] private Vector2 cellSize = Vector2.one;
    [SerializeField] private Vector2Int gridSize = Vector2Int.one;
    [SerializeField] private Vector2 offset;
}
