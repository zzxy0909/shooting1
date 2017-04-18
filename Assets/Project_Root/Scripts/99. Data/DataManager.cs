using UnityEngine;
using System.Collections;

public sealed class DataManager {

    public static string _DataBaseFileName_StartupBalanceDB = "demo_data_balance.db";
    public static string _DataBaseFileName_StartupSaveDB = "demo_data_save.db";
    public static string _DataBaseFileName_BalanceDB = "Balance_data_0001.db";
    public static string _DataBaseFileName_SaveDB = "Save_data_0001.db";
    public const int _GiftSlotNo = -10;

	static DataManager instance=null;
    static readonly object padlock = new object();
	DataManager()
    {
	}
	public static DataManager Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance==null)
                {
                    instance = new DataManager();
				}
                return instance;
            }
        }
    }
	
	public SetupDataManager _SetupDataManager = null;
	public SqlSavePlayerData _SqlSavePlayerData = new SqlSavePlayerData();
	public SqlBalance_level_info _SqlBalance_level_exp = new SqlBalance_level_info();
	public SqlBalance_mon_game_point _SqlBalance_mon_game_point = new SqlBalance_mon_game_point();
	public SqlBalance_player_weapon _SqlBalance_player_weapon = new SqlBalance_player_weapon();
	public SqlSavedata_player_weapon _SqlSavedata_player_weapon = new SqlSavedata_player_weapon();
    public SqlBalance_upgrade_info _SqlBalance_upgrade_info = new SqlBalance_upgrade_info();
    public SqlSavedata_upgrade _SqlSavedata_upgrade = new SqlSavedata_upgrade();
    public SqlBalance_style_info _SqlBalance_style_info = new SqlBalance_style_info();
    public SqlSavedata_player_stage _SqlSavedata_player_stage = new SqlSavedata_player_stage();
    public SqlBalance_stage _SqlBalance_stage = new SqlBalance_stage();
    public SqlBalance_pzlevel _SqlBalance_pzlevel = new SqlBalance_pzlevel();
    public SqlSavedata_info _SqlSavedata_info = new SqlSavedata_info();
    public SqlSavedata_unit_inven _SqlSavedata_unit_inven = new SqlSavedata_unit_inven();
    public SqlBalance_unit _SqlBalance_unit = new SqlBalance_unit();
    public SqlBalance_stage_spawn _SqlBalance_stage_spawn = new SqlBalance_stage_spawn();

    public void Update_pos_slot(int n, float x, float y)
    {
        string data_code = "pos_slot" + n;
        string strdata = string.Format("{0:0.00}|{1:0.00}", x, y);

        _SqlSavedata_info.Update_str_value(strdata, data_code);
    }
    public Vector2 Get_pos_slot(int n)
    {
        Vector2 rtn = Vector2.zero;
        string data_code = "pos_slot" + n;
        string strdata = _SqlSavedata_info.Get_str_value(data_code);
        string[] arrdata = strdata.Split('|');
        if (arrdata.Length > 1)
        {
            try
            {
                rtn.x = System.Convert.ToSingle(arrdata[0]);
                rtn.y = System.Convert.ToSingle(arrdata[1]);
            }
            catch
            {
                Debug.LogError("~~~~~~~~~~~ System.Convert.ToSingle(arrdata[0])");
            }
        }

        return rtn;
    }

    public void AddUnitExp(string idx, int addval)
    {
        int val = _SqlSavedata_unit_inven.Get_total_exp(idx);
        if (val <= 0)
        {
            NGUIDebug.Log("AddUnitExp Error!");
            return;
        }
        val += addval;
        _SqlSavedata_unit_inven.Update_total_exp(val, idx);
    }
    public int GetUnitLevel(string idx, int a_classNo)
    {
        int val = _SqlSavedata_unit_inven.Get_total_exp(idx);
        if (val <= 0)
        {
            NGUIDebug.Log("GetUnitLevel Error!");
            return 1;
        }
        return _SqlBalance_level_exp.Get_level(val, a_classNo);
    }

    public void AddPlayScore(int n_score)
    {
        string data_code = "total_play_score";
        int val = _SqlSavedata_info.Get_value(data_code);
        val += n_score;
        _SqlSavedata_info.Update_value(val, data_code);
    }
    public void UpdateTotalPlayScore(int n_score)
    {
        string data_code = "total_play_score";
        _SqlSavedata_info.Update_value(n_score, data_code);
    }
    public int GetTotalPlayScore()
    {
        string data_code = "total_play_score";
        int val = _SqlSavedata_info.Get_value(data_code);
        return val;
    }

    // upgrade_hp datacode : up_hp
    public int Get_TotalDef()
    {
        string datacode = "up_def";
        int currLv = _SqlSavedata_upgrade.Get_upgrade_level(datacode);
        int currUp_def = (int)_SqlBalance_upgrade_info.Get_upgrade_value(datacode, currLv);

        return currUp_def;
    }
    

//================================
	public string GetDataFilePath_StartupBalanceDB()
	{
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
		string rtn = Application.persistentDataPath + "/" + _DataBaseFileName_StartupBalanceDB;
#elif UNITY_ANDROID
		string rtn =  Application.persistentDataPath + "/" + _DataBaseFileName_StartupBalanceDB;	           
#endif
		return rtn;
		
	}
	public string GetDataFilePath_StartupSaveDB()
	{
		#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
		string rtn = Application.persistentDataPath + "/" + _DataBaseFileName_StartupSaveDB;
		#elif UNITY_ANDROID
		string rtn =  Application.persistentDataPath + "/" + _DataBaseFileName_StartupSaveDB;	           
		#endif
		return rtn;
		
	}
	public string GetDataFilePath_BalanceDB()
	{
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
		string rtn = Application.persistentDataPath + "/" + _DataBaseFileName_BalanceDB;
#elif UNITY_ANDROID
		string rtn =  Application.persistentDataPath + "/" + _DataBaseFileName_BalanceDB;	           
#endif
		return rtn;
		
	}
	public string GetDataFilePath_SaveDB()
	{
		#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
		string rtn = Application.persistentDataPath + "/" + _DataBaseFileName_SaveDB;
		#elif UNITY_ANDROID
		string rtn =  Application.persistentDataPath + "/" + _DataBaseFileName_SaveDB;	           
		#endif
		return rtn;
		
	}
}
