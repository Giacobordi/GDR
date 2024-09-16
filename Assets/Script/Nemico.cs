using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nemico : PersonaggioBase
{
    public string Nome {  get; set; }
    private string nome;
    public Nemico(string nome, double vitan, double attaccon, double difesan) : base(vitan, attaccon, difesan)
    {
        Nome = nome;

    }
}

public class Giocatore : PersonaggioBase
{
    public string Nome { get; set; }
    public double Vitamax { get; set; }


    private string nome;
    private double vitamax;
    public Giocatore(string nome, double vitamax, double _vita, double _attacco, double _difesa) : base(_vita, _attacco, _difesa)
    { 
        Nome = nome;   
        Vitamax = vitamax;
       
    }
}


