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

    //public Animator animator;

    private float _currentSpeed;

    private Animator _currentPlayer;

    [Header("Jump Collision Check")]
    public Collider2D collider2D;
    public float distToGround;
    public float spaceToGround = .1f;
    public ParticleSystem jumpVfx;

    private void Awake()
    {
        if (healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }

        _currentPlayer = Instantiate(soPlayerSetup.player, transform);

        if (collider2D != null)
        {
            distToGround = collider2D.bounds.extents.y;
        }

        GunBase gunBase = _currentPlayer.GetComponentInChildren<GunBase>();

        gunBase.SetPlayer(this);

      
    }

    private bool IsGrounded()
    {
        Debug.DrawRay(transform.position, -Vector2.up, Color.magenta, distToGround + spaceToGround);
        return Physics2D.Raycast(transform.position, -Vector2.up, distToGround + spaceToGround);
    }

    private void OnPlayerKill()
    {
        healthBase.OnKill -= OnPlayerKill;
        _currentPlayer.SetTrigger(soPlayerSetup.triggerDeath);
    }

    private void Update()
    {
        IsGrounded();
        HandleJump();
        HandleMoviment();
    }

    private void HandleMoviment()
    {
        if (Input.GetKey(KeyCode.LeftShift)) {
            _currentSpeed = soPlayerSetup.speedRun;
            _currentPlayer.speed = 2;
        }
        else
        {
            _currentSpeed = soPlayerSetup.speed;
            _currentPlayer.speed = 1;
        }


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //myRigibody.MovePosition(myRigibody.position - velocity * Time.deltaTime);
            myRigibody.velocity = new Vector2(-_currentSpeed, myRigibody.velocity.y);
            if (myRigibody.transform.localScale.x != -1)
            {
                myRigibody.transform.DOScaleX(-1, soPlayerSetup.playerSwipeDuration);
            }
            _currentPlayer.SetBool(soPlayerSetup.boolRun, true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //myRigibody.MovePosition(myRigibody.position + velocity * Time.deltaTime);
            myRigibody.velocity = new Vector2(_currentSpeed, myRigibody.velocity.y);
            if (myRigibody.transform.localScale.x != 1)
            {
                myRigibody.transform.DOScaleX(1, soPlayerSetup.playerSwipeDuration);
            }
            _currentPlayer.SetBool(soPlayerSetup.boolRun, true);
        }
        else
        {
            _currentPlayer.SetBool(soPlayerSetup.boolRun, false);
            _currentPlayer.SetBool(soPlayerSetup.boolRun, false);
        }

        Debug.Log(myRigibody.velocity);

        if (myRigibody.velocity.x > 0)
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
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())

            myRigibody.velocity = Vector2.up * soPlayerSetup.forceJump;
        //myRigibody.transform.localScale = Vector2.one;  
        if (myRigibody.transform.localScale.x != -1)
        {
            myRigibody.transform.DOScaleX(-1, soPlayerSetup.playerSwipeDuration);
            
        }

        DOTween.Kill(myRigibody.transform);

        HandleScaleJump();
        PlayJumpVfx();
    }

    private void PlayJumpVfx()
    {
        VFXManager.instance.PlayVFXByType(VFXManager.VFXType.JUMP, transform.position);
        //if (jumpVfx != null) jumpVfx.Play();
    }

    private void HandleScaleJump()
    {
        float mult = 1f;
        if (transform.localScale.x < 0)
            mult = -1;

        myRigibody.transform.DOScaleY(soPlayerSetup.jumpScaleY, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
        myRigibody.transform.DOScaleX(soPlayerSetup.jumpScaleX * mult, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
    }
    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
