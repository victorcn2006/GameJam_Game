using UnityEngine;
using DG.Tweening;

public class Deflector : MonoBehaviour, IInteractive
{
    [SerializeField] private int initialPuzzlePositionId;
    [SerializeField] private PuzzlePositions _puzzlePositions;
    [SerializeField] private float moveDuration = 0.3f;
    [SerializeField] private GameObject _deflector;

    private int _currentRowId;
    private int _currentColumnId;

    private DeflectorSide.Side _pendingSide;


    private void Start()
    {
        _currentRowId = initialPuzzlePositionId / 3;
        _currentColumnId = initialPuzzlePositionId % 3;
        transform.position = _puzzlePositions.GetPosition(_currentRowId, _currentColumnId).position;
    }

    public void Interact()
    {

        switch (_pendingSide)
        {
            case DeflectorSide.Side.Left:
                _currentColumnId = Mathf.Clamp(_currentColumnId + 1, 0, 2);
                break;
            case DeflectorSide.Side.Right:
                _currentColumnId = Mathf.Clamp(_currentColumnId - 1, 0, 2);
                break;
            case DeflectorSide.Side.Front:
                _currentRowId = Mathf.Clamp(_currentRowId + 1, 0, 2);
                break;
            case DeflectorSide.Side.Back:
                _currentRowId = Mathf.Clamp(_currentRowId - 1, 0, 2);
                break;
        }

        Transform target = _puzzlePositions.GetPosition(_currentRowId, _currentColumnId);
        transform.DOMove(target.position, moveDuration).SetEase(Ease.OutSine);
    }

    public void OnSidePlayerDetected(DeflectorSide.Side side)
    {
        _pendingSide = side;
    }
}