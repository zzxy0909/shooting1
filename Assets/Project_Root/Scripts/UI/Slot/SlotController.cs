using UnityEngine;
using System.Collections;

public class SlotController : MonoBehaviour {
    public int _SloitNo = 0;
	// Use this for initialization
	void Start () {
	        
    }
    public void LoadUnitData()
    {
        if (_SloitNo > 0)
        {
            StartCoroutine(IE_StartData());
        }
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
                else
                {
                    Debug.Log("~~~~~~~~~~~~~~~~ DataManager.Instance._SetupDataManager == null !");
                    yield return new WaitForSeconds(1f);
                }
            }
            yield return new WaitForFixedUpdate();
        }

        ST_S_unit_invenRec rec = DataManager.Instance._SqlSavedata_unit_inven.Get_All_From_slot_no(_SloitNo);
        GameObject prefab = GameWorld.Instance._UnitList.GetPrefab_Unit(rec.unit_code);
        if (prefab)
        {
            GameObject childObj = (GameObject)Instantiate(prefab);
            childObj.transform.parent = this.transform;
            childObj.transform.localPosition = new Vector3(0f, 0f, 0f);
            childObj.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
