
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class PlayerUIVariables : UdonSharpBehaviour
{
    public GameObject YenUI;
    public int currentYen;
    Color currentCol;
    public void Start()
    {
        currentYen = 1000;
        currentCol = YenUI.GetComponent<Text>().color;
        YenUI.GetComponent<Text>().text = "" + 1000;
    }

    public void AddYen(int add)
    {
        currentYen += add;
        UpdateYen();
    }

    public void SubtractYen(int subtract)
    {
        if(subtract > currentYen)
        {
            //Do nothing
        }
        else
        {
            currentYen -= subtract;
            UpdateYen();
        }
    }

    public void UpdateYen()
    {
        YenUI.GetComponent<Text>().text = "" + currentYen;
        YenUI.GetComponent<Text>().color = currentCol;
    }

    public void NotEnoughYen()
    {

    }
}
