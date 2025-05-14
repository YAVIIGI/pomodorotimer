
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Pomo_7Seg : UdonSharpBehaviour
{

    int[] SEG_DATA_0 = { 1, 1, 1, 1, 1, 1, 0 };
    int[] SEG_DATA_1 = { 0, 1, 1, 0, 0, 0, 0 };
    int[] SEG_DATA_2 = { 1, 1, 0, 1, 1, 0, 1 };
    int[] SEG_DATA_3 = { 1, 1, 1, 1, 0, 0, 1 };
    int[] SEG_DATA_4 = { 0, 1, 1, 0, 0, 1, 1 };
    int[] SEG_DATA_5 = { 1, 0, 1, 1, 0, 1, 1 };
    int[] SEG_DATA_6 = { 1, 0, 1, 1, 1, 1, 1 };
    int[] SEG_DATA_7 = { 1, 1, 1, 0, 0, 0, 0 };
    int[] SEG_DATA_8 = { 1, 1, 1, 1, 1, 1, 1 };
    int[] SEG_DATA_9 = { 1, 1, 1, 1, 0, 1, 1 };

    [SerializeField]
    GameObject[] segObject;

    [SerializeField]
    [UdonSynced]
    public int number;

    void Start()
    {

    }

    public void SetDisplay(int num)
    {
        for (int i = 0; i < 7; i++)
        {
            switch(num)
            {
                case 0: segObject[i].SetActive(SEG_DATA_0[i] == 1); break;
                case 1: segObject[i].SetActive(SEG_DATA_1[i] == 1); break;
                case 2: segObject[i].SetActive(SEG_DATA_2[i] == 1); break;
                case 3: segObject[i].SetActive(SEG_DATA_3[i] == 1); break;
                case 4: segObject[i].SetActive(SEG_DATA_4[i] == 1); break;
                case 5: segObject[i].SetActive(SEG_DATA_5[i] == 1); break;
                case 6: segObject[i].SetActive(SEG_DATA_6[i] == 1); break;
                case 7: segObject[i].SetActive(SEG_DATA_7[i] == 1); break;
                case 8: segObject[i].SetActive(SEG_DATA_8[i] == 1); break;
                case 9: segObject[i].SetActive(SEG_DATA_9[i] == 1); break;
            }
        }
    }

    public void SetDisplay()
    {
        SetDisplay(number);
    }
}
