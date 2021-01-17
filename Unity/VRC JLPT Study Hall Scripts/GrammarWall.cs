
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class GrammarWall : UdonSharpBehaviour
{
    public GameObject databaseUse;

    [UdonSynced(UdonSyncMode.None)] public int randam1 = 1;
    [UdonSynced(UdonSyncMode.None)] public int randam2 = 1;
    [UdonSynced(UdonSyncMode.None)] public int randam3 = 1;
    [UdonSynced(UdonSyncMode.None)] public int randam4 = 1;
    [UdonSynced(UdonSyncMode.None)] public int randam5 = 1;

    public GameObject CardNo;
    public GameObject Grammar;
    public GameObject Meaning;
    void Start()
    {
        
    }

    public override void Interact()
    {
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Flip");
    }

    public void Flip()
    {
        if (Networking.IsMaster)
        {
            if(gameObject.name == "Grammar Wall N5")
            {
                randam5 = Random.Range(0, databaseUse.GetComponent<DataBase>().grammar_hiragana_n5.Length-1);
            }
            if (gameObject.name == "Grammar Wall N4")
            {
                randam4 = Random.Range(0, databaseUse.GetComponent<DataBase>().grammar_hiragana_n4.Length - 1);
            }
            if (gameObject.name == "Grammar Wall N3")
            {
                randam3 = Random.Range(0, databaseUse.GetComponent<DataBase>().grammar_hiragana_n3.Length - 1);
            }
            if (gameObject.name == "Grammar Wall N2")
            {
                randam2 = Random.Range(0, databaseUse.GetComponent<DataBase>().grammar_hiragana_n2.Length - 1);
            }
            if (gameObject.name == "Grammar Wall N1")
            {
                randam1 = Random.Range(0, databaseUse.GetComponent<DataBase>().grammar_hiragana_n1.Length - 1);
            }
        }
    }

    public void Update()
    {
        if(gameObject.name == "Grammar Wall N5")
        {
            Grammar.GetComponent<Text>().text = databaseUse.GetComponent<DataBase>().grammar_hiragana_n5[randam5];
            Meaning.GetComponent<Text>().text = databaseUse.GetComponent<DataBase>().grammar_meaning_n5[randam5];
            CardNo.GetComponent<Text>().text = "CardNo. " + randam5 + " out of 83";
        }
        if (gameObject.name == "Grammar Wall N4")
        {
            Grammar.GetComponent<Text>().text = databaseUse.GetComponent<DataBase>().grammar_hiragana_n4[randam4];
            Meaning.GetComponent<Text>().text = databaseUse.GetComponent<DataBase>().grammar_meaning_n4[randam4];
            CardNo.GetComponent<Text>().text = "CardNo. " + randam4 + " out of 132";
        }
        if (gameObject.name == "Grammar Wall N3")
        {
            Grammar.GetComponent<Text>().text = databaseUse.GetComponent<DataBase>().grammar_hiragana_n3[randam3];
            Meaning.GetComponent<Text>().text = databaseUse.GetComponent<DataBase>().grammar_meaning_n3[randam3];
            CardNo.GetComponent<Text>().text = "CardNo. " + randam3 + " out of 182";
        }
        if (gameObject.name == "Grammar Wall N2")
        {
            Grammar.GetComponent<Text>().text = databaseUse.GetComponent<DataBase>().grammar_hiragana_n2[randam2];
            Meaning.GetComponent<Text>().text = databaseUse.GetComponent<DataBase>().grammar_meaning_n2[randam2];
            CardNo.GetComponent<Text>().text = "CardNo. " + randam2 + " out of 195";
        }
        if (gameObject.name == "Grammar Wall N1")
        {
            Grammar.GetComponent<Text>().text = databaseUse.GetComponent<DataBase>().grammar_hiragana_n1[randam1];
            Meaning.GetComponent<Text>().text = databaseUse.GetComponent<DataBase>().grammar_meaning_n1[randam1];
            CardNo.GetComponent<Text>().text = "CardNo. " + randam1 + " out of 83";
        }
    }
}
