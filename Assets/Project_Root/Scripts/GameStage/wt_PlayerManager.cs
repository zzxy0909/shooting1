using UnityEngine;
using System.Collections;

public class wt_PlayerManager : MonoBehaviour {
    public GameObject _pfPlayerController;
    public wt_PlayerController _PlayerController;
    public Transform _SpawnPos;
    public Player_DataRecord _Player_DataRecord;
	
    void Awake()
    {
        GamePlayManager.Instance._PlayerManager = this;
    }

    public void SpawnPlayer()
    {
        if (_PlayerController)
        {
            Destroy(_PlayerController.gameObject);
        }

        GameObject obj = (GameObject)Instantiate(_pfPlayerController, _SpawnPos.position, _SpawnPos.rotation);
        obj.transform.parent = this.transform;

        _PlayerController = obj.GetComponent<wt_PlayerController>();

    }
    // Use this for initialization
    void Start()
    {

        //~~~~~~~~~~~~~~~ test data	
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

        // 각 playercontroller슬롯의 LoadData()로 성정.
        /*        _Player_DataRecord._HP = DataManager.Instance.Get_TotalHP();
                _Player_DataRecord._Attack = DataManager.Instance.Get_TotalAttack();
                _Player_DataRecord._Def = DataManager.Instance.Get_TotalDef();
                _Player_DataRecord._AttackSpeed = DataManager.Instance.Get_TotalAttackSpeeed();
                _Player_DataRecord._CriticalAttackRatio = DataManager.Instance.Get_TotalCriticalRatio();

                string str = string.Format("~~~~~~~~ set hp:{0}, _Attack:{1}, _Def:{2}, _AttackSpeed:{3}, _CriticalAttackRatio:{4} "
                    , _Player_DataRecord._HP
                    , _Player_DataRecord._Attack
                    , _Player_DataRecord._Def
                    , _Player_DataRecord._AttackSpeed
                    , _Player_DataRecord._CriticalAttackRatio
                    );

                Debug.Log(str);
         * */
        
        //        _Player_Info.Reset();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
