using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreController : MonoBehaviour
{
    
    public GameObject Canvas;
    public Text Score;
    public static ScoreController instance;
    [SerializeField] private float totalScore;
    private void Awake()
    {
        if (ScoreController.instance == null)
        {
            
            ScoreController.instance = this;
            DontDestroyOnLoad(this.gameObject);
            DontDestroyOnLoad(this.Canvas);
        }
        else
        {
            Destroy(gameObject);
            Destroy(this.Canvas);
        }

    }


    void Update()
    {
        Score.text = "Score: " + totalScore;
    }

    public void IncreasePoints(float amount)
    {
        totalScore += amount;
    }
}
