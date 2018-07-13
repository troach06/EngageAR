using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectTap : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                if (raycastHit.collider.name == "Abigail")
                {
                    GetComponent<AudioSource>().Play();
                    SceneManager.LoadScene("Abigail");
                }
                if (raycastHit.collider.name == "Britney")
                {
                    GetComponent<AudioSource>().Play();
                    SceneManager.LoadScene("Britney");
                }
                if (raycastHit.collider.name == "Cindy")
                {
                    GetComponent<AudioSource>().Play();
                    SceneManager.LoadScene("Cindy");
                }
            }
        }
    }
}
