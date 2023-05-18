using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreController : MonoBehaviour
{
    
    public GameObject Canvas;
    public GameObject Leader;
    public LeaderBoard leaderboard;
    public Text Score;
    public static ScoreController instance;
    public bool allLevelsCleared = false;
    [SerializeField] public float totalScore;
    private void Awake()
    {
        if (ScoreController.instance == null)
        {
            
            //Se crea una instancia del Scorecontroller para luego ponerlo en la función
            //DontDestroyOnload que es una funcion de Unity para no destruir el objeto cuando
            //cargue una nueva escena y asi sigan conservando los punto que ha hecho el jugador
            ScoreController.instance = this;
            DontDestroyOnLoad(this.gameObject);
            DontDestroyOnLoad(this.Canvas);
            DontDestroyOnLoad(this.Leader);

            
        }
        else
        {
            Destroy(gameObject);
            Destroy(this.Canvas);
            Destroy(this.Leader);
        }

    }


    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        Score.text = "Score: " + totalScore;
        if (scene.buildIndex == 0 && allLevelsCleared == true)
        {
            totalScore = 0;
            allLevelsCleared = false;
        }
        else if (scene.buildIndex == 1 && allLevelsCleared == true)
        {
            print("Has ganado el nivel");
            SubmitScore();
        }
    }

    public void ResetPoints()
    {
        totalScore = 0;
    }
    public void IncreasePoints(float amount)
    {
        totalScore += amount;
    }

    public void AllLevelsCleared()
    {
        allLevelsCleared = true;
    }

    public void SubmitScore()
    {
        int integerScore = (int)totalScore;
        leaderboard.SubmitScoreRoutine(integerScore);

    }
}
