using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceSystem : MonoBehaviour
{
    // resources is the amount of any given resource a machine has, maxResources is the machines max capacaity, lossAmount is amount lost per interval, lossRate is how often energy is lost, gainRate is how often energy is gained, starting resources is what percent of the max is started with
    [SerializeField] private int resource, maxResources, lossAmount, lossRate, gainRate, startingResources, machineFixTime;
    //broken determines wether or not the machine is functioning or requires fixing, gainingPower and LosingPower are purly logical operators
    [SerializeField] private bool broken, gainingPower, losingPower, isBroken, allowPlay;
    //contains the script for the plug
    [SerializeField] private BatteryPowerInteractions plugInfo;
    //contains the plug game object
    [SerializeField] private AudioSource soundManager;
    [SerializeField] private AudioClip lowResource, fixMachineSound;
    public GameObject plug;
    [SerializeField] private Sprite brokenSprite, regularSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;
    //the text that indicates the current amount in the UI
    [SerializeField] private Text percentageIndicator;

    private void Start()
    {
        resource = startingResources;
        updateIndicator();
        gainingPower = false;
        losingPower = true;
        isBroken = false;
        allowPlay = true;

    }
    private void Update()
    {
        //starts losing power when there is no plug
        if(!hasPlug() && losingPower || hasPlug() && broken && losingPower)
        {
            losingPower = false;
            Invoke("loseResources", lossRate);
        }
        //begins gaining power when there is a plug
        else if(!gainingPower && !broken)
        {
            gainingPower = true;
            Invoke("gainResources", gainRate);
        }
        //if the plug object is attached the plug script is initaliezed on plugInfo
        if(hasPlug())
        {
            getScriptComponent();
        }

        fixMachine();
    }

    private void updateSound(AudioClip clip)
    {
        if (allowPlay)
        {

            soundManager.clip = clip;
            StartCoroutine(playSound());
            allowPlay = false;

        }

    }

    private IEnumerator playSound()
    {
        soundManager.Play(0);
        yield return new WaitForSeconds(soundManager.clip.length);
        soundManager.Stop();
        soundManager.clip = null;
        allowPlay = true;
    }

    //updates the percentage indicator in the UI
    private void updateIndicator ()
    {
        percentageIndicator.text = resource.ToString();
    }

    //sets the state of the broken variable
    public void setBreak(bool isBroken)
    {
        broken = isBroken;
        if(broken)
        {
            spriteRenderer.sprite = brokenSprite;
            updateSound(fixMachineSound);
        }
        else
        {
            spriteRenderer.sprite = regularSprite;

        }
    }

    public bool getBroken() { return broken; }

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
        updateIndicator();
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
        losingPower = true;
        updateIndicator();
        if(resource < 5)
        {
            updateSound(lowResource);
        }
    }

    //initializes pluginfo to the plugs script
    public void getScriptComponent()
    {
        plugInfo = plug.GetComponent<BatteryPowerInteractions>();
    }

    // determines wheter the plug gameobject is attached
    public bool hasPlug()
    {
        return plug != null;
    }

    public void plugIn(GameObject battery)
    {
        plug = battery;
    }

    public void removePlug()
    {
        plug = null;
        plugInfo = null;

    }

    public int getResources()
    {
        return resource;
    }

    private void setMachineFix()
    {
        setBreak(false);
    }

    private void fixMachine()
    {
        if (broken && Input.GetMouseButtonDown(0) && this.GetComponent<Collider2D>().bounds.Contains(MouseActions.mousePos))
        {
            isBroken = true;
            Invoke("setMachineFix", machineFixTime);
        }
    }
}
