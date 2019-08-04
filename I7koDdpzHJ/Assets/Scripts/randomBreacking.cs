using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomBreacking : MonoBehaviour
{
    [SerializeField] private GameObject[] systems;
    private bool allowPlay;
    [SerializeField] private float breakChance, timeBeforeStartBreaking, BreakChanceInterval;
    [SerializeField] private AudioClip gasRelease, powerDown;
    [SerializeField] private AudioSource soundManager;
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
            if(Random.value < breakChance)
            {
                updateSound(gasRelease);
            }
            else
            {
                updateSound(powerDown);
            }
        }
    }

    private void updateSound(AudioClip clip)
    {
        soundManager.clip = clip;
        StartCoroutine(playSound());
        allowPlay = false;
    }

    private IEnumerator playSound()
    {
        soundManager.Play(0);
        yield return new WaitForSeconds(soundManager.clip.length);
        soundManager.Stop();
        soundManager.clip = null;
        allowPlay = true;
    }

}
