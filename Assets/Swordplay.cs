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

    string[] animAttack = { "attack0", "attack1", "attack2",
                               "attack3", "attack4", "attack5",
                               "attack6", "attack7"
                             };

    float[] timeAttack = { 1.4f, 3.4f, 1.4f,
                           2.3f, 1.2f, 2.2f,
                           1.2f, 1.6f
                         };




    string[] triggerDefend = { "trigDefend0", "trigDefend1", "trigDefend2",
                               "trigDefend3", "trigDefend4", "trigDefend5"
                             };

    string[] animDefend = { "defense0", "defense1", "defense2",
                               "defense3", "defense4", "defense5"
                             };

    float[] timeDefend = { 2, 2, 2,
                           2, 2, 2
                         };


    string[] triggerResult = { "trigResult0", "trigResult1", "trigResult2","trigResult3", "trigResult4" };

    string[] animResult = { "result0", "result1", "result2", "result3", "result4" };

    float[] timeResult = { 1, 1, 1,
                           1, 1  };

    //now define result look up table

    private const int cols = 6;
    private const int rows = 8;

    //just a datatype for now in col,row order
    int[,] resultTable = new int[cols, rows];

    //TODO: define enums for table


    // Start is called before the first frame update
    void Start()
    {

        //build combat result table at startup
        //this is Defender response to Attacker
        //TODO: make a LUT for Attacker response to Defender result (victory, missedhit)
        //Hit fall forward = 0
        //Hit fall back = 1
        //Victory = 2
        //Glance = 3
        //Miss = 4
        
        //column order
        // D0, A0 etc...
        resultTable[0, 0] = 4;
        resultTable[0, 1] = 0;
        resultTable[0, 2] = 0;
        resultTable[0, 3] = 0;
        resultTable[0, 4] = 3;
        resultTable[0, 5] = 4;
        resultTable[0, 6] = 0;
        resultTable[0, 7] = 1;

        //Hit fall forward = 0
        //Hit fall back = 1
        //Victory = 2
        //Glance = 3
        //Miss = 4

        resultTable[1, 0] = 3;
        resultTable[1, 1] = 3;
        resultTable[1, 2] = 3;
        resultTable[1, 3] = 1;
        resultTable[1, 4] = 4;
        resultTable[1, 5] = 4;
        resultTable[1, 6] = 3;
        resultTable[1, 7] = 0;

        //Hit fall forward = 0
        //Hit fall back = 1
        //Victory = 2
        //Glance = 3
        //Miss = 4

        resultTable[2, 0] = 3;
        resultTable[2, 1] = 3;
        resultTable[2, 2] = 3;
        resultTable[2, 3] = 0;
        resultTable[2, 4] = 4;
        resultTable[2, 5] = 4;
        resultTable[2, 6] = 3;
        resultTable[2, 7] = 1;


        //Hit fall forward = 0
        //Hit fall back = 1
        //Victory = 2
        //Glance = 3
        //Miss = 4

        resultTable[3, 0] = 4;
        resultTable[3, 1] = 4;
        resultTable[3, 2] = 4;
        resultTable[3, 3] = 4;
        resultTable[3, 4] = 3;
        resultTable[3, 5] = 4;
        resultTable[3, 6] = 3;
        resultTable[3, 7] = 3;

        //Hit fall forward = 0
        //Hit fall back = 1
        //Victory = 2
        //Glance = 3
        //Miss = 4

        resultTable[4, 0] = 4;
        resultTable[4, 1] = 4;
        resultTable[4, 2] = 4;
        resultTable[4, 3] = 4;
        resultTable[4, 4] = 0;
        resultTable[4, 5] = 3;
        resultTable[4, 6] = 3;
        resultTable[4, 7] = 1;

        //Hit fall forward = 0
        //Hit fall back = 1
        //Victory = 2
        //Glance = 3
        //Miss = 4

        resultTable[5, 0] = 3;
        resultTable[5, 1] = 1;
        resultTable[5, 2] = 0;
        resultTable[5, 3] = 4;
        resultTable[5, 4] = 4;
        resultTable[5, 5] = 0;
        resultTable[5, 6] = 1;
        resultTable[5, 7] = 3;

    }


    // Update is called once per frame
    float timer = 0;
    float duration = 0;  //i have only found a dumbass hardcode way to do this
    int fstate = -1;
    int K1result;
    int K2result;
    void Update()
    {
        //TODO: make states for each knight's "turn" as attacker

  
        if (fstate == 0)
        {

            K1animator.SetBool("isAttack", true);
            K1animator.SetTrigger(triggerAttack[K1attack.value]);

            K2animator.SetBool("isDefend", true);
            K2animator.SetTrigger(triggerDefend[K2defend.value]);

            fstate = 1;

            timer = Time.time;
            
            duration = timeAttack[K1attack.value];

            K2result = resultTable[K2defend.value, K1attack.value];
                
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

            K1result = resultTable[K1defend.value, K2attack.value];

            fstate = 2;

            return;  //give it air
        }
        if(fstate == 2 && Time.time - timer > duration)
        {
            K1animator.SetBool("resetDefend", true);
            K2animator.SetBool("resetAttack", true);

            K2animator.SetBool("isResult", true);
            K1animator.SetBool("isResult", true);

            //select 

            K1animator.SetTrigger(triggerResult[K1result]);  
            K2animator.SetTrigger(triggerResult[K2result]);  
            
            timer = Time.time;

            duration = timeResult[2];

            fstate = 3;

            return; //air
        }
        if (fstate == 3 && Time.time - timer > duration)
        {
            

            K2animator.SetBool("resetResult", true);
            K1animator.SetBool("resetResult", true);

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
