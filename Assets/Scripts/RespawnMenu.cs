using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnMenu : MonoBehaviour
{

    public static bool PlayerIsDead = false;
    public GameObject respawnMenuUI;

    // Update is called once per frame
    void Update () {
 
        if (GameObject.FindGameObjectsWithTag("Player").Length == 0)
        {
            Respawn();
        }
    }

    void Respawn ()
    {
        respawnMenuUI.SetActive(true);
        PlayerIsDead = true;
    }

    public void RespawnButton ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerIsDead = false;
    }

    public void returnToMainNext ()
    {
        SceneManager.LoadScene(0);
        PlayerIsDead = false;
    }
}
