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

    float[] timeAttack = { 1.4f, 3.4f, 1.4f,
                           2.3f, 1.2f, 2.2f,
                           1.2f, 1.6f
                         };




    string[] animAttack = { "attack0", "attack1", "attack2",
                               "attack3", "attack4", "attack5",
                               "attack6", "attack7"
                             };


    string[] triggerDefend = { "trigDefend0", "trigDefend1", "trigDefend2",
                               "trigDefend3", "trigDefend4", "trigDefend5"
                             };

    float[] timeDefend = { 2, 2, 2,
                           2, 2, 2
                         };

    
    string[] animDefend = { "defense0", "defense1", "defense2",
                               "defense3", "defense4", "defense5"
                             };


    string[] triggerResult = { "result0", "result1", "result2" };

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    float timer = 0;
    float duration = 0;  //i have only found a dumbass hardcode way to do this
    int fstate = -1;

    void Update()
    {
        if (fstate == 0)
        {

            K1animator.SetBool("isAttack", true);
            K1animator.SetTrigger(triggerAttack[K1attack.value]);

            K2animator.SetBool("isDefend", true);
            K2animator.SetTrigger(triggerDefend[K2defend.value]);

            fstate = 1;

            timer = Time.time;
            
            duration = timeAttack[K1attack.value];
                        

            return;  //give it air
        }
        if (fstate == 1 && Time.time - timer > duration)
        {
            K2animator.SetBool("resetDefend", true);
            K1animator.SetBool("resetAttack", true);


            K1animator.SetBool("isDefend", true);
            K1animator.SetTrigger(triggerDefend[K1defend.value]);

            K2animator.SetBool("isAttack", true);
            K2animator.SetTrigger(triggerAttack[K2attack.value]);

            timer = Time.time;
            
            duration = timeAttack[K2attack.value];


            fstate = 2;

            return;  //give it air
        }
        if(fstate == 2 && Time.time -timer > duration)
        {
            K1animator.SetBool("resetDefend", true);
            K2animator.SetBool("resetAttack", true);

        


        }
        if (fstate == 3 && Time.time - timer > duration)
        {
            //for now, i'm tired, just want to see a complete sequence
            //TODO: make the combat table
            K1animator.SetTrigger(triggerResult[2]);
            K2animator.SetTrigger(triggerResult[1]);

            K2animator.SetBool("isResult", true);
            K1animator.SetBool("isResult", true);


            timer = Time.time;
            fstate = -1;

        }

    }
    bool AnimatorIsPlaying(Animator animator)
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
    bool AnimatorIsPlaying(string stateName, Animator animator)
    {
        return AnimatorIsPlaying(animator) && animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }
    public void Fight()
    {

        K1animator.SetBool("isAttack", false);
        K1animator.SetBool("isDefend", false);
        K1animator.SetBool("isResult", false);

        K2animator.SetBool("isAttack", false);
        K2animator.SetBool("isDefend", false);
        K2animator.SetBool("isResult", false);


        K1animator.SetBool("resetDefend", false);
        K1animator.SetBool("resetAttack", false);
        K1animator.SetBool("resetResult", false);

        K2animator.SetBool("resetDefend", false);
        K2animator.SetBool("resetAttack", false);
        K2animator.SetBool("resetResult", false);

        fstate = 0;
    }
}
