using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GameController : MonoBehaviour {

    public double time;// duration of game
    public double income;

    public Transform fence0;
    public Transform fence1;
    public Transform fence2;
    public Transform fence3;

    public GameObject anim0;//prefab anim0
    public GameObject anim1;//up
    public GameObject anim2;//up
    public GameObject anim3;//up
    public List<Animal> animals = new List<Animal>();//list of animals in game

    List<Animal> allAnimals = new List<Animal>();//all possible animals

    public int whichOne = -1;

    GameObject animal;
    Animal script;
    UserData uD;

    //prefabs of turists
    public GameObject turistPrefab;
    public GameObject femalePrefab;
    public GameObject soldierPrefab;
    GameObject thisPrefab;

    public Transform turistSpawn;
    public double turistTimer;
    public double turistInterest;

    public int howManyAvailable;
    bool stillLoop = true;
    public GameObject stopLoop;

    public bool isStart = false;
    public bool isGO = false;

    public int howManyStarted;

    public Transform cloudSpawn;
    GameObject cloudPrefab;
    public GameObject smallCloudPrefab;
    public GameObject bigCloudPrefab;
    float timeCloud;

    UIController uI;

    public double minLeft;
    public double secLeft;
    public double left;

    private void Awake()
    {
        //reset static controller
        GameControllerStatic.fence0Available = true;
        GameControllerStatic.fence1Available = true;
        GameControllerStatic.fence2Available = true;
        GameControllerStatic.fence3Available = true;
        GameControllerStatic.diedOld = 0;
        GameControllerStatic.starved = 0;
        GameControllerStatic.diedSick = 0;
        foreach (Turist item in GameControllerStatic.turists)
        {
            Destroy(item);
        }
    }

    // Use this for initialization
    void Start() {
        uI = GameObject.Find("UI").GetComponent<UIController>();
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
        TimeLeft();
    }

    public void StartSpawn(Transform where)
    {
        uI.animalCanvas.SetActive(false);
        uI.fanceCanvas.SetActive(false);
        if (where != null)
        {
            Create(where);
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
            uI.animalCanvas.SetActive(true);
        }
        else//turn game on
        {
            Time.timeScale = 1;
            uI.gameCanvas.SetActive(true);
            uI.sliderCanvas.SetActive(true);
        }

    }

    void Spawn()//first run
    {
        stopLoop.SetActive(false);
        Time.timeScale = 0;
        uI.gameCanvas.SetActive(false);
        uI.animalCanvas.SetActive(true);
        CanAfford();
    }

    void Create(Transform where)//creates animal
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

    void CanAfford()//can player afford the animal
    {
        uI.anim0Button.SetActive(false);
        uI.anim1Button.SetActive(false);
        uI.anim2Button.SetActive(false);
        uI.anim3Button.SetActive(false);

        List<Animal> CAAnim = allAnimals.FindAll(item => item.price <= UserData.money);

        foreach (Animal Anim in CAAnim)
        {
            switch (Anim.race)
            {
                case "anim0":
                    uI.anim0Button.SetActive(true);
                    break;
                case "anim1":
                    uI.anim1Button.SetActive(true);
                    break;
                case "anim2":
                    uI.anim2Button.SetActive(true);
                    break;
                case "anim3":
                    uI.anim3Button.SetActive(true);
                    break;
            }
        }
    }

    void TuristSpawn()//spawn the turist
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

    void TuristInterest()//set the interrest of turist
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
        animals.RemoveAll(item => item.health <= 0 || item.hunger <= 0 || item.leftLife <= 0);
    }

    void IsEveryoneDead()
    {
        if (!animals.Any() && isStart && !isGO)
        {
            isGO = true;
            GameOver();
        }
    }

    void GameOver()
    {
        foreach (Turist item in GameControllerStatic.turists)
        {
            Destroy(item);
        }

        Time.timeScale = 0;
        uI.pauseCanvas.SetActive(false);
        uI.gameCanvas.SetActive(false);
        uI.fanceCanvas.SetActive(false);
        uI.animalCanvas.SetActive(false);
        uI.eatCanvas.SetActive(false);
        uI.healthCanvas.SetActive(false);
        uI.infoCanvas.SetActive(false);
        uI.sliderCanvas.SetActive(false);

        Score();
        uI.starved.text = GameControllerStatic.starved.ToString();
        uI.diedOld.text = GameControllerStatic.diedOld.ToString();
        uI.diedSick.text = GameControllerStatic.diedSick.ToString();
        uI.result.text = UserData.score.ToString();
        
        if (howManyStarted == GameControllerStatic.diedOld)
        {
            uI.goodGame.SetActive(true);
        }
        else if (GameControllerStatic.starved + GameControllerStatic.diedSick >= howManyStarted/2)
        {
            uI.badGame.SetActive(true);
        }
        if(UserData.score > UserData.record)
        {
            PlayerPrefs.SetInt("Record", UserData.score);
            PlayerPrefs.Save();
            uI.newRecord.SetActive(true);
            UserData.record = PlayerPrefs.GetInt("Record");
        }
        uI.gameOverCanvas.SetActive(true);
    }

    void Score()
    {
        if (GameControllerStatic.diedOld != 0 && (GameControllerStatic.starved != 0 || GameControllerStatic.diedSick != 0))
            UserData.score = (int)Math.Floor((UserData.money / (GameControllerStatic.starved * 2 + GameControllerStatic.diedSick * 3)) * GameControllerStatic.diedOld);
        else if (GameControllerStatic.diedOld != 0)
            UserData.score = (int)Math.Floor(UserData.money * GameControllerStatic.diedOld);
        else
            UserData.score = (int)Math.Floor(UserData.money / (GameControllerStatic.starved * 2 + GameControllerStatic.diedSick * 3));
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

    void TimeLeft()//show time is left in game
    {
        if (animals.Any())
        {
            left = animals.Max(item => item.leftLife);
            minLeft = Math.Floor(left / 60);
            secLeft = Math.Round(left - minLeft * 60);
            if (minLeft >= 0 && secLeft >= 0)
            {
                uI.minLeft.text = minLeft.ToString();
                uI.secLeft.text = secLeft.ToString();
                if (minLeft <= 0)
                    uI.secLeft.color = new Color(1, 0, 0);
                else
                    uI.secLeft.color = new Color(0, 0, 0);
            }
        }
        else
        {
            uI.minLeft.text = "00";
            uI.secLeft.text = "00";
        }
    }
}