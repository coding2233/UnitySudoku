using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour {

    public GameObject MenuPanel;
    public GameObject LoadText;
    public Animator MenuAnim;

    void Awake()
    {
        Screen.SetResolution(1280, 720, false);
    }

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
        MenuAnim.SetBool("IsStart", true);
        Invoke("OpenScene", 2.5f);
    }

    void OpenScene()
    {
        LoadText.SetActive(true);
        SceneManager.LoadScene("Play");
    }
}
