using UnityEngine;
using System.Collections.Generic;

public class SudokuFactory {

    int ArrayCout = 9;
    SudokuInfor[,] arrayInfor;

    [HideInInspector]
    public List<SudokuInfor[,]> listSudoku = new List<SudokuInfor[,]>();

    #region 外部调用
    private static SudokuFactory Sudoku;
    public static SudokuFactory GetInstance()
    {
        if (Sudoku == null)
        {
            Sudoku = new SudokuFactory();
            Sudoku.GetSudoku(3);
        }
        return Sudoku;
    }
    #endregion

    #region 获取数独
    /// <summary>
    /// 获取数独
    /// </summary>
    /// <param name="MaxCout">列表中的最大值</param>
    public void GetSudoku(int MaxCout)
    {
        while (listSudoku.Count < MaxCout)
        {
            CalcCout = 0;
            CreateArray();
            InitArray();
            CalcArray();
        }
    }
    #endregion

    #region 创建信息的二维数组
    void CreateArray()
    {
        arrayInfor = new SudokuInfor[ArrayCout,ArrayCout];
        for (int i = 0; i < ArrayCout; i++)
        {
            for (int j = 0; j < ArrayCout; j++)
            {
                arrayInfor[i, j] = new SudokuInfor();

                //创建二维数组
                arrayInfor[i, j].ID = new Vector2(i, j);

                arrayInfor[i, j].array01 = new Vector2[9];
                arrayInfor[i, j].array02 = new Vector2[9];
                arrayInfor[i, j].array03 = new Vector2[9];
                
                //添加每行每列的数组
                for (int m = 0; m < 9; m++)
                {
                    arrayInfor[i, j].array01[m] = new Vector2(i, m);
                    arrayInfor[i, j].array02[m] = new Vector2(m, j);
                }

                //添加每个宫格的数组
                int p = i-(i % 3);
                int q = j - (j % 3);
                int n = 0;
                for (int k = 0; k < 3; k++)
                {
                    for (int l = 0; l < 3; l++)
                    {
                        arrayInfor[i, j].array03[n] = new Vector2(p + k, q + l);
                        n++;
                    }
                }
            }
        }
    }
    #endregion

    #region 初始化数组
    void InitArray()
    {
        for (int i = 0; i < ArrayCout; i++)
        {
            for (int j = 0; j < ArrayCout; j++)
            {
                if (arrayInfor[i, j].tempNum.Count <= 0)
                {
                    continue;
                }

                int index = Random.Range(0, arrayInfor[i,j].tempNum.Count);
                int tempNum= arrayInfor[i,j].tempNum[index];
                arrayInfor[i, j].Num = tempNum;
                for (int m = 0; m < 9; m++)
                {
                    Vector2 tempVec2 = arrayInfor[i, j].array01[m];
                    SudokuInfor tempInfor = arrayInfor[(int)tempVec2.x, (int)tempVec2.y];
                
                    for (int n = 0; n < tempInfor.tempNum.Count; n++)
                    {
                        if (tempInfor.tempNum[n] == tempNum)
                        {
                            tempInfor.tempNum.RemoveAt(n);
                        }
                    }

                    tempVec2 = arrayInfor[i, j].array02[m];
                    tempInfor = arrayInfor[(int)tempVec2.x, (int)tempVec2.y];
                 
                    for (int n = 0; n < tempInfor.tempNum.Count; n++)
                    {
                        if (tempInfor.tempNum[n] == tempNum)
                        {
                            tempInfor.tempNum.RemoveAt(n);
                        }
                    }

                    tempVec2 = arrayInfor[i, j].array03[m];
                    tempInfor = arrayInfor[(int)tempVec2.x, (int)tempVec2.y];
                   
                    for (int n = 0; n < tempInfor.tempNum.Count; n++)
                    {
                        if (tempInfor.tempNum[n] == tempNum)
                        {
                            tempInfor.tempNum.RemoveAt(n);
                        }
                    }
                }
            }
        }
    }
    #endregion


