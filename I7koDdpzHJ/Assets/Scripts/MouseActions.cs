using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class MouseActions : MonoBehaviour
{

    //this boolean will tell the system if the mouse is holding an object or not
    [SerializeField] private bool holdB;
    public bool mouseRealeased;
    //This variable should be the position of the mouse throughout the game
    [SerializeField] private Vector3 mousePos; 
    //This variable should be the position of the plug throughout the game
    [SerializeField] private Vector3 batteryPos;
    [SerializeField] private GameObject cursor, battery;
 
 
    // Start is called before the first frame update
    void Start()
    {
        //getMousePosition();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        getMousePosition();
        getHeldObject();
        heldBattery();
        setPointer();
        if(Input.GetMouseButtonDown(0))
        {
            mouseRealeased = false;
        }
    }

    private void setPointer()
    {
        cursor.GetComponent<Transform>().position = mousePos;
    }

    /*
     This will get the object that the mouse is holding, in this 
     case it should be the plug that will interact with the terminals
     */
    private void getHeldObject()
    {

        batteryPos = battery.GetComponent<Transform>().position;
    }
    
    /*
     This function will get the position of the mouse on the screen
     */
    private void getMousePosition()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 0);
    }

    /*
     This function will determine whether or not the battery is being
    held by the mouse or if it is somewhere on screen
    */
    private void heldBattery()
    {
        if (holdB || Input.GetMouseButton(0) && this.GetComponent<CircleCollider2D>().bounds.Contains(new Vector3(mousePos.x, mousePos.y, 0)))
        {
            mouseRealeased = false;
            holdB = true;
            GetComponent<Rigidbody2D>().position = mousePos;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        if(Input.GetMouseButtonUp(0))
        {
            mouseRealeased = true;
            holdB = false;
        }
    }

}