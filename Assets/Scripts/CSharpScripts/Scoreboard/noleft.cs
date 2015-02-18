using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class noleft : Intugate
{

    private string Rulename = "No Left Turn";
    private bool sign = true;
    private string desc = "ขับรถไม่ปฏิบัติตามสัญญาณจราจร \n หรือเครื่องหมายจราจรที่ได้ติดตั้งไว้หรือทำให้ปรากฏ \n ในทางหรือที่พนักงานเจ้าหน้าที่แสดงให้ทราบ \n \n ปรับไม่เกิน 1,000 บาท";
    private bool leftview = false;
    private bool rightview = false;
    private bool backview = false;
    private bool emergencylight = false;
    private bool leftlight = false;
    private bool rightlight = false;
    private float speed = 0;
    private bool turn = false;
    private bool RGW = false;
    private float disbe = 0;

    public override int score(RecordedFrame[] replayRange)
    {
        int a = 100;
        a -= 50;
        return a;
    }

     public override string loadname()
     {
         return Rulename;
     }

}
