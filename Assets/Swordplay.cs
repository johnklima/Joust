using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swordplay : MonoBehaviour
{

    public Animator K1animator;
    public Animator K2animator;

    public Dropdown K1attack;
    public Dropdown K2defend;

    //define here the animation trigger names
    string[] triggerAttack = { "trigAttack0", "trigAttack1", "trigAttack2",
                               "trigAttack3", "trigAttack4", "trigAttack5",
                               "trigAttack6", "trigAttack7"
                             };

    string[] triggerDefend = { "trigDefend0", "trigDefend1", "trigDefend2",
                               "trigDefend3", "trigDefend4", "trigDefend5"
                             };


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fight()
    {
        K1animator.SetBool("isDefend", false);
        K1animator.SetBool("isAttack", true);
        K1animator.SetTrigger(triggerAttack[K1attack.value]);

        K2animator.SetBool("isAttack", false);
        K2animator.SetBool("isDefend", true);
        K2animator.SetTrigger(triggerDefend[K2defend.value]);
    
    }
}
