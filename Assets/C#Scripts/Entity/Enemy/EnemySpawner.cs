using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人生成器脚本，临时，现在只生成一种敌人，需修改
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float maxInterval = 4; //每波最大间隔

    [SerializeField] 
    private float minInterval = 2; //每波最小间隔

    [SerializeField]
    private float minDistance; //生成时和玩家的最大距离

    [SerializeField] 
    private float maxDistance; //生成时和玩家的最小距离

    [SerializeField]
    private int count = 30; //每轮数量

    [SerializeField]
    private int maxWaveCount = 6; //每波最大数量

    [SerializeField]
    private int minWaveCount = 3; //每波最小数量

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while(count > 0)
        {
            yield return new WaitForSeconds(Random.Range(minInterval,maxInterval));
            var waveCount = Random.Range(minWaveCount, maxWaveCount);

            for(int i = 0; i < waveCount; i++)
                SpawnSingleEnemy();
        }
    }

    private void SpawnSingleEnemy()
    {
        if (count < 0) return;

        var pos = (Vector2)Player.Instance.transform.position + Random.insideUnitCircle.normalized * Random.Range(minDistance, maxDistance);
        PoolManager.Instance.GetGameObject(DataManager.Instance.prefabCenter.simpleEnemy, pos, Quaternion.identity);
        count--;
    }
}
