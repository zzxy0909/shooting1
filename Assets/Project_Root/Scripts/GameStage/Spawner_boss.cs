using UnityEngine;
using System.Collections;

public class Spawner_boss : MonoBehaviour
{
	public float spawnCheckTime = 1f;		// The amount of time between each spawn.
    public int Count = 10;
    public GameObject[] enemies;		// Array of enemy prefabs.
    bool _IsBossSpawn = false;
    Enemy _enBoss;		
    public Spawner[] _arrSpawner;		
    public bool _isStop = false;
    public wt_PathAttribute[] _arrPath;

	void Start ()
	{
		
	}
    float _nextSpawnTime = 0f;
    void Update()
    {
        if (GamePlayManager.Instance._State == E_PlayState.GamePlaying
            && Time.time > _nextSpawnTime
            && _isStop == false
            )
        {
            if (_nextSpawnTime == 0f)
            {
                _nextSpawnTime = Time.time + spawnCheckTime;
                return;
            }
            _nextSpawnTime = Time.time + spawnCheckTime;
            Spawn();
        }

    }
    void LateUpdate()
    {
        CheckBossDie();
    }
    void CheckBossDie()
    {
        if (GamePlayManager.Instance._State == E_PlayState.GamePlaying
            &&  _IsBossSpawn == true
            )
        {
            if (_enBoss == null)
            {
                SetBossDie();
            }
            else if (_enBoss._UnitInfo._DeathFlag == true)
            {
                SetBossDie();
            }

        }
    }
    void SetBossDie()
    {
        // 
        _IsBossSpawn = false;
        ChildYesSpawn();
    }

	void Spawn ()
	{
        if (GamePlayManager.Instance._State == E_PlayState.GamePlaying
            && _isStop == false
            )
        {
            // Instantiate a random enemy.
            int enemyIndex = Random.Range(0, enemies.Length);
            GameObject obj = (GameObject) Instantiate(enemies[enemyIndex], transform.position, transform.rotation);

            _enBoss = obj.GetComponent<Enemy>();
            if (_enBoss._EnemyType == E_EnemyType.champ)
            {
                ChildNoSpawn_Random();
            }
            _IsBossSpawn = true;

            wt_PathMover mover = obj.GetComponent<wt_PathMover>();
            mover._arrPath = _arrPath;
            float r = Random.Range(0f, 1f);
            if (r < 0.5f)
            {
                mover._RandomType = E_PathRandomType.List;
            }
            else
            {
                mover._RandomType = E_PathRandomType.RandomList;
            }
            mover.PlayPath();
        }
	}
    public void ChildNoSpawn_Random()
    {
        foreach (Spawner spn in _arrSpawner)
        {
            float r = Random.Range(0f, 1f);
            if (r < 0.5f)
            {
                spn._isStop = true;
            }
            else
            {
                spn._isStop = false;
            }
        }
    }
    public void ChildNoSpawn()
    {
        foreach(Spawner spn in _arrSpawner)
        {
            spn._isStop = true;
        }
    }
    public void ChildYesSpawn()
    {
        foreach (Spawner spn in _arrSpawner)
        {
            spn._isStop = false;
        }
    }
}
