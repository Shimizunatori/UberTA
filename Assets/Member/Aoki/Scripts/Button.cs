using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public void OnButtonClickStart()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnButtonClickTitleBack()
    {
        SceneManager.LoadScene("Title");
    }
}
