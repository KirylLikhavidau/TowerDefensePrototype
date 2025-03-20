using System.Numerics;

public class EnemySpawner : UnitSpawner
{
    protected override void Spawn()
    {
        var enemy = _pool.GetObject();
        enemy.gameObject.SetActive(true);
        enemy.transform.position = _spawnPoint.position;
    }
}
