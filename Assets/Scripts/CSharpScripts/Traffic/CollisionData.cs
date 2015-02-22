using UnityEngine;
using System.Collections;

[System.Serializable]
public class CollisionData {

    public string colliderTag;
    public int colliderID;

    public CollisionData(string Tag, int ID)
    {
        colliderTag = Tag;
        colliderID = ID;
    }

}
