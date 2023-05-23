using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LootLocker.Requests;

public class ArcadeScoreboard : MonoBehaviour
{
    public InputField MemberID, PlayerScore;
    public int ID;
    int MaxScores = 7;
    public Text[] Entries;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoginRoutine());
    }


    IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Success, Player logged in");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.Log("Could not Start session");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    public void ShowScores()
    {
        LootLockerSDKManager.GetScoreList(ID, MaxScores, (response) =>
          {
              if (response.success)
              {
                  LootLockerLeaderboardMember[] scores = response.items;

                  for (int i = 0; i < scores.Length; i++)
                  {
                      Entries[i].text = (scores[i].rank +".   "+ scores[i].member_id + "    " + scores[i].score);
                  }

                  if (scores.Length < MaxScores)
                  {
                      for (int i = scores.Length; i < MaxScores; i++)
                      {
                          Entries[i].text = (i + 1).ToString() + ".    none";
                      }
                  }
              }
              else
              {
                  Debug.Log("Failed");
              }
          });
    }

    // Update is called once per frame
    public void SubmitScore()
    {
        LootLockerSDKManager.SubmitScore(MemberID.text, int.Parse(PlayerScore.text), ID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully submitted");
            }
            else
            {
                Debug.Log("could not submit your score..." + response.Error);
            }
        });
    }
}
