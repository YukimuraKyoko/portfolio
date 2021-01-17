
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class HiraKana : UdonSharpBehaviour
{
    public GameObject databaseUse;
    [UdonSynced(UdonSyncMode.None)] public int randamH = 0;
    [UdonSynced(UdonSyncMode.None)] public int randamK = 0;
    public GameObject character;
    public GameObject romaji;
    public GameObject CardNo;
    public GameObject Cover;

    void Start()
    {
        
    }

    public override void Interact()
    {
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Flip");
        Cover.SetActive(true);
    }

    public void Flip()
    {
        if (Networking.IsMaster)
        {
            if (gameObject.name == "HiraganaWallJPEN" || gameObject.name == "HiraganaWallENJP")
            {
                randamH = Random.Range(0, databaseUse.GetComponent<DataBase>().hiragana_character.Length - 1);
            }
            if (gameObject.name == "KatakanaWallJPEN" || gameObject.name == "KatakanaWallENJP")
            {
                randamK = Random.Range(0, databaseUse.GetComponent<DataBase>().katakana_character.Length - 1);
            }
        }
    }
    public void Update()
    {
        if (gameObject.name == "HiraganaWallJPEN" || gameObject.name == "HiraganaWallENJP")
        {
            character.GetComponent<Text>().text = databaseUse.GetComponent<DataBase>().hiragana_character[randamH];
            romaji.GetComponent<Text>().text = databaseUse.GetComponent<DataBase>().romaji[randamH];
            CardNo.GetComponent<Text>().text = "CardNo. " + randamH + " out of " + databaseUse.GetComponent<DataBase>().hiragana_character.Length;
        }
        if (gameObject.name == "KatakanaWallJPEN" || gameObject.name == "KatakanaWallENJP")
        {
            character.GetComponent<Text>().text = databaseUse.GetComponent<DataBase>().katakana_character[randamK];
            romaji.GetComponent<Text>().text = databaseUse.GetComponent<DataBase>().romaji[randamK];
            CardNo.GetComponent<Text>().text = "CardNo. " + randamK + " out of " + databaseUse.GetComponent<DataBase>().katakana_character.Length;
        }
    }
}
