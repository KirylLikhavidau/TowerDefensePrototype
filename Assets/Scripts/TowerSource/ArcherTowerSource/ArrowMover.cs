using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using Units.Enemy;

namespace Towers.Source
{
    public class ArrowMover : MonoBehaviour
    {
        [SerializeField] private Arrow _arrow;
        [SerializeField] private float _flyDuration;

        [HideInInspector] public EnemyUnit Target;
        public event Action<Arrow> FlyFinished;

        private Vector3 _defaultPosition;
        private Vector3[] _path;

        private void Awake()
        {
            _defaultPosition = transform.position;
        }

        private void OnEnable()
        {
            if (Target != null)
                StartCoroutine(nameof(MoveArrowToEnemy));    
        }

        private void OnDisable()
        {
            transform.position = _defaultPosition;
        }

        private IEnumerator MoveArrowToEnemy()
        {
            transform.DOLookAt(Target.transform.position, 0.001f, up: Vector3.back);
            Tween myTween = transform.DOMove(Target.transform.position, _flyDuration).SetEase(Ease.Linear); 
            yield return myTween.WaitForCompletion();
            FlyFinished?.Invoke(_arrow);
            yield break;
        }
    }
}