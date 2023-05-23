using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public TMP_InputField playerNameInputfield;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoginRoutine());
    }

    public void SetPlayerName()
    {
        LootLockerSDKManager.SetPlayerName(playerNameInputfield.text, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully set player name...");
                SceneManager.LoadScene("Level1");
            }
            else
            {
                Debug.Log("could not set player name"+response.Error);
            }
        });
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
        yield return new WaitWhile(()=> done == false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
