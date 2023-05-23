using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;

public class LeaderBoard : MonoBehaviour
{
    int leaderboardID = 14305;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SubmitScoreToTable(int score)
    {
        StartCoroutine(SubmitScoreRoutine(score));
    }

    public IEnumerator SubmitScoreRoutine(int scoreToUpload)
    {
        print("Checking...");
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, leaderboardID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Success, Score uploaded");
                done = true;
            }
            else
            {
                Debug.Log("Failed" + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}
