using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This class handles the ship as it interacts with the game
public class Ship : MonoBehaviour
{
    [SerializeField] private float shipHealth, lossIncrement;
    [SerializeField] private GameObject oxygenSystem, batterySystem, computerSystem, shieldSystem, engineSystem;
    private ResourceSystem oxygenInfo, batteryInfo, computerInfo, shieldInfo, engineInfo;
    //These variables will determine if the ship has the power or not
    public bool oxygenOn, engineOn, computerOn, shieldOn;
    [SerializeField] private (double, double) startUp;
    [SerializeField] private (double, double) endUp;
    [SerializeField] private float moveTime, timeToMove;

    public GameObject shipMovement;
    public GameObject planet;
    
    //time variables for the ship
    public float minimumTime = 100;
    public float maximumTime = 1f;
    public Transform target;


    // Start is called before the first frame update
    void Start()
    {
        oxygenInfo = oxygenSystem.GetComponent<ResourceSystem>();
        batteryInfo = batterySystem.GetComponent<ResourceSystem>();
        computerInfo = computerSystem.GetComponent<ResourceSystem>();
        shieldInfo = shieldSystem.GetComponent<ResourceSystem>();
        engineInfo = engineSystem.GetComponent<ResourceSystem>();
        showHealth();
        startUp = (-5.374, 2.95);
        endUp = (5.0, 2.95);
        moveTime = 1;
        minimumTime = (float) 0.05;
        timeToMove = 1;
        shipMovement = GameObject.Find("TrackerShip");
        planet = GameObject.Find("planet");
        /*minimum = transform.position.x;
        maximum = transform.position.x + 3;*/
        
        

    }

    // Update is called once per frame
    void Update()
    {
        systemsPowered();
        showHealth();
        moveShip();
    }

    /*
     This makes sure that all systems are functional, if not the ship's health will decrease
     */
    private void systemsPowered()
    {
        //if the engine power fails
        if (oxygenInfo.getResources() == 0.0f)
        {
            loseHealth();
        }

        //if the computer power fails
        if (shieldInfo.getResources() == 0.0f)
        {
            loseHealth();
        }

        //if the oxygen power fails
        if (computerInfo.getResources() == 0.0f)
        {
            oxygenOn = false;
            //Invoke("loseHealth");
        }

        //if the shield power fails
        if (engineInfo.getResources() == 0.0f)
        {
            loseHealth();
        }

    }

    /*
     This function is called whenever the ship loses health
     */
    public void loseHealth()
    {
        shipHealth -= lossIncrement;
    }

    /*
     This function will display the ships health and with update as the ship takes damage
     */
    public void showHealth()
    {

    }

    /*
     This function will move the ship on top of the screen
     */
    private void moveShip()
    {

        Vector3 check = shipMovement.transform.position;
        //Vector3 end = planet.transform.position;
        //Debug.Log("Position 1: " + check);
        //Debug.Log("Position 2: " + end);
        if (check.x < 5.00)
        {

            float step = minimumTime * Time.deltaTime; // calculate distance to move
            shipMovement.transform.position =
                Vector3.MoveTowards(shipMovement.transform.position, target.position, step);

            // Check if the position of the cube and sphere are approximately equal.
            if (Vector3.Distance(shipMovement.transform.position, target.position) < 0.001f)
            {
                // Swap the position of the cylinder.
                target.position *= -1.0f;
            }
        }
        else
        {
            minimumTime = 0;
            //shipMovement
            SceneManager.LoadScene(2);
        }


    }

    

    //transform.position =new Vector3(Mathf.PingPong(Time.time*2,maximum-minimum)+minimum, transform.position.y, transform.position.z);
        
       /* moveTime -= Time.deltaTime;
        if (moveTime > 0)
        {
            Vector2 distance = endUp - startUp;
            float degree_of_movement = (timeToMove - moveTime) / timeToMove;
            transform.position = new Vector2 (startUp.x + (distance.x * degree_of_movement), startUp.y + (distance.y * degree_of_movement));

        }*/
    }


