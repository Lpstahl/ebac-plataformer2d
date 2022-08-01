using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigibody;
    public HealthBase healthBase;

    [Header("Setup")]
    public SOPlayerSetup soPlayerSetup;

    public Animator animator;

    private float _currentSpeed;

    private void Awake()
    {
        if(healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }
    }

    private void OnPlayerKill()
    {

        healthBase.OnKill -= OnPlayerKill;
        animator.SetTrigger(soPlayerSetup.triggerDeath);
        
    }

    private void Update()
    {
        HandleJump();
        HandleMoviment();   
    }

    private void HandleMoviment()
    {
        if (Input.GetKey(KeyCode.LeftShift)) { 
            _currentSpeed = soPlayerSetup.speedRun;
            animator.speed = 2;
        }
        else
        {
            _currentSpeed = soPlayerSetup.speed;
            animator.speed = 1;
        }


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //myRigibody.MovePosition(myRigibody.position - velocity * Time.deltaTime);
            myRigibody.velocity = new Vector2(-_currentSpeed, myRigibody.velocity.y);
            if (myRigibody.transform.localScale.x != -1)
            {
                myRigibody.transform.DOScaleX(-1, soPlayerSetup.playerSwipeDuration);
            }
            animator.SetBool(soPlayerSetup.boolRun, true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //myRigibody.MovePosition(myRigibody.position + velocity * Time.deltaTime);
            myRigibody.velocity = new Vector2(_currentSpeed, myRigibody.velocity.y);
            if (myRigibody.transform.localScale.x != 1)
            {
                myRigibody.transform.DOScaleX(1, soPlayerSetup.playerSwipeDuration);
            }
            animator.SetBool(soPlayerSetup.boolRun, true);
        }
        else
        {
            animator.SetBool(soPlayerSetup.boolRun, false);
        }

        Debug.Log(myRigibody.velocity);

        if(myRigibody.velocity.x > 0)
        {
            myRigibody.velocity += soPlayerSetup.friction;
        }
        else if (myRigibody.velocity.x < 0)
        {
            myRigibody.velocity -= soPlayerSetup.friction;
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigibody.velocity = Vector2.up * soPlayerSetup.forceJump;
            myRigibody.transform.localScale = Vector2.one;

            DOTween.Kill(myRigibody.transform);

            HandleScaleJump();
        }
    }
    private void HandleScaleJump()
    {
        myRigibody.transform.DOScaleY(soPlayerSetup.jumpScaleY, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
        myRigibody.transform.DOScaleX(soPlayerSetup.jumpScaleX, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
