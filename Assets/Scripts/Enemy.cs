using System;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Enemy : Unit
{
    [SerializeField, Range(0, 100)] private float _sensetivityToDamage;
    [SerializeField, Range(1, 20)] private int _damageToPlayer;
    [SerializeField, Range(0, 100)] private int _incomeFromDeath;

    public event Action RightMovement;
    public event Action LeftMovement;
    public event Action DownMovement;
    public event Action UpMovement;

    public int IncomeFromDeath => _incomeFromDeath;
    public int DamageToPlayer => _damageToPlayer;

    private Vector3 _lastPosition;

    private void Start()
    {
        _lastPosition = transform.position;
    }

    private void Update()
    {
        float dotUp = Vector2.Dot(Vector2.up, (transform.position - _lastPosition).normalized);
        float dotDown = Vector2.Dot(Vector2.down, (transform.position - _lastPosition).normalized);
        float dotRight = Vector2.Dot(Vector2.right, (transform.position - _lastPosition).normalized);
        float dotLeft = Vector2.Dot(Vector2.left, (transform.position - _lastPosition).normalized);
        
        float MaxValue = float.MinValue;

        foreach(var value in new float[]{dotUp, dotDown, dotRight, dotLeft })
            if (value > MaxValue)
                MaxValue = value;

        if (MaxValue == dotUp)
            UpMovement.Invoke();
        if (MaxValue == dotDown)
            DownMovement.Invoke();
        if (MaxValue == dotRight)
            RightMovement.Invoke();
        if (MaxValue == dotLeft)
            LeftMovement.Invoke();

        _lastPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Arrow arrow))
            if (IsDead == false)
                TakeDamage(arrow.Tower.CurrentDamageToUnits);
    }

    protected override void TakeDamage(float damage)
    {
        _health -= damage / 100 * _sensetivityToDamage ;
        if (_health <= 0)
        {
            IsDead = true;
            Die();
        }
    }
}
