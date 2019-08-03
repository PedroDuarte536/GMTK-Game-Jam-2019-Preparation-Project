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
    void Update()
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

        batteryPos= GameObject.Find("Plug").transform.position;
        //GameObject.FindGameObjectsWithTag("aa").transform.position;
        Debug.Log("Plug Position at this spot is: " + batteryPos);
    }
    
    /*
     This function will get the position of the mouse on the screen
     */
    private void getMousePosition()
    {
    
     
        mousePos= Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("Mouse Position at this spot is: " + mousePos);
        
            //Older Code
        /* mousePos = Input.mousePosition;
        //if(Input.GetKeyDown(KeyCode.Mouse0))
        //{
            
        //}
 
        //transform.position = Vector3.MoveTowards(transform.position, mousePos, Time.deltaTime * 5);
        */
    }

    /*
     This function will determine whether or not the battery is being
    held by the mouse or if it is somewhere on screen
    */
    private void heldBattery()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            holdB = true;
            if (holdB == true)
            {
                this.gameObject.transform.localPosition = new Vector3(mousePos.x,mousePos.y,0);
                Debug.Log("WORKING!!!!!!!!!!!!");
            }
            
            
        }
        
    }

}