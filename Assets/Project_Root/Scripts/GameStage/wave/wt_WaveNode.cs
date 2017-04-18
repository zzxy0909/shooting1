using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class wt_WaveNode : MonoBehaviour {
    public bool _isAutoPlay = true;
    public List<Enemy> _lstChildEnemy = new List<Enemy>();
    public bool _isPlay = false;
    public Spawner_link _SpawnerLink;
    public bool _isLoadUnitData = false;

    public bool isPlayEnd()
    {
        bool rtn = true;
        foreach (Enemy en in _lstChildEnemy)
        {
            if (en != null)
            {
                rtn = false;
            }
        }
        return rtn;
    }
    public void PlayNode()
    {
        _isPlay = true;
        foreach (Enemy en in _lstChildEnemy)
        {
            if (en)
            {
                en.transform.parent = null;
                GameWorld.SetActiveRecursively(en.gameObject, true);

                wt_PathMover path = en.GetComponent<wt_PathMover>();
                if (path)
                {
                    if (path.enabled == true)
                    {
                        StartCoroutine(DelayAction(0.1f, () =>
                        {
                            path.PlayPath();
                        }));
                    }
                }
            }
        }

        if (_SpawnerLink)
        {
            _SpawnerLink.StartSpawn();
        }
    }
    // delay method coroutine
    IEnumerator DelayAction(float dTime, System.Action callback)
    {
        yield return new WaitForSeconds(dTime);
        callback();
    }
    void StartupNode()
    {
        if (_lstChildEnemy.Count <= 0)
        {
            Enemy[] arr = transform.GetComponentsInChildren<Enemy>();

            if (_isLoadUnitData == true)
            {
                foreach (Enemy en in arr)
                {
                    en.LoadUnitData();
                }
            }

            _lstChildEnemy.AddRange(arr);
        }

        ActiveNode(false);
    }
    void ActiveNode(bool b)
    {
        foreach (Enemy en in _lstChildEnemy)
        {
            GameWorld.SetActiveRecursively(en.gameObject, b);
        }
    }
    void Awake()
    {
        StartupNode();
    }
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
