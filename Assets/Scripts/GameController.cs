using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using System;

public class GameController : MonoBehaviour {

    public double time;
    public double income;

    public Transform fence0;
    public Transform fence1;
    public Transform fence2;
    public Transform fence3;

    public GameObject spawnCanvas;
    public GameObject gameCanvas;

    public GameObject anim0;
    public GameObject anim1;
    public GameObject anim2;
    public GameObject anim3;
    public List<Animal> animals = new List<Animal>();

    List<Animal> allAnimals = new List<Animal>();

    public GameObject fence0Button; //ChosenFance0
    public GameObject fence1Button;
    public GameObject fence2Button;
    public GameObject fence3Button;

    public GameObject anim0Button;
    public GameObject anim1Button;
    public GameObject anim2Button;
    public GameObject anim3Button;

    public int whichOne = -1;
    public Transform where = null;

    GameObject animal;
    Animal script;
    UserData uD;

    public GameObject turistPrefab;
    public GameObject femalePrefab;
    public GameObject soldierPrefab;
    GameObject thisPrefab;


    public Transform turistSpawn;
    public double turistTimer;
    public double turistInterest;

    public int howManyAvailable;
    public GameObject animalCanvas;
    public GameObject fanceCanvas;
    bool stillLoop = true;
    public GameObject stopLoop;

    public GameObject pauseCanvas;
    public GameObject eatCanvas;
    public GameObject healthCanvas;
    public GameObject infoCanvas;
    public GameObject SliderCanvas;

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

    public bool isStart = false;
    public bool isGO = false;
    public GameObject gameOverCanvas;

    public Text starved;
    public Text diedSick;
    public Text diedOld;
    public Text result;

    public GameObject goodGame;
    public GameObject badGame;
    public GameObject newRecord;

    public int howManyStarted;

    public Transform cloudSpawn;
    GameObject cloudPrefab;
    public GameObject smallCloudPrefab;
    public GameObject bigCloudPrefab;
    float timeCloud;

    private void Awake()
    {
        GameControllerStatic.fence0Available = true;
        GameControllerStatic.fence1Available = true;
        GameControllerStatic.fence2Available = true;
        GameControllerStatic.fence3Available = true;
        GameControllerStatic.diedOld = 0;
        GameControllerStatic.starved = 0;
        GameControllerStatic.diedSick = 0;
    }

    // Use this for initialization
    void Start() {
        uD = GameObject.Find("Player").GetComponent<UserData>();
        allAnimals.Add(anim0.GetComponent<Animal>());
        allAnimals.Add(anim1.GetComponent<Animal>());
        allAnimals.Add(anim2.GetComponent<Animal>());
        allAnimals.Add(anim3.GetComponent<Animal>());

        Spawn();
    }

    // Update is called once per frame
    void Update() {
        time = Time.time;
        if (income > 0)
        {
            UserData.money += income;
            StartCoroutine(uD.Flashing());
            income = 0;
        }
        TuristInterest();
        TuristSpawn();
        IsEveryoneDead();

        timeCloud -= Time.deltaTime;
        if(timeCloud<=0)
            CloudSpawn();
    }

    public void StartSpawn()
    {
        animalCanvas.SetActive(false);
        fanceCanvas.SetActive(false);
        if (where != null)
        {
            Create();
            howManyStarted++;
            isStart = true;
        }
        else
            stillLoop = false;
        GameControllerStatic.HowManyAvailable();
        if (howManyAvailable > 0 && stillLoop)
        {
            CanAfford();
            stopLoop.SetActive(true);
            animalCanvas.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            gameCanvas.SetActive(true);
            SliderCanvas.SetActive(true);
        }

    }

    public void Spawn()
    {
        stopLoop.SetActive(false);
        Time.timeScale = 0;
        gameCanvas.SetActive(false);
        animalCanvas.SetActive(true);
        CanAfford();
    }

    public void Opction0()
    {
        where = fence0;
        StartSpawn();
    }
    public void Opction1()
    {
        where = fence1;
        StartSpawn();
    }
    public void Opction2()
    {
        where = fence2;
        StartSpawn();
    }
    public void Opction3()
    {
        where = fence3;
        StartSpawn();
    }
    public void This0()
    {
        whichOne = 0;
        animalCanvas.SetActive(false);
        fanceCanvas.SetActive(true);
    }
    public void This1()
    {
        whichOne = 1;
        animalCanvas.SetActive(false);
        fanceCanvas.SetActive(true);
    }
    public void This2()
    {
        whichOne = 2;
        animalCanvas.SetActive(false);
        fanceCanvas.SetActive(true);
    }
    public void This3()
    {
        whichOne = 3;
        animalCanvas.SetActive(false);
        fanceCanvas.SetActive(true);
    }

    public void Create()
    {
        GameObject tmp = null;
        switch (whichOne)
        {
            case 0:
                tmp = anim0;
                break;
            case 1:
                tmp = anim1;
                break;
            case 2:
                tmp = anim2;
                break;
            case 3:
                tmp = anim3;
                break;
        }
        animal = Instantiate(tmp, where);
        script = animal.GetComponent<Animal>();
        script.race = tmp.ToString();
        UserData.money -= script.price;
        script.where = where;
        animals.Add(script);

        whichOne = -1;
        where = null;
    }

    public void CanAfford()
    {
        anim0Button.SetActive(false);
        anim1Button.SetActive(false);
        anim2Button.SetActive(false);
        anim3Button.SetActive(false);

        List<Animal> CAAnim = allAnimals.FindAll(item => item.price <= UserData.money);

        foreach (Animal Anim in CAAnim)
        {
            switch (Anim.race)
            {
                case "anim0":
                    anim0Button.SetActive(true);
                    break;
                case "anim1":
                    anim1Button.SetActive(true);
                    break;
                case "anim2":
                    anim2Button.SetActive(true);
                    break;
                case "anim3":
                    anim3Button.SetActive(true);
                    break;
            }
        }
    }

