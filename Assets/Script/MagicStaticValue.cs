using UnityEngine;
using System.Collections;

public class MagicStaticValue {

    /// <summary>
    /// 困难等级 30(简单) 40(一般) 50(困难) 60(疯狂) 70(烧脑)
    /// </summary>
    public int _Level=30;
    /// <summary>
    /// 提示次数
    /// </summary>
    public int _HintCount = 10;

    private static MagicStaticValue staticValue;

    public static MagicStaticValue GetInstance()
    {
        if (staticValue == null)
            staticValue = new MagicStaticValue();
        return staticValue;
    }

}
