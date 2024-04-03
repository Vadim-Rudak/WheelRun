using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 dir;
    private Animator anim;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private int coins;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private Text coinsText;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject longPanel;

    private bool isSliding;
    private bool of=true;

    private int lineToMove = 1;
    public float lineDistance = 4;
    private float maxSpeed = 110;
    private float time = 0;
    private bool FirstJump;
    
    // Start is called before the first frame update
    void Start()
    {
        FirstJump = true;
        time = 0;
        JoystickButton.pressed = false;
        longPanel.SetActive(true);
        anim = GetComponentInChildren<Animator>();
        losePanel.SetActive(false);
        Time.timeScale = 1;
        controller = GetComponent<CharacterController>();
        StartCoroutine(SpeedIncrease());
    }

    private void Update()
    {
        if (SwipeController.swipeRight)
        {
            if (lineToMove<2)
            {
                lineToMove++;
            }
        }
        if (SwipeController.swipeLeft)
        {
            if (lineToMove>0)
            {
                lineToMove--;
            }
        }

        if (SwipeController.swipeUp)
        {
            if (controller.isGrounded)
            {
                jump();
            }
        }

        if (JoystickButton.pressed)
        {
            if (FirstJump)
            {
                time += Time.deltaTime;
                Debug.Log(time);
                if (time>=2)
                {
                    jump2();
                    longPanel.SetActive(false);
                    time = 0;
                    FirstJump = false;
                }
            }

        }else
        {
            time = 0;
        }

        if (controller.isGrounded && !isSliding)
        {
            anim.SetBool("isRunning",true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
        

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (lineToMove==0)
        {
            targetPosition += Vector3.left * lineDistance;
        }
        else if (lineToMove==2)
        {
            targetPosition += Vector3.right * lineDistance;
        }

        if (transform.position == targetPosition)
        {
            return;
        }

        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
        {
            controller.Move(moveDir);
        }
        else
        {
            controller.Move(diff);
        }
        
    }

    private void jump()
    {
        dir.y = jumpForce;
        anim.SetTrigger("isJump");
    }
    
    private void jump2()
    {
        dir.y = jumpForce+15;
        anim.SetTrigger("isJump");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dir.z = speed;
        dir.y += gravity * Time.fixedDeltaTime;
        controller.Move(dir * Time.fixedDeltaTime);
        if (player.position.y<=0)
        {
            losePanel.SetActive(true);
            Time.timeScale = 0;
            longPanel.SetActive(false);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "obstecal")
        {
            longPanel.SetActive(false);
            losePanel.SetActive(true);
            Time.timeScale = 0;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            coins++;
            coinsText.text = coins.ToString();
            Destroy(other.gameObject);
        }
    }

    private IEnumerator SpeedIncrease()
    {
        yield return new WaitForSeconds(4);
        if (speed<maxSpeed)
        {
            speed += 2;
            jumpForce += 1/2;
            StartCoroutine(SpeedIncrease());
        }
    }
}
