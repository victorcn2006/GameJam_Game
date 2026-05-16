using DG.Tweening;
using System.Collections;
using UnityEngine;
using static UnityEditorInternal.VersionControl.ListControl;

public class Deflector : MonoBehaviour, IInteractive
{
    [SerializeField] private int initialPuzzlePositionId;
    [SerializeField] private PuzzlePositions _puzzlePositions;
    [SerializeField] private float moveDuration;
    [SerializeField] private GameObject _deflector;
    [SerializeField] private GameObject _rayLight;

    private int _currentRowId;
    private int _currentColumnId;

    private DeflectorSide.Side _pendingSide;


    private void Start()
    {
        _currentRowId = initialPuzzlePositionId / 3;
        _currentColumnId = initialPuzzlePositionId % 3;
        transform.position = _puzzlePositions.GetPosition(_currentRowId, _currentColumnId).position;

        _rayLight.SetActive(false);
    }

    public void Interact()
    {
        int _newColumnId = _currentColumnId;
        int _newRowId = _currentRowId;

        switch (_pendingSide)
        {
            case DeflectorSide.Side.Left:
                _newColumnId = Mathf.Clamp(_currentColumnId + 1, 0, 2);
                break;
            case DeflectorSide.Side.Right:
                _newColumnId = Mathf.Clamp(_currentColumnId - 1, 0, 2);
                break;
            case DeflectorSide.Side.Front:
                _newRowId = Mathf.Clamp(_currentRowId + 1, 0, 2);
                break;
            case DeflectorSide.Side.Back:
                _newRowId = Mathf.Clamp(_currentRowId - 1, 0, 2);
                break;
        }

        Transform target = _puzzlePositions.GetPosition(_newRowId, _newColumnId);

        if(!_puzzlePositions.IsOccupied(_newRowId, _newColumnId))
        {
            transform.DOMove(target.position, moveDuration).SetEase(Ease.Linear);
            _currentColumnId = _newColumnId;
            _currentRowId = _newRowId;

            if (_puzzlePositions.isIluminated(_newRowId, _newColumnId)) ActivateLight();
            else DeactivateLight();

        }
    }

    public void OnSidePlayerDetected(DeflectorSide.Side side)
    {
        _pendingSide = side;
    }

    public void ActivateLight()
    {
        StartCoroutine(LightDelay(moveDuration));
        _rayLight.SetActive(true);
    }

    public void DeactivateLight()
    {
        _rayLight.SetActive(false);
        StartCoroutine(LightDelay(moveDuration));
    }

    private IEnumerator LightDelay(float secondsToWait)
    {
        yield return new WaitForSeconds(secondsToWait);
    }

}