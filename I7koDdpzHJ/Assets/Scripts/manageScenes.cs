using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class manageScenes : MonoBehaviour
{
    private void Update()
    {
        if(checkMouseClickPlay())
        {
            SceneManager.LoadScene(0);
        }
    }

    private bool checkMouseClickPlay()
    {
        if(Input.GetMouseButtonDown(0) && this.GetComponent<Collider2D>().bounds.Contains(new Vector2 (Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y)))
        {
            return true;
        }
        return false;
    }
}
