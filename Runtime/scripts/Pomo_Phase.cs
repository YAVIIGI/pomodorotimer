
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Pomo_Phase : UdonSharpBehaviour
{
    [SerializeField]
    GameObject[] objects;

    public void SetDisplay(int num)
    {
        for(int i=0;i<10;i++)
        {
            objects[i].SetActive(i == num);
        }
    }
}
