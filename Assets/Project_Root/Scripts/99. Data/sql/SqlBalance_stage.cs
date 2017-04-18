#define ENCRYPTION

using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Threading;
using System.Diagnostics;

public struct ST_B_stageRec{
	public int idx;
	public int stage_no;
	public int round_no;
	public int boss_bonus_normal;
	public int boss_bonus_special;
	public float boss_fail_ratio;
}

public class SqlBalance_stage {

	private SQLiteDB _db = null;

	private string _querySelect_all_stage_round = "SELECT * FROM balance_stage where stage_no = {0} and round_no = {1} ;";

    public SqlBalance_stage()
    {
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}
	}
	
	string GetFileName_DB()
	{
		return DataManager.Instance.GetDataFilePath_BalanceDB();
		
	}

    public ST_B_stageRec Get_stageData(int a_stage_no, int a_round_no)
    {
        if (_db == null)
        {
            _db = new SQLiteDB();
        }
        string filename = GetFileName_DB();
        ST_B_stageRec rec = new ST_B_stageRec();
        try
        {
            _db.Open(filename);
            SQLiteQuery qr;
            string strsql = string.Format(_querySelect_all_stage_round, a_stage_no, a_round_no); // _querySelect_exp
            qr = new SQLiteQuery(_db, strsql);
            while (qr.Step())
            {
                rec.idx = qr.GetInteger("idx");
                rec.stage_no = qr.GetInteger("stage_no");
                rec.round_no = qr.GetInteger("round_no");
                rec.boss_bonus_normal = qr.GetInteger("boss_bonus_normal");
                rec.boss_bonus_special = qr.GetInteger("boss_bonus_special");
                rec.boss_fail_ratio = (float)qr.GetDouble("boss_fail_ratio");

             }
            qr.Release();
            _db.Close();

        }
        catch (Exception e)
        {
            if (_db != null)
            {
                _db.Close();
            }
            UnityEngine.Debug.LogError(e.ToString());
        }

        return rec;
    }
	

}
