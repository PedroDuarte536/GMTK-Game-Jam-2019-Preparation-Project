using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomBreacking : MonoBehaviour
{
    [SerializeField] private GameObject[] systems;
    [SerializeField] private float breakChance, timeBeforeStartBreaking, BreakChanceInterval;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("breakSystem", timeBeforeStartBreaking, BreakChanceInterval);
    } 

    private void breakSystem()
    {
        GameObject selectedSystem = systems[Random.Range(0, systems.Length)];
        if (Random.value < breakChance && !selectedSystem.GetComponent<ResourceSystem>().getBroken())
        {
            selectedSystem.GetComponent<ResourceSystem>().setBreak(true);
            print("broken");

        }
    }
}
