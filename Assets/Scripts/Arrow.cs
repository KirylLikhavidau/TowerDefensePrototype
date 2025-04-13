using System;
using UnityEngine;

public class Arrow : PoolObject 
{
    [SerializeField] private ArrowMover _mover;

    [HideInInspector] public ArcherTower Tower;

    private void OnEnable()
    {
        _mover.FlyFinished += InvokeFellEvent;
    }

    private void OnDisable()
    {
        _mover.FlyFinished -= InvokeFellEvent;
    }

    public event Action<Arrow> Fell;

    private void InvokeFellEvent(Arrow arrow)
    {
        Fell.Invoke(arrow);
    }
}