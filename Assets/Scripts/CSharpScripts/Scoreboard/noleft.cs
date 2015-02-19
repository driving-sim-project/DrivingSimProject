using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class noleft : Intugate
{

    private string Rulename = "No Left Turn";
    private string desc = "ขับรถไม่ปฏิบัติตามสัญญาณจราจร \n หรือเครื่องหมายจราจรที่ได้ติดตั้งไว้หรือทำให้ปรากฏ \n ในทางหรือที่พนักงานเจ้าหน้าที่แสดงให้ทราบ \n \n ปรับไม่เกิน 1,000 บาท";

    public override int score()
    {
        throw new System.NotImplementedException();
    }

    public int score(RecordedFrame[] replayRange)
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
