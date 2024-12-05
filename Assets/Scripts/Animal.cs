using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Animal : MonoBehaviour {

    public double health; //current health
    public double leftLife; //how long can live
    public double maxHealth; //starting health
    public double timer; //timer for badluck
    public int price;
    public int atraction;
    public int maintenanceCost; //for hunger
    public Transform where; //where animal is

    double badLuck;
    public string race;

    public double hunger = 100;

    Slider healthSlider;
    Slider hungerSlider;

    SpriteRenderer spriteRen;
    UIController uI;
    int answer;

    public Sprite ghost;
    public Sprite blink;
    public Sprite normal;
    float blinkGen;

    bool isGhost = false; //is animal is ghost

    // Use this for initialization
    void Start () {
        answer = GameControllerStatic.FenceNOTAvailable(where); 
        uI = GameObject.Find("UI").GetComponent<UIController>();
        spriteRen = GetComponent<SpriteRenderer>();
        health = maxHealth;//set to maxHealth
        leftLife = maxHealth;
        if (answer == 1 || answer == 2)
            spriteRen.sortingLayerName = "AnimalFrontRow"; //bring animals sprite to front layer
        else
            spriteRen.sortingLayerName = "Animal"; //leaves animal sprite at default layer

        WhichSlider(); //set proper sliders to animal
        timer = UnityEngine.Random.Range(30, 60);
    }

    // Update is called once per frame
    void Update () {
        if (!isGhost)
        {
            Health();
            Hunger();
            blinkGen -= Time.deltaTime;
            if (blinkGen<=0)
                StartCoroutine(Blink());
            Death();
            Slider(); 
        }
    }

    void Death()//decrement the health and if died its executing it
    {
        health -= Time.deltaTime;
        leftLife -= Time.deltaTime;
        if (leftLife <= 0)
        {
            GhostGone();
            StartCoroutine(Kill(GameControllerStatic.diedOld++));
        }
        else if (health <= 0)
        {
            GhostGone();
            StartCoroutine(Kill(GameControllerStatic.diedSick++));
        }
        else if (hunger <= 0)
        {
            GhostGone();
            StartCoroutine(Kill(GameControllerStatic.starved++));
        }
    }

    void Health()// change the health + random divide to health
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            badLuck = Math.Floor(UnityEngine.Random.Range(2f, 10f));
            health -= health/badLuck;
            timer = UnityEngine.Random.Range(30, 60);
        }
    }

    public void Heal()// heals the animal
    {
        float ten;
        ten = (float)health / 10;
        if (1 <= UserData.aidKit)
        {
            if (health + ten <= maxHealth)
            {
                health += ten;
                UserData.aidKit -= 1;
            }
            else if (health < maxHealth)
            {
                health += maxHealth - health;
                UserData.aidKit -= 1;
            }
        }
    }

    void Hunger()//change the hunger
    {
        hunger -= maintenanceCost * Time.deltaTime;
    }

    public void Meal()//feed the animal
    {
        if (1 <= UserData.snacks && hunger < 100)
        {
            if (hunger + 10 <= 100)
            {
                hunger += 10;
                UserData.snacks -= 1;
            }
            else
            {
                hunger += 100 - hunger;
                UserData.snacks -= 1;
            }
        }
    }

    void Slider()//actualise sliders
    {
        healthSlider.maxValue = (float)maxHealth;
        healthSlider.value = (float)health;
        hungerSlider.value = (float)hunger;
    }

    void WhichSlider()// connects correct sliders
    {
        switch(answer)
        {
            case 0:
                healthSlider = uI.healthSlider0;
                hungerSlider = uI.hungerSlider0;
                break;
            case 1:
                healthSlider = uI.healthSlider1;
                hungerSlider = uI.hungerSlider1;
                break;
            case 2:
                healthSlider = uI.healthSlider2;
                hungerSlider = uI.hungerSlider2;
                break;
            case 3:
                healthSlider = uI.healthSlider3;
                hungerSlider = uI.hungerSlider3;
                break;
            default:
                break;
        }
    }

    IEnumerator Blink() //animation of bliking
    {
        blinkGen = UnityEngine.Random.Range(1, 3);
        spriteRen.sprite = blink;
        yield return new WaitForSeconds(0.5f);
        if(!isGhost)
            spriteRen.sprite = normal;
    }

    void GhostGone()//aimation of becoming ghost
    {
        isGhost = true;
        Rigidbody2D cF = GetComponent<Rigidbody2D>();
        spriteRen.sprite = ghost;
        cF.velocity = new Vector2(0, 1);
    }

    IEnumerator Kill(int diedHow)//wait for time
    {
        yield return new WaitForSeconds(4);
        diedHow++;
        GameControllerStatic.FenceAvailable(where);
        Destroy(this.gameObject);
    }
}