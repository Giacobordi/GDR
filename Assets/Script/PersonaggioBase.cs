using System.Collections;
using System.Collections.Generic;
using UnityEditor.Media;
using UnityEngine;

public class PersonaggioBase
{
    public double Vita 
    { 
        get
        {
            return vita;
        }
        set
        {
            vita = value;
            if (vita <= 0)
            {
                vita = 0;
            }
        } 
    }
    public double Attacco 
    {
        get
        {
            return attacco;
        }
        set
        {
            attacco = value;
            if(attacco <= 0) 
            { 
                attacco = 0;
            }
        } 
    }
    public double Difesa 
    {
        get
        {
            return difesa;
        }
        set
        {
            difesa = value;
            if(difesa <= 0)
            {
                difesa = 0;
            }
        } 
    }

    private double vita;
    private double attacco;
    private double difesa;


    public PersonaggioBase(double vita, double attacco, double difesa) 
    {
        Vita = vita;
        Attacco = attacco;
        Difesa = difesa;
    }








}
