using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            SceneManager.LoadScene("Victory");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
