using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TaskSel : MonoBehaviour
{

    public Image bgObject;
    public Image[] canvasList;

    public int defaultMenu = 0;
    public Sprite[] menuList;
    public Sprite emptyMenu;
    public string[] sceneList;


    private float[] guiPosition = new float[2];
    private GUIStyle titleBox;
    private int selectedMenu;
    private float selectTime;

    void Awake()
    {
        guiPosition[0] = Screen.width / 2f - 320;
        guiPosition[1] = Screen.height / 2f - 240;
        Screen.lockCursor = true;
        selectedMenu = defaultMenu;
        selectTime = 0f;
    }

    void Start()
    {
        MenuUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > selectTime + 0.5f && Mathf.Abs(Input.GetAxis("Horizontal")) >= 0.1f)
        {
            if (Input.GetAxis("Horizontal") > 0f)
                selectedMenu += 1;

            if (Input.GetAxis("Horizontal") < -0f)
                selectedMenu -= 1;

            if (selectedMenu < 0)
                selectedMenu = menuList.Length - 1;

            selectedMenu = selectedMenu % menuList.Length;

            MenuUpdate();

            selectTime = Time.time;
        }
    }

    void FixedUpdate()
    {
        guiPosition[0] = Screen.width / 2f - Screen.width / 4f;
        guiPosition[1] = Screen.height / 2f - Screen.height / 4f;
    }

    void OnGUI()
    {

        //GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), titleBackground, ScaleMode.StretchToFill);

        if(Time.timeSinceLevelLoad > 1f && Input.GetAxis("Vertical") < -0.5f){
            GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, Screen.width / 4, Screen.height / 4), "Exit", titleBox);
            Application.LoadLevel("StartMenu");
        }

        if (Time.timeSinceLevelLoad > 1f && Input.GetAxis("Vertical") > 0.5f)
        {
            titleBox = GUI.skin.box;
            titleBox.alignment = TextAnchor.MiddleCenter;
            if(selectedMenu == menuList.Length - 1){
                GUI.Box(new Rect(guiPosition[0], guiPosition[1], guiPosition[0] * 2, guiPosition[1] * 2), "You have to pass pre-mission first.", titleBox);
            }
            else
            {
                GUI.Box(new Rect(guiPosition[0], guiPosition[1], guiPosition[0] * 2, guiPosition[1] * 2), "Please Wait.....", titleBox);
                if (SceneManager.GoScene == "replay")
                {
                    SceneManager.setSeqScenes(sceneList[selectedMenu], "replay");
                }
                else
                {
                    SceneManager.setSeqScenes("calib_scene", sceneList[selectedMenu]);
                }
                Application.LoadLevel(SceneManager.FromScene);
            }
            
        }

    }

    void MenuUpdate()
    {
        int tmpMenu;
        for (int i = 0; i < canvasList.Length; i++)
        {
            tmpMenu = selectedMenu - 2 + i;
            if (tmpMenu < 0 || tmpMenu >= menuList.Length)
                canvasList[i].sprite = emptyMenu;
            else
                canvasList[i].sprite = menuList[tmpMenu];
        }

        bgObject.sprite = menuList[selectedMenu];
    }

}
