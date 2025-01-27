using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Validate : MonoBehaviour
{

    public JoustMatrix joustMatrix;
    public Dropdown initializer;

    protected void Start()
    {
        Toggle toggle = gameObject.GetComponent<Toggle>();

        //if toggle is not null, and this defense dropdown is K1 (the initializer)
        if (toggle != null && joustMatrix.K1_Defend == initializer)
        {

            //previous round restrictions and limits K1

            if (joustMatrix.prevK1restrict == true && joustMatrix.K1_Defend.value != joustMatrix.col_SteadySeat_Defense)
            {
                //I guess I am shaken, and Must recover
                Debug.Log("RULE: lost helm or broken lance, you must assume steady seat");
                joustMatrix.K1_Defend.value = joustMatrix.col_SteadySeat_Defense;

                //DISABLE THE REST
                if (toggle.name.Contains("0"))
                    toggle.interactable = false;
                if (toggle.name.Contains("1"))
                    toggle.interactable = false;
                if (toggle.name.Contains("2"))
                    toggle.interactable = false;
                if (toggle.name.Contains("4"))
                    toggle.interactable = false;
                if (toggle.name.Contains("5"))
                    toggle.interactable = false;

                if (toggle.name.Contains("3"))
                    toggle.isOn = true;
            }

            //Aim/Defense restriction
            if (joustMatrix.K1_Aim.value == joustMatrix.row_Helm_Aim)
            {

                if (toggle.name.Contains("0"))
                {
                    toggle.interactable = false;
                    toggle.isOn = false;
                }

                if (toggle.name.Contains("1"))
                    toggle.interactable = false;
                if (toggle.name.Contains("2"))
                    toggle.interactable = false;


            }

            if (joustMatrix.K1_Aim.value == joustMatrix.row_DC_Aim)
            {
                if (toggle.name.Contains("0"))
                    toggle.interactable = false;
                if (toggle.name.Contains("1"))
                    toggle.interactable = false;


            }

            if (joustMatrix.K1_Aim.value == joustMatrix.row_SC_Aim)
            {
                if (toggle.name.Contains("0"))
                    toggle.interactable = false;
                if (toggle.name.Contains("2"))
                    toggle.interactable = false;


            }

            if (joustMatrix.K1_Aim.value == joustMatrix.row_DF_Aim)
            {
                if (toggle.name.Contains("0"))
                    toggle.interactable = false;
                if (toggle.name.Contains("1"))
                    toggle.interactable = false;
                if (toggle.name.Contains("2"))
                    toggle.interactable = false;



            }

            if (joustMatrix.K1_Aim.value == joustMatrix.row_SF_Aim)
            {
                if (toggle.name.Contains("0"))
                    toggle.interactable = false;
                if (toggle.name.Contains("1"))
                    toggle.interactable = false;
                if (toggle.name.Contains("2"))
                    toggle.interactable = false;


            }

            if (joustMatrix.K1_Aim.value == joustMatrix.row_Base_Aim)
            {
                if (toggle.name.Contains("1"))
                    toggle.interactable = false;
                if (toggle.name.Contains("2"))
                    toggle.interactable = false;



            }
        }
        //K2 restrictions
        //if toggle is not null, and this defense dropdown is K1 (the initializer)
        if (toggle != null && joustMatrix.K2_Defend == initializer)
        {

            //previous round restrictions and limits K1

            if (joustMatrix.prevK2restrict == true && joustMatrix.K2_Defend.value != joustMatrix.col_SteadySeat_Defense)
            {
                //I guess I am shaken, and Must recover
                Debug.Log("RULE: lost helm or broken lance, you must assume steady seat");
                joustMatrix.K2_Defend.value = joustMatrix.col_SteadySeat_Defense;
                

                //DISABLE THE REST
                if (toggle.name.Contains("0"))
                    toggle.interactable = false;
                if (toggle.name.Contains("1"))
                    toggle.interactable = false;
                if (toggle.name.Contains("2"))
                    toggle.interactable = false;
                if (toggle.name.Contains("4"))
                    toggle.interactable = false;
                if (toggle.name.Contains("5"))
                    toggle.interactable = false;

                if (toggle.name.Contains(joustMatrix.col_SteadySeat_Defense.ToString()))
                    toggle.isOn = true;

            }

            //Aim/Defense restriction
            if (joustMatrix.K2_Aim.value == joustMatrix.row_Helm_Aim)
            {

                if (toggle.name.Contains("0"))
                {
                    toggle.interactable = false;
                    
                }

                if (toggle.name.Contains("1"))
                    toggle.interactable = false;
                if (toggle.name.Contains("2"))
                    toggle.interactable = false;


            }

            if (joustMatrix.K2_Aim.value == joustMatrix.row_DC_Aim)
            {
                if (toggle.name.Contains("0"))
                    toggle.interactable = false;
                if (toggle.name.Contains("1"))
                    toggle.interactable = false;


            }

            if (joustMatrix.K2_Aim.value == joustMatrix.row_SC_Aim)
            {
                if (toggle.name.Contains("0"))
                    toggle.interactable = false;
                if (toggle.name.Contains("2"))
                    toggle.interactable = false;


            }

            if (joustMatrix.K2_Aim.value == joustMatrix.row_DF_Aim)
            {
                if (toggle.name.Contains("0"))
                    toggle.interactable = false;
                if (toggle.name.Contains("1"))
                    toggle.interactable = false;
                if (toggle.name.Contains("2"))
                    toggle.interactable = false;


            }

            if (joustMatrix.K2_Aim.value == joustMatrix.row_SF_Aim)
            {
                if (toggle.name.Contains("0"))
                    toggle.interactable = false;
                if (toggle.name.Contains("1"))
                    toggle.interactable = false;
                if (toggle.name.Contains("2"))
                    toggle.interactable = false;


            }

            if (joustMatrix.K2_Aim.value == joustMatrix.row_Base_Aim)
            {
                if (toggle.name.Contains("1"))
                    toggle.interactable = false;
                if (toggle.name.Contains("2"))
                    toggle.interactable = false;


            }

        }
        
    }
}
