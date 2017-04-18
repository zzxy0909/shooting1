using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
	public float _spawnCheckTime = 1f;		// The amount of time between each spawn.
	public float _spawnDelay = 3f;		// The amount of time before spawning starts.
    public bool _isStop = false;  // 
    public Queue<ST_B_stage_spawnRec> _pfEnemyQ = new Queue<ST_B_stage_spawnRec>();
    public GameObject _LastSpawn = null;

	void Start ()
	{
		// Start calling the Spawn function repeatedly after a delay .
//        InvokeRepeating("Spawn", spawnDelay, spawnCheckTime);
        
	}
    public void StartSpawn()
    {
        _isStop = false;
        _NextCheckSpawnTime = Time.time + _spawnDelay;
    }

    float _NextCheckSpawnTime = 0f;
    // Update is called once per frame
    void Update()
    {
        if (GamePlayManager.Instance._State == E_PlayState.GamePlaying
            && _isStop == false
            && _NextCheckSpawnTime < Time.time
            )
        {
            _NextCheckSpawnTime = Time.time + _spawnCheckTime;
            Spawn();
        }

    }
    public void Add_pfEnemy(ST_B_stage_spawnRec a_rec)
    {
        _pfEnemyQ.Enqueue(a_rec);
    }

	void Spawn ()
	{
        if (_pfEnemyQ.Count <= 0)
        {
            _isStop = true;
            return;
        }
        // Instantiate
        ST_B_stage_spawnRec rec = _pfEnemyQ.Dequeue();

        Enemy enemy = GamePlayManager.Instance._EnemyPool._pfArrEnemy[rec.spawn_pool_idx];

        _LastSpawn = (GameObject) Instantiate(enemy.gameObject, transform.position, transform.rotation);
        Enemy enemy_LastSpawn = _LastSpawn.GetComponent<Enemy>();
        enemy_LastSpawn._SpawnRec = rec;
	}
}
