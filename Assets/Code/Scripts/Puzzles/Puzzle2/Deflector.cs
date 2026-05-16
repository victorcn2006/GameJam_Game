using DG.Tweening;
using System.Collections;
using UnityEngine;

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

    public void InteractB()
    {
        _deflector.transform.DORotate(_deflector.transform.eulerAngles + new Vector3(0, 90f, 0), moveDuration/2);
    }

    public void OnSidePlayerDetected(DeflectorSide.Side side)
    {
        _pendingSide = side;
    }

    public void ActivateLight()
    {
        StartCoroutine(LightDelay(moveDuration, true));
    }

    public void DeactivateLight()
    {
        StartCoroutine(LightDelay(moveDuration, false));
    }

    private IEnumerator LightDelay(float secondsToWait, bool nextState)
    {
        if (nextState)
        {
            yield return new WaitForSeconds(secondsToWait);
            _rayLight.SetActive(true);
            Color color = _rayLight.GetComponent<MeshRenderer>().material.color;
            color.a = 0f;
            _rayLight.GetComponent<MeshRenderer>().material.color = color;
            _rayLight.GetComponent<MeshRenderer>().material.DOFade(1f, 0.3f);
        }
        else
        {
            _rayLight.GetComponent<MeshRenderer>().material.DOFade(0f, 0.3f).OnComplete(() =>
            {
                _rayLight.SetActive(false);
            });
        }
    }

}