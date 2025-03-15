using System;
using UnityEngine;

public class HeartZone : MonoBehaviour
{
    [SerializeField] private ObjectPool _pool;

    private void OnTriggerEnter2D(Collider2D other)
    {
        _pool.PutObject(other.GetComponent<Enemy>());
    }
}
