using UnityEngine;
using System.Collections;

public enum E_FireType{
    melee,
    range,
    multi,
    multi_missile,
    missile,
}
public enum E_EnemyType
{
    normal,
    champ,
    Boss_sub,
    Boss_main,
}
public enum E_SetDataType
{
    SpawnRecAuto,
    SceneManual,
}

public class Enemy : MonoBehaviour
{
    public ST_B_stage_spawnRec _SpawnRec;
    public E_SetDataType _SetDataType = E_SetDataType.SpawnRecAuto;
    public UnitInfo _UnitInfo;
	public int _CostPoint = 1;

    public wt_AnimationController _AniController;
    public GameObject _pfSlash01;
    public GameObject shot_range;
    public GameObject shot_melee;
    public Transform shotSpawn;
    public E_FireType _FireType = E_FireType.multi;
    public float fireRate = 2.5f;
    public bool _isRandom_fireRate = false;
    public Vector2 _fireRate_RandomRange;
    public bool _isAutoFire = true;
    public float _StartFireDelay = 2.5f;
    public bool _isDash = false;
    public float _DashValue = -1f;
    private float nextFire;

    public E_EnemyType _EnemyType = E_EnemyType.normal;

    public void LoadUnitData()
    {
        if (_UnitInfo == null)
        {
            _UnitInfo = transform.GetComponentInChildren<UnitInfo>();
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
    }

	void Start()
	{
        Ready();
    }

    void Ready()
    {
        if (_isRandom_fireRate == true)
        {
            if (_fireRate_RandomRange != Vector2.zero)
            {
                fireRate = Random.Range(_fireRate_RandomRange.x, _fireRate_RandomRange.y);
            }
            
        }
        if (_UnitInfo == null)
        {
            _UnitInfo = transform.GetComponentInChildren<UnitInfo>();
        }
        _UnitInfo._DeathFlag = false;
        // _UnitInfo._HP = _UnitInfo._HP_Default;
        SetData_UnitInfo();

        _isAutoFire = false;
        StartCoroutine(DelayAction(_StartFireDelay, () =>
        {
            _isAutoFire = true;
        }));
    }

    void SetData_UnitInfo()
    {
        if (_SetDataType == E_SetDataType.SpawnRecAuto)
        {
            _UnitInfo._HP = _UnitInfo._HP_Default = _UnitInfo._HP_Default * _SpawnRec.level_min;
            _UnitInfo._Attack = _UnitInfo._Attack * _SpawnRec.level_min;
            _UnitInfo._Level = _SpawnRec.level_min;
        }
        else if (_SetDataType == E_SetDataType.SceneManual)
        {

        }
    }

    // delay method coroutine
    IEnumerator DelayAction(float dTime, System.Action callback)
    {
        yield return new WaitForSeconds(dTime);
        callback();
    }
    void Update()
    {
        if (Time.time > nextFire
            && _isAutoFire == true
            )
        {
            nextFire = Time.time + fireRate;
            PlayShot();
        }

    }

    void PlayShot()
    {
        switch (_FireType)
        {
            case E_FireType.missile:
                Shot_Missile();
                    
                break;
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

    void Shot_Missile()
    {
        if (_AniController == null)
        {
            return;
        }

        _AniController.PlayAttackShoot(fireRate);
        StartCoroutine(DelayAction(_AniController._ShootDelay, () =>
        {
            GameObject obj = (GameObject)Instantiate(shot_range, shotSpawn.position, shotSpawn.rotation);
            Vector3 target_pos = GamePlayManager.Instance._PlayerManager._PlayerController.transform.position;
            Vector3 val = (target_pos - shotSpawn.position).normalized;
            wt_Mover mov = obj.GetComponent<wt_Mover>();
            if (mov)
            {
                mov._isMulti = true;
                mov._speedX = val.x * mov.speed;
                mov._speedY = val.y * mov.speed;
            }

            SetDamageTrigger(obj);
            GetComponent<AudioSource>().Play();
        }));

    }
    void Shot_Melee()
    {
        if (_AniController == null)
        {
            return;
        }

        _AniController.PlayAttackSlash1(fireRate);

        StartCoroutine(DelayAction(_AniController._SlashDelay, () =>
        {
            GameObject obj = (GameObject)Instantiate(shot_melee, shotSpawn.position, shotSpawn.rotation);
            obj.transform.parent = shotSpawn;

            SetDamageTrigger(obj);
            GetComponent<AudioSource>().Play();

            if (_isDash)
            {
                Vector3 pos = transform.position + new Vector3(_DashValue, 0f, 0f);
                TweenPosition.Begin(this.gameObject, 0.5f, pos).method = UITweener.Method.EaseInOut;
                    
            }
        }));
    }
    void Shot_Range()
    {
        if (_AniController == null)
        {
            return;
        }

        _AniController.PlayAttackShoot(fireRate);
        StartCoroutine(DelayAction(_AniController._ShootDelay, () =>
        {
            GameObject obj = (GameObject)Instantiate(shot_range, shotSpawn.position, shotSpawn.rotation);
            SetDamageTrigger(obj);
            GetComponent<AudioSource>().Play();
        }));
    }
    
	void FixedUpdate ()
	{
        if (_UnitInfo == null)
        {

        }
        else
        {
            if (_UnitInfo._HP <= 0 && _UnitInfo._DeathFlag == false)
            {
                _UnitInfo._DeathFlag = true;
                Death();
            }
        }
	}
	
	public void Hurt(int n)
	{
		// Reduce the number of hit points by n.
        _UnitInfo._HP -= n;
	}
	
	void Death()
	{
        GamePlayManager.Instance._Score.AddScore(_CostPoint);
        Destroy(this.gameObject);
	}


	public void Flip()
	{
		// Multiply the x component of localScale by -1.
		Vector3 enemyScale = transform.localScale;
		enemyScale.x *= -1;
		transform.localScale = enemyScale;
	}
}
