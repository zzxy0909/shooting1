using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner_link : MonoBehaviour
{
    public wt_WaveNode _WaveNode;
	public int _spawnCount = 20;		// The amount of time between each spawn.
	public float _spawnDelay = 0.3f;		// The amount of time before spawning starts.
    public wt_PathAttribute[] _pathInfo;
    public wt_PathMover _baseEnemy;


	void Start ()
	{
		// Start calling the Spawn function repeatedly after a delay .
//        InvokeRepeating("Spawn", spawnDelay, spawnCheckTime);

        _WaveNode = transform.parent.GetComponent<wt_WaveNode>();
	}
    public void StartSpawn()
    {
        if (_WaveNode == null)
        {
            _WaveNode = transform.parent.GetComponent<wt_WaveNode>();
        }
        StartCoroutine(IE_LinkSpawn());
    }

    void Update()
    {

    }
    

	IEnumerator IE_LinkSpawn ()
	{
        while (_baseEnemy.gameObject.activeInHierarchy == false)
        {
            yield return new WaitForSeconds(_spawnDelay);
        }

        for (int i = 0; i < _spawnCount; i++)
        {
            // Instantiate
            GameObject obj = (GameObject)Instantiate(_baseEnemy.gameObject, transform.position, Quaternion.identity);
//            Debug.Log("~~~~~~ _baseEnemy " + i);
            wt_PathMover mov = obj.GetComponent<wt_PathMover>();
            mov.enabled = true;
            yield return new WaitForSeconds(_spawnDelay);
            mov.PlayPath();

            Enemy en = obj.GetComponent<Enemy>();
            if (en)
            {
                _WaveNode._lstChildEnemy.Add(en);
            }
        }

        _baseEnemy.enabled = true;
        yield return new WaitForSeconds(_spawnDelay);
        _baseEnemy.PlayPath();
	}
}
