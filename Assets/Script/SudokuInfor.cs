using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SudokuInfor  {
    /// <summary>
    /// 二维数组索引ID
    /// </summary>
    public Vector2 ID = new Vector2();
    /// <summary>
    /// 数值
    /// </summary>
    public int Num=0;
    /// <summary>
    /// 行
    /// </summary>
    public Vector2[] array01=new Vector2[] { };
    /// <summary>
    /// 列
    /// </summary>
    public Vector2[] array02 = new Vector2[] { };
    /// <summary>
    /// 宫
    /// </summary>
    public Vector2[] array03 = new Vector2[] { };
    /// <summary>
    /// 备用可用值
    /// </summary>
    public List<int> tempNum=new List<int>() { 1,2,3,4,5,6,7,8,9};
}
