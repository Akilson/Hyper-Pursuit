using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateControl : MonoBehaviour
{
    Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w") || Input.GetKey("d") || Input.GetKey("a") || Input.GetKey("s"))
	    {
	        animator.SetBool("IsWalking", true);
	    }

        if (!Input.GetKey("w") && !Input.GetKey("d") && !Input.GetKey("a") && !Input.GetKey("s"))
        {
            animator.SetBool("IsWalking", false);
        }
    }
}
