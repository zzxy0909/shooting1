using UnityEngine;
using System.Collections;

public class Display_HP_Lv : MonoBehaviour {
    public GameObject _BaseObject;
    public UnitInfo _UnitInfo;
    public Transform _posBase;
    public GameObject _pfHPBar;
    public GameObject _pfLb_Lv;
    public UISlider _HPBar;
    public UILabel _Lb_Lv;
    public Vector3 targetOffset;
    public Vector3 _lb_LvOffset;
	
	// Use this for initialization
	void Start () {
        _posBase = GamePlayManager.Instance._UI_Center;

        GameObject obj = Instantiate(_pfHPBar) as GameObject;
        obj.transform.parent = _posBase;
        obj.transform.localScale = new Vector3(1f, 1f, 1f);
        obj.transform.localPosition = new Vector3(0f, 0f, 0f);

        _HPBar = obj.GetComponent<UISlider>();

        if (_pfLb_Lv)
        {
            GameObject obj1 = Instantiate(_pfLb_Lv) as GameObject;
            obj1.transform.parent = _posBase;
            obj1.transform.localScale = new Vector3(1f, 1f, 1f);
            obj1.transform.localPosition = new Vector3(0f, 0f, 0f);

            _Lb_Lv = obj1.GetComponent<UILabel>();
        }
    }

    void OnDestroy()
    {
        if (_HPBar)
        {
            Destroy(_HPBar.gameObject);
        }
        if (_Lb_Lv)
        {
            Destroy(_Lb_Lv.gameObject);
        }
    }
    	
	public void DisplayHP()
	{
	}
	
	// Update is called once per frame
//	void Update () {
		
	
//	}
	Camera _AICam = null;
	void LateUpdate()
	{
					
		{
			if(_UnitInfo == null)
			{
                if (_BaseObject == null)
                {
                    Enemy en = this.transform.parent.GetComponent<Enemy>();
                    if (en)
                    {
                        _UnitInfo = en._UnitInfo;
                    }
                    else
                    {
                        wt_PlayerController cont = this.transform.parent.GetComponent<wt_PlayerController>();
                        if (cont)
                        {
                            _UnitInfo = cont._UnitInfo;
                        }
                    }
                }
                else
                {
                    Enemy en = _BaseObject.GetComponent<Enemy>();
                    if (en)
                    {
                        _UnitInfo = en._UnitInfo;
                    }
                    else
                    {
                        wt_PlayerController cont = _BaseObject.GetComponent<wt_PlayerController>();
                        if (cont)
                        {
                            _UnitInfo = cont._UnitInfo;
                        }
                    }
                }
			}else{
                if (_UnitInfo._DeathFlag == false)
                {
                    if (_HPBar.gameObject.activeInHierarchy == false)
                    {
                        GameWorld.SetActiveRecursively(_HPBar.gameObject, true);
                    }

                    this.transform.rotation = Quaternion.identity;

                    //월드좌표의 카메라객체입니다.
                    if (_AICam == null)
                    {
                        _AICam = NGUITools.FindCameraForLayer(this.gameObject.layer);
                        if (_AICam == null)
                            return;
                    }

                    //GUI객체의 카메라 객체입니다.
                    Camera guiCam = UICamera.currentCamera;
                    if (guiCam == null)
                        return;

                    //타겟의 포지션을 월드좌표에서 ViewPort좌표로 변환하고 다시 ViewPort좌표를 NGUI월드좌표로 변환합니다.
                    Vector3 targetPos = _UnitInfo.transform.position + targetOffset;
                    Vector3 pos = guiCam.ViewportToWorldPoint(_AICam.WorldToViewportPoint(targetPos));
                    //Z는 0으로...
                    pos.z = 0f;

                    _HPBar.transform.position = pos;
                    _HPBar.sliderValue = (float)_UnitInfo._HP / (float)_UnitInfo._HP_Default;

                    if (_Lb_Lv)
                    {
                        _Lb_Lv.transform.position = pos + _lb_LvOffset;
                        _Lb_Lv.text = "Lv"+_UnitInfo._Level.ToString();
                    }

                    return;
                }
                else
                {
                    if (_HPBar.gameObject.activeInHierarchy == true)
                    {
                        GameWorld.SetActiveRecursively(_HPBar.gameObject, false);
                    }
                    if (_Lb_Lv)
                    {
                        if (_Lb_Lv.gameObject.activeInHierarchy == true)
                        {
                            GameWorld.SetActiveRecursively(_Lb_Lv.gameObject, false);
                        }
                    }
                }
			}
			
			
		}
	}
}
