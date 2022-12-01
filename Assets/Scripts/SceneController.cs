using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject losePopup;

    [SerializeField] private GameObject winPopup;

    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject player;

    //[SerializeField] private SceneController sceneController;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        OpenWinPopup();
        OpenLosePopup();
    }

    
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    void OpenWinPopup()
    {
        if(boss == null)
        {
            winPopup.SetActive(true);
        }
    }

    void OpenLosePopup()
    {
        if(player == null)
        {
            losePopup.SetActive(true);
        }
    }
}
