using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSystem : MonoBehaviour
{
    // resources is the amount of any given resource a machine has, maxResources is the machines max capacaity, lossAmount is amount lost per interval, lossRate is how often energy is lost, gainRate is how often energy is gained, starting resources is what percent of the max is started with
    [SerializeField] private float resource, maxResources, lossAmount, lossRate, gainRate, startingResources;
    //broken determines wether or not the machine is functioning or requires fixing, gainingPower and LosingPower are purly logical operators
    [SerializeField] private bool broken, gainingPower, losingPower;
    //contains the script for the plug
    [SerializeField] private BatteryPowerInteractions plugInfo;
    //contains the plug game object
    [SerializeField] public GameObject plug;

    private void Start()
    {
        resource = maxResources * startingResources;
        gainingPower = false;
        losingPower = false;
    }
    private void Update()
    {
        //starts losing power when there is no plug
        if(!hasPlug() && !losingPower)
        {
            losingPower = true;
            Invoke("loseResources", lossRate);
        }
        //begins gaining power when there is a plug
        else if(!gainingPower)
        {
            gainingPower = true;
            Invoke("gainResources", gainRate);
        }
        //if the plug object is attached the plug script is initaliezed on plugInfo
        if(hasPlug())
        {
            plugIn();
        }
    }

    //sets the state of the broken variable
    public void setBreak(bool isBroken)
    {
        broken = isBroken;
    }

    //increases the amount of resource a machine has and keeps it from going above max
    public void gainResources()
    {
        if (hasPlug())
        {
            if (resource < maxResources)
            {
                resource += plugInfo.drainPower();
            }
            else
            {
                resource = maxResources;
            }
        }
        gainingPower = false;
    }

    //decreases the amount of resource a machine has and keeps it from going below 0
    public void loseResources()
    {
        if (resource > 0)
        {
            resource -= lossAmount;
        }
        else
        {
            resource = 0;
        }
        losingPower = false;
    }

    //initializes pluginfo to the plugs script
    public void plugIn()
    {
        plugInfo = plug.GetComponent<BatteryPowerInteractions>();
    }

    // determines wheter the plug gameobject is attached
    public bool hasPlug()
    {
        return plug != null;
    }

    public float getResources()
    {
        return resource;
    }
}
