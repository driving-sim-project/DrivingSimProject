using UnityEngine;
using System.Collections;

public class ScreenTexture : MonoBehaviour {
    public int thumbProportion = 25;
    public Color borderColor = Color.white;
    public int borderWidth = 2;
    public Camera camera;
    public GameObject mirrorPlane;
    public float interval = 0.02f;
    private Texture2D texture;
    private Texture2D border; 
    private float time;


    void  Start (){
        texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        time = Time.time;
    }

    private void OnPostRender()
    {
        if (Time.time > time + interval)
        {
            Camera.main.projectionMatrix = camera.projectionMatrix;
            texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            texture.Apply();
            mirrorPlane.renderer.material.SetTexture( 0, texture);
            mirrorPlane.renderer.material.SetTextureScale("_MainTex", new Vector2(-1f, 1f));
            Resources.UnloadUnusedAssets();
            time = Time.time;
        }
    }
}