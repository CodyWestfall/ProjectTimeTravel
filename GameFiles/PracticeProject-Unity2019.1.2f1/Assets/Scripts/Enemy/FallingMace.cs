using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallingMace : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public float raiseSpeed = 2f, WaitTimeInSec = 2f, LiftHeightPosition;

    private bool Lifting;

    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }


    void Update()
    {

        if (Lifting)
        {
            
            if (transform.position.y != LiftHeightPosition)
            {

                //Moves the Object to the given position with a given speed.
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, LiftHeightPosition), raiseSpeed * Time.deltaTime);
            }
            else
            {                
                Lifting = false;

                //Invoke calls the given Methode after the given time.
                Invoke("ReleaseMace", WaitTimeInSec);
            }


        }
        

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //When the Object hits the Ground the countdown will begin to relift it again.
        if(collision.gameObject.tag == "Ground")
        {
            Invoke("LiftUp", WaitTimeInSec);
            rb2d.isKinematic = true;

        }

        //simple way to "kill" the Player if he collides with the object.
        if(collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    #region InvokeMethodes
    private void LiftUp()
    {
       Lifting = true;       
    }

    private void ReleaseMace()
    {
        rb2d.isKinematic = false;
    }
    #endregion
}
