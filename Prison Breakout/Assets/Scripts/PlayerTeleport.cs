using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    public Transform teleportDestination;
    public CharacterController characterController;

    private bool isTeleporting = false;

    private void OnTriggerEnter(Collider other) //Checks if player enters collider
    {
        if (!isTeleporting && other.CompareTag("Player")) //If not teleporting already and player has entered
        {
            TeleportPlayer(other.GetComponent<CharacterController>()); //Run teleportplayer function
        }
    }

    private void TeleportPlayer(CharacterController playerController)
    {
        if (playerController != null) //If player exists
        {
            playerController.enabled = false; //Disable playercontroller

            playerController.transform.position = teleportDestination.position; //Teleport playercontroller

            Invoke("EnablePlayer", 0.1f); //Enable player shortly after teleport

            isTeleporting = true; //doesnt allow multiple teleportation
        }
    }

    private void EnablePlayer()
    {
        characterController.enabled = true; //Enable playercontroller
        isTeleporting = false; //allows future teleportation
    }
}