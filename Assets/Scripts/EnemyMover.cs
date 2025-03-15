using UnityEngine;
using DG.Tweening;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private Transform _way;
    [SerializeField] private float _speed;

    private Vector3[] _wayPoints;
    private float _duration;
    private Tweener _tween;

    private void Start()
    {
        _wayPoints = GetPointsFromWay(_way);
        _duration = GetDistanceOfWay(_wayPoints) / _speed;
        _tween = transform.DOPath(_wayPoints, _duration, PathType.CatmullRom, PathMode.TopDown2D).SetEase(Ease.Linear).SetAutoKill(false);
    }

    private void OnEnable()
    {
        _tween.Restart();
    }

    private float GetDistanceOfWay(Vector3[] way)
    {
        float distance = 0;

        for (int i = 0; i < way.Length - 2; i++)
        {
            distance += Vector3.Distance(_wayPoints[i], _wayPoints[i+1]);
        }

        return distance;
    }

    private Vector3[] GetPointsFromWay(Transform way)
    {
        Vector3[] wayPoints = new Vector3[way.childCount - 1];

        for (int i = 0; i < way.childCount - 1; i++)
        {
            Transform child = way.GetChild(i);
            wayPoints[i] = child.position;
        }

        return wayPoints;
    }
}
