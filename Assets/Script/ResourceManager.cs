using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class ResourceManager : MonoBehaviour
{
    TMP_Text woodText1;
    TMP_Text woodText2;
    TMP_Text goldText1;
    TMP_Text goldText2;
    public int wood1;
    public int wood2;
    public int gold1;
    public int gold2;
    public float wood1f;
    public float wood2f;
    public float gold1f;
    public float gold2f;

    // Start is called before the first frame update
    void Start()
    {
        woodText1 = GameObject.Find("WoodText1").GetComponent<TMP_Text>();
        woodText2 = GameObject.Find("WoodText2").GetComponent<TMP_Text>();
        goldText1 = GameObject.Find("GoldText1").GetComponent<TMP_Text>();
        goldText2 = GameObject.Find("GoldText2").GetComponent<TMP_Text>();
        SetWoodText1();
        SetWoodText2();
        SetGoldText1();
        SetGoldText2();
    }

    // Update is called once per frame
    void Update()
    {
        SetWoodText1();
        SetWoodText2();
        SetGoldText1();
        SetGoldText2();
    }

    void SetWoodText1()
    {
        wood1f = GameObject.Find("Cat Base").GetComponent<TeamController>().wood;
        wood1 = (int) Math.Round(wood1f);
       
        woodText1.text = "Wood: " + wood1;
    }

    void SetWoodText2()
    {
        wood2f = GameObject.Find("Dog Base").GetComponent<TeamController>().wood;
        wood2 = (int) Math.Round(wood2f);

        woodText2.text = "Wood: " + wood2;
    }

    void SetGoldText1()
    {
        gold1f = GameObject.Find("Cat Base").GetComponent<TeamController>().gold;
        gold1 = (int) Math.Round(gold1f);

        goldText1.text = "Gold: " + gold1;
    }

    void SetGoldText2()
    {
        gold2f = GameObject.Find("Dog Base").GetComponent<TeamController>().gold;
        gold2 = (int) Math.Round(gold2f);
          
        goldText2.text = "Gold: " + gold2;
    }

}
