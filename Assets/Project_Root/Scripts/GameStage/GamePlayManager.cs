using UnityEngine;
using System.Collections;

public enum E_PlayState
{
    GameReady,
    GamePlaying,
    GameEnd,
}

public class GamePlayManager : MonoBehaviour {
    private static GamePlayManager _instance;
    public static GamePlayManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.FindObjectOfType(typeof(GamePlayManager)) as GamePlayManager;
                if (!_instance)
                {
                    Debug.LogError("PlayManager _instance null");
                    return null;
                }
            }
            return _instance;
        }
    }
    void Awake()
    {
        if (!_instance)
        {
            _instance = this;
            //            DontDestroyOnLoad(_instance);
        }
    }
    public UIScore _Score;
    public UIBestScore _BestScore;
    public E_PlayState _State;
    public GameObject _GamePlayUI;
    public GameObject _pfResult;
    public wt_PlayerManager _PlayerManager;
    public Transform _UI_Center;
    public UserExp _UserExp;
    public EnemyPool _EnemyPool;
    public int _CurrentWave = 1;
    public SpawnController _SpawnController;
    public wt_WaveLoader _WaveLoader;


    void Ready()
    {
        _State = E_PlayState.GameReady;
    }

    public void EditTime()
    {
    }

    public void GamePlay()
    {
/*        Enemy[] arrEnemy = FindObjectsOfType<Enemy>();
        foreach (Enemy en in arrEnemy)
        {
            Destroy(en.gameObject);
        }
*/
/*        if (_CurrentWave > 1)
        {
            _CurrentWave = (int) (_CurrentWave  * 0.7f);
        }
        _SpawnController.LoadSpawnData();
        _SpawnController.Play_Spawn();
        */

        _Score.SetScore(0);
        _State = E_PlayState.GamePlaying;
        GameWorld.SetActiveRecursively(_GamePlayUI, false);
        _PlayerManager.SpawnPlayer();

        _WaveLoader.LoadCurrentWaveSet();
    }
    public void GameEnd()
    {
        _State = E_PlayState.GameEnd;
        // game end report...
        StartCoroutine(IE_GameEnd());

    }
    IEnumerator IE_GameEnd()
    {
        float delay = 2f;
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds( delay* Time.timeScale);
        
        Enemy[] arrEnemy = FindObjectsOfType<Enemy>();
        foreach (Enemy en in arrEnemy)
        {
            Destroy(en.gameObject);
        }

        Time.timeScale = 1f;

        bool isFail = false;
        if (_PlayerManager._PlayerController._UnitInfo._HP <= 0)
        {
            isFail = true;
        }

        Destroy(_PlayerManager._PlayerController.gameObject);

        GameObject obj = (GameObject)Instantiate(_pfResult);
        obj.transform.parent = _UI_Center;
        obj.transform.localScale = new Vector3(1f, 1f, 1f);
        UIResult game_result = obj.GetComponent<UIResult>();
        if (isFail == true)
        {
            game_result._MessageTitle.text = "Mission Fail !";
            game_result._btnNext.gameObject.SetActive( false );
        }

        SaveUserExp();
        SaveUnitData();
    }
    void SaveUserExp()
    {
        if (_UserExp._Value > 0)
        {
            DataManager.Instance.UpdateTotalPlayScore(_UserExp._Value);
        }
    }
    void SaveUnitData()
    {
        if (_PlayerManager._PlayerController)
        {
            _PlayerManager._PlayerController.SaveUnitData();
        }
    }

	// Use this for initialization
	void Start () {

        Ready();
        _BestScore.SetBestScore(0);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void LoadStartMenu()
    {
        Application.LoadLevel(GameWorld._Name_StartMenu);
    }
}
