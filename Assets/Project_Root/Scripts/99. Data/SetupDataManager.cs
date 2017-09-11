using UnityEngine;
using System.Collections;

using System.IO;


public class SetupDataManager : MonoBehaviour {
	public bool _ViewLog = false;
	private string _log;
	void OnGUI()
	{
		if(_ViewLog == true)
		{
			GUI.Label (new Rect (10,70,600,600), _log);
		}
	}
	public bool _SetupOK = false;
	public int _SetupDbCount = 0;
	public int _CompleteCount = 2;

    public bool _StartupData = false;
    public float _SetupDelay = 1f;
	
    public GameObject _pfWaitPopup;
//    public Transform _PopupBase;
    public GameObject _objWaitPopup=null;
	// Use this for initialization
	void Start () {
        if (_pfWaitPopup)
        {
            _objWaitPopup = (GameObject)Instantiate(_pfWaitPopup);
            GameObject obj = GameObject.FindGameObjectWithTag("GUI_Base");
            if (obj)
            {
                _objWaitPopup.transform.parent = obj.transform;
                _objWaitPopup.transform.localScale = new Vector3(1f, 1f, 1f);
                _objWaitPopup.transform.localPosition = new Vector3(0f, 0f, -100f);
            }

        }
	}
	
	// Update is called once per frame
	void Update () {
		if(_SetupDbCount == _CompleteCount)
		{
            Destroy(_objWaitPopup);
			_SetupOK = true;
        }
        else if (_SetupOK == true && _objWaitPopup != null)
        {
            Destroy(_objWaitPopup);
			
        }
	
	}
	
	void Awake()
	{
		if(DataManager.Instance._SetupDataManager == null)
		{
        	DataManager.Instance._SetupDataManager = this;
            Debug.Log("~~~~~~~~~~~~~~~~~~~~~~~~ DataManager.Instance._SetupDataManager = this; ");
		}else{
			Destroy(this.gameObject);
            Debug.Log("~~~~~~~~~~~~~~~~~~~~~~~~ Destroy(this.gameObject); DataManager.Instance._SetupDataManager = this; ");
            return;
		}
        transform.parent = null;
		DontDestroyOnLoad(this);
		if(CheckSaveFile() == false)
		{
			StartCoroutine(IE_SetupTestData());
			StartCoroutine(IE_SetupTestBalance());
		}else{
			
			_SetupOK = true;
		}
		
	}

    public void ResetBalanceFile()
    {
        StartCoroutine(IE_SetupTestBalance());
    }
    public void ResetDataFile()
    {
        StartCoroutine(IE_SetupTestBalance());
    }

	public bool CheckSaveFile()
	{
		// ver 관리 필요...

        if (_StartupData == true)
        {
            return false;
        }
		
		string filename = Application.persistentDataPath + "/" + DataManager._DataBaseFileName_SaveDB;
		if(File.Exists(filename))
		{
			return true;
		}else{
			return false;
		}
	}

    public TextAsset _defaultData;
    public TextAsset _defaultBalance;

    IEnumerator IE_SetupTestData()
	{
        yield return new WaitForSeconds(_SetupDelay);

		string dbfilename = DataManager._DataBaseFileName_StartupSaveDB;
		byte[] bytes = null;				
		
		
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
		string dbpath = "file://" + Application.streamingAssetsPath + "/" + dbfilename; 	_log += "asset path is: " + dbpath;
#elif UNITY_ANDROID
		string dbpath =  Application.streamingAssetsPath + "/" + dbfilename;	            _log += "asset path is: " + dbpath;
#endif
		WWW www = new WWW(dbpath);				
		yield return www;		
//		if (!string.IsNullOrEmpty(www.error) )
//		{
//		    log += " Can't read";
//		}

        if(_defaultData !=null)
        {
            bytes = _defaultData.bytes;
            Debug.Log("~~~~~~~~ _defaultData.bytes");
        }
        else
        {
            bytes = www.bytes;
        }

		_log += "\n www.error = [" + www.error + "] www.size = " + www.size;
				
		if ( bytes != null )
		{
			try{	
				string filename = Application.persistentDataPath + "/" + DataManager._DataBaseFileName_SaveDB; // demo_from_streamingAssets.db";						
				using( FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write) )
				{
					fs.Write(bytes,0,bytes.Length);             _log += "\nCopy database from streaminAssets to persistentDataPath: " + filename;
				}
				
				//_SetupOK = true;
				_SetupDbCount++;
				
			}catch (System.Exception e){
				_log += 	"\nTest Fail with Exception " + e.ToString();
				_log += 	"\n\n Did you copy File into StreamingAssets ?\n";
			}
		}
	}

    public string _BalanceDataUrl = "";
	IEnumerator IE_SetupTestBalance()
	{
        yield return new WaitForSeconds(_SetupDelay);
        
        string dbfilename = DataManager._DataBaseFileName_StartupBalanceDB;
		byte[] bytes = null;				
		
		//=============
		#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
		string dbpath = "file://" + Application.streamingAssetsPath + "/" + dbfilename;
		#elif UNITY_ANDROID
		string dbpath =  Application.streamingAssetsPath + "/" + dbfilename;
		#endif
        //==================
        
 //       dbpath = _BalanceDataUrl;

        _log += "asset path is: " + dbpath;
		WWW www = new WWW(dbpath);				
		yield return www;		
		if (!string.IsNullOrEmpty(www.error) )
		{
            _log += " Can't read";

#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
            dbpath = "file://" + Application.streamingAssetsPath + "/" + dbfilename; _log += "asset path is: " + dbpath;
#elif UNITY_ANDROID
		    dbpath =  Application.streamingAssetsPath + "/" + dbfilename;	            _log += "asset path is: " + dbpath;
#endif
            www = new WWW(dbpath);
            yield return www;
		}

        if (_defaultBalance != null)
        {
            bytes = _defaultBalance.bytes;
            Debug.Log("~~~~~~~~ _defaultBalance.bytes");
        }
        else
        {
            bytes = www.bytes;
        }

        bytes = www.bytes;
		
		_log += "\n www.error = [" + www.error + "] www.size = " + www.size;
		
		if ( bytes != null )
		{
			try{	
				string filename = Application.persistentDataPath + "/" + DataManager._DataBaseFileName_BalanceDB; // demo_from_streamingAssets.db";						
				using( FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write) )
				{
					fs.Write(bytes,0,bytes.Length);             _log += "\nCopy database from streaminAssets to persistentDataPath: " + filename;
				}
				
				//_SetupOK = true;
				_SetupDbCount++;
				
			}catch (System.Exception e){
				_log += 	"\nTest Fail with Exception " + e.ToString();
				_log += 	"\n\n Did you copy File into StreamingAssets ?\n";
			}
		}
	}
}