    #region 回溯计算
    int CalcCout = 0;
    void CalcArray()
    {
        CalcCout++;
        //限制回溯次数  解决堆栈溢出
        if (CalcCout >= 100)
        {
            Debug.Log("计算失败,回溯次数超过100次");
            return;
        }

        bool reStart = false;


        List<Vector2> ZeroIndex = new List<Vector2>();
        for (int i = 0; i < ArrayCout; i++)
        {
            for (int j = 0; j < ArrayCout; j++)
            {
                if (arrayInfor[i, j].Num == 0)
                {
                    ZeroIndex.Add(new Vector2(i, j));
                }
            }
        }


        for (int i = 0; i < ZeroIndex.Count; i++)
        {
            for (int m = 0; m < 9; m++)
            {
                Vector2 tempVec2 = arrayInfor[(int)ZeroIndex[i].x, (int)ZeroIndex[i].y].array01[m];
                SudokuInfor tempInfor = arrayInfor[(int)tempVec2.x, (int)tempVec2.y];

                tempInfor.Num = 0;
                tempInfor.tempNum = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

                tempVec2 = arrayInfor[(int)ZeroIndex[i].x, (int)ZeroIndex[i].y].array02[m];
                tempInfor = arrayInfor[(int)tempVec2.x, (int)tempVec2.y];

                tempInfor.Num = 0;
                tempInfor.tempNum = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

                tempVec2 = arrayInfor[(int)ZeroIndex[i].x, (int)ZeroIndex[i].y].array03[m];
                tempInfor = arrayInfor[(int)tempVec2.x, (int)tempVec2.y];

                tempInfor.Num = 0;
                tempInfor.tempNum = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            }
        }

        for (int i = 0; i < ArrayCout; i++)
        {
            for (int j = 0; j < ArrayCout; j++)
            {
                int tempNum = arrayInfor[i, j].Num;
                if (arrayInfor[i,j].Num==0)
                {
                    if (arrayInfor[i, j].tempNum.Count <= 0|| arrayInfor[i, j].tempNum==null)
                    {
                        reStart = true;
                        continue;
                    }
                    int index = Random.Range(0, arrayInfor[i,j].tempNum.Count);
                    tempNum = arrayInfor[i, j].tempNum[index];
                    arrayInfor[i, j].Num = tempNum;
                }

                for (int m = 0; m < 9; m++)
                {
                    Vector2 tempVec2 = arrayInfor[i, j].array01[m];
                    SudokuInfor tempInfor = arrayInfor[(int)tempVec2.x, (int)tempVec2.y];

                    for (int n = 0; n < tempInfor.tempNum.Count; n++)
                    {
                        if (tempInfor.tempNum[n] == tempNum)
                        {
                            tempInfor.tempNum.RemoveAt(n);
                        }
                    }

                    tempVec2 = arrayInfor[i, j].array02[m];
                    tempInfor = arrayInfor[(int)tempVec2.x, (int)tempVec2.y];

                    for (int n = 0; n < tempInfor.tempNum.Count; n++)
                    {
                        if (tempInfor.tempNum[n] == tempNum)
                        {
                            tempInfor.tempNum.RemoveAt(n);
                        }
                    }

                    tempVec2 = arrayInfor[i, j].array03[m];
                    tempInfor = arrayInfor[(int)tempVec2.x, (int)tempVec2.y];

                    for (int n = 0; n < tempInfor.tempNum.Count; n++)
                    {
                        if (tempInfor.tempNum[n] == tempNum)
                        {
                            tempInfor.tempNum.RemoveAt(n);
                        }
                    }
                }
            }
        }

        if (reStart)
        {
            CalcArray();
        }
        else
        {
            //添加数独到列表中
            listSudoku.Add(arrayInfor);
            Debug.Log("计算成功,回溯次数:" + (CalcCout + 1));
        }

    }
    #endregion

    #region 最开始的显示测试
    //void OnGUI()
    //{
    //    if (arrayInfor == null)
    //        return;
    //    if (listSudoku.Count <= 0)
    //        return;

    //    for (int i = 0; i < ArrayCout; i++)
    //    {
    //        for (int j = 0; j < ArrayCout; j++)
    //        {
    //            // GUI.Label(new Rect(260+i * 35, 100+j * 35, 30, 30), arrayInfor[i, j].Num.ToString());
    //            GUI.Label(new Rect(260 + i * 35, 100 + j * 35, 30, 30), listSudoku[0][i,j].Num.ToString());
    //        }
    //    }
    //}
    #endregion

}
