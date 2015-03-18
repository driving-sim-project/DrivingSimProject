using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Checkpoint : MonoBehaviour {

    public string[] rules;
    public Transform[] rulesRefObj;

    public List<rulesList> cpRulesList = new List<rulesList>();

    public struct rulesList
    {
        public string ruleName;
        public int RefObj;

        public rulesList(string name, int refob)
        {
            ruleName = name;
            RefObj = refob;
        }

        public rulesList( string name )
        {
            ruleName = name;
            RefObj = 0;
        }
    }

    void Awake()
    {
        for (int i = 0; i < rules.Length; i++ )
        {
            if (rulesRefObj[i] == null)
            {
                cpRulesList.Add(new rulesList( rules[i] ));
            }
            else
            {
                cpRulesList.Add(new rulesList( rules[i], rulesRefObj[i].GetInstanceID() ) );
                Debug.Log(rulesRefObj[i].GetInstanceID());
            }
        }
    }

}
