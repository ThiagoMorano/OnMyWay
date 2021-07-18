using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplayButton : MonoBehaviour
{
    ClickableElement clickableElement;

    // Start is called before the first frame update
    void Start()
    {
        clickableElement = GetComponent<ClickableElement>();
        clickableElement.onPointerUpCallback = RestartGame;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("Scene_01");
    }
}
