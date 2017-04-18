using UnityEngine;
using System.Collections;

public class SlotManager : MonoBehaviour {
    public SlotController[] _arrSlotController;
    public UISlot[] _arrUISlot;
    public int _SelectIx = 0;
    public UISprite _UISelect;

	// Use this for initialization
	void Start () {
        GameWorld.Instance._SlotManager = this;

        Startup();        
	}
    void Startup()
    {
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
                else
                {
                    Debug.Log("~~~~~~~~~~~~~~~~ DataManager.Instance._SetupDataManager == null !");
                    yield return new WaitForSeconds(1f);
                }
            }
            yield return new WaitForFixedUpdate();
        }

        while (true)
        {
            Camera guiCam = UICamera.currentCamera;
            if (guiCam != null)
            {
                break;
            }
            yield return new WaitForSeconds(0.5f);
        }

        for (int i = 0; i < _arrSlotController.Length; i++)
        {
            _arrUISlot[i]._SlotNo = _arrSlotController[i]._SloitNo;
            FollowNgui followObj = _arrUISlot[i].GetComponent<FollowNgui>();
            if (followObj == null)
            {
                Debug.LogError("~~~~~~~~~~~~~~~ followObj == null");
            }
            else
            {
                followObj.StopFollow();
                followObj._FromObj = _arrSlotController[i].transform; 
                Vector2 pos_fromObj = DataManager.Instance.Get_pos_slot(_arrSlotController[i]._SloitNo);
                if (pos_fromObj != Vector2.zero)
                {
                    _arrSlotController[i].transform.localPosition = pos_fromObj;
                    followObj.SetPos_FromObj();
                    yield return new WaitForFixedUpdate();
                }

                followObj.PlayFollow();
            }
            

            _arrSlotController[i].LoadUnitData();
            
        }

        SelectSlotNo(1);
    }

    public void SaveSlotPos()
    {
        for (int i = 0; i < _arrSlotController.Length; i++)
        {
            if (_arrSlotController[i]._SloitNo > 0)
            {
                DataManager.Instance.Update_pos_slot(_arrSlotController[i]._SloitNo,
                    _arrSlotController[i].transform.localPosition.x,
                    _arrSlotController[i].transform.localPosition.y
                    );
            }
        }
    }

    public void SelectSlotNo(int n)
    {
        if (n > 0 && _arrUISlot.Length >= n)
        {
            _SelectIx = n - 1;
            _UISelect.transform.parent = _arrUISlot[_SelectIx].transform;
            _UISelect.transform.localPosition = Vector3.zero;
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
