using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
    //public delegate void SpinCompleteDelegate();

    private float totalSpinAmount;

    private void Update()
    {
        if (!isActive) return;

        //This actually will set how many degree will it spin
        float spinAddAmount = 200f  * Time.deltaTime;

        //This will spin the characters based on degree above
        transform.eulerAngles += new Vector3(0, spinAddAmount, 0);

        //For each spin, add it into totalspin amount, if the degree is more than 360 then stop.
        totalSpinAmount += spinAddAmount;
        if (totalSpinAmount >= 360)
        {
            isActive = false;
            onActionComplete();

        }

    }

    public void Spin(Action OnSpinComplete) 
    {
        this.onActionComplete = OnSpinComplete;
        isActive = true;
        totalSpinAmount = 0;   
    }

    public override string GetActionName()
    {
        return "Spin";
    }
}
