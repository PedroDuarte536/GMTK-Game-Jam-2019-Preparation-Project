using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class manageScenes : MonoBehaviour
{

    public int connectToWhichScene;
    public GameObject ship;
    private void Update()
    {
        if(checkMouseClickPlay())
        {
            SceneManager.LoadScene(connectToWhichScene);
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

    private void checkEndGame()
    {
        if(ship != null && ship.GetComponent<Ship>().shipHealth == 0)
        {
            SceneManager.LoadScene(3);
        }
    }
}
