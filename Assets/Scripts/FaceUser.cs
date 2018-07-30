using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FaceUser : MonoBehaviour
{
    Vector3 prePos;
    Animator animator = new Animator();
    float preY;
    Transform user;
    bool canTurn = true;

    // Use this for initialization
    void Start()
    {
        Quaternion r1 = Quaternion.LookRotation(transform.position - Camera.main.transform.position, Vector3.up);
        Vector3 euler2 = transform.eulerAngles;
        transform.rotation = Quaternion.Euler(euler2.x, r1.eulerAngles.y + 180, euler2.z);
        prePos = Camera.main.transform.position;
        preY = transform.rotation.y;
        animator = GetComponent<Animator>();
        user = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Face();
    }

    // Determine avatar rotation based on relative user position
    void Face()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            Quaternion r1 = Quaternion.LookRotation(transform.position - user.position, Vector3.up);
            Vector3 euler2 = transform.eulerAngles;
            transform.rotation = Quaternion.Euler(euler2.x, r1.eulerAngles.y + 180, euler2.z);
        }
        else
        {
            if (Camera.main.transform.position != prePos)
            {
                Quaternion r1 = Quaternion.LookRotation(transform.position - user.position, Vector3.up);
                Vector3 euler2 = transform.eulerAngles;
                transform.rotation = Quaternion.Euler(euler2.x, r1.eulerAngles.y + 180, euler2.z);
                if ((transform.rotation.y) > preY + 0.1f && canTurn)
                {
                    animator.Play("Turn Left");
                    canTurn = false;
                    StartCoroutine(EnableTurn());
                }
                else if ((transform.rotation.y) < preY - 0.1f && canTurn)
                {
                    animator.Play("Turn Right");
                    canTurn = false;
                    StartCoroutine(EnableTurn());
                }
            }
            prePos = user.position;
            preY = transform.rotation.y;
        }
    }

    // Allow avatar to turn again
    IEnumerator EnableTurn()
    {
        yield return new WaitForSeconds(1.5f);
        canTurn = true;
    }
}