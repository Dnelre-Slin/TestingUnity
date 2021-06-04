using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Animator anim;

    [SerializeField]
    private bool testPlay = false;

    private bool stageOne;

    void Start()
    {
        anim = GetComponent<Animator>();

        stageOne = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (testPlay)
        {
            // Debug.Log("Animating");
            // anim.SetBool("PlayBoxAnim", true);
            if (stageOne)
            {
                anim.Play("AnimateBox");
                stageOne = false;
            }
            else
            {
                anim.Play("AnimateBoxReverse");
                stageOne = true;
            }
            testPlay = false;
        }
    }
}
