using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class MouseActions : MonoBehaviour
{

    //this boolean will tell the system if the mouse is holding an object or not
    [SerializeField] private Boolean holdB;
    //This variable should be the position of the mouse throughout the game
    [SerializeField] private Vector3 mousePos; 
    //This variable should be the position of the plug throughout the game
    [SerializeField] private Vector3 batteryPos; 
 
 
    // Start is called before the first frame update
    void Start()
    {
        //getMousePosition();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        getMousePosition();
        getHeldObject();
        heldBattery();

    }

    /*
     This will get the object that the mouse is holding, in this 
     case it should be the plug that will interact with the terminals
     */
    private void getHeldObject()
    {

        batteryPos = GameObject.Find("Plug").transform.position;
    }
    
    /*
     This function will get the position of the mouse on the screen
     */
    private void getMousePosition()
    {
        mousePos= Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    /*
     This function will determine whether or not the battery is being
    held by the mouse or if it is somewhere on screen
    */
    private void heldBattery()
    {
        if (holdB || (Input.GetKey(KeyCode.Mouse0) && this.GetComponent<CircleCollider2D>().bounds.Contains(new Vector3(mousePos.x, mousePos.y, 0))))
        {
            holdB = true;
            this.GetComponent<Rigidbody2D>().position = new Vector3(mousePos.x,mousePos.y,0);
        }
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            holdB = false;
        }
        
    }

}