    public void HealFence0()
    {
        Animal tmp = animals.First(item => item.where == fence0);
        tmp.Heal();
    }

    public void HealFence1()
    {
        Animal tmp = animals.First(item => item.where == fence1);
        tmp.Heal();
    }

    public void HealFence2()
    {
        Animal tmp = animals.First(item => item.where == fence2);
        tmp.Heal();
    }

    public void HealFence3()
    {
        Animal tmp = animals.First(item => item.where == fence3);
        tmp.Heal();
    }

    void TuristSpawn()
    {
        if (turistTimer <= 0)
        {
            if (turistInterest != 0)
            {
                int rand = UnityEngine.Random.Range(1, 4);
                switch (rand)
                {
                    case 1:
                        thisPrefab = turistPrefab;
                        break;
                    case 2:
                        thisPrefab = femalePrefab;
                        break;
                    case 3:
                        thisPrefab = soldierPrefab;
                        break;
                    default:
                        thisPrefab = femalePrefab;
                        break;
                }
                Instantiate(thisPrefab, turistSpawn);
                turistTimer = UnityEngine.Random.Range(5000, 6000) / turistInterest;
            }
        }
        turistTimer -= Time.deltaTime;
    }

    void TuristInterest()
    {
        double divisor = 0;
        turistInterest = 0;
        foreach (Animal anim in animals)
        {
            turistInterest += anim.health * anim.atraction;
        }
        foreach (Animal anim in animals)
        {
            divisor += anim.atraction;
        }
        if (divisor != 0)
            turistInterest = turistInterest / divisor;
    }

    public void DeleteAnimal()
    {
        animals.RemoveAll(item => item.health <= 0 || item.hunger <= 0);
    }

    public void Pause(GameObject from)
    {
        Time.timeScale = 0;
        from.SetActive(false);
        SliderCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
    }

    public void SwitchTo(GameObject to)
    {
        gameCanvas.SetActive(false);
        to.SetActive(true);
    }

    public void Return()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
        SliderCanvas.SetActive(true);
    }

    public void BackToGame(GameObject from)
    {
        Time.timeScale = 1;
        from.SetActive(false);
        gameCanvas.SetActive(true);
        SliderCanvas.SetActive(true);
    }

    public void GoMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Info()
    {
        pauseCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        infoCanvas.SetActive(true);
    }

    public void MealFence0()
    {
        Animal tmp = animals.First(item => item.where == fence0);
        tmp.Meal();
    }

    public void MealFence1()
    {
        Animal tmp = animals.First(item => item.where == fence1);
        tmp.Meal();
    }

    public void MealFence2()
    {
        Animal tmp = animals.First(item => item.where == fence2);
        tmp.Meal();
    }

    public void MealFence3()
    {
        Animal tmp = animals.First(item => item.where == fence3);
        tmp.Meal();
    }

    public void BuyAidKid()
    {
        if (UserData.money >= 10000)
        {
            UserData.money -= 10000;
            UserData.aidKit += 10;
        }
    }

    public void BuyMeals()
    {
        if (UserData.money >= 5000)
        {
            UserData.money -= 5000;
            UserData.snacks += 100;
        }
    }

    public void IsEveryoneDead()
    {
        if (!animals.Any() && isStart)
        {
            isGO = true;
        }
        if (isGO)
            GameOver();
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        pauseCanvas.SetActive(false);
        gameCanvas.SetActive(false);
        fanceCanvas.SetActive(false);
        animalCanvas.SetActive(false);
        eatCanvas.SetActive(false);
        healthCanvas.SetActive(false);
        infoCanvas.SetActive(false);
        SliderCanvas.SetActive(false);

        Score();
        starved.text = GameControllerStatic.starved.ToString();
        diedOld.text = GameControllerStatic.diedOld.ToString();
        diedSick.text = GameControllerStatic.diedSick.ToString();
        result.text = UserData.score.ToString();
        
        if (howManyStarted == GameControllerStatic.diedOld)
        {
            goodGame.SetActive(true);
        }
        else if (GameControllerStatic.starved + GameControllerStatic.diedSick >= howManyStarted/2)
        {
            badGame.SetActive(true);
        }
        if(UserData.score > UserData.record)
        {
            PlayerPrefs.SetInt("Record", UserData.score);
            PlayerPrefs.Save();
            newRecord.SetActive(true);
            UserData.record = PlayerPrefs.GetInt("Record");
        }
        gameOverCanvas.SetActive(true);
    }

    public void Score()
    {
        if(GameControllerStatic.diedOld != 0)
            UserData.score = (int)Math.Ceiling((UserData.money / (GameControllerStatic.starved * 2 + GameControllerStatic.diedSick * 3)) * GameControllerStatic.diedOld);
        else
            UserData.score = (int)Math.Ceiling(UserData.money / (GameControllerStatic.starved * 2 + GameControllerStatic.diedSick * 3));
    }

    void CloudSpawn()
    {
        timeCloud = UnityEngine.Random.Range(5,10);
        int rand = UnityEngine.Random.Range(1,3);
        switch (rand)
        {
            case 1:
                cloudPrefab = smallCloudPrefab;
                break;
            case 2:
                cloudPrefab = bigCloudPrefab;
                break;
            default:
                cloudPrefab = smallCloudPrefab;
                break;
        }
        Instantiate(cloudPrefab, cloudSpawn);
    }
}