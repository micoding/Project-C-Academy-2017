using System.Collections.Generic;
using UnityEngine;

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

    static public List<Turist> turists = new List<Turist>();

    public static bool WhichLayerTurist() // change the layer of next turist to prevent the mess in the body sprites
    {
        whichLayerTurist = !whichLayerTurist;
        return whichLayerTurist;
    }

    public static void FenceAvailable(Transform where) //when animal die this func stops to stop the turist
    {
        GameController gC = GameObject.Find("GameController").GetComponent<GameController>();
        UIController uI = GameObject.Find("UI").GetComponent<UIController>();
        gC.DeleteAnimal();

        if (where == gC.fence0)
        {
            fence0Available = true;
            uI.healthSlider0.gameObject.SetActive(false);
            uI.hungerSlider0.gameObject.SetActive(false);
            uI.healFence0.SetActive(false);
            uI.mealFence0.SetActive(false);
        }
        else if (where == gC.fence1)
        {
            fence1Available = true;
            uI.healthSlider1.gameObject.SetActive(false);
            uI.hungerSlider1.gameObject.SetActive(false);
            uI.healFence1.SetActive(false);
            uI.mealFence1.SetActive(false);
        }
        else if (where == gC.fence2)
        {
            fence2Available = true;
            uI.healthSlider2.gameObject.SetActive(false);
            uI.hungerSlider2.gameObject.SetActive(false);
            uI.healFence2.SetActive(false);
            uI.mealFence2.SetActive(false);
        }
        else if (where == gC.fence3)
        {
            fence3Available = true;
            uI.healthSlider3.gameObject.SetActive(false);
            uI.hungerSlider3.gameObject.SetActive(false);
            uI.healFence3.SetActive(false);
            uI.mealFence3.SetActive(false);
        }
    }

    public static int FenceNOTAvailable(Transform where) // func to activate the stops for turists
    {
        GameController gC = GameObject.Find("GameController").GetComponent<GameController>();
        UIController uI = GameObject.Find("UI").GetComponent<UIController>();

        if (where == gC.fence0)
        {
            fence0Available = false;
            uI.fence0Button.SetActive(false);
            uI.healthSlider0.gameObject.SetActive(true);
            uI.hungerSlider0.gameObject.SetActive(true);
            uI.healFence0.SetActive(true);
            uI.mealFence0.SetActive(true);
            return 0;
        }
        else if (where == gC.fence1)
        {
            fence1Available = false;
            uI.fence1Button.SetActive(false);
            uI.healthSlider1.gameObject.SetActive(true);
            uI.hungerSlider1.gameObject.SetActive(true);
            uI.healFence1.SetActive(true);
            uI.mealFence1.SetActive(true);
            return 1;
        }
        else if (where == gC.fence2)
        {
            fence2Available = false;
            uI.fence2Button.SetActive(false);
            uI.healthSlider2.gameObject.SetActive(true);
            uI.hungerSlider2.gameObject.SetActive(true);
            uI.healFence2.SetActive(true);
            uI.mealFence2.SetActive(true);
            return 2;
        }
        else if (where == gC.fence3)
        {
            fence3Available = false;
            uI.fence3Button.SetActive(false);
            uI.healthSlider3.gameObject.SetActive(true);
            uI.hungerSlider3.gameObject.SetActive(true);
            uI.healFence3.SetActive(true);
            uI.mealFence3.SetActive(true);
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