using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SudokuManager : MonoBehaviour {

    public GameObject _SudokuItem;
    public GameObject _SudokuPanel;

    public GameObject _SudokuNumberPanel;
    public Text _TimeText;
    public Text _StepText;

    public Button[] _ArrayMenuBtn;

    public Text _HintText;

    public GameObject _GameOverPanel;

    SudokuInfor[,] _SudokuInfor;
    GameObject[,] _SudokuItemInfor;

    int[,] _OldSudokuInfor;
    int _SudokuLength = 9;

    GameObject _LastSudokuBtn = null;

    GameObject[] _ArraySudokuItem;

    void Awake()
    {
        Screen.SetResolution(1280, 720, false);
    }

    // Use this for initialization
    void Start() {
        Init();
        AddListener();
    }

    #region 初始函数
    void Init()
    {
        _LastSudokuBtn = null;
        fTime = 0.0f;
        _iSteps = 0;
        _fPercent = 0.0f;

        _TimeText.text = "";
        _StepText.text = "0steps\n0%";

        if (MagicStaticValue.GetInstance()._HintCount > 0)
            _HintText.text = "+" + MagicStaticValue.GetInstance()._HintCount;
        else
            _HintText.text = "";


        _SudokuNumberPanel.SetActive(false);

        InitSudokuItem();
    }
    #endregion

    float fTime = 0.0f;
    // Update is called once per frame
    void Update() {

        //计时
        fTime += Time.deltaTime;
        if ((int)(fTime / 60) > 0)
        {
            _TimeText.text = (int)(fTime / 60) + "minutes\n" + (int)(fTime % 60) + "seconds";
        }
        else
        {
            _TimeText.text = (int)(fTime % 60) + "seconds";
        }
    }

    #region 添加监听
    void AddListener()
    {
        Button[] BtnKids = _SudokuNumberPanel.GetComponentsInChildren<Button>();
        for (int i = 0; i < BtnKids.Length; i++)
        {
            GameObject go = BtnKids[i].gameObject;
            BtnKids[i].onClick.AddListener(delegate () { ClickSudokuNumber(go); });
        }

        _SudokuPanel.GetComponent<Button>().onClick.AddListener(ClickBgPanel);

        for (int i = 0; i < _ArrayMenuBtn.Length; i++)
        {
            GameObject go = _ArrayMenuBtn[i].gameObject;
            _ArrayMenuBtn[i].onClick.AddListener(delegate () { ClickMenuBtn(go); });
        }
    }
    #endregion

    #region 点击菜单按钮
    void ClickMenuBtn(GameObject go)
    {
        switch (go.name)
        {
            case "0_Button":
                HintSudoku();
                break;

            case "1_Button":
                if (_GameOverPanel.activeSelf)
                    _GameOverPanel.SetActive(false);
                Init();
                break;

            case "2_Button":
                SceneManager.LoadScene("Start");
                break;

            case "3_Button":
                Application.Quit();
                break;

            default:
                break;
        }
    }
    #endregion

    #region 点击背景面板
    void ClickBgPanel()
    {
        _SudokuNumberPanel.SetActive(false);
    }
    #endregion

    #region 获取数独
    void GetSukuInfor()
    {
        if (SudokuFactory.GetInstance().listSudoku.Count <= 0)
            return;

        _SudokuInfor = SudokuFactory.GetInstance().listSudoku[0];

        _OldSudokuInfor = new int[_SudokuLength, _SudokuLength];
        for (int i = 0; i < _SudokuLength; i++)
        {
            for (int j = 0; j < _SudokuLength; j++)
            {
                _OldSudokuInfor[i, j] = _SudokuInfor[i, j].Num;
            }
        }

        //int tempLevel = _Level[Random.Range(0, _Level.Length)];
        int tempLevel = MagicStaticValue.GetInstance()._Level;
        Debug.Log("困难等级:" + tempLevel);

        for (int i = 0; i < tempLevel; i++)
        {
            int tempX = Random.Range(0, _SudokuLength);
            int tempY = Random.Range(0, _SudokuLength);
            if (_SudokuInfor[tempX, tempY].Num != 0)
                _SudokuInfor[tempX, tempY].Num = 0;
            else
                i--;
        }

        SudokuFactory.GetInstance().listSudoku.RemoveAt(0);
        SudokuFactory.GetInstance().GetSudoku(3);
    }
    #endregion


    #region 初始化数独
    void InitSudokuItem()
    {
        GetSukuInfor();
        if (_SudokuInfor == null)
            return;

        _ArraySudokuItem = new GameObject[MagicStaticValue.GetInstance()._Level];
        int AllItemIndex = 0;

        if (_SudokuItemInfor == null)
        {
            _SudokuItemInfor = new GameObject[_SudokuLength, _SudokuLength];

            for (int i = 0; i < _SudokuLength; i++)
            {
                for (int j = 0; j < _SudokuLength; j++)
                {
                    GameObject go = Instantiate(_SudokuItem);
                    go.transform.parent = _SudokuPanel.transform;
                    go.transform.localScale = Vector3.one;
                    if (_SudokuInfor[i, j].Num != 0)
                    {
                        go.GetComponent<Button>().enabled = false;
                        go.transform.FindChild("Text").GetComponent<Text>().text = _SudokuInfor[i, j].Num.ToString();
                    }
                    else
                    {
                        go.transform.FindChild("Text").GetComponent<Text>().text = "";
                        _ArraySudokuItem[AllItemIndex] = go;
                        AllItemIndex++;
                    }
                    go.GetComponent<ItemIndex>().X = i;
                    go.GetComponent<ItemIndex>().Y = j;

                    go.GetComponent<Button>().onClick.AddListener(delegate () { ClickSudokuItem(go); });

                    _SudokuItemInfor[i, j] = go;
                }
            }
        }
        else
        {
            for (int i = 0; i < _SudokuLength; i++)
            {
                for (int j = 0; j < _SudokuLength; j++)
                {
                    GameObject go = _SudokuItemInfor[i, j];
                    if (_SudokuInfor[i, j].Num != 0)
                    {
                        go.GetComponent<Button>().enabled = false;
                        go.transform.FindChild("Text").GetComponent<Text>().text = _SudokuInfor[i, j].Num.ToString();
                        go.transform.FindChild("Text").GetComponent<Text>().color = Color.black;
                    }
                    else
                    {
                        go.GetComponent<Button>().enabled = true;
                        go.transform.FindChild("Text").GetComponent<Text>().text = "";
                        _ArraySudokuItem[AllItemIndex] = go;
                        AllItemIndex++;
                    }
                    go.GetComponent<ItemIndex>().PlayRotationAnimation();
                }
            }
        }
    }
    #endregion

    #region 点击数独空格
    void ClickSudokuItem(GameObject go)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount > 0)
            {
                _SudokuNumberPanel.transform.position = Input.GetTouch(0).position;
            }
        }
        else
        {
            _SudokuNumberPanel.transform.position = Input.mousePosition;
        }
        _SudokuNumberPanel.SetActive(true);
        _LastSudokuBtn = go;
    }
    #endregion

    #region 点击数字
    int _iSteps = 0;
    float _fPercent = 0.0f;
    void ClickSudokuNumber(GameObject go)
    {
        if (_LastSudokuBtn == null)
            return;

        _LastSudokuBtn.transform.FindChild("Text").GetComponent<Text>().text = go.name.Trim();
        _LastSudokuBtn.transform.FindChild("Text").GetComponent<Text>().color = go.GetComponent<Image>().color;

        ItemIndex tempItemIndex = _LastSudokuBtn.GetComponent<ItemIndex>();

        if (CheckNumber(tempItemIndex.X, tempItemIndex.Y, int.Parse(go.name.Trim())))
            tempItemIndex.isRotation = false;
        else
            tempItemIndex.isRotation = true;

        _SudokuNumberPanel.SetActive(false);

        _iSteps++;
        _fPercent = CheckFinish() * 100.0f;

        _StepText.text = _iSteps + "steps\n" + (int)_fPercent + "%";

        _LastSudokuBtn = null;

        if ((int)(_fPercent) == 100)
        {
            ShowGameOverPanel();
        }
    }
    #endregion

    #region 检查当前数字是否符合要求
    bool CheckNumber(int X, int Y, int iNumber)
    {
        if (_SudokuInfor == null)
            return false;

        SudokuInfor tempSudoku = _SudokuInfor[X, Y];
        for (int i = 0; i < 9; i++)
        {
            Vector2 tempVec2 = tempSudoku.array01[i];
            SudokuInfor tempAboutSudoku = _SudokuInfor[(int)tempVec2.x, (int)tempVec2.y];

            if (tempAboutSudoku != tempSudoku
                && tempAboutSudoku.Num == iNumber)
                return false;

            tempVec2 = tempSudoku.array02[i];
            tempAboutSudoku = _SudokuInfor[(int)tempVec2.x, (int)tempVec2.y];

            if (tempAboutSudoku != tempSudoku
                && tempAboutSudoku.Num == iNumber)
                return false;

            tempVec2 = tempSudoku.array03[i];
            tempAboutSudoku = _SudokuInfor[(int)tempVec2.x, (int)tempVec2.y];

            if (tempAboutSudoku != tempSudoku
                && tempAboutSudoku.Num == iNumber)
                return false;
        }

        tempSudoku.Num = iNumber;

        return true;
    }
    #endregion

    #region 检查完成进度
    float CheckFinish()
    {
        if (_ArraySudokuItem.Length <= 0)
            return 0.0f;

        float index = 0.0f;

        for (int i = 0; i < _ArraySudokuItem.Length; i++)
        {
            ItemIndex tempItem = _ArraySudokuItem[i].GetComponent<ItemIndex>();

            if (_SudokuInfor[tempItem.X, tempItem.Y].Num != 0 && !tempItem.isRotation
                && _ArraySudokuItem[i].transform.FindChild("Text").GetComponent<Text>().text != "")
            {
                index++;
            }
        }

        return index / (float)_ArraySudokuItem.Length;
    }
    #endregion

    #region 数独提示
    void HintSudoku()
    {
        if (_LastSudokuBtn == null || _OldSudokuInfor == null
            || MagicStaticValue.GetInstance()._HintCount <= 0)
            return;

        Text tempText = _LastSudokuBtn.transform.FindChild("Text").GetComponent<Text>();
        ItemIndex tempItem = _LastSudokuBtn.GetComponent<ItemIndex>();
        if (tempText.text == "" || tempItem.isRotation)
        {
            Debug.Log(tempItem.X + "  " + tempItem.Y);
            tempText.text = _OldSudokuInfor[tempItem.X, tempItem.Y].ToString();
            tempText.color = Color.white / 2.0f;

            if (tempItem.isRotation)
                tempItem.isRotation = false;
        }

        _SudokuNumberPanel.SetActive(false);

        MagicStaticValue.GetInstance()._HintCount--;

        if (MagicStaticValue.GetInstance()._HintCount > 0)
            _HintText.text = "+" + MagicStaticValue.GetInstance()._HintCount;
        else
        {
            MagicStaticValue.GetInstance()._HintCount = 0;
            _HintText.text = "";
        }
    }
    #endregion

    #region 显示结果面板
    void ShowGameOverPanel()
    {
        _GameOverPanel.transform.FindChild("ScoreText").GetComponent<Text>().text = (int)(fTime / 60) + "minutes" + (int)(fTime % 60) + "seconds\n"+_iSteps + "steps";
        _GameOverPanel.SetActive(true);
    }
    #endregion

}
