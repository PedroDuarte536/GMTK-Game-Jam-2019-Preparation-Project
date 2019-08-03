using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class handles the ship as it interacts with the game
public class Ship : MonoBehaviour
{
    [SerializeField] private float shipHealth, lossIncrement;
    [SerializeField] private GameObject oxygenSystem, batterySystem, computerSystem, shieldSystem, engineSystem;
    private ResourceSystem oxygenInfo, batteryInfo, computerInfo, shieldInfo, engineInfo;
    //These variables will determine if the ship has the power or not
    public bool oxygenOn, engineOn, computerOn, shieldOn;




    // Start is called before the first frame update
    void Start()
    {
        oxygenInfo = oxygenSystem.GetComponent<ResourceSystem>();
        batteryInfo = batterySystem.GetComponent<ResourceSystem>();
        computerInfo = computerSystem.GetComponent<ResourceSystem>();
        shieldInfo = shieldSystem.GetComponent<ResourceSystem>();
        engineInfo = engineSystem.GetComponent<ResourceSystem>();
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






}