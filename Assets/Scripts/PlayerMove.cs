using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 1f;
    public GameObject player;
    public GameObject goTo;
    public Vector2 startPos;
    public Vector2 direction;
    public float move_radius = 2.5f;
    public float jumpStr = 10f;
    public GameObject forTest;
    public float maxJump = 1f;

    private Vector3 myPosition;

    private bool playerRun;
    public bool haveGround;
    public float playerSpeed = 3f;
    private string prewMove;
    private bool touchEnd;
    private Vector3 StartScale;
    

    public bool imHaveDownShift;
    public bool imHaveUpShift = true;
    public float downTime = 2f;
    //public BoxCollider ForDownCollider;
    void Start()
    {
    
        player = (GameObject)this.gameObject; 
        playerRun = false;
        StartScale = player.GetComponent<Transform>().localScale;
        StartCoroutine("JumpCD");
        
        

        
    }

    // Update is called once per frame
    void Update()
    {
        myPosition = player.GetComponent<Transform>().position;
        // Track a single touch as a direction control.
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Handle finger movements based on TouchPhase
            switch (touch.phase)
            {
                //When a touch has first been detected, change the message and record the starting position
                case TouchPhase.Began:
                    // Record initial touch position.
                    startPos = touch.position;
                    touchEnd = false;
                    break;

                //Determine if the touch is a moving touch
                case TouchPhase.Moved:
                    // Determine direction by comparing the current touch position with the initial one
                    direction = touch.position - startPos;
                    break;

                case TouchPhase.Ended:
                    // Report that the touch has ended when it ends
                    touchEnd = true;
                    
                    break;
            }
        }

        if (Input.GetKey(KeyCode.D) || (touchEnd && direction.x > 10))
        {
            Debug.Log("Move_Right");
            //plaerMove_Right();
            //player.transform.Translate(move_radius, 0, 0);
            MyMove("Right");
            //direction = new Vector2(0, direction.y);
        }     
        if (Input.GetKey(KeyCode.A) || (touchEnd && direction.x < -10))
        {
            Debug.Log("Move_Left");
            //plaerMove_Left();
            MyMove("Left");
            //direction = new Vector2(0, direction.y);
        }     
        if (Input.GetKey(KeyCode.W) || (touchEnd && direction.y > 10))
        {
            Debug.Log("Move_Up");
            CheckGround();
            //plaerMove_Up();
            MyMove("Jump");
            //direction = new Vector2(direction.x, 0);
        }     
        if (Input.GetKey(KeyCode.S) || (touchEnd && direction.y < -10))
        {
            Debug.Log("Move_Down");
            CheckGround();
            //plaerMove_Down();
            //direction = new Vector2(0, direction.y);
            MyMove("Down");
        }    
        
        

        if(playerRun)
        {
            player.transform.position = Vector3.MoveTowards 
                (
                    myPosition, 
                    new Vector3(goTo.transform.position.x, myPosition.y, myPosition.z), 
                    playerSpeed * Time.deltaTime
                );
        }

        //Debug.Log(playerRun);

        switch(transform.position.x)
        {
            case 2.5f:
                MyMove("Stop");
                break;
            case 0:
                MyMove("Stop");
                break;    
            case (-2.5f):
                MyMove("Stop");
                break;

            default:
                break;

        }

        

    }
    void FixedUpdate() 
    {
        
        //goTo.transform.position.y = player.transform.position.y;
        /*
        if(imDown)
        {
            print("down true");
            //ForRunCollider.enabled;
            //player.GetComponent<BoxCollider>().col = 2f;
            var boxCollider = gameObject.GetComponent<BoxCollider>();
            var orig = (boxCollider.center, boxCollider.size);
            boxCollider.center = new Vector3(0, -0.5f, 0);
            boxCollider.size = new Vector3(1, 1, 1);

            player.transform.rotation = new Quaternion(-90, 0, 0, 0);

            forDown();

            (boxCollider.center, boxCollider.size) = orig;
            //boxCollider.center = ;
            //boxCollider.size = new Vector3(1, 1, 1);

            player.transform.rotation = new Quaternion(0, 0, 0, 0);
            imDown = false;
            
        }
        */
    }

    void myRoll()
    {

        Debug.Log("down true");
        imHaveUpShift = false;

        player.transform.localScale = StartScale - new Vector3(0, 0.5f, 0);


        StartCoroutine("forDown");

        //boxCollider.center = ;
        //boxCollider.size = new Vector3(1, 1, 1);

        //imDown = false;
    }

    public IEnumerator forDown()
    {
        yield return new WaitForSeconds(downTime);
        Debug.Log("wait");
        player.transform.localScale = StartScale;
        imHaveUpShift = true;
        
    }

    //private Vector3 goTo = myPosition;
    void MyMove(string move)
    {
        if(move != prewMove)
        {
            switch(move)
            {
                case "Left":
                    if(goTo.transform.position.x > -move_radius)
                    {    
                        goTo.transform.position -= new Vector3(move_radius, 0, 0);
                        playerRun = true;
                    }
                    break;

                case "Right":
                    if(goTo.transform.position.x < move_radius)
                    {
                        goTo.transform.position += new Vector3(move_radius, 0, 0);
                        playerRun = true;
                    }
                    break;

                case "Jump":
                    if(haveGround && imHaveUpShift)
                    {
                        Debug.Log("I jump");
                        imHaveUpShift = false;
                        player.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpStr, ForceMode.Impulse);
                        imHaveDownShift = true;
                        StartCoroutine("JumpCD");
                        //Invoke(imHaveUpShift = true, 2f);
                        //player.GetComponent<Rigidbody>().velocity = (Vector3.up * jumpStr);
                    }
                    break;

                case "Down":
                    if(haveGround && imHaveUpShift)
                    {
                        Debug.Log("down move");
                        //imDown = true;
                        myRoll();
                        
                    }    
                    else 
                        if(imHaveDownShift)
                        {
                            player.GetComponent<Rigidbody>().AddForce(Vector3.down * jumpStr, ForceMode.Impulse);
                            imHaveDownShift = false;
                        }     

                    break;

                case "Stop":
                    playerRun = false;
                    break;

                default:
                    playerRun = false;
                    break;
            }
        }
        prewMove = move;
        touchEnd = false;

    }

    void CheckGround()
    {
        Ray ray = new Ray (myPosition, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast (ray, out hit, 1.1f))
        {
            //Debug.Log(hit.rigidbody.tag);
            if(hit.rigidbody.tag == "Ground" )
            {
                haveGround = true;
                imHaveDownShift = false;
                //imHaveUpShift = true;
                StartCoroutine("JumpCD");
            }
        } 
        else 
        {
            Debug.Log("not");
            haveGround = false;
        }

        
    }

    public IEnumerator JumpCD()
        {
            Debug.Log("JumpCdStart");
            if(!imHaveUpShift)
            {
                yield return new WaitForSeconds(1f);
                imHaveUpShift= true;
                Debug.Log("JumpCdEnd");
            }

        } 

    

}
