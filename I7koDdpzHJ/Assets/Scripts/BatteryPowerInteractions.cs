﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPowerInteractions : MonoBehaviour
{
    //Power is the amount of power in the battery currently, maxpower is the amount the battery can hold, powerRechargeAmount is the amount of power the battery passivly recives per interval, 
    //powerRechargeRate how often power is recived, powerTransferAmount how much power is given up to whatever it is plugged into, percentMaxStart the percent of the max that the battery starts with
    [SerializeField] private int power, maxPower, powerRechargeAmount, powerRechargeRate, powerTransferAmount, startAmount;
    //is the battery charging
    [SerializeField] private bool charging;
    
    //whatever object the battery is connected to
    public GameObject connectedTo;

    //the green display in the center
    private PercentageDisplay powerDisplay;

    private void Start()
    {
        power = startAmount;

        powerDisplay = GameObject.FindGameObjectWithTag("Power Bar").GetComponent<PercentageDisplay>();
        powerDisplay.setPercentage(power);
    }

    private void Update()
    {
        //determines when recharge
        if(connectedTo != null && connectedTo.tag.Equals("Charging Station") && !charging)
        {
            charging = true;
            Invoke("charge", powerRechargeRate);
        }
        
    }

    //adds amount to the total power
    private void changePower(int amount)
    {
        power += amount;
        powerDisplay.setPercentage(power);
    }

    //sets power to desired number
    public void setPower(int amount)
    {
        power = amount;
        powerDisplay.setPercentage(power);
    }
    
    //returns power
    public int getPower() { return power; }

    //returns what machine the plug is connected to
    public ResourceSystem getConnectedMachineSystem()
    {
        return connectedTo.GetComponent<ResourceSystem>();
    }

    //charging the battery
    public void charge()
    {
        if (power < maxPower)
        {
            power += powerRechargeAmount;
            powerDisplay.setPercentage(power);
            charging = false;
        }
        charging = false;
    }

    //returns the drained power so whatever is draining it can take it
    public int drainPower()
    {
        if(power > 0)
        {
            power -= powerTransferAmount;
            powerDisplay.setPercentage(power);
            return powerTransferAmount;
        }
        return 0;
    }

    //returns true if battery is connected to something
    public bool batteryConnected()
    {
        return connectedTo != null;
    }

}
