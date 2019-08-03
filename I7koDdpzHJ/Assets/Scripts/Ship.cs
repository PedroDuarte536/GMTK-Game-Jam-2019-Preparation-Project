using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class handles the ship as it interacts with the game
public class Ship : MonoBehaviour
{
    [SerializeField] private float shipHealth;

    /*
     * These variables will determine if the ship has the power or not
     */
    public bool oxygenVar;
    public bool engineVar;
    public bool computerVar;
    public bool shieldVar;




    // Start is called before the first frame update
    void Start()
    {
        shipHealth = 10;
        showHealth();
    }

    // Update is called once per frame
    void Update()
    {
        systemsPowered();
        showHealth();
    }

    /*
     This makes sure that all systems are functional, if not the ship's health will decrease
     */
    private void systemsPowered()
    {
        //if the engine power fails
        if (engineVar == true)
        {
            loseHealth();
        }

        //if the computer power fails
        if (computerVar == true)
        {
            loseHealth();
        }

        //if the oxygen power fails
        if (oxygenVar == true)
        {
            loseHealth();
        }

        //if the shield power fails
        if (shieldVar == true)
        {
            loseHealth();
        }

    }

    /*
     This function is called whenever the ship loses health
     */
    public void loseHealth()
    {
        shipHealth = shipHealth - 1;
    }

    /*
     This function will display the ships health and with update as the ship takes damage
     */
    public void showHealth()
    {





    }






}