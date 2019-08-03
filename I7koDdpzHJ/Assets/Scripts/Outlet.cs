using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outlet : MonoBehaviour
{
    public bool enteredOutletSpace;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Plug"))
        {
            enteredOutletSpace = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Plug"))
        {
            enteredOutletSpace = false;
        }
    }
}
