using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class wt_ContactObject : MonoBehaviour
{
    public GameObject _RootObject;
	public GameObject explosion;
	public int scoreValue;

	void Start ()
	{
		
	}

    // delay method coroutine
    IEnumerator DelayAction(float dTime, System.Action callback)
    {
        yield return new WaitForSeconds(dTime);
        callback();
    }

    List<GameObject> _lstTriggerEnter_Obj = new List<GameObject>();
	void OnTriggerEnter2D (Collider2D other)
	{
        if (other.tag == "Boundary")
        {
            wt_DestroyByBoundary boundary = other.GetComponent<wt_DestroyByBoundary>();
            if (boundary != null
                && boundary._BoundaryType == E_BoundaryType.right )
            {

            }
            else
            {
                if (this.tag != "Player")
                {
                    Destroy(_RootObject);
                }
            }
        }

		if ( (other.tag == "MeleeCheck" && this.tag == "Enemy")
            || (other.tag == "MeleeCheck" && this.tag == "Player")
            || _lstTriggerEnter_Obj.Contains(other.gameObject) == true
            )
		{
			return;
		}

        Collider2D tmp = other;
        _lstTriggerEnter_Obj.Add(tmp.gameObject);
        StartCoroutine(DelayAction(1f, () =>
        {
            if (tmp != null)
            {
                _lstTriggerEnter_Obj.Remove(tmp.gameObject);
            }
            else
            {
                _lstTriggerEnter_Obj.Remove(null);
            }
        }));

        switch (this.tag)
        {
            case "Enemy":
                OnTriggerEnter_Enemy(other);
                break;
            case "Asteroid":
                OnTriggerEnter_Enemy(other);
                break;
            case "Player":
                OnTriggerEnter_Hero(other);
                break;
        }

	}
    void OnTriggerEnter_Hero(Collider2D other)
    {
        if (other.tag == "P_AttackRange"
            || other.tag == "Bolt"
        )
        {
            return;
        }
//        Debug.Log("~~~~~~~~~~~~~~~~~~OnTriggerEnter_Hero : " + other.tag);
        Instantiate(explosion, transform.position, transform.rotation);
        if (other.tag == "Asteroid"
            || other.tag == "Bolt_enemy"
            )
        {
            wt_PlayerController hero = _RootObject.GetComponent<wt_PlayerController>();
            DamageTrigger damage = other.GetComponent<DamageTrigger>();
            if (damage)
            {
                hero.Hurt(damage._DamageValue);
            }

            if (other.tag == "Bolt_enemy")
            {
                Destroy(other.gameObject);
            }
        }
    }

    void OnTriggerEnter_Enemy(Collider2D other)
    {
        if (other.tag == "Asteroid"
            || other.tag == "Bolt_enemy"
            )
        {
            return;
        }

        if (explosion != null)
        {
//            Debug.Log("~~~~~~~~~~~~~~~~~~OnTriggerEnter_Enemy : " + other.tag);
            Instantiate(explosion, transform.position, transform.rotation);
        }
        if (other.tag == "P_AttackRange"
            || other.tag == "Bolt")
        {
            
            Enemy en = _RootObject.GetComponent<Enemy>();
            if (en)
            {
                DamageTrigger damage = other.GetComponent<DamageTrigger>();
                if (damage)
                {
                    en.Hurt(damage._DamageValue);
                }

                if (other.tag == "Bolt")
                {
                    Destroy(other.gameObject);
                }
                else if (other.tag == "P_AttackRange")
                {
                    float dur = 0.5f;
                    Vector3 pos = _RootObject.transform.position + new Vector3(0.5f, 0f,0f);
                    TweenPosition.Begin(_RootObject, dur, pos).method = UITweener.Method.EaseInOut;
                    if (en._EnemyType == E_EnemyType.normal)
                    {
                        en._AniController.PlayHit(dur);
                    }
                }
            }
        }

        //		Destroy (_RootObject ); // gameObject);
    }
}