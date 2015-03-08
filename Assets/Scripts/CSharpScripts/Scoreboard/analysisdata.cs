using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class analysisdata {

    private List<string> desc = new List<string>() { "Average Driving.You are Driver who don't break rules and drive with aware ", "You are An 1st Attandance Driver.Have Nothing to warn you for driving","You are High Speed Driver. Every turn has an exited feel like Adrenaline flow with blood","test","test","test","test","test","test","test"};
    private List<string> name = new List<string>() {"Normal Driver" , "Should Be Driver" , "Racer Type","aggressive","Risky","Scary Type","Rule Breaker","Waver","Gamer","Careful","Careless" };


    public string loaddesc(int i)
    {
        return desc[i];
    }

    public string loadname(int i)
    {
        return name[i];
    }
    }
