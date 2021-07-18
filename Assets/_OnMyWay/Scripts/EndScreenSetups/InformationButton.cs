using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationButton : MonoBehaviour
{
    public GameObject informationPanel;
    private ClickableElement clickableElement;

    private EndScreenController endScreenController;

    // Start is called before the first frame update
    void Start()
    {
        informationPanel?.SetActive(false);
        clickableElement = GetComponent<ClickableElement>();
        clickableElement.onPointerUpCallback += OnClick;

        endScreenController = FindObjectOfType<EndScreenController>(true);
    }

    private void OnClick()
    {
        endScreenController.CloseAllInformationPanels();
        this.SetPanelActive(true);
    }

    public void SetPanelActive(bool value) {
        informationPanel.SetActive(value);
    }
}
