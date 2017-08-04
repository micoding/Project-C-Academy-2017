using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public GameObject fence0Button; //ChosenFance0
    public GameObject fence1Button;
    public GameObject fence2Button;
    public GameObject fence3Button;

    public GameObject anim0Button;
    public GameObject anim1Button;
    public GameObject anim2Button;
    public GameObject anim3Button;

    public GameObject gameCanvas;
    public GameObject animalCanvas;
    public GameObject fanceCanvas;
    public GameObject pauseCanvas;
    public GameObject eatCanvas;
    public GameObject healthCanvas;
    public GameObject infoCanvas;
    public GameObject sliderCanvas;
    public GameObject gameOverCanvas;

    public Slider healthSlider0;
    public Slider healthSlider1;
    public Slider healthSlider2;
    public Slider healthSlider3;

    public Slider hungerSlider0;
    public Slider hungerSlider1;
    public Slider hungerSlider2;
    public Slider hungerSlider3;

    public GameObject mealFence0;
    public GameObject mealFence1;
    public GameObject mealFence2;
    public GameObject mealFence3;

    public GameObject healFence0;
    public GameObject healFence1;
    public GameObject healFence2;
    public GameObject healFence3;

    public Text starved;
    public Text diedSick;
    public Text diedOld;
    public Text result;
    public Text secLeft;
    public Text minLeft;

    public GameObject goodGame;
    public GameObject badGame;
    public GameObject newRecord;

    GameController gC;
    
    private void Awake()
    {
        gC = GameObject.Find("GameController").GetComponent<GameController>();
    }

    public void Fence0()
    {
        gC.StartSpawn(gC.fence0);
    }
    public void Fence1()
    {
        gC.StartSpawn(gC.fence1);
    }
    public void Fence2()
    {
        gC.StartSpawn(gC.fence2);
    }
    public void Fence3()
    {
        gC.StartSpawn(gC.fence3);
    }
    public void This0()
    {
        gC.whichOne = 0;
        animalCanvas.SetActive(false);
        fanceCanvas.SetActive(true);
    }
    public void This1()
    {
        gC.whichOne = 1;
        animalCanvas.SetActive(false);
        fanceCanvas.SetActive(true);
    }
    public void This2()
    {
        gC.whichOne = 2;
        animalCanvas.SetActive(false);
        fanceCanvas.SetActive(true);
    }
    public void This3()
    {
        gC.whichOne = 3;
        animalCanvas.SetActive(false);
        fanceCanvas.SetActive(true);
    }

    public void HealFence0()
    {
        gC.animals.First(item => item.where == gC.fence0).Heal();
    }

    public void HealFence1()
    {
        gC.animals.First(item => item.where == gC.fence1).Heal();
    }

    public void HealFence2()
    {
        gC.animals.First(item => item.where == gC.fence2).Heal();
    }

    public void HealFence3()
    {
        gC.animals.First(item => item.where == gC.fence3).Heal();
    }

    public void MealFence0()
    {
        gC.animals.First(item => item.where == gC.fence0).Meal();
    }

    public void MealFence1()
    {
        gC.animals.First(item => item.where == gC.fence1).Meal();
    }

    public void MealFence2()
    {
        gC.animals.First(item => item.where == gC.fence2).Meal();
    }

    public void MealFence3()
    {
        gC.animals.First(item => item.where == gC.fence3).Meal();
    }

    public void Pause()
    {
        Time.timeScale = 0;
        gameCanvas.SetActive(false);
        sliderCanvas.SetActive(false);
        infoCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
    }

    public void GameSwitchTo(GameObject to)
    {
        gameCanvas.SetActive(false);
        to.SetActive(true);
    }

    public void Return()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
        sliderCanvas.SetActive(true);
    }

    public void BackToGame(GameObject from)
    {
        Time.timeScale = 1;
        from.SetActive(false);
        gameCanvas.SetActive(true);
        sliderCanvas.SetActive(true);
    }

    public void GoMenu()
    {
        foreach (Turist item in GameControllerStatic.turists)
        {
            Destroy(item);
        }
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Info()
    {
        Debug.Log("info");
        pauseCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        infoCanvas.SetActive(true);
    }
    public void DeepResume()
    {
        infoCanvas.SetActive(false);
        if (gC.isGO)
        {
            gameOverCanvas.SetActive(true);
        }
        else
        {
            pauseCanvas.SetActive(true);
        }
    }

    public void Continue()
    {
        animalCanvas.SetActive(false);
        fanceCanvas.SetActive(false);
        Time.timeScale = 1;
        gameCanvas.SetActive(true);
        sliderCanvas.SetActive(true);
    }

    public void BuyAidKid()
    {
        if (UserData.money >= 5000)
        {
            UserData.money -= 5000;
            UserData.aidKit += 10;
        }
    }

    public void BuyMeals()
    {
        if (UserData.money >= 2000)
        {
            UserData.money -= 2000;
            UserData.snacks += 100;
        }
    }
}