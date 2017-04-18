using UnityEngine;
using System.Collections;

public class UnitList : MonoBehaviour {
    public GameObject[] _pfArrUnit;

    void Awake()
    {
        if (GameWorld.Instance._UnitList == null)
        {
            GameWorld.Instance._UnitList = this;
        }
        else
        {
            Destroy(this.gameObject);
            Debug.Log("~~~~~~~~~~~~~~~~~~~~~~~~ Destroy(this.gameObject); GameWorld.Instance._UnitList ");
            return;
        }
        transform.parent = null;
        // DontDestroyOnLoad(this);

        PrefabListController pfcont = GetComponent<PrefabListController>();
        if (pfcont)
        {
            _pfArrUnit = pfcont._ArrPrefabList;
        }
    }

    public GameObject GetPrefab_Unit(string strname)
    {
        foreach (GameObject obj in _pfArrUnit)
        {
            if (obj == null)
            {
                continue;
            }
            if (obj.name == strname)
            {
                return obj;
            }
        }
        return null;
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
