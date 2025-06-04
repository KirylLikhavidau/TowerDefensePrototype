using System;
using UnityEngine;
using Towers.Source;
using Pool;

namespace Units.Enemy
{
    public class EnemyUnit : PoolObject
    {
        [SerializeField, Range(0, 100)] private float _sensetivityToDamage;
        [SerializeField, Range(1, 20)] private int _damageToPlayer;
        [SerializeField, Range(0, 100)] private int _incomeFromDeath;
        [SerializeField] private float _health;
        [SerializeField] private float _damageToUnits;
        [SerializeField] private float _dyingTime;

        public bool IsDead { get; set; }
        public float DamageToUnits => _damageToUnits;
        public float DyingTime => _dyingTime;
        public int IncomeFromDeath => _incomeFromDeath;
        public int DamageToPlayer => _damageToPlayer;

        public event Action<EnemyUnit> Died;
        public event Action Dying;
        public event Action RightMovement;
        public event Action LeftMovement;
        public event Action DownMovement;
        public event Action UpMovement;

        private Vector3 _lastPosition;
        private float _defaultHealth;

        private void Awake()
        {
            _defaultHealth = _health;
        }

        private void OnEnable()
        {
            IsDead = false;
            _health = _defaultHealth;
        }

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

            foreach (var value in new float[] { dotUp, dotDown, dotRight, dotLeft })
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
                    TakeDamage(arrow.Tower.DamageToUnits);
        }

        private void TakeDamage(float damage)
        {
            _health -= damage / 100 * _sensetivityToDamage;
            if (_health <= 0)
            {
                IsDead = true;
                Die();
            }
        }

        private void Die()
        {
            Dying?.Invoke();
            Died?.Invoke(gameObject.GetComponent<EnemyUnit>());
        }
    }
}
