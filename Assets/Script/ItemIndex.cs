using UnityEngine;
using System.Collections;

public class ItemIndex : MonoBehaviour {
    [HideInInspector]
    public int X;
    [HideInInspector]
    public int Y;
    [HideInInspector]
    public bool isRotation = false;
    [HideInInspector]
    GameObject goText = null;

    void Update()
    {
        if (goText == null)
            goText = transform.FindChild("Text").gameObject;
       
        if (isRotation)
        {
            goText.transform.Rotate(Vector3.back, 30 * Time.deltaTime*5);
        }
        else
        {
            goText.transform.localEulerAngles = Vector3.zero;
        }
    }
}
