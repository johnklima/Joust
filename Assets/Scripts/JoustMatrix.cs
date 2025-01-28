using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
public class JoustMatrix : MonoBehaviour
{
    //animation and/or UI will reference this to determine visualization
    //BUT for ease of code, join them all here, make it work, and then refactor
    //TODO: refactor


    public Dropdown K1_Aim;
    public Dropdown K2_Aim;
    public Dropdown K1_Defend;
    public Dropdown K2_Defend;

    public Animator K1_animator;
    public Animator K2_animator;

    //inputs to the joust round
    public int knight1Aim = 0;
    public int knight1Defense= 0;
    public int knight2Aim = 0;
    public int knight2Defense = 0;
    public bool resolve = false;

    public int resultK1;
    public int resultK2;

    private const int cols = 6;
    private const int rows = 8;

    //just a datatype for now in col,row order
    int [,] resultTable =  new int[cols, rows] ;

    //define rows and cols, consider enums
    public  int col_LowHelm_Defense = 0;     //self explanitory
    public  int col_LeanRight_Defense = 1;
    public  int col_LeanLeft_Defense = 2;
    public  int col_SteadySeat_Defense = 3;
    public  int col_SheildHigh_Defense = 4;
    public  int col_SheildLow_Defense = 5;

    public  int row_Helm_Aim = 0;   //Helm
    public  int row_DC_Aim = 1;     //Dexter Chief
    public  int row_CP_Aim = 2;     //Cheif Pale
    public  int row_SC_Aim = 3;     //Sinister Chief
    public  int row_DF_Aim = 4;     //Dexter Fess
    public  int row_FP_Aim = 5;     //Fess Pale
    public  int row_SF_Aim = 6;     //Sinister Fess
    public  int row_Base_Aim = 7;   //Base

    //results:
    public const int res_BreakLance = 0;
    public const int res_GlanceOff = 1;
    public const int res_HelmKnockOff = 2;
    public const int res_Injured = 3;
    public const int res_Miss = 4;
    public const int res_Unhorsed = 5;
    public const int res_BUI = 6;
    public const int res_BU = 7;
    public const int res_UI = 8;

    //message strings mapped to col and row in the table
    //rows
    string[] Aims = { "Helm", "Dexter Chief", "Cheif Pale", 
                      "Sinister Chief", "Dexter Fess", 
                      "Fess Pale","Sinister Fess", "Base"  };
    //colums
    string[] Defences = {"Low Helm", "Lean Right", "Lean Left",
                         "Steady Seat", "Sheild High", "Sheild Low" };

    //results
    string[] results = { "Break Lance", "GlanceOff", "Helm Knock Off",
                         "Injured", "Miss", "Unhorsed", "Break Lance, Unhorsed, and Injured",
                         "Break Lance and Unhorsed", "Unhorsed and Injured" };

    public bool prevK1restrict;    //rule if break lance or helm off last round must assume 
                                    //steady seat next round (why? as Gygax! oh shit, he's dead)

    public bool prevK2restrict;    //rule if break lance or helm off last round must assume 
                                   //steady seat next round (why? as Gygax! oh shit, he's dead)

