using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMovement : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private float _speed;
    private Transform[] _pathPoints;
    private int _currentPoint;

    private void Start()
    {
        _pathPoints = new Transform[_path.childCount];

        for (int i = 0; i < _pathPoints.Length; i++)
        {
            _pathPoints[i] = _path.GetChild(i);
        }
    }

    public void Patrol()
    {
        Transform target = _pathPoints[_currentPoint];

        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        if (transform.position == target.position)
        {
            _currentPoint++;

            if (_currentPoint >= _pathPoints.Length)
            {
                _currentPoint = 0;
            }
        }
    }
}
