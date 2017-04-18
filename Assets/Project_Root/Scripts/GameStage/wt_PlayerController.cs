using UnityEngine;
using System.Collections;

[System.Serializable]
public class wt_Boundary 
{
	public float xMin, xMax, yMin, yMax;
}

public enum E_HeroType
{
    main,
    sub,
}
public class wt_PlayerController : MonoBehaviour
{
	static float _speed = 7f;
    public E_HeroType _HeroType = E_HeroType.main;
    public wt_PlayerController[] _arrSubController;
	public wt_Boundary boundary;

    public GameObject _pfSlash01;
	public GameObject _pfRangeShoot;
	public Transform shotSpawn;
	public float _AutoShotRate;
    public bool _isAutoShot = true;	 
	private float nextFire;
    public E_FireType _FireType = E_FireType.range;
    public wt_PlayerMeleeCheck _PlayerMeleeCheck;

    public wt_AnimationController _AniController;
    public Transform _JointRoot;
    public UnitInfo _UnitInfo;
    public int _slot_no = 0;
    public Transform _UnitRoot;

    void Start()
    {
        //~~~~~~~~~~~~~~~ data	
        StartCoroutine(IE_StartData());
    }
    IEnumerator IE_StartData()
    {
        while (true)
        {
            if (DataManager.Instance != null)
            {
                if (DataManager.Instance._SetupDataManager)
                {
                    if (DataManager.Instance._SetupDataManager._SetupOK == true)
                    {
                        break;
                    }
                }
            }
            yield return new WaitForFixedUpdate();
        }
        while (true)
        {
            Camera guiCam = UICamera.currentCamera;
            if (guiCam != null)
            {
                break;
            }
            yield return new WaitForFixedUpdate();
        }

        LoadData();
        Ready();
    }

    void LoadData()
    {
        if(_HeroType == E_HeroType.main)
        {
            _slot_no = 1;
        }
        ST_S_unit_invenRec saveUnit = new ST_S_unit_invenRec();
        saveUnit = DataManager.Instance._SqlSavedata_unit_inven.Get_All_From_slot_no(_slot_no);
        if (saveUnit.idx > 0)
        {
            GameObject prefab = GameWorld.Instance._UnitList.GetPrefab_Unit(saveUnit.unit_code);
            if (prefab)
            {
                GameObject childObj = (GameObject)Instantiate(prefab);
                if (_HeroType == E_HeroType.sub && _JointRoot != null)
                {
                    _JointRoot.parent = null;
                    childObj.transform.parent = _JointRoot;
                }
                else
                {
                    childObj.transform.parent = this.transform;
                }
                
                childObj.transform.localPosition = new Vector3(0f, 0f, 0f);
                childObj.transform.localScale = new Vector3(1f, 1f, 1f);
                _UnitRoot = childObj.transform;

                _UnitInfo = _UnitRoot.GetComponentInChildren<UnitInfo>();
                _AniController = _UnitRoot.GetComponentInChildren<wt_AnimationController>();

                _UnitInfo._Table_idx = saveUnit.idx;
                _UnitInfo._unit_code = saveUnit.unit_code;
                _UnitInfo._ClassNo = saveUnit.class_no;
                _UnitInfo._Level = DataManager.Instance.GetUnitLevel(_UnitInfo._Table_idx.ToString(), _UnitInfo._ClassNo);
                _UnitInfo._TotalExp = DataManager.Instance._SqlSavedata_unit_inven.Get_total_exp(_UnitInfo._Table_idx.ToString());

                _FireType = _UnitInfo._FireType;
            }
            else
            {
                NGUIDebug.Log("Error LoadData() -- GetPrefab_Unit");
            }
            
            
        }
        else
        {
            NGUIDebug.Log("Error LoadData() 1");
        }

        ST_B_UnitRec balanceUnit = new ST_B_UnitRec();
        balanceUnit = DataManager.Instance._SqlBalance_unit.Get_UnitRec(_UnitInfo._unit_code, _UnitInfo._Level, _UnitInfo._ClassNo);
        if (balanceUnit.class_no > 0)
        {
            _UnitInfo._HP_Default = _UnitInfo._HP = balanceUnit.hp;
            _UnitInfo._Attack = balanceUnit.attack;
        }
        else
        {
            Debug.Log("Error LoadData() 2-->" + "," + _UnitInfo._unit_code + "," + _UnitInfo._Level + "," + _UnitInfo._ClassNo);
        }

        if (_HeroType == E_HeroType.sub)
        {
 /* 타입별로 포지션 정해지게끔 수정 함.
  *         Vector2 pos = DataManager.Instance.Get_pos_slot(_slot_no);
            this.transform.localPosition = pos;
            Collider2D col = GetComponentInChildren <Collider2D>();
            if(col)
            {
                Debug.Log("~~~~~~~ " + col.gameObject.name);
                col.enabled = true;
            }
  * */
        }
    }
    public void SaveUnitData()
    {
        if (_UnitInfo == null || _UnitInfo._Table_idx <= 0)
        {
            return;
        }

        DataManager.Instance._SqlSavedata_unit_inven.Update_total_exp(_UnitInfo._TotalExp, _UnitInfo._Table_idx.ToString());
    }

