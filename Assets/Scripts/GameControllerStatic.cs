using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class GameControllerStatic
{
    public static bool fence0Available = true;
    public static bool fence1Available = true;
    public static bool fence2Available = true;
    public static bool fence3Available = true;

    public static int starved;
    public static int diedSick;
    public static int diedOld;

    static bool whichLayerTurist = true;

    public static bool WhichLayerTurist()
    {
        whichLayerTurist = !whichLayerTurist;
        return whichLayerTurist;
    }

    public static void FenceAvailable(Transform where)
    {
        GameController gC = GameObject.Find("GameController").GetComponent<GameController>();
        gC.DeleteAnimal();

        if (where == gC.fence0)
        {
            fence0Available = true;
            gC.healthSlider0.gameObject.SetActive(false);
            gC.hungerSlider0.gameObject.SetActive(false);
            gC.healFence0.SetActive(false);
            gC.mealFence0.SetActive(false);
        }
        else if (where == gC.fence1)
        {
            fence1Available = true;
            gC.healthSlider1.gameObject.SetActive(false);
            gC.hungerSlider1.gameObject.SetActive(false);
            gC.healFence1.SetActive(false);
            gC.mealFence1.SetActive(false);
        }
        else if (where == gC.fence2)
        {
            fence2Available = true;
            gC.healthSlider2.gameObject.SetActive(false);
            gC.hungerSlider2.gameObject.SetActive(false);
            gC.healFence2.SetActive(false);
            gC.mealFence2.SetActive(false);
        }
        else if (where == gC.fence3)
        {
            fence3Available = true;
            gC.healthSlider3.gameObject.SetActive(false);
            gC.hungerSlider3.gameObject.SetActive(false);
            gC.healFence3.SetActive(false);
            gC.mealFence3.SetActive(false);
        }
    }

    public static int FenceNOTAvailable(Transform where)
    {
        GameController gC = GameObject.Find("GameController").GetComponent<GameController>();

        if (where == gC.fence0)
        {
            fence0Available = false;
            gC.fence0Button.SetActive(false);
            gC.healthSlider0.gameObject.SetActive(true);
            gC.hungerSlider0.gameObject.SetActive(true);
            gC.healFence0.SetActive(true);
            gC.mealFence0.SetActive(true);
            return 0;
        }
        else if (where == gC.fence1)
        {
            fence1Available = false;
            gC.fence1Button.SetActive(false);
            gC.healthSlider1.gameObject.SetActive(true);
            gC.hungerSlider1.gameObject.SetActive(true);
            gC.healFence1.SetActive(true);
            gC.mealFence1.SetActive(true);
            return 1;
        }
        else if (where == gC.fence2)
        {
            fence2Available = false;
            gC.fence2Button.SetActive(false);
            gC.healthSlider2.gameObject.SetActive(true);
            gC.hungerSlider2.gameObject.SetActive(true);
            gC.healFence2.SetActive(true);
            gC.mealFence2.SetActive(true);
            return 2;
        }
        else if (where == gC.fence3)
        {
            fence3Available = false;
            gC.fence3Button.SetActive(false);
            gC.healthSlider3.gameObject.SetActive(true);
            gC.hungerSlider3.gameObject.SetActive(true);
            gC.healFence3.SetActive(true);
            gC.mealFence3.SetActive(true);
            return 3;
        }
        else
        {
            return 4;
        }
    }

    public static void HowManyAvailable()
    {
        GameController gC = GameObject.Find("GameController").GetComponent<GameController>();
        gC.howManyAvailable = -1;

        if (fence0Available)
            gC.howManyAvailable++;
        if (fence1Available)
            gC.howManyAvailable++;
        if (fence2Available)
            gC.howManyAvailable++;
        if (fence3Available)
            gC.howManyAvailable++;
    }
}