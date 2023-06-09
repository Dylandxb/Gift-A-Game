using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonToggle : MonoBehaviour
{
    public GameObject textDisplay;                  //Displays UI text "E"

    private bool playerInZone;                      //Check if player is in collider


    void Start()
    {
        playerInZone = false;                       //Set to false until user is in zone
        textDisplay.SetActive(false);
    }

    
    void Update()
    {
        if(playerInZone && Input.GetKeyDown(KeyCode.E))
        {
            gameObject.GetComponent<Animator>().Play("press");              //When in zone and Pressing E key, trigger button animation and play audio clip
            gameObject.GetComponent<AudioSource>().Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")                           //Only set Text game Object to true when the "Player" tag is in the collider trigger
        {
            textDisplay.SetActive(true);
            playerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            textDisplay.SetActive(false);                       //Disable UI when not in range
            playerInZone = false;
        }
    }
}
