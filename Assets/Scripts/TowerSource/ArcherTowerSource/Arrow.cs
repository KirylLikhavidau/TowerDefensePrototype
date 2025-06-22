using System;
using UnityEngine;
using Pool;

namespace Towers.Source
{
    public class Arrow : PoolObject 
    {
        [SerializeField] private ArrowMover _mover;
        [HideInInspector] public ArcherTower Tower;

        public event Action<Arrow> Fell;

        private void OnEnable()
        {
            _mover.FlyFinished += (obj) => Fell.Invoke(obj);
        }

        private void OnDisable()
        {
            _mover.FlyFinished -= (obj) => Fell.Invoke(obj);
        }
    }
}