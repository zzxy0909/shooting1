using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour {
    public Spawner[] _arrSpawner;
    public Spawner_boss _Spawner_boss;
    public bool _isPlay = false;

    public ST_B_stage_spawnRec[] _arrCurrentRec;
    public int _CurrentIx = -1;
    public int _CurrentEnemyCount = 0;
	// Use this for initialization
	void Start () {

        GamePlayManager.Instance._SpawnController = this;
	}

    float _CheckSpawnTime = 1f;
    float _NextCheckSpawnTime = 0f;
    // Update is called once per frame
    void Update()
    {
        if (_isPlay == true
            && _NextCheckSpawnTime < Time.time
            )
        {
            _NextCheckSpawnTime += _CheckSpawnTime;
            CheckSpawn();
        }

    }
    
    void CheckSpawn()
    {
        if (_arrCurrentRec == null
            || _arrCurrentRec.Length <= 0
            || _CurrentIx < 0
            || _CurrentIx >= _arrCurrentRec.Length
            )
        {
            _CurrentIx = -1;
            return;
        }

        if (_arrCurrentRec[_CurrentIx].time_limit == 0
            && _CurrentEnemyCount > 0)
        {
            _CurrentEnemyCount = 0;
            foreach (Spawner s in _arrSpawner)
            {
                if (s._isStop == false || s._LastSpawn != null)
                {
                    _CurrentEnemyCount++;
                }
            }

            if (_CurrentEnemyCount == 0)
            {
                SetNextWave();
            }
        }
    }
    public void SetNextWave()
    {
        GamePlayManager.Instance._CurrentWave++;
        LoadSpawnData();

    }
    public void LoadSpawnData()
    {
        _arrCurrentRec = DataManager.Instance._SqlBalance_stage_spawn.Get_stage_spawnData(GamePlayManager.Instance._CurrentWave);
        if (_arrCurrentRec == null
            || _arrCurrentRec.Length <= 0)
        {
            // Debug.Log("~~~~~~~~~~~~~~~ _arrCurrentRec == null || arr_arrCurrentRecRec.Length <= 0 " + _arrCurrentRec);
            // return;
            int ntmp = GamePlayManager.Instance._CurrentWave;
            _arrCurrentRec = new ST_B_stage_spawnRec[1];
            _arrCurrentRec[0].idx = ntmp;
            _arrCurrentRec[0].level_min = ntmp-1;
            _arrCurrentRec[0].level_max = ntmp;
            _arrCurrentRec[0].spawn_cost = ntmp;
            _arrCurrentRec[0].time_limit = 0;
        }

        _CurrentIx = 0;

        CurrentSpawn();
    }
    void CurrentSpawn()
    {
        ST_B_stage_spawnRec rec = _arrCurrentRec[_CurrentIx];
        int calc_cost = rec.spawn_cost +1;
        do
        {
            int r_spawner_idx = Random.Range(0, _arrSpawner.Length);
            int r_cost = 1;// Random.Range(1, calc_cost);
            int r_pool_idx = Random.Range(0, GamePlayManager.Instance._EnemyPool._pfArrEnemy.Length);
            calc_cost -= r_cost;

            // 해당 코스트의 적을 만들어 스포너에 넣는다.
            switch (r_cost)
            {
                case 1:
                    rec.spawn_pool_idx = r_pool_idx;
                    _arrSpawner[r_spawner_idx].Add_pfEnemy(rec);
                    _arrSpawner[r_spawner_idx]._spawnDelay = 0.5f;
                    _arrSpawner[r_spawner_idx]._spawnCheckTime = 0.5f;
                    _arrSpawner[r_spawner_idx].StartSpawn();

                    break;
            }

            _CurrentEnemyCount++;
        } while (calc_cost > 1);

    }

    public void Play_Spawn()
    {
        _isPlay = true;
    }
}
