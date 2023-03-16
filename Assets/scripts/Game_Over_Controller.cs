using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game_Over_Controller : MonoBehaviour
{
    public TextMeshProUGUI deadText;
    // Start is called before the first frame update
    void Start()
    {
        deadText.text = "Score: " + PlayerPrefs.GetInt("score").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
