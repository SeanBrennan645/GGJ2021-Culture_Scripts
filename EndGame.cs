using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            SceneManager.LoadScene("MenuScene");
        }
    }
}
