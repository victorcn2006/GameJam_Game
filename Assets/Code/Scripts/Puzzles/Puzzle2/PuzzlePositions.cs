using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

[System.Serializable]
public class TransformRow
{
    public Transform[] columns;
}

public class PuzzlePositions : MonoBehaviour
{
    [SerializeField] private TransformRow[] grid = new TransformRow[3];

    public Transform GetPosition(int row, int column)
    {
        return grid[row].columns[column];
    }

    public bool IsOccupied(int row, int column)
    {
        return grid[row].columns[column].GetComponent<PuzzlePosition>().IsOccupied();
    }

    public bool isIluminated(int row, int column)
    {
        return grid[row].columns[column].GetComponent<PuzzlePosition>().IsIluminated();
    }

}