
using UdonSharp;
using UnityEngine;
using UnityEngine.UIElements;
using VRC.SDKBase;
using VRC.Udon;

public class Pomo_Timer : UdonSharpBehaviour
{
    const int POMODORO_TIME = 25 * 60;
    const int COFFEE_TIME = 5 * 60;

    [SerializeField]
    Pomo_7Seg[] pomo_7Segs;
    [SerializeField]
    GameObject icon_tomato;
    [SerializeField]
    GameObject icon_coffee;
    [SerializeField]
    GameObject icon_run;
    [SerializeField]
    GameObject icon_pause;
    [SerializeField]
    Pomo_Phase phaseObj;

    [UdonSynced]
    int timerValue = POMODORO_TIME;
    [UdonSynced]
    int phase = 1;
    [UdonSynced]
    bool isRunning = false;
    [UdonSynced]
    bool isPomodoroMode = true;

    private float timer = 0f;
    private float interval = 1f;

    [SerializeField]
    AudioSource se_play;
    [SerializeField]
    AudioSource se_finish;
    [SerializeField]
    AudioSource se_stop;

    void Start()
    {
        timerValue = POMODORO_TIME;
        UpdateDisplay();
    }

    private void Update()
    {
        if (Networking.IsOwner(gameObject))
        {
            if(isRunning)
            {
                timer += Time.deltaTime;
                if (timer >= interval)
                {
                    Debug.Log(timerValue);
                    timer = 0f;
                    timerValue--;
                    if (timerValue == -1)
                    {
                        isPomodoroMode = !isPomodoroMode;
                        if (isPomodoroMode)
                        {
                            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, nameof(PlayPlaySound));
                            timerValue = POMODORO_TIME;
                            phase++;
                            phase %= 10;
                        }
                        else
                        {
                            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, nameof(PlayFinishSound));
                            timerValue = COFFEE_TIME;
                        }
                    }
                    UpdateDisplay();
                    RequestSerialization();
                }
            }
        }
    }

    public void PlayPlaySound()
    {
        se_play.Play();
    }
    public void PlayStopSound()
    {
        se_stop.Play();
    }
    public void PlayFinishSound()
    {
        se_finish.Play();
    }

    public override void OnDeserialization()
    {
        UpdateDisplay();
        base.OnDeserialization();
    }

    public void UpdateDisplay()
    {
        int seg1 = (timerValue % 60) % 10;
        int seg2 = (timerValue % 60) / 10;
        int seg3 = (timerValue / 60 % 60) % 10;
        int seg4 = (timerValue / 60 % 60) / 10;
        pomo_7Segs[0].SetDisplay(seg1);
        pomo_7Segs[1].SetDisplay(seg2);
        pomo_7Segs[2].SetDisplay(seg3);
        pomo_7Segs[3].SetDisplay(seg4);

        icon_tomato.SetActive(isPomodoroMode);
        icon_coffee.SetActive(!isPomodoroMode);
        icon_run.SetActive(isRunning);
        icon_pause.SetActive(!isRunning);

        phaseObj.SetDisplay(phase);

    }

    public void ResetValues()
    {
        isRunning = false;
        isPomodoroMode = true;
        timerValue = POMODORO_TIME;
        phase = 1;
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, nameof(PlayStopSound));
        UpdateDisplay();
        RequestSerialization();
    }
    public void ChangeRunning()
    {
        isRunning = !isRunning;
        if(!isRunning)
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, nameof(PlayStopSound));
        }
        else
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, nameof(PlayPlaySound));
        }
        UpdateDisplay();
        RequestSerialization();
    }

    public void OnRunningButtonPressed()
    {
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, nameof(ChangeRunning));
    }

    public void OnResetButtonPressed()
    {
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, nameof(ResetValues));
    }
}
