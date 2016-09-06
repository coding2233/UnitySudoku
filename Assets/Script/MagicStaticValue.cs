using UnityEngine;
using System.Collections;

public class MagicStaticValue {

    public int _Level=30;

    private static MagicStaticValue staticValue;

    public static MagicStaticValue GetInstance()
    {
        if (staticValue == null)
            staticValue = new MagicStaticValue();
        return staticValue;
    }

}
