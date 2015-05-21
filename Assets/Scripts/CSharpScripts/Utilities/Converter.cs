using UnityEngine;
using System.Collections;

[System.Serializable]
public class Converter {

    [System.Serializable]
    public struct Vector3
    {
        public float x ;
        public float y ;
        public float z ;

        public Vector3( UnityEngine.Vector3 vector3 )
        {
            x = vector3.x;
            y = vector3.y;
            z = vector3.z;
        }
    }

    [System.Serializable]
    public struct Quaternion
    {
        public float w ;
        public float x ;
        public float y ;
        public float z ;

        public Quaternion( UnityEngine.Quaternion quaternion)
        {
            w = quaternion.w;
            x = quaternion.x;
            y = quaternion.y;
            z = quaternion.z;
        }
    }

    public static UnityEngine.Vector3 ConvertVector3 ( Vector3 vector3 )
    {
        return new UnityEngine.Vector3( vector3.x, vector3.y, vector3.z);
    }

    public static UnityEngine.Quaternion ConvertQuaternion( Quaternion quaternion )
    {
        return new UnityEngine.Quaternion( quaternion.x, quaternion.y, quaternion.z, quaternion.w);
    }

    public static PlayerFrame RecordFrameToPlayerFrame ( RecordedFrame frame ){
        return (PlayerFrame)frame;
    }

}
