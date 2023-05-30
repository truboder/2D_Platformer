using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWayPointMovement : MonoBehaviour
{
    [SerializeField] Transform _patrolWay;
    [SerializeField] private float _speed;

    private int _currentPoint;
    private Transform[] _points;

    private void Start()
    {
        _points = new Transform[_patrolWay.childCount];

        for (int i = 0; i < _patrolWay.childCount; i++)
        {
            _points[i] = _patrolWay.GetChild(i);
        }
    }

    private void Update()
    {
        Transform target = _points[_currentPoint];

        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        if (transform.position == target.position)
        {
            _currentPoint++;

            Rotate(Vector2.zero);

            if (_currentPoint >= _points.Length)
            {
                Rotate(new Vector2(0, 180));
                _currentPoint = 0;
            }
        }
    }

    public void Rotate(Vector2 rotation)
    {
        transform.rotation = Quaternion.Euler(rotation);
    }
}
