#define ENCRYPTION

using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Threading;
using System.Diagnostics;

public struct ST_B_pzLevelRec
{
    public int idx;
    public int level_no;
    public int move_type;
    public int board_type;
    public int block_count;
    public int puzzle_count;
    public int target_max_num;
    public int target_score;
    public int target_max_count;
    public int moves_count;
    public float limit_time;
}

public class SqlBalance_pzlevel {

	private SQLiteDB _db = null;

    private string _querySelect_pzlevel = "SELECT * FROM balance_pzlevel where level_no = {0} ;";
    private string _queryUpdate_pzlevel_score = "Update balance_pzlevel set target_score = ? where level_no = {0} ;";

    public SqlBalance_pzlevel()
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
    public void Update_score(int a_level_no, int a_score)
    {
        if (_db == null)
        {
            _db = new SQLiteDB();
        }
        try
        {
            _db.Open(GetFileName_DB());

            SQLiteQuery qr;
            string strsql = string.Format(_queryUpdate_pzlevel_score, a_level_no); //

            qr = new SQLiteQuery(_db, strsql);
            qr.Bind(a_score);
            qr.Step();
            qr.Release();
            _db.Close();

        }
        catch (Exception e)
        {
            if (_db != null)
            {
                _db.Close();
                _db = null;
            }
            UnityEngine.Debug.LogError(e.ToString());
        }

        return;
    }

    public ST_B_pzLevelRec Get_levelData(int a_level_no)
    {
        if (_db == null)
        {
            _db = new SQLiteDB();
        }
        string filename = GetFileName_DB();
        ST_B_pzLevelRec rec = new ST_B_pzLevelRec();
        try
        {
            _db.Open(filename);
            SQLiteQuery qr;
            string strsql = string.Format(_querySelect_pzlevel, a_level_no); // _querySelect_exp
            qr = new SQLiteQuery(_db, strsql);
            while (qr.Step())
            {
                rec.idx = qr.GetInteger("idx");
                rec.level_no = qr.GetInteger("level_no");
                rec.move_type = qr.GetInteger("move_type");
                rec.board_type = qr.GetInteger("board_type");
                rec.block_count = qr.GetInteger("block_count");
                rec.puzzle_count = qr.GetInteger("puzzle_count");
                rec.target_max_num = qr.GetInteger("target_max_num");
                rec.target_score = qr.GetInteger("target_score");
                rec.target_max_count = qr.GetInteger("target_max_count");
                rec.moves_count = qr.GetInteger("moves_count");
                rec.limit_time = (float)qr.GetInteger("limit_time"); ;
                
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
