using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu]
public class SOPlayerSetup : ScriptableObject
{
    [Header("Speed setup")]
    public Vector2 friction = new Vector2(.1f, 0);
    public float speed;
    public float speedRun;
    public float forceJump;


    [Header("Animation setup")]
    public float jumpScaleY;
    public float jumpScaleX;
    public float animationDuration;
    public Ease ease = Ease.OutBack;

    [Header("Animation PLayer")]
    public string boolRun = "Run";
    public string boolRunning = "Running";
    public string triggerDeath = "Death";
    public float playerSwipeDuration = .1f;
}
