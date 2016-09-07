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

    bool StartAnim = true;

    float fSpeed = 5.0f;

    void Start()
    {
        PlayRotationAnimation();
    }

    public void PlayRotationAnimation()
    {
        isRotation = true;
        fSpeed = 10.0f;
        Invoke("StopRotation", 1.2f);
    }

    void StopRotation()
    {
        fSpeed = 5.0f;
        isRotation = false;
    }

    void Update()
    {
        if (goText == null)
            goText = transform.FindChild("Text").gameObject;
       
        if (isRotation)
        {
            goText.transform.Rotate(Vector3.back, 30 * Time.deltaTime* fSpeed);
        }
        else
        {
            goText.transform.localEulerAngles = Vector3.zero;
        }
    }
}
