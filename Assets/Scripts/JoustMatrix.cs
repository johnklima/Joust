using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class JoustMatrix : MonoBehaviour
{
    //animation and/or UI will reference this to determine visualization


    public Dropdown K1_Aim;
    public Dropdown K2_Aim;
    public Dropdown K1_Defend;
    public Dropdown K2_Defend;



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
    public const int col_LowHelm_Defense = 0;     //self explanitory
    public const int col_LeanRight_Defense = 1;
    public const int col_LeanLeft_Defense = 2;
    public const int col_SteadySeat_Defense = 3;
    public const int col_SheildHigh_Defense = 4;
    public const int col_SheildLow_Defense = 5;

    public const int row_Helm_Aim = 0;   //Helm
    public const int row_DC_Aim = 1;     //Dexter Chief
    public const int row_CP_Aim = 2;     //Cheif Pale
    public const int row_SC_Aim = 3;     //Sinister Chief
    public const int row_DF_Aim = 4;     //Dexter Fess
    public const int row_FP_Aim = 5;     //Fess Pale
    public const int row_SF_Aim = 6;     //Sinister Fess
    public const int row_Base_Aim = 7;   //Base

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
                return;            
            }

            //take our input integers and figure out what is next
            //TODO: enforce input rules, that's a gui issue, not a matrix issue

            resolve = false;

            joustKnight1resolution(knight1Aim, knight2Defense);
            joustKnight2resolution(knight2Aim, knight1Defense);

        }
    }

    bool Validate()
    {
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

        //debug log a message based on string arrays
        Debug.Log("Knight 1 Aims at " + Aims[knight1aim] + 
                  " Knight2 Defends with " + Defences[knight2def]  );

        int res = resultTable[knight2def, knight1Aim];
       

        //check results for attacker breaking lance, BU, and BUI
        if (res == res_BreakLance)
        {
            Debug.Log("Knight 1 Breaks his lance!");
        }
        else if (res == res_BUI)
        {
            Debug.Log("Knight 1 Breaks his lance!");
            Debug.Log("Knight 2 defender is unhorsed and injured.");
        }
        else if (res == res_BU)
        {
            Debug.Log("Knight 1 Breaks his lance!");
            Debug.Log("Knight 2 defender is unhorsed.");
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

        //debug log a message based on string arrays
        Debug.Log("Knight 2 Aims at " + Aims[knight2aim] +
                  " Knight1 Defends with " + Defences[knight1def]);

        int res = resultTable[knight1def, knight2Aim];

        //check results for attacker breaking lance, BU, and BUI
        if (res == res_BreakLance)
        {
            Debug.Log("Knight 2 Breaks his lance!");
        }
        else if (res == res_BUI)
        {
            Debug.Log("Knight 2 Breaks his lance!");
            Debug.Log("Knight 1 defender is unhorsed and injured.");
        }
        else if (res == res_BU)
        {
            Debug.Log("Knight 2 Breaks his lance!");
            Debug.Log("Knight 1 defender is unhorsed.");
        }
        else
        {
            Debug.Log("The result upon Knight1 defender is " + results[res]);
        }

        return res;

    }

}
