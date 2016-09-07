using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TestAudio : MonoBehaviour {

    public Color[] ArrayColor;
    public GameObject ImageItem;
    public GameObject ImagePanel;
    GameObject[] ArrayItem;

    new public AudioSource audio;

    int ArraySize = 64;

	// Use this for initialization
	void Start () {
        ArrayItem = new GameObject[ArraySize];
        spectrum = new float[ArraySize];
        for (int i = 0; i < ArraySize; i++)
        {
            ArrayItem[i] = Instantiate(ImageItem);
            ArrayItem[i].transform.parent = ImagePanel.transform;
            // ArrayItem[i].GetComponent<Image>().color = ArrayColor[Random.Range(0, ArrayColor.Length)];
            ArrayItem[i].transform.localScale = Vector3.one;
            ArrayItem[i].SetActive(true);
        }
    }

    float[] spectrum;
    // Update is called once per frame
    void Update () {
        audio.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
        for (int i = 0; i < ArraySize; i++)
        {
            float ScaleValue = Mathf.Clamp01(spectrum[i] * 100.0f);
            iTween.ScaleTo(ArrayItem[i], new Vector3(1, ScaleValue, 1), 0.1f);
        }
    }
}
