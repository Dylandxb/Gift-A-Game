using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectButton : MonoBehaviour
{
    public GameObject textDisplay;                  //Displays UI text "E"

    private bool playerInZone;                      //Check if player is in collider

    public GameObject wallMove;
    void Start()
    {
        playerInZone = false;
        textDisplay.SetActive(false);
        wallMove.SetActive(true);                               //Keeps walls active on start
    }


    void Update()
    {
        if (playerInZone && Input.GetKeyDown(KeyCode.E))
        {
            gameObject.GetComponent<Animator>().Play("press");
            gameObject.GetComponent<AudioSource>().Play();
            //GetComponent from a Wall Script to play an animation to move the wall down over a period of time
            StartCoroutine(wallDelay());
            
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            textDisplay.SetActive(true);
            playerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            textDisplay.SetActive(false);
            playerInZone = false;
        }
    }

    IEnumerator wallDelay()
    {
        yield return new WaitForSeconds(2f);                            //Waits 2 seconds for button animation to finish before executing
        wallMove.SetActive(false);                                      //Once the correct answer is solved, button press activates and clears the walls
        textDisplay.SetActive(false);
    }
}
