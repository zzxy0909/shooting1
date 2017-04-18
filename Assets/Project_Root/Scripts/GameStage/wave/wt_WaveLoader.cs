using UnityEngine;
using System.Collections;

public class wt_WaveLoader : MonoBehaviour {
//    public Transform _Pos_StartWave;
    public GameObject[] _pfArrWaveSet;
    public wt_WaveSetController _CurrentWaveSet; 
    // Use this for initialization
    void Start()
    {
        GamePlayManager.Instance._WaveLoader = this;
        _CurrentWaveSet = null;
    }	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadCurrentWaveSet()
    {
        string loadName = string.Format("WaveSet{0:00#}{1:0#}", GameWorld.Instance._CurrentStageNo, GameWorld.Instance._CurrentRoundNo);
        foreach (GameObject pfobj in _pfArrWaveSet)
        {
            if (pfobj == null)
            {
                continue;
            }

            if (pfobj.name == loadName)
            {
                GameObject obj = (GameObject)Instantiate(pfobj);
                obj.transform.parent = this.transform;

                _CurrentWaveSet = obj.GetComponent<wt_WaveSetController>();
//                _CurrentWaveSet._Trigger_StartPos = _Pos_StartWave;
                break;
            }
        }
    }
}
