using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuGameController : MonoBehaviour {

    public GameObject infoCanvas;
    public GameObject scoreCanvas;
    public GameObject canvas;
    public Text record;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Info()
    {
        canvas.SetActive(false);
        infoCanvas.SetActive(true);
    }

    public void BackToMenu(GameObject from)
    {
        from.SetActive(false);
        canvas.SetActive(true);
    }

    public void HighScore()
    {
        record.text = UserData.record.ToString();
        canvas.SetActive(false);
        scoreCanvas.SetActive(true);
    }
}