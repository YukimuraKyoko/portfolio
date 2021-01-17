
using TMPro;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;


public class Flashcard : UdonSharpBehaviour
{
    public GameObject databaseUse;
    [UdonSynced(UdonSyncMode.None)] public int randam1 = 1;
    [UdonSynced(UdonSyncMode.None)] public int randam2 = 1;
    [UdonSynced(UdonSyncMode.None)] public int randam3 = 1;
    [UdonSynced(UdonSyncMode.None)] public int randam4 = 1;
    [UdonSynced(UdonSyncMode.None)] public int randam5 = 1;
    public GameObject EN;
    public GameObject JP;
    public GameObject Hira;
    public GameObject CardNoFront;
    public GameObject CardNoBack;
    void Start()
    {
    
    }

    void OnEnable()
    {

    }

    public override void OnPickupUseDown()
    {
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Flip");
    }

    public override void OnPickup()
    {
        
    }

    public override void Interact()
    {
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Flip");

    }
    public void Update()
    {
        

        if (gameObject.name == "N1FlashCard" || gameObject.name == "N1JPtoENG")
        {
            EN.GetComponent<Text>().text = databaseUse.GetComponent<DataBase>().vocab_english_n1[randam1];
            if (databaseUse.GetComponent<DataBase>().vocab_kanji_n1[randam1] == " ")
            {
                JP.GetComponent<Text>().text = databaseUse.GetComponent<DataBase>().vocab_hiragana_n1[randam1];
            }
            else
            {
                JP.GetComponent<Text>().text = databaseUse.GetComponent<DataBase>().vocab_kanji_n1[randam1];
            }
            Hira.GetComponent<Text>().text = databaseUse.GetComponent<DataBase>().vocab_hiragana_n1[randam1];
            CardNoBack.GetComponent<Text>().text = "CardNo. " + randam1 + " out of 3476";
            CardNoFront.GetComponent<Text>().text = "CardNo. " + randam1 + " out of 3476";
        }

        if (gameObject.name == "N2FlashCard" || gameObject.name == "N2JPtoENG")
        {
            EN.GetComponent<Text>().text = databaseUse.GetComponent<DataBase>().vocab_english_n2[randam2];
            if (databaseUse.GetComponent<DataBase>().vocab_kanji_n2[randam2] == " ")
            {
                JP.GetComponent<Text>().text = databaseUse.GetComponent<DataBase>().vocab_hiragana_n2[randam2];
            }
            else
            {
                JP.GetComponent<Text>().text = databaseUse.GetComponent<DataBase>().vocab_kanji_n2[randam2];
            }
            Hira.GetComponent<Text>().text = databaseUse.GetComponent<DataBase>().vocab_hiragana_n2[randam2];
            CardNoBack.GetComponent<Text>().text = "CardNo. " + randam2 + " out of 1835";
            CardNoFront.GetComponent<Text>().text = "CardNo. " + randam2 + " out of 1835";
        }

        if (gameObject.name == "N3FlashCard" || gameObject.name == "N3JPtoENG")
        {
            EN.GetComponent<Text>().text = databaseUse.GetComponent<DataBase>().vocab_english_n3[randam3];
            if (databaseUse.GetComponent<DataBase>().vocab_kanji_n3[randam3] == " ")
            {
                JP.GetComponent<Text>().text = databaseUse.GetComponent<DataBase>().vocab_hiragana_n3[randam3];
            }
            else
            {
                JP.GetComponent<Text>().text = databaseUse.GetComponent<DataBase>().vocab_kanji_n3[randam3];
            }
            Hira.GetComponent<Text>().text = databaseUse.GetComponent<DataBase>().vocab_hiragana_n3[randam3];
            CardNoBack.GetComponent<Text>().text = "CardNo. " + randam3 + " out of 1835";
            CardNoFront.GetComponent<Text>().text = "CardNo. " + randam3 + " out of 1835";
        }

        if (gameObject.name == "N4FlashCard" || gameObject.name == "N4JPtoENG")
        {
            EN.GetComponent<Text>().text = databaseUse.GetComponent<DataBase>().vocab_english_n4[randam4];
            if (databaseUse.GetComponent<DataBase>().vocab_kanji_n4[randam4] == " ")
            {
                JP.GetComponent<Text>().text = databaseUse.GetComponent<DataBase>().vocab_hiragana_n4[randam4];
            }
            else
            {
                JP.GetComponent<Text>().text = databaseUse.GetComponent<DataBase>().vocab_kanji_n4[randam4];
            }
            Hira.GetComponent<Text>().text = databaseUse.GetComponent<DataBase>().vocab_hiragana_n4[randam4];
            CardNoBack.GetComponent<Text>().text = "CardNo. " + randam4 + " out of 634";
            CardNoFront.GetComponent<Text>().text = "CardNo. " + randam4 + " out of 634";
        }

        if (gameObject.name == "N5FlashCard" || gameObject.name == "N5JPtoENG")
        {
            EN.GetComponent<Text>().text = databaseUse.GetComponent<DataBase>().vocab_english_n5[randam5];
            if (databaseUse.GetComponent<DataBase>().vocab_kanji_n5[randam5] == " ")
            {
                JP.GetComponent<Text>().text = databaseUse.GetComponent<DataBase>().vocab_hiragana_n5[randam5];
            }
            else
            {
                JP.GetComponent<Text>().text = databaseUse.GetComponent<DataBase>().vocab_kanji_n5[randam5];
            }
            Hira.GetComponent<Text>().text = databaseUse.GetComponent<DataBase>().vocab_hiragana_n5[randam5];
            CardNoBack.GetComponent<Text>().text = "CardNo. " + randam5 + " out of 669";
            CardNoFront.GetComponent<Text>().text = "CardNo. " + randam5 + " out of 669";
        }

    }

    public void Flip()
    {
        if (Networking.IsMaster)
        {
            if (gameObject.name == "N1FlashCard" || gameObject.name == "N1JPtoENG")
            {
                randam1 = Random.Range(0, databaseUse.GetComponent<DataBase>().vocab_hiragana_n1.Length-1);
            }
            if (gameObject.name == "N2FlashCard" || gameObject.name == "N2JPtoENG")
            {
                randam2 = Random.Range(0, databaseUse.GetComponent<DataBase>().vocab_hiragana_n2.Length-1);
            }
            if (gameObject.name == "N3FlashCard" || gameObject.name == "N3JPtoENG")
            {
                randam3 = Random.Range(0, databaseUse.GetComponent<DataBase>().vocab_hiragana_n3.Length-1);
            }
            if (gameObject.name == "N4FlashCard" || gameObject.name == "N4JPtoENG")
            {
                randam4 = Random.Range(0, databaseUse.GetComponent<DataBase>().vocab_hiragana_n4.Length-1);
            }
            if (gameObject.name == "N5FlashCard" || gameObject.name == "N5JPtoENG")
            {
                randam5 = Random.Range(0, databaseUse.GetComponent<DataBase>().vocab_hiragana_n5.Length-1);
            }
        }
    }
   

}




