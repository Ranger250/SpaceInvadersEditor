using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Controller : MonoBehaviour
{

    public void onButtonClick(int scene_num)
    {
        SceneManager.LoadScene(scene_num);
    }

    public void OnExitClick()
    {
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
