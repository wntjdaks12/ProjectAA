using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    public int entityId;
    public float spawnRadius;
    public int maxSpawnCount;
    public float spawnDuration;
    public enum SpawnTypes {Monster, Hero};
    public SpawnTypes spawnType;

    public void Start()
    {
        switch (spawnType)
        {
            case SpawnTypes.Monster: StartCoroutine(StartSpawn<Monster, MonsterObject>()); break;
            case SpawnTypes.Hero: StartCoroutine(StartSpawn<Hero, HeroObject>()); break;
        }
    }

    public IEnumerator StartSpawn<T, K>() where T : Entity where K : EntityObject
    {
        yield return new WaitForSeconds(1f);

        var spawnCount = maxSpawnCount;

        while (true)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                var randCirclePos = Random.insideUnitCircle * spawnRadius;
                var spawnPos = transform.position; spawnPos.x += randCirclePos.x; spawnPos.z += randCirclePos.y;

                GameApplication.Instance.EntityController.Spawn<T, K>(entityId, spawnPos, Quaternion.identity);
            }

            yield return new WaitForSeconds(spawnDuration);

            var curSpawnCount = GameApplication.Instance.GameModel.RunTimeData.ReturnDatas<T>(typeof(T).ToString()).Length;

            spawnCount = maxSpawnCount - curSpawnCount;
        }
    }
}