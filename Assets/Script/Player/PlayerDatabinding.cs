using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDatabinding : MonoBehaviour
{
    public Animator animator;

    public bool DoneGame
    {
        set
        {
            if(value)
            {
                animator.SetTrigger(doneGame_key);
            }
        }
    }

    public bool Throw
    {
        set
        {
            if (value)
            {
                animator.SetTrigger(throw_key);
            }
        }
    }

    public bool Buff
    {
        set
        {
            if (value)
            {
                animator.SetTrigger(buff_key);
            }
        }
    }

    public bool Sad
    {
        set
        {
            if (value)
            {
                animator.SetTrigger(sad_key);
            }
        }
    }

    public bool HardDrag
    {
        set
        {
            animator.SetBool(hardDrag_key, value);
        }
    }

    public bool NormalDrag
    {
        set
        {
            animator.SetBool(normalDrag_key, value);
        }
    }

    public bool Walk
    {
        set
        {
            animator.SetBool(walk_key, value);
        }
    }

    int doneGame_key;
    int throw_key;
    int buff_key;
    int sad_key;

    int hardDrag_key;
    int normalDrag_key;
    int walk_key;

    // Start is called before the first frame update
    void Start()
    {
        doneGame_key = Animator.StringToHash("DoneGame");
        hardDrag_key = Animator.StringToHash("HardDrag");
        normalDrag_key = Animator.StringToHash("NormalDrag");
        throw_key = Animator.StringToHash("Throw");
        buff_key = Animator.StringToHash("Buff");
        sad_key = Animator.StringToHash("Sad");
        walk_key = Animator.StringToHash("Walk");
    }
}