    private void FixedUpdate()
    {
        //runs fewer times, so lets plop validate here
        GetComponent<Button>().interactable = Validate();

    }
    private void Start()
    {
        //And now, ya just gotta say what is what, if it is not
        //an equation!!


        //Lower Helm Defense (make yourself super-small in the saddle)
        resultTable[col_LowHelm_Defense, row_Helm_Aim] = res_Miss;
        resultTable[col_LowHelm_Defense, row_DC_Aim] = res_Unhorsed;
        resultTable[col_LowHelm_Defense, row_CP_Aim] = res_BUI;
        resultTable[col_LowHelm_Defense, row_SC_Aim] = res_GlanceOff;
        resultTable[col_LowHelm_Defense, row_DF_Aim] = res_BreakLance;
        resultTable[col_LowHelm_Defense, row_FP_Aim] = res_BU;
        resultTable[col_LowHelm_Defense, row_SF_Aim] = res_GlanceOff;
        resultTable[col_LowHelm_Defense, row_Base_Aim] = res_BreakLance;
                
        //Lean Right Defense
        resultTable[col_LeanRight_Defense, row_Helm_Aim] = res_Miss;
        resultTable[col_LeanRight_Defense, row_DC_Aim] = res_BreakLance;
        resultTable[col_LeanRight_Defense, row_CP_Aim] = res_Unhorsed;
        resultTable[col_LeanRight_Defense, row_SC_Aim] = res_Miss;
        resultTable[col_LeanRight_Defense, row_DF_Aim] = res_BU;
        resultTable[col_LeanRight_Defense, row_FP_Aim] = res_GlanceOff;
        resultTable[col_LeanRight_Defense, row_SF_Aim] = res_Miss;
        resultTable[col_LeanRight_Defense, row_Base_Aim] = res_GlanceOff;

        //Lean Left  Defense
        resultTable[col_LeanLeft_Defense, row_Helm_Aim] = res_Miss;
        resultTable[col_LeanLeft_Defense, row_DC_Aim] = res_Miss;
        resultTable[col_LeanLeft_Defense, row_CP_Aim] = res_GlanceOff;
        resultTable[col_LeanLeft_Defense, row_SC_Aim] = res_BreakLance;
        resultTable[col_LeanLeft_Defense, row_DF_Aim] = res_Miss;
        resultTable[col_LeanLeft_Defense, row_FP_Aim] = res_BreakLance;
        resultTable[col_LeanLeft_Defense, row_SF_Aim] = res_BU;
        resultTable[col_LeanLeft_Defense, row_Base_Aim] = res_Unhorsed;

        //Steady Seat
        resultTable[col_SteadySeat_Defense, row_Helm_Aim] = res_HelmKnockOff;
        resultTable[col_SteadySeat_Defense, row_DC_Aim] = res_BreakLance;
        resultTable[col_SteadySeat_Defense, row_CP_Aim] = res_BreakLance;
        resultTable[col_SteadySeat_Defense, row_SC_Aim] = res_GlanceOff;
        resultTable[col_SteadySeat_Defense, row_DF_Aim] = res_BreakLance;
        resultTable[col_SteadySeat_Defense, row_FP_Aim] = res_BU;
        resultTable[col_SteadySeat_Defense, row_SF_Aim] = res_GlanceOff;
        resultTable[col_SteadySeat_Defense, row_Base_Aim] = res_BreakLance;

        //Shield High
        resultTable[col_SheildHigh_Defense, row_Helm_Aim] = res_Unhorsed;
        resultTable[col_SheildHigh_Defense, row_DC_Aim] = res_BreakLance;
        resultTable[col_SheildHigh_Defense, row_CP_Aim] = res_BU;
        resultTable[col_SheildHigh_Defense, row_SC_Aim] = res_GlanceOff;
        resultTable[col_SheildHigh_Defense, row_DF_Aim] = res_Miss;
        resultTable[col_SheildHigh_Defense, row_FP_Aim] = res_BUI;
        resultTable[col_SheildHigh_Defense, row_SF_Aim] = res_GlanceOff;
        resultTable[col_SheildHigh_Defense, row_Base_Aim] = res_BUI;

        //Shield Low
        resultTable[col_SheildLow_Defense, row_Helm_Aim] = res_Miss;
        resultTable[col_SheildLow_Defense, row_DC_Aim] = res_Miss;
        resultTable[col_SheildLow_Defense, row_CP_Aim] = res_UI;
        resultTable[col_SheildLow_Defense, row_SC_Aim] = res_Unhorsed;
        resultTable[col_SheildLow_Defense, row_DF_Aim] = res_BreakLance;
        resultTable[col_SheildLow_Defense, row_FP_Aim] = res_BreakLance;
        resultTable[col_SheildLow_Defense, row_SF_Aim] = res_GlanceOff;
        resultTable[col_SheildLow_Defense, row_Base_Aim] = res_BreakLance;
    }

    private void Update()
    {
        if(resolve)
        {
            if (Validate() == false)
            {
                resolve = false;
                Debug.Log("BAD SUSHI!!");
                return;            
            }

            //take our input integers and figure out what is next
            //TODO: enforce input rules, that's a gui issue, not a matrix issue

            resolve = false;

            //TODO: generalize all below
            //using completion animations, should not need a timer?
            //K1Aim and K2Defend will fire first
            //K2Aim fires AFTER K2Defend completes, K1Defend fires
            //if anim lengths are more or less the same
            //should not create just the right amount of confusion 
            doKnight1AimAnimation();
            doKnight2DefendAnimation();
            //doKnight2AimAnimation();
            //doKnight1DefendAnimation();

            int rk1 = 0;
            int rK2 = 0;

            rk1 = joustKnight1resolution(knight1Aim, knight2Defense);
            rK2 = joustKnight2resolution(knight2Aim, knight1Defense);

            doKnight1ResolveAnimation(rk1);
            doKnight2ResolveAnimation(rK2);
        }
    }

    //define here the animation trigger names
    string[] triggerAttack = { "trigAttack0", "trigAttack1", "trigAttack2",
                               "trigAttack3", "trigAttack4", "trigAttack5",
                               "trigAttack6", "trigAttack7"               
                             };

    string[] triggerDefend = { "trigDefense0", "trigDefense1", "trigDefense2",
                               "trigDefense3", "trigDefense4", "trigDefense5"
                             };

    string[] triggerResult = { "trigResult0", "trigResult1", "trigResult2",
                               "trigResult3", "trigResult4", "trigResult5"
                             };




    void doKnight1AimAnimation()
    {
        K1_animator.SetTrigger(triggerAttack[knight1Aim]);
    }
    void doKnight2DefendAnimation()
    {
        K2_animator.SetTrigger(triggerDefend[knight2Defense]);
    }
    void doKnight2AimAnimation() 
    {
        K2_animator.SetTrigger(triggerAttack[knight2Aim]);
    }
    void doKnight1DefendAnimation() 
    {
        K1_animator.SetTrigger(triggerDefend[knight1Defense]);
    }
    void doKnight1ResolveAnimation(int result)
    {
        Debug.Log(result.ToString());
        K1_animator.SetTrigger(triggerResult[result]);

    }
    void doKnight2ResolveAnimation(int result)
    {
        K2_animator.SetTrigger(triggerResult[result]);

    }



