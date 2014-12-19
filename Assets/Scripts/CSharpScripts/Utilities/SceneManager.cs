using UnityEngine;
using System.Collections;
using TETCSharpClient;
using TETCSharpClient.Data;
using Assets.Scripts;
using System;
using System.Collections.Generic;


public static class SceneManager {

    private static string fromScene = null;
    private static string nextScene = null;

    public static string FromScene
    {
        get
        {
            return fromScene;
        }
    }

    public static string GoScene
    {
        get
        {
            return nextScene;
        }
    }

    public static void setSeqScenes(string goFrom, string goTo)
    {

        fromScene = goFrom;
        nextScene = goTo;

    }

}
