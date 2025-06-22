using UnityEngine;
using System.Collections;

namespace Pool
{
    public abstract class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] protected ObjectPool _pool;
        [SerializeField] protected Transform _spawnPoint;
        [SerializeField] private float _delay;

        private IEnumerator SpawnObjects()
        {
            while (gameObject.activeSelf)
            {
                Spawn();
                yield return new WaitForSeconds(_delay);
            }
        }

        protected virtual void Spawn()
        {
            var obj = _pool.GetObject();
            obj.gameObject.SetActive(true);
            obj.transform.position = _spawnPoint.position;
        }

        private void OnEnable()
        {
            StartCoroutine(nameof(SpawnObjects));
        }

        private void OnDisable()
        {
            StopCoroutine(nameof(SpawnObjects));
        }
    }
}
