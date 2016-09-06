using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SudokuManager : MonoBehaviour {

    public GameObject _SudokuItem;
    public GameObject _SudokuPanel;

    public GameObject _SudokuNumberPanel;
    public Text _TimeText;


    SudokuInfor[,] _SudokuInfor;
    int _SudokuLength = 9;

    public int[] _Level = new int[] { 30, 40, 50, 60 };

    GameObject _LastSudokuBtn=null;

    // Use this for initialization
    void Start() {
        InitSudokuItem();
        AddListener();
    }

    float fTime = 0.0f;

    // Update is called once per frame
    void Update() {
        fTime += Time.fixedDeltaTime;
        _TimeText.text = (int)(fTime/60)+"m "+(int)(fTime%60)+"s";
    }


    void AddListener()
    {
        Button[] BtnKids = _SudokuNumberPanel.GetComponentsInChildren<Button>();
        for (int i = 0; i < BtnKids.Length; i++)
        {
            GameObject go = BtnKids[i].gameObject;
            BtnKids[i].onClick.AddListener(delegate () { ClickSudokuNumber(go); });
        }
    }

    void GetSukuInfor()
    {
        if (SudokuFactory.GetInstance().listSudoku.Count <= 0)
            return;

        _SudokuInfor = SudokuFactory.GetInstance().listSudoku[0];

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
        //SudokuFactory.GetInstance().listSudoku.RemoveAt(0);
        // SudokuFactory.GetInstance().GetSudoku(3);
    }

    void InitSudokuItem()
    {
        GetSukuInfor();
        if (_SudokuInfor == null)
            return;

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
                    go.transform.FindChild("Text").GetComponent<Text>().text = "";
                go.GetComponent<ItemIndex>().X = i;
                go.GetComponent<ItemIndex>().Y = j;

                go.GetComponent<Button>().onClick.AddListener(delegate () { ClickSudokuItem(go); });
            }
        }
    }

    void ClickSudokuItem(GameObject go)
    {
        _SudokuNumberPanel.transform.position = Input.mousePosition;
        _SudokuNumberPanel.SetActive(true);
        _LastSudokuBtn = go;
    }

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
    }

    bool CheckNumber(int X,int Y,int iNumber)
    {
        if (_SudokuInfor == null)
            return false;

        SudokuInfor tempSudoku= _SudokuInfor[X, Y];
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
}
