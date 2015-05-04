﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class Noover : Intugate
{

    public Noover()
    {
        Rulename = "No Passed Over Post";
        picname = "NOver";
        desc = "ขับรถไม่ปฏิบัติตามสัญญาณจราจร \n หรือเครื่องหมายจราจรที่ได้ติดตั้งไว้หรือทำให้ปรากฏ \n ในทางหรือที่พนักงานเจ้าหน้าที่แสดงให้ทราบ \n \n ปรับไม่เกิน 1,000 บาท";
        sc = 0;
    }



    public override int getscore()
    {
        return sc;
    }

    public override void score()
    {
        int a = 100;

        if (fa)
        {

            a -= 100;

        }

        this.sc = a;
    }
    

    public override string loadpic()
    {
        return picname;
    }

    public override string loadname()
    {
        return Rulename;
    }

    public override string loaddesc()
    {
        return desc;
    }

}
