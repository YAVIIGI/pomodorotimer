
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Pomo_Button : UdonSharpBehaviour
{
    [SerializeField]
    Pomo_Timer timerObj;

    [SerializeField]
    bool isRunningButton = true;

    public override void Interact()
    {
        if (isRunningButton)
        {
            timerObj.OnRunningButtonPressed();
        }
        else
        {
            timerObj.OnResetButtonPressed();
        }

    }

}
