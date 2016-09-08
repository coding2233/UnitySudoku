using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour {

    public GameObject MenuPanel;
    public GameObject LoadText;
    public Animator MenuAnim;

    public AudioSource _AudioSource;

    #region 设置屏幕分辨率
    private int scaleWidth = 0;
    private int scaleHeight = 0;
    void setDesignContentScale()
    {
        if (scaleWidth == 0 && scaleHeight == 0)
        {
            int width = Screen.currentResolution.width;
            int height = Screen.currentResolution.height;
            int designWidth = 1280;
            int designHeight = 720;
            float s1 = (float)designWidth / (float)designHeight;
            float s2 = (float)width / (float)height;
            if (s1 < s2)
            {
                designWidth = (int)Mathf.FloorToInt(designHeight * s2);
            }
            else if (s1 > s2)
            {
                designHeight = (int)Mathf.FloorToInt(designWidth / s2);
            }
            float contentScale = (float)designWidth / (float)width;
            if (contentScale < 1.0f)
            {
                scaleWidth = designWidth;
                scaleHeight = designHeight;
            }
        }
        if (scaleWidth > 0 && scaleHeight > 0)
        {
            if (scaleWidth % 2 == 0)
            {
                scaleWidth += 1;
            }
            else {
                scaleWidth -= 1;
            }
            Screen.SetResolution(scaleWidth, scaleHeight, true);
        }
    }
    #endregion

    void Awake()
    {
        setDesignContentScale();
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
        if (!_AudioSource.isPlaying)
            _AudioSource.Play();

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
        Invoke("OpenScene", 3.2f);
    }

    void OpenScene()
    {
        LoadText.SetActive(true);
        SceneManager.LoadScene("Play");
    }
}
