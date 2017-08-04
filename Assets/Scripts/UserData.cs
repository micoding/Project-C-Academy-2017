using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UserData : MonoBehaviour {

    public static int score;
    public static int record;
    public static double money;
    public double moneyForInspector;
    public static int aidKit;
    public int aidKitForInspector;
    public static int snacks;
    public int snacksForInspector;

    public Text moneyTextGame;
    public Text moneyTextPause;
    public Text moneyTextAnimal;
    public Text moneyTextFence;
    public Text snacksText;
    Text moneyText;

    public float flashTime;

    public Text aidText;

    private void Awake()
    {
        money = moneyForInspector;
        aidKit = aidKitForInspector;
        snacks = snacksForInspector;
        record = PlayerPrefs.GetInt("Record");
    }
	// Update is called once per frame
	void Update () {
        WhichMoneyText();
        moneyText.text = Math.Floor(money).ToString();     
        aidText.text = aidKit.ToString();
        snacksText.text = snacks.ToString();
    }

    public IEnumerator Flashing() // func to flash the money text
    {
        moneyText.color = new Color(1, 0, 0);

        yield return new WaitForSeconds(flashTime);

        moneyText.color = new Color(0, 0, 0);
    }

    void WhichMoneyText() //func setting money to display in proper place
    {
        if (moneyTextGame.gameObject.activeInHierarchy)
            moneyText = moneyTextGame;
        else if (moneyTextPause.gameObject.activeInHierarchy)
            moneyText = moneyTextPause;
        else if (moneyTextAnimal.gameObject.activeInHierarchy)
            moneyText = moneyTextAnimal;
        else if (moneyTextFence.gameObject.activeInHierarchy)
            moneyText = moneyTextFence;
    }
}