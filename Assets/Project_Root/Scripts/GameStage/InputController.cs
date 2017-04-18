using UnityEngine;
using System.Collections;

public enum E_InputType
{
	_None,
	_DragOnly,
	_DragClick,
}

public class InputController : MonoBehaviour
{
    public E_InputType _eInputType = E_InputType._None;
    public LayerMask _ClickObj_Layer;
    public float _ClickRange = 20f;

    float _Xpos_Old = 0f;
    float _Ypos_Old = 0f;
    float _Xpos = 0f;
    float _Ypos = 0f;
    float _XTolerance = 0.05f;
    float _YTolerance = 0.05f;
    bool _Normalize = true;

	// Use this for initialization
	void Start () {
					
	}

    bool CheckInputNGUI()
	{
        return false;

        // RaycastHit hit = new RaycastHit();
        return UICamera.Raycast(Input.mousePosition); // , out hit) ;
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetMouseButtonDown(0))
		{
			// 적이 없으면 드레그만 유효. _DragOnly
			// 적이 있으면 드레그와 클릭 모두 유효. _DragClick
			_Xpos_Old= Input.mousePosition.x;
			if (_Normalize)
					_Xpos_Old /= Screen.width;
			_Ypos_Old= Input.mousePosition.y;
			if (_Normalize)
					_Ypos_Old /= Screen.width;
			
			Camera cam = GameWorld.GetMainCamera();
			if(cam == null)
			{
				Debug.Log(" Main Camera Null !!!");
				return;
			}
			
			if(CheckInputNGUI() == true)
			{
//				Debug.Log("~~~~~~~~~~~~~~  CheckInputNGUI() == true  !!!");
				return;
			}
			
// 다운시 선택에서 업시 선택으로 변경.
			Ray ray = cam.ScreenPointToRay( Input.mousePosition );	
			RaycastHit hit;
 
			if(Physics.Raycast(ray, out hit, _ClickRange, _ClickObj_Layer ))
			{
                _eInputType = E_InputType._DragClick;				
//				GameWorld.Instance.SetSelectAI(hit.collider.gameObject);
				
			}else{
                //_eInputType = E_InputType._DragOnly;
                // 전체 텝
                _eInputType = E_InputType._DragClick;				

			}
		
		}
		if(Input.GetMouseButtonUp(0))
		{
			// _DragOnly 이면 올드 마우스 포지션 확인 후,  전후좌우 처리.
			// _DragClick 이면 올드 마우스 포지션 확인 후, 전후좌우 확인 처리. 이동이 아니면, 클릭 처리.
            if (_eInputType == E_InputType._DragOnly)
			{
				_Xpos= Input.mousePosition.x;
				if (_Normalize)
						_Xpos /= Screen.width;
				_Ypos= Input.mousePosition.y;
				if (_Normalize)
						_Ypos /= Screen.width;
				float xval = Mathf.Abs( _Xpos-_Xpos_Old );
				float yval = Mathf.Abs( _Ypos-_Ypos_Old );
				
				if(yval > _YTolerance ||  xval > _XTolerance )
				{
					if(yval >= xval 
	//					
						)
					{
						if(_Ypos > _Ypos_Old)
						{
                            Swipe_Front();
						}else{
                            Swipe_Back();
						}
					}else
					// if(yval < xval)
					{
						if(_Xpos > _Xpos_Old)
						{
                            Swipe_Right();
						}else{
                            Swipe_Left();
						}
					}
				}else {
				}
            }
            else if (_eInputType == E_InputType._DragClick)
			{
				_Xpos= Input.mousePosition.x;
				if (_Normalize)
						_Xpos /= Screen.width;
				_Ypos= Input.mousePosition.y;
				if (_Normalize)
						_Ypos /= Screen.width;
				float xval = Mathf.Abs( _Xpos-_Xpos_Old );
				float yval = Mathf.Abs( _Ypos-_Ypos_Old );
				
				if(yval > _YTolerance ||  xval > _XTolerance )
				{
					if(yval >= xval)
					{
						if(_Ypos > _Ypos_Old)
						{
                            Swipe_Front();
						}else{
                            Swipe_Back();
						}
					}else // if(yval < xval)
					{
						if(_Xpos > _Xpos_Old)
						{
                            Swipe_Right();
						}else{
                            Swipe_Left();
						}
					}
				}else{									
					// InputClick
                    InputClick();
				}
			}
            _eInputType = E_InputType._None;
		}
	}
    void InputClick()
    {
        Debug.Log("~~~~~~~~~~~~~ Click!");
    }
    void Swipe_Front()
    {

//        Debug.Log("~~~~~~~~~~~~~ Front swipe!");
    }
    void Swipe_Back()
    {
 //        Debug.Log("~~~~~~~~~~~~~ Back swipe!");
    }
    void Swipe_Left()
    {
 //        Debug.Log("~~~~~~~~~~~~~ Left swipe!");
    }
    void Swipe_Right()
    {
 //        Debug.Log("~~~~~~~~~~~~~ Right swipe!");
    }
}
