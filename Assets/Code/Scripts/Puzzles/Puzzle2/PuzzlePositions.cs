using System.Collections.Generic;
using UnityEngine;

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
}