    bool Validate()
    {
        //Validate is tied to the GUI, prolly shouldn't do that, fix that later
        //OR assume validation happens in the GUI prior to resolution, in which case
        //figure out how to disable an entry in the list? DONE

        //this is redundant unless AI code is running this, not GUI - code needs
        //similar pre-check TODO: refactor this for AI, or AI make AI know the rules

        //previous round restrictions
        if (prevK1restrict == true && K1_Defend.value != col_SteadySeat_Defense)
        {
            K1_Defend.value = col_SteadySeat_Defense;
        }

        if (prevK2restrict == true && K2_Defend.value != col_SteadySeat_Defense)
        {
            K2_Defend.value = col_SteadySeat_Defense;
        }
        
        //Aim/Defense restriction  - TODO: find generalization?
        if(K1_Aim.value == row_Helm_Aim &&  K1_Defend.value < 3 )
        {
            return false;
        }
        if (K2_Aim.value == row_Helm_Aim && K2_Defend.value < 3)
        {
            return false;
        }
        if (K1_Aim.value == row_DC_Aim && K1_Defend.value < 2)
        {
            return false;
        }
        if (K2_Aim.value == row_DC_Aim && K2_Defend.value < 2)
        {
            return false;
        }
        if (K1_Aim.value == row_SC_Aim && K1_Defend.value < 3 && K1_Defend.value != 1)
        {
            return false;
        }
        if (K2_Aim.value == row_SC_Aim && K2_Defend.value < 3 && K2_Defend.value != 1)
        {
            return false;
        }
        if (K1_Aim.value == row_DF_Aim && K1_Defend.value < 3 )
        {
            return false;
        }

        if (K2_Aim.value == row_DF_Aim && K2_Defend.value < 3)
        {
            return false;
        }

        if (K1_Aim.value == row_SF_Aim && K1_Defend.value < 3)
        {
            return false;
        }
        
        if (K2_Aim.value == row_SF_Aim && K2_Defend.value < 3)
        {
            return false;
        }

        if (K1_Aim.value == row_Base_Aim && K1_Defend.value < 3 && K1_Defend.value != 0)
        {
            return false;
        }

        if (K2_Aim.value == row_Base_Aim && K2_Defend.value < 3 && K2_Defend.value != 0)
        {
            return false;
        }

        //All good
        return true;
    }

    public void Joust()
    {
        knight1Aim = K1_Aim.value;
        knight1Defense = K1_Defend.value;
        knight2Aim = K2_Aim.value;
        knight2Defense = K2_Defend.value;

        resolve = true;

    }

    int joustKnight1resolution(int knight1aim, int knight2def)
    {
        //params are passed in as ints, need a gui to represent them, but resolution
        //should not give a shit about that, it just becomes a return on the array location

        prevK1restrict = false;  //clear flag


        //debug log a message based on string arrays
        Debug.Log("Knight 1 Aims at " + Aims[knight1aim] + 
                  " Knight2 Defends with " + Defences[knight2def]  );

        int res = resultTable[knight2def, knight1Aim];
       

        //check results for attacker breaking lance, BU, and BUI
        if (res == res_BreakLance)
        {
            Debug.Log("Knight 1 Breaks his lance!");
            prevK1restrict = true;

        }
        else if (res == res_BUI)
        {
            Debug.Log("Knight 1 Breaks his lance!");
            Debug.Log("Knight 2 defender is unhorsed and injured.");
            prevK1restrict = true;
        }
        else if (res == res_BU)
        {
            Debug.Log("Knight 1 Breaks his lance!");
            Debug.Log("Knight 2 defender is unhorsed.");
            prevK1restrict = true;
        }
        else 
        {
            Debug.Log("The result upon Knight2 defender is " + results[res]);
        }
        
        return res;

    }

    int joustKnight2resolution(int knight2aim, int knight1def)
    {
        //params are passed in as ints, need a gui to represent them, but resolution
        //should not give a shit about that, it just becomes a return on the array location

        prevK2restrict = false;   //clear flag


        //debug log a message based on string arrays
        Debug.Log("Knight 2 Aims at " + Aims[knight2aim] +
                  " Knight1 Defends with " + Defences[knight1def]);

        int res = resultTable[knight1def, knight2Aim];

        //check results for attacker breaking lance, BU, and BUI
        if (res == res_BreakLance)
        {
            Debug.Log("Knight 2 Breaks his lance!");
            prevK2restrict = true;

        }
        else if (res == res_BUI)
        {
            Debug.Log("Knight 2 Breaks his lance!");
            Debug.Log("Knight 1 defender is unhorsed and injured.");
            prevK2restrict = true;

        }
        else if (res == res_BU)
        {
            Debug.Log("Knight 2 Breaks his lance!");
            Debug.Log("Knight 1 defender is unhorsed.");
            prevK2restrict = true;

        }
        else
        {
            Debug.Log("The result upon Knight1 defender is " + results[res]);
        }

        return res;

    }

}
