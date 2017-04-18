using UnityEngine;
using System.Collections;

public class wt_Mover : MonoBehaviour
{
    public bool _AutoStart = true;
    public float _StartDelay = 0f;
    public bool _isLocalMover = false;
    public float speed;
    public bool _isMulti = false;
    public float _speedX;
    public float _speedY;
    public bool _IsPlayMove = false;

    
	void Start ()
	{
        if (_isLocalMover == false)
        {
            if (_isMulti == false)
            {
                Vector2 movement = new Vector2(1, 0);
                GetComponent<Rigidbody2D>().velocity = movement * speed;
            }
            else
            {
                Vector2 movement = new Vector2(_speedX, _speedY);
                GetComponent<Rigidbody2D>().velocity = movement;
            }
        }
        else
        {

        }

        if (_AutoStart == true)
        {
            PlayMove();
        }
	}
    public void PlayMove()
    {
        Invoke("SetPlayMove", _StartDelay);
    }
    void SetPlayMove()
    {
        _IsPlayMove = true;
    }

    void Update()
    {
        if (_isLocalMover == true
            && _IsPlayMove == true)
        {
            Vector3 move_dir = new Vector3(1, 0, 0);
            transform.localPosition = transform.localPosition + ( move_dir * speed * Time.deltaTime );
        }
    }
}
