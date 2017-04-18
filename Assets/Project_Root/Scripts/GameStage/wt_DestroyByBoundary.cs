using UnityEngine;
using System.Collections;

public enum E_BoundaryType
{
    normal,
    top,
    left,
    right,
    bottom,
}

public class wt_DestroyByBoundary : MonoBehaviour
{
    public E_BoundaryType _BoundaryType = E_BoundaryType.normal;
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_BoundaryType == E_BoundaryType.right)
        {
            if (other.tag == "P_AttackRange"
                || other.tag == "Bolt"
                || other.tag == "Bolt_enemy"
                )
            {
                Destroy(other.gameObject);
            }
        }
        else
        {
            if (other.tag == "Enemy")
            {
                wt_ContactObject contact = other.GetComponent<wt_ContactObject>();
                if (contact)
                {
                    Destroy(contact._RootObject);
                }
            }
            else
            {
                Destroy(other.gameObject);
            }
        }
    }
}