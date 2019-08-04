using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class manageScenes : MonoBehaviour
{
    private void Update()
    {
        if(!SceneManager.GetActiveScene().name.Equals(SceneManager.GetSceneByBuildIndex(1).name) && checkMouseClickPlay())
        {
            SceneManager.LoadScene(1);
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
            Cursor.visible = true;
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

    private bool checkEndGame()
    {
        return true;
    }
}
