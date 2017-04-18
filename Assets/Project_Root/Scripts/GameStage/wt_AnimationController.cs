using UnityEngine;
using System.Collections;

[System.Serializable]
public class Property_Animation
{
    public int _Hit = 0;
    public int _AttackType = 0;
    public int _StateType = 0;

    public void SetProperty(Property_Animation p)
    {
        _Hit = p._Hit;
        _AttackType = p._AttackType;
        _StateType = p._StateType;
    }
}

public class wt_AnimationController : MonoBehaviour {
    public Property_Animation _Property = new Property_Animation();
    public Property_Animation _PropertyNext = new Property_Animation();
    public Animation _ani;
    public float _ShootDelay;
    public float _SlashDelay;

    string _strIdle = "IDLE";
    string _strSlash = "SLASH";
    string _strSlash2 = "SLASH_2";
    string _strSlash3 = "SLASH_3";
    string _strShoot = "SHOOT";
    string _strHit = "HIT";

	// Use this for initialization
	void Start () {

        
	}
	
	// Update is called once per frame
	void Update () {
        if (_ani)
        {
            CheckProperty_Play();
        }
        else
        {
            _ani = GetComponent<Animation>();
        }
	}

    void Play_Ani(string strname, float dur, bool onlyDur)
    {
        if (_ani == null)
        {
            return;
        }

        AnimationClip clip = _ani.GetClip(strname);
        if (clip != null)
        {
            /*
            if (clip.length > 0.3f)
            {
                _ani.CrossFade(strname, 0.15f);
            }
            else
            {
                _ani.PlayQueued(strname);
            }
             * */
            _CurrentAnimationState = _ani[strname];

            float speed = 1f;
            if (dur > 0f)
            {
                if (_CurrentAnimationState.length < dur && onlyDur == false)
                {
                    speed = 1f;
                }
                else
                {
                    speed = _CurrentAnimationState.length / dur;
                }
            }
            _CurrentAnimationState.speed = speed;
            
            _ani.Play(strname);

            

            _PropertyNext._Hit = 0; //  next hit는 play후 0 유지.
            _PropertyNext._AttackType = 0; //  next _AttackType는 play후 0 유지.
        }
        else
        {
            NGUIDebug.Log("~~~~ Error! Play_Ani : " + strname);
        }

    }
    /// <summary>
    /// current play가 95% 진행 되었다면, 다음 적용 여부 설정후 OnPlayFinal() 호출.
    /// </summary>
    const float _OnPlayFinalRatio = 0.95f;
    void OnPlayFinal()
    {

    }

    public AnimationState _CurrentAnimationState = null;
    void CheckProperty_Play()
    {
        if (_CurrentAnimationState == null)
        {
            SetProperty_idle();
            SetPropertyNext_idle();
            Play_Ani(_strIdle, -1, false);
            return;
        }
        bool bApplyNext = false;

        // Loop 속정 확인 후 다음 속성 적용
        if (_CurrentAnimationState.wrapMode == WrapMode.Loop
            // && _OldAnimationState != _CurrentAnimationState
            )
        {
            bApplyNext = true;
        }
        else
        {
            // 조건에 따라 다음 적용 
            // 1. next hit != 0 면, Hit 동작 수행. - next hit는 play후 0 유지.
            if (_PropertyNext._Hit != 0)
            {
                bApplyNext = true;
            }

            float play_ratio = _CurrentAnimationState.time / _CurrentAnimationState.length;
            // current play가 _OnPlayFinalRatio % 진행 되었다면 다음 동작 수행.
            if (play_ratio > _OnPlayFinalRatio)
            {
                // 2. next attack 이 current 와 다르고,  current play가 _OnPlayFinalRatio % 진행 되었다면 다음 동작 수행.
                // 99. next state가 current 와 다르면 적용. ( WrapMode.Loop 인 경우와 부합될수 있지만 변경 속석을 정할때 필요. )
                if (_Property._AttackType != _PropertyNext._AttackType)
                {
                    bApplyNext = true;
                }
                else if (_Property._AttackType != _PropertyNext._AttackType
                   || _Property._Hit != _PropertyNext._Hit
                   || _Property._StateType != _PropertyNext._StateType
                   )
                {
                    bApplyNext = true;
                }

                OnPlayFinal();
            }
            else if (_ani.isPlaying == false)
            {
                bApplyNext = true;
            }

        }

        if (bApplyNext == true)
        {
            _Property.SetProperty(_PropertyNext);

            switch (_Property._Hit)
            {
                case 1:
                    Play_Ani(_strHit, -1, false);
                    return;
            }

            switch (_Property._AttackType)
            {
                case 1:
                    Play_Ani(_strShoot, -1, false);
                    return;
                case 2:
                    Play_Ani(_strSlash, -1, false);
                    return;
            }

            switch (_Property._StateType)
            {
                case 0:
                    Play_Ani(_strIdle, -1, false);
                    return;
            }
        }
    }

    void SetProperty_idle()
    {
        _Property._StateType = 0;
        _Property._Hit = 0;
        _Property._AttackType = 0;
    }
    public void SetPropertyNext_idle()
    {
        _PropertyNext._StateType = 0;
        _PropertyNext._Hit = 0;
        _PropertyNext._AttackType = 0;
    }

    public void NextAttackShoot()
    {
        _PropertyNext._AttackType = 1;
    }
    public void NextAttackSlash1()
    {
        _PropertyNext._AttackType = 2;
    }

    public void PlayHit(float dur)
    {
        _PropertyNext._Hit = 1;
        _Property._Hit = 1;
        Play_Ani(_strHit, dur, true);
    }

    public void PlayAttackShoot(float dur)
    {
        _PropertyNext._AttackType = 1;
        _Property._AttackType = 1;
        Play_Ani(_strShoot, dur, false);
    }
    public void PlayAttackSlash1(float dur)
    {
        _PropertyNext._AttackType = 2;
        _Property._AttackType = 2;
        Play_Ani(_strSlash, dur, false);
    }


    public void PlayAttackSlash(float dur)
    {
        dur = dur * 1.2f;
        if (_Property._AttackType == 2)
        {
            AnimationClip clip = _ani.GetClip(_strSlash2);
            if (clip != null)
            {
                _PropertyNext._AttackType = 3;
                _Property._AttackType = 3;
                Play_Ani(_strSlash2, dur, false);
            }
            else
            {
                _PropertyNext._AttackType = 2;
                _Property._AttackType = 2;
                Play_Ani(_strSlash, dur, false);
            }
        }
        else if (_Property._AttackType == 3)
        {
            AnimationClip clip = _ani.GetClip(_strSlash3);
            if (clip != null)
            {
                _PropertyNext._AttackType = 4;
                _Property._AttackType = 4;
                Play_Ani(_strSlash3, dur, false);
            }
            else
            {
                _PropertyNext._AttackType = 2;
                _Property._AttackType = 2;
                Play_Ani(_strSlash, dur, false);
            }
        }
        else
        {
            _PropertyNext._AttackType = 2;
            _Property._AttackType = 2;
            Play_Ani(_strSlash, dur, false);
        }
    }
}
