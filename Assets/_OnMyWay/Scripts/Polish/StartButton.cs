using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public string firstGameScene = "Scene_01";

    ClickableElement clickableElement;

    FadeTween fade;

    // Start is called before the first frame update
    void Start()
    {
        fade = FindObjectOfType<FadeTween>(true);

        clickableElement = GetComponent<ClickableElement>();
        clickableElement.onPointerUpCallback += StartGame;
    }


    public void StartGame()
    {
        if (fade != null)
        {
            fade.StartFading(this.StartNextScene);
        }
        else
        {
            StartNextScene();
        }
    }

    private void StartNextScene()
    {
        SceneManager.LoadScene(firstGameScene);
    }
}
