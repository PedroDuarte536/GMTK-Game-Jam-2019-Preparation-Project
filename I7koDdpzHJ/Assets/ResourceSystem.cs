using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSystem : MonoBehaviour
{
    private int resouceAmount;
    private bool isBroken;
    private bool isPlugged;
    // Start is called before the first frame update
    void Start()
    {
        resouceAmount = 100;
        isBroken = false;
        isPlugged = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void gainResources(int amount)
    {
        resouceAmount += amount;
    }

    public void loseResources(int amount)
    {
        if (resouceAmount - amount >= 0)
            resouceAmount -= amount;
        else
            resouceAmount = 0;
    }

    public void setBreak(bool broken)
    {
        isBroken = broken;
    }

    public bool hasPlug()
    {
        return isPlugged;
    }

    public bool hasResouces ()
    {
        if (resouceAmount > 0)
            return true;
        return false;
    }
}
