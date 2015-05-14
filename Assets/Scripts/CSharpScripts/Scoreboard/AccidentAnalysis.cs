using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AccidentAnalysis {


    PlayerFrame[] dataFrames;
    int hitObjID;
    int hitDirection;
    string anaTxt = "";

    public void getDataFrame( PlayerFrame[] dataList)
    {
        dataFrames = dataList;
    }

    public void getHitObjID(int ID)
    {
        hitObjID = ID;
    }

    public void getHitDirection(int direction)
    {
        hitDirection = direction;
    }

    public void analyze()
    {

        Debug.Log(hitObjID);
        //anaTxt = "";
        //for (int i = 0; i < dataFrames[dataFrames.Length - 1].collisionID.Length; i++)
        //{
        //    Debug.Log(dataFrames[dataFrames.Length - 1].collisionID[i]);
        //    if (dataFrames[dataFrames.Length - 1].collisionID[i] == hitObjID)
        //    {
        //        anaTxt += "You hited by " + dataFrames[dataFrames.Length - 1].collisionTag;
        //        switch (i)
        //        {
        //            case 0:
        //                anaTxt += " in front of you.";
        //                break;
        //            case 1:
        //                anaTxt += " on your left.";
        //                break;
        //            case 2:
        //                anaTxt += " on your right.";
        //                break;
        //            case 3:
        //                anaTxt += " in the back.";
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        anaTxt += "You hit a footpath in the sideways. Sometimes you can't see it but it's there.";
        //    }
        //}
    }

    public string details()
    {
        return anaTxt;
    }

}