    void Ready()
    {
        if (_UnitInfo == null)
        {
            _UnitInfo = _UnitRoot.GetComponentInChildren<UnitInfo>();
        }
        _UnitInfo._DeathFlag = false;
        _UnitInfo._HP = _UnitInfo._HP_Default;
    }

    public void AddUnitExp(int n)
    {
        if (_UnitInfo == null)
        {
            _UnitInfo = _UnitRoot.GetComponentInChildren<UnitInfo>();
        }
        if (_UnitInfo._Table_idx <= 0)
        {
            return;
        }
        _UnitInfo._TotalExp += n;
    }
    public void Hurt(int n)
    {
        // Reduce the number of hit points by n.
        _UnitInfo._HP -= n;
    }

    // delay method coroutine
    IEnumerator DelayAction(float dTime, System.Action callback)
    {
        yield return new WaitForSeconds(dTime);
        callback();
    }

	void Update ()
	{
		if ( Time.time > nextFire
            && _isAutoShot == true
            ) 
		{
			nextFire = Time.time + _AutoShotRate;
            if (_HeroType == E_HeroType.main)
            {
                PlayShot_main();
            }
            else if (_HeroType == E_HeroType.sub)
            {
                PlayShot_sub();
            }
		}

        if (_HeroType == E_HeroType.main)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isDrag = true;
                _oldMousePos = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0))
            {
                _isDrag = false;
            }
        }
	}
    void PlayShot_main()
    {
        if (_PlayerMeleeCheck != null)
        {
            bool b = _PlayerMeleeCheck.CheckMelee();
            if (b)
            {
                Shot_Melee();
            }
            else
            {
                Shot_Range();
            }
        }
        else
        {
            switch (_FireType)
            {
                case E_FireType.range:
                    Shot_Range();
                    break;
                case E_FireType.melee:
                    Shot_Melee();
                    _FireType = E_FireType.range;
                    break;
            }
        }

    }
    void PlayShot_sub()
    {
        switch (_FireType)
        {
            case E_FireType.multi_missile:
                {
                    int r = Random.Range(0, 2);
                    if (r == 0)
                    {
                        Shot_Missile();
                    }
                    else
                    {
                        Shot_Melee();
                    }
                }
                break;
            case E_FireType.multi:
                {
                    int r = Random.Range(0, 2);
                    if (r == 0)
                    {
                        Shot_Range();
                    }
                    else
                    {
                        Shot_Melee();
                    }
                }
                break;
            case E_FireType.range:
                Shot_Range();
                break;
            case E_FireType.melee:
                Shot_Melee();
                break;
        }
    }
    void Shot_Missile()
    {
        if (_AniController == null)
        {
            return;
        }

        _AniController.PlayAttackShoot(_AutoShotRate);
        StartCoroutine(DelayAction(_AniController._ShootDelay, () =>
        {
            GameObject obj = (GameObject)Instantiate(_pfRangeShoot, shotSpawn.position, shotSpawn.rotation);
            SetDamageTrigger(obj);
            GetComponent<AudioSource>().Play();
        }));
    }
    void SetDamageTrigger(GameObject obj)
    {
/*        DamageTrigger dtRoot = obj.GetComponent<DamageTrigger>();
        if (dtRoot != null && _UnitInfo != null)
        {
            dtRoot._DamageValue += _UnitInfo._Attack;
        }
*/
        DamageTrigger[] arrdamage = obj.GetComponentsInChildren<DamageTrigger>();
        foreach (DamageTrigger damage in arrdamage)
        {
            if (damage != null && _UnitInfo != null)
            {
                damage._DamageValue += _UnitInfo._Attack;
            }
        }
    }
    void Shot_Melee()
    {
        if (_AniController == null)
        {
            return;
        }

        _AniController.PlayAttackSlash(_AutoShotRate);

        StartCoroutine(DelayAction(_AniController._SlashDelay, () =>
        {
            GameObject obj = (GameObject)Instantiate(_pfSlash01, shotSpawn.position, shotSpawn.rotation);
            SetDamageTrigger(obj);
            GetComponent<AudioSource>().Play();
        }));
    }
    void Shot_Range()
    {
        if (_AniController == null)
        {
            return;
        }

        _AniController.PlayAttackShoot(_AutoShotRate);
        StartCoroutine(DelayAction(_AniController._ShootDelay, () =>
        {
            GameObject obj = (GameObject)Instantiate(_pfRangeShoot, shotSpawn.position, shotSpawn.rotation);
            SetDamageTrigger(obj);
            GetComponent<AudioSource>().Play();
        }));
    }

    bool _isDrag = false;
    Vector2 _oldMousePos = Vector2.zero;

    public float moveHorizontal;
    public float moveVertical;
	void FixedUpdate ()
	{
        if (_HeroType == E_HeroType.main)
        {
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");

            if (_isDrag == true)
            {
                float move_ratioX = (0.16f * 1.5f);
                float move_ratioY = (0.16f * 1.5f);
                moveHorizontal = (Input.mousePosition.x - _oldMousePos.x) * move_ratioX;
                moveVertical = (Input.mousePosition.y - _oldMousePos.y) * move_ratioY;
                _oldMousePos = Input.mousePosition;
            }

            Vector2 movement = new Vector2(moveHorizontal, moveVertical);

            GetComponent<Rigidbody2D>().velocity = movement * _speed;

            GetComponent<Rigidbody2D>().transform.position = new Vector3
            (
                Mathf.Clamp(GetComponent<Rigidbody2D>().transform.position.x, boundary.xMin, boundary.xMax),
                Mathf.Clamp(GetComponent<Rigidbody2D>().transform.position.y, boundary.yMin, boundary.yMax),
                0.0f
            );

            if (_UnitInfo == null)
            {

            }
            else
            {
                if (_UnitInfo._HP <= 0 && _UnitInfo._DeathFlag == false)
                {
                    _UnitInfo._DeathFlag = true;
                    Death_main();
                }
            }
        }
        else if (_HeroType == E_HeroType.sub)
        {
            if (_UnitInfo == null)
            {

            }
            else
            {
                if (_UnitInfo._HP <= 0 && _UnitInfo._DeathFlag == false)
                {
                    _UnitInfo._DeathFlag = true;
                    Death_sub();
                }
            }
        }
		
	}
    void Death_sub()
    {
        // Destroy(this.gameObject);
        _isAutoShot = false;
        // hpbar의 ngui 는 hud 관련 작업 할때 작업 필요. 지금은 0.5f동안 LateUpdate에서 _UnitInfo._DeathFlag == true 로 처리 하는 것으로 한다.
        StartCoroutine(DelayAction(0.5f, () =>
        {
            GameWorld.SetActiveRecursively(_JointRoot.gameObject, false);
        }));
        
    }
    void Death_main()
    {
        // GameOver
        GamePlayManager.Instance.GameEnd();
    }
    public void PlaySlash1()
    {
        _isAutoShot = false;
        _AniController.PlayAttackSlash1(_AutoShotRate);
        StartCoroutine(DelayAction(0.3f, () =>
        {
            GameObject obj = (GameObject) Instantiate(_pfSlash01, transform.position, Quaternion.identity);
            GetComponent<AudioSource>().Play();
        }));

        StartCoroutine(DelayAction(1f, () =>
        {
            _isAutoShot = true;
        }));
        
    }
}
