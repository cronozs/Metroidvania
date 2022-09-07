using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exam : MonoBehaviour
{
    public GameObject ball;
    public int positive;
    public int negative;
    private bool flag = true;


    // Update is called once per frame
    void Update()
    {
        Cambios();
    }

    void Cambios()
    {
        if (ball.transform.position.y <= positive && flag == true)
        {
            ball.transform.Translate(0, positive * Time.deltaTime, 0);
            if (ball.transform.position.y >= positive)
            {
                flag = false;
            }
        }
        else if (ball.transform.position.y >= negative && flag == false)
        {
            ball.transform.Translate(0, negative * Time.deltaTime, 0);
            if (ball.transform.position.y <= negative)
            {
                flag = true;
            }
        }
    }
}
