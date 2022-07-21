using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTest : MonoBehaviour
{
    public Animator animator;

    public KeyCode keyToBool = KeyCode.A;
    public KeyCode keyToExit = KeyCode.S;
    public string triggerToPlay;

    private void OnValidate()
    {
        if(animator != null) animator = GetComponent<Animator>();
    }



    private void Update()
    {
        if (Input.GetKeyDown(keyToBool))
        {
            animator.SetBool(triggerToPlay, !animator.GetBool(triggerToPlay));
        }
       
    }
}
