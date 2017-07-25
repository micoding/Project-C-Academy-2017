using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Animal : MonoBehaviour {

    public double health;
    public double maxHealth;
    public double duration;
    public double timer;
    public int price;
    public int atraction;
    public int maintenanceCost;
    public Transform where;

    double badLuck;
    public string race;

    public double hunger = 100;

    GameController gC;
    int answer;
    Slider healthSlider;
    Slider hungerSlider;

    int random;

    SpriteRenderer spriteRen;

    public Sprite ghost;
    public Sprite blink;
    public Sprite normal;
    float blinkGen;

    bool isGhost = false;

    // Use this for initialization
    void Start () {
        answer = GameControllerStatic.FenceNOTAvailable(where);
        gC = GameObject.Find("GameController").GetComponent<GameController>();
        spriteRen = GetComponent<SpriteRenderer>();
        health = maxHealth;
        if (answer == 1 || answer == 2)
            spriteRen.sortingLayerName = "AnimalFrontRow";
        else
            spriteRen.sortingLayerName = "Animal";
       
        WhichSlider();
        random = UnityEngine.Random.Range(30, 60);
    }

    // Update is called once per frame
    void Update () {
        Duration();
        if (!isGhost)
        {
            Health();
            Hunger();
            blinkGen -= Time.deltaTime;
            if (blinkGen<=0)
                StartCoroutine(Blink());
            Death();
        }
        Slider(); 
    }

    void Death()
    {
        health -= Time.deltaTime;
        maxHealth -= Time.deltaTime;
        if (maxHealth <= 0)
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

    void Health()
    {
        timer += Time.deltaTime;
        if (timer >= random)
        {
            badLuck = UnityEngine.Random.Range(1.1f, 1.5f);
            health = health / badLuck;
            random = UnityEngine.Random.Range(30, 60);
            timer=0;
        }
    }

    public void Heal()
    {
        float ten;
        ten = (float)maxHealth / 10;
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

    void Duration()
    {
        duration += Time.deltaTime;
    }

    void Hunger()
    {
        hunger -= maintenanceCost * Time.deltaTime;
    }

    public void Meal()
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

    public void Slider()
    {
        healthSlider.maxValue = (float)maxHealth;
        healthSlider.value = (float)health;
        hungerSlider.value = (float)hunger;
    }

    public void WhichSlider()
    {
        switch(answer)
        {
            case 0:
                healthSlider = gC.healthSlider0;
                hungerSlider = gC.hungerSlider0;
                break;
            case 1:
                healthSlider = gC.healthSlider1;
                hungerSlider = gC.hungerSlider1;
                break;
            case 2:
                healthSlider = gC.healthSlider2;
                hungerSlider = gC.hungerSlider2;
                break;
            case 3:
                healthSlider = gC.healthSlider3;
                hungerSlider = gC.hungerSlider3;
                break;
            default:
                break;
        }
    }

    IEnumerator Blink()
    {
        blinkGen = UnityEngine.Random.Range(1, 3);
        spriteRen.sprite = blink;
        yield return new WaitForSeconds(0.5f);
        if(!isGhost)
            spriteRen.sprite = normal;
    }

    void GhostGone()
    {
        isGhost = true;
        Rigidbody2D cF = GetComponent<Rigidbody2D>();
        spriteRen.sprite = ghost;
        cF.velocity = new Vector2(0, 1);
    }

    IEnumerator Kill(int diedHow)
    {
        yield return new WaitForSeconds(4);
        diedHow++;
        GameControllerStatic.FenceAvailable(where);
        Destroy(this.gameObject);
    }
}