using UnityEngine;
using System.Collections;

[System.Serializable]
public class CollisionData {

    public string colliderTag;
    public int colliderID;
    public float collisionTime;
    public float leaveTime;

    public CollisionData(string Tag, int ID, float Time)
    {
        colliderTag = Tag;
        colliderID = ID;
        collisionTime = Time;
    }

    public void LeaveTime( float Time )
    {
        leaveTime = Time;
    }

}
