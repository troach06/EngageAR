using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FaceUser : MonoBehaviour
{
    Vector3 prePos;
    float preY;
    bool canTurn = true;
    // Use this for initialization
    void Start()
    {
        Quaternion r1 = Quaternion.LookRotation(transform.position - Camera.main.transform.position, Vector3.up);
        Vector3 euler2 = transform.eulerAngles;
        transform.rotation = Quaternion.Euler(euler2.x, r1.eulerAngles.y + 180, euler2.z);
        prePos = Camera.main.transform.position;
        preY = transform.rotation.y;
    }

    // Update is called once per frame
    void Update()
    {
        Face();
    }

    void Face()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            Quaternion r1 = Quaternion.LookRotation(transform.position - Camera.main.transform.position, Vector3.up);
            Vector3 euler2 = transform.eulerAngles;
            transform.rotation = Quaternion.Euler(euler2.x, r1.eulerAngles.y + 180, euler2.z);
        }
        else
        {
            if (Camera.main.transform.position != prePos)
            {
                Quaternion r1 = Quaternion.LookRotation(transform.position - Camera.main.transform.position, Vector3.up);
                Vector3 euler2 = transform.eulerAngles;
                transform.rotation = Quaternion.Euler(euler2.x, r1.eulerAngles.y + 180, euler2.z);
                if ((transform.rotation.y) > preY + 0.01f && canTurn)
                {
                    GetComponent<Animator>().Play("Turn Left");
                    canTurn = false;
                    StartCoroutine(EnableTurn());
                }
                else if ((transform.rotation.y) < preY - 0.01f && canTurn)
                {
                    GetComponent<Animator>().Play("Turn Right");
                    canTurn = false;
                    StartCoroutine(EnableTurn());
                }
            }
            prePos = Camera.main.transform.position;
            preY = transform.rotation.y;
        }
    }

    IEnumerator EnableTurn()
    {
        yield return new WaitForSeconds(1.5f);
        canTurn = true;
    }
}