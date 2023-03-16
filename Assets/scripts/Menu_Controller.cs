using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteKey("score");
        PlayerPrefs.DeleteKey("wave");
        PlayerPrefs.DeleteKey("score");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
