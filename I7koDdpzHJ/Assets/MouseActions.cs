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
 
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     This will get the object that the mouse is holding, in this 
     case it should be the plug that will interact with the terminals
     */
    private void getHeldObject()
    {
        
    }
    
    /*
     This function will get the position of the mouse on the screen
     */
    private void getMousePosition()
    {
     mousePos = Input.mousePosition;
     
     Debug.Log("Mouse Position at this spot is: " + mousePos);

    }

    /*
     This function will determine whether or not the battery is being
    held by the mouse or if it is somewhere on screen
    */
    private void heldBattery()
    {
        
    }

}
