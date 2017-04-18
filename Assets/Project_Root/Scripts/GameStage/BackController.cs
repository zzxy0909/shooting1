using UnityEngine;
using System.Collections;

public class BackController : MonoBehaviour {
    public float _Speed = 1f;
    public AnimationClip _StartClip;
    AnimationState _DefaultClip;
    public Animation _ani;
    // Use this for initialization
	void Start () {
        if(_ani==null)
        {
            _ani = GetComponent<Animation>();
        }
        _DefaultClip = _ani[_StartClip.name];
	}
	
	// Update is called once per frame
	void Update () {
        if (_ani)
        {
            CheckPlay();
        }
        else
        {
            _ani = GetComponent<Animation>();
        }
	}

    void CheckPlay()
    {
        if (GamePlayManager.Instance._State == E_PlayState.GamePlaying)
        {
            if (_ani.isPlaying == false)
            {
                _DefaultClip = _ani[_StartClip.name];

                _DefaultClip.speed = _Speed;
                // _ani.Play(_DefaultClip.name);
                _ani.CrossFade(_DefaultClip.name, 0.3f);
            }
        }
        else if(GamePlayManager.Instance._State == E_PlayState.GameReady)
        {
            // _ani.Rewind();
        }
    }
}
