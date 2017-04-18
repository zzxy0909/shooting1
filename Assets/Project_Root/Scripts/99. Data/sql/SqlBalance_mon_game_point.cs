#define ENCRYPTION

using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Threading;
using System.Diagnostics;


public class SqlBalance_mon_game_point {

	private SQLiteDB _db = null;
	string _db_filename;

    private string _querySelect_attack = "SELECT attack FROM balance_mon_game_point where model_name = '{0}' ;";
    private string _querySelect_hp = "SELECT hp FROM balance_mon_game_point where model_name = '{0}' ;";
    private string _querySelect_point_min = "SELECT point_min FROM balance_mon_game_point where model_name = '{0}' ;";
    private string _querySelect_point_max = "SELECT point_max FROM balance_mon_game_point where model_name = '{0}' ;";
	private string _querySelect_gold_min = "SELECT gold_min FROM balance_mon_game_point where model_name = '{0}' ;";
	private string _querySelect_gold_max = "SELECT gold_max FROM balance_mon_game_point where model_name = '{0}' ;";
	
	public SqlBalance_mon_game_point()
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
    public int Get_attack(string a_model_name)
    {
        _db_filename = GetFileName_DB();
        if (_db == null)
        {
            _db = new SQLiteDB();
        }
        int rtn = 0;//---
        try
        {
            _db.Open(_db_filename);

            SQLiteQuery qr;
            string strsql = string.Format(_querySelect_attack, a_model_name); //--- _querySelect_exp
            qr = new SQLiteQuery(_db, strsql);
            while (qr.Step())
            {
                rtn = qr.GetInteger("attack"); //---
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
        return rtn; //---
    }
    public int Get_hp(string a_model_name)
    {
        _db_filename = GetFileName_DB();
        if (_db == null)
        {
            _db = new SQLiteDB();
        }
        int rtn = 0;//---
        try
        {
            _db.Open(_db_filename);

            SQLiteQuery qr;
            string strsql = string.Format(_querySelect_hp, a_model_name); //--- _querySelect_exp
            qr = new SQLiteQuery(_db, strsql);
            while (qr.Step())
            {
                rtn = qr.GetInteger("hp"); //---
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
        return rtn; //---
    }
	public int Get_point_min(string a_model_name)
	{
		_db_filename = GetFileName_DB();
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}		
		int rtn = 0;//---
		try{
			_db.Open(_db_filename);
			
			SQLiteQuery qr;
			string strsql = string.Format(_querySelect_point_min, a_model_name); //--- _querySelect_exp
			qr = new SQLiteQuery(_db, strsql); 
			while( qr.Step() )
			{
				rtn = qr.GetInteger("point_min"); //---
			}
			qr.Release();                                     
			_db.Close();			
		} catch (Exception e){
			if(_db != null)
			{
				_db.Close();
			}			
			UnityEngine.Debug.LogError( e.ToString() );			
		}		
		return rtn; //---
	}
	public int Get_point_max(string a_model_name)
	{
		_db_filename = GetFileName_DB();
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}		
		int rtn = 0;//---
		try{
			_db.Open(_db_filename);
			
			SQLiteQuery qr;
			string strsql = string.Format(_querySelect_point_max, a_model_name); //--- _querySelect_exp
			qr = new SQLiteQuery(_db, strsql); 
			while( qr.Step() )
			{
				rtn = qr.GetInteger("point_max"); //---
			}
			qr.Release();                                     
			_db.Close();			
		} catch (Exception e){
			if(_db != null)
			{
				_db.Close();
			}			
			UnityEngine.Debug.LogError( e.ToString() );			
		}		
		return rtn; //---
	}
	public int Get_gold_min(string a_model_name)
	{
		_db_filename = GetFileName_DB();
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}		
		int rtn = 0;//---
		try{
			_db.Open(_db_filename);
			
			SQLiteQuery qr;
			string strsql = string.Format(_querySelect_gold_min, a_model_name); //--- _querySelect_exp
			qr = new SQLiteQuery(_db, strsql); 
			while( qr.Step() )
			{
				rtn = qr.GetInteger("gold_min"); //---
			}
			qr.Release();                                     
			_db.Close();			
		} catch (Exception e){
			if(_db != null)
			{
				_db.Close();
			}			
			UnityEngine.Debug.LogError( e.ToString() );			
		}		
		return rtn; //---
	}
	public int Get_gold_max(string a_model_name)
	{
		_db_filename = GetFileName_DB();
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}		
		int rtn = 0;//---
		try{
			_db.Open(_db_filename);
			
			SQLiteQuery qr;
			string strsql = string.Format(_querySelect_gold_max, a_model_name); //--- _querySelect_exp
			qr = new SQLiteQuery(_db, strsql); 
			while( qr.Step() )
			{
				rtn = qr.GetInteger("gold_max"); //---
			}
			qr.Release();                                     
			_db.Close();			
		} catch (Exception e){
			if(_db != null)
			{
				_db.Close();
			}			
			UnityEngine.Debug.LogError( e.ToString() );			
		}		
		return rtn; //---
	}
	
}
