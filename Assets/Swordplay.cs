using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swordplay : MonoBehaviour
{

    public Animator K1animator;
    public Animator K2animator;

    public Dropdown K1attack;
    public Dropdown K1defend;

    public Dropdown K2attack;
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
    float timer = 0;
    int fstate = -1;

    void Update()
    {
        if (fstate == 0 && Time.time - timer > 2)
        {

            K1animator.SetBool("isAttack", true);
            K1animator.SetTrigger(triggerAttack[K1attack.value]);

            K2animator.SetBool("isDefend", true);
            K2animator.SetTrigger(triggerDefend[K2defend.value]);

            fstate = 1;

            timer = Time.time;

            return;  //give it air
        }
        if (fstate == 1 && Time.time - timer > 2)
        {
            K2animator.SetBool("resetDefend", true);
            K1animator.SetBool("resetAttack", true);


            K1animator.SetBool("isDefend", true);
            K1animator.SetTrigger(triggerDefend[K1defend.value]);

            K2animator.SetBool("isAttack", true);
            K2animator.SetTrigger(triggerAttack[K2attack.value]);

            timer = Time.time;
            fstate = 3;

            return;  //give it air
        }
        if(fstate == 3 && Time.time - timer > 2)
        {
            K1animator.SetBool("resetDefend", true);
            K2animator.SetBool("resetAttack", true);

            timer = Time.time;
            fstate = 3;

        }

    }

    public void Fight()
    {

        K1animator.SetBool("isAttack", false);
        K1animator.SetBool("isDefend", false);

        K2animator.SetBool("isAttack", false);
        K2animator.SetBool("isDefend", false);


        K1animator.SetBool("resetDefend", false);
        K1animator.SetBool("resetAttack", false);

        K2animator.SetBool("resetDefend", false);
        K2animator.SetBool("resetAttack", false);

        fstate = 0;
    }
}
