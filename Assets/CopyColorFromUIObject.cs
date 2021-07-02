using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopyColorFromUIObject : MonoBehaviour
{
    public bool red;
    public bool green;
    public bool blue;
    public bool alpha;
    public Image imageToCopyFrom;

    Image image;
    Color newColor;
    Color defaultColor;


    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        defaultColor = image.color;
    }

    // Update is called once per frame
    void Update()
    {
        float redValue = red? imageToCopyFrom.color.r : defaultColor.r;
        float blueValue = blue? imageToCopyFrom.color.b : defaultColor.b;
        float greenValue = green? imageToCopyFrom.color.g : defaultColor.g;
        float alphaValue = alpha? imageToCopyFrom.color.a : defaultColor.a;
        image.color = new Color(redValue, blueValue, greenValue, alphaValue);
    }
}
