using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour {

    public GameObject MenuPanel; 

	// Use this for initialization
	void Start () {
        AddListener();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void AddListener()
    {
        Button[] BtnKids = MenuPanel.GetComponentsInChildren<Button>();
        for (int i = 0; i < BtnKids.Length; i++)
        {
            GameObject go = BtnKids[i].gameObject;
            BtnKids[i].onClick.AddListener(delegate () { ClickMenuBtn(go); });
        }
    }

    void ClickMenuBtn(GameObject go)
    {
        switch (go.name)
        {
            case "0_Button":
                MagicStaticValue.GetInstance()._Level = 30;
                break;
            case "1_Button":
                MagicStaticValue.GetInstance()._Level = 40;
                break;
            case "2_Button":
                MagicStaticValue.GetInstance()._Level = 50;
                break;
            case "3_Button":
                MagicStaticValue.GetInstance()._Level = 60;
                break;
            case "4_Button":
                MagicStaticValue.GetInstance()._Level = 70;
                break;

            default:
                break;
        }
        SceneManager.LoadScene("Play");
    }
}
