using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class wt_WaveSetController : MonoBehaviour {
    public wt_WaveNode[] _ChildNode;
    public Transform _Trigger_StartPos;
    public Transform _Move_Root;

    public bool _isStop = false;  //
    public float _CheckNodeTime = 1f;
    public float _CheckNodeShiftValue = 1f;
    public int _CuttentIx = 0;

    void StartupNode()
    {
        if (_ChildNode.Length <= 0)
        {
            _ChildNode = new wt_WaveNode[_Move_Root.childCount];
            for (int i = 1; i <= transform.childCount; i++)
            {
                Transform child = _Move_Root.FindChild(i.ToString());
                if (child)
                {
                    _ChildNode[i - 1] = child.GetComponent<wt_WaveNode>();
                }
            }
        }

        _CuttentIx = 0;
        _isStop = false;
    }
	// Use this for initialization
	void Start () {
        HOTween.Init(true, true, true);
        StartupNode();
	
	}

    float _NextCheckTime = 0f;
    // Update is called once per frame
    void Update()
    {
        if (GamePlayManager.Instance._State == E_PlayState.GamePlaying
            && _isStop == false
            && _NextCheckTime < Time.time
            )
        {
            _NextCheckTime = Time.time + _CheckNodeTime;
            PlayCurentNode();
        }

    }

    void PlayCurentNode()
    {
        if(_CuttentIx >= _ChildNode.Length
            )
        {
            // NGUIDebug.Log("Done Wave !!!");
            _isStop = true;
            GamePlayManager.Instance.GameEnd();
            return;
        }
        if (_ChildNode[_CuttentIx] != null)
        {
            if (_ChildNode[_CuttentIx]._isPlay == true)
            {
                if (_ChildNode[_CuttentIx].isPlayEnd() == true)
                {
                    _CuttentIx++;
                }
                else
                {
                }
            }
            else
            {
                float xCalc = _ChildNode[_CuttentIx].transform.position.x - _Trigger_StartPos.position.x;
                if (xCalc <= 0)
                {
                    if (_ChildNode[_CuttentIx]._isAutoPlay)
                    {
                        _ChildNode[_CuttentIx].PlayNode();
                    }
                }
                else if (xCalc > 0
                   )
                {
                    HOTween.To(_Move_Root, _CheckNodeTime, "position", new Vector3(_Move_Root.position.x - _CheckNodeShiftValue, _Move_Root.position.y, 0));
                }
            }
        }
        else
        {
            Debug.LogError("~~~~~~~~~~~~ _ChildNode[_CuttentIx] == null ");
            _CuttentIx++;
        }
    }
}
