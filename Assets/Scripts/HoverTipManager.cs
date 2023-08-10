using System;
using UnityEngine;
using TMPro;

public class HoverTipManager : MonoBehaviour
{
    public TextMeshProUGUI tipText;
    public RectTransform tipWindow;

    public static Action<string, Vector2> onMouseHover; //String is message displayed, mouse hover is where its displayed
    public static Action onMouseLoseFocus;

    // Whenver onMouseHover is called, show the tip
    private void OnEnable()
    {
        onMouseHover += ShowTip;            //Subscribe showTip to onMouseHover Action
        onMouseLoseFocus += HideTip;        //Subscribe HideTip to onMouseLoseFocus Action
    }

    private void OnDisable()    //Unsubscribe to assure there will be no null reference exceptions
    {
        onMouseHover -= ShowTip;
        onMouseLoseFocus -= HideTip;
    }


    // Start is called before the first frame update
    void Start()
    {
        HideTip(); 
    }

    private void ShowTip(string tip, Vector2 mousePos)
    {
        tipText.text = tip;
        tipWindow.sizeDelta = new Vector2(tipText.preferredWidth > 300 ? 300 : tipText.preferredWidth, tipText.preferredHeight);

        tipWindow.gameObject.SetActive(true);
        if(mousePos.x + 300 >= Screen.width) // If near the right edge of screen flip tip so it does not go over the screen
        {            
            //Tip towards left
            tipWindow.transform.position = new Vector2(mousePos.x - tipWindow.sizeDelta.x / 2, mousePos.y); //Move the tip slightly so the mouse does not cover it
        }
        else
        {
            // Tip Towards right
            tipWindow.transform.position = new Vector2(mousePos.x + tipWindow.sizeDelta.x / 2, mousePos.y); //Move the tip slightly so the mouse does not cover it
        }

    }
    private void HideTip()
    {
        tipText.text = default; //Set to null
        tipWindow.gameObject.SetActive(false); //disables tip at start
    }
}
