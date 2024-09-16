//Gioco
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;



public class GameHandler : MonoBehaviour
{

    public Giocatore Hermann;
    public Nemico programmazione;
    public Nemico programmazione_2;
    public int livello;
    public int npozioni;
    public double mod;


    //public GameObject ESCI;
    //public GameObject DINUOVO;
    public GameObject GAMEOVER;
    public GameObject CREANEMICI;
    public GameObject ATTACCO1;
    public GameObject ATTACCO2;
    public GameObject CURA;


    [SerializeField]
    private Slider VitaHermann;

    [SerializeField]
    private TextMeshProUGUI txtVitahermann;

    [SerializeField]
    private TextMeshProUGUI txtVitanemico;

    [SerializeField]
    private TextMeshProUGUI txtVitanemico_2;

    [SerializeField]
    private TextMeshProUGUI livelloGiocato;

    [SerializeField]
    private TextMeshProUGUI numeroPozioni;

    [SerializeField]
    private TextMeshProUGUI difesaGiocatore;





    void Start()
    {
        GAMEOVER.SetActive(false);
        CREANEMICI.SetActive(false);
        ATTACCO1.SetActive(true);
        ATTACCO2.SetActive(true);
        CURA.SetActive(true);
        livello = 1;
        npozioni = 2;
        mod = 0.1;
        Hermann = new Giocatore("hERMANN", 50, 50, 15, 5);

        programmazione = new Nemico("EDO", Random.Range(50, 100) * mod, Random.Range(50, 70) * mod, Random.Range(1, 8));
        programmazione_2 = new Nemico("GIACOMOBORDIGA", Random.Range(50, 100) * mod, Random.Range(50, 70) * mod, Random.Range(1, 8));
        
        txtVitahermann.text = (Convert.ToInt32(Hermann.Vita)).ToString();
        VitaHermann.value = 50;
        VitaHermann.maxValue = 50;
        txtVitanemico.text = (Convert.ToInt32(programmazione.Vita)).ToString();
        txtVitanemico_2.text = (Convert.ToInt32(programmazione_2.Vita)).ToString();
        livelloGiocato.text = livello.ToString();
        numeroPozioni.text = npozioni.ToString();
        difesaGiocatore.text = Hermann.Difesa.ToString();


    }

    public void CreaNemico()
    {
        NuovoLivello();
        CREANEMICI.SetActive(false);
        ATTACCO1.SetActive(true);
        ATTACCO2.SetActive(true);
        CURA.SetActive(true);
        programmazione = new Nemico("EDO", Random.Range(50, 100) * mod, Random.Range(50, 70) * mod, Random.Range(livello, 5 + livello));
        programmazione_2 = new Nemico("GIACOMOBORDIGA", Random.Range(50, 100) * mod, Random.Range(50, 70) * mod, Random.Range(livello, 5 + livello));
        Updatevita();
    }

    public void NuovoLivello()
    {
        Hermann.Vita = Hermann.Vita + livello + 10;
        Hermann.Attacco = Hermann.Attacco + livello + 5;
        Hermann.Difesa = Hermann.Difesa + livello;
        Debug.Log(Hermann.Attacco);
        livello = livello + 1;
        mod = livello / 10.0;
        UpdateLivello();

        if (livello % 10 == 0)
        {
            npozioni = npozioni + (livello / 5);
            numeroPozioni.text = npozioni.ToString();

            Hermann.Vitamax = Hermann.Vitamax + livello + 20;                   //slider value
            VitaHermann.maxValue = (float)Hermann.Vitamax + livello + 20;       //slidermax

            Hermann.Vita = Hermann.Vita + livello + 10;

        }

        Updatevita();
        difesaGiocatore.text = Hermann.Difesa.ToString();
    }

    public void CHIUDI()
    {
        Application.Quit();
    }
    public void RESTART()
    {
        Start();
    }


    private void UpdateLivello()
    {
        livelloGiocato.text = livello.ToString();
    }
    private void Updatevita()
    {
        if (Hermann.Vita > Hermann.Vitamax)
        {
            Hermann.Vita = Hermann.Vitamax;
        }
        txtVitahermann.text = (Convert.ToInt32(Hermann.Vita)).ToString();
        VitaHermann.value = (float)Hermann.Vita; //slider
        txtVitanemico.text = (Convert.ToInt32(programmazione.Vita)).ToString();
        txtVitanemico_2.text = (Convert.ToInt32(programmazione_2.Vita)).ToString();

        if (Hermann.Vita == 0)
        {
            GAMEOVER.SetActive(true);
        }
    }


    public void Attacca(PersonaggioBase attaccante, PersonaggioBase difensore)
    {
        double danno = attaccante.Attacco - difensore.Difesa;
        if (danno > 0)
        {
            difensore.Vita -= danno;
        }
    }
    public void Attacco_1()
    {
        Attacca(Hermann, programmazione);
        Updatevita();
        if (programmazione_2.Vita > 0.5)
        {
            Attacca(programmazione_2, Hermann);
            Updatevita();
        }
        if (programmazione.Vita > 0.5)
        {
            Attacca(programmazione, Hermann);
            Updatevita();
        }
        else
        {
            ATTACCO1.SetActive(false);

            if (programmazione_2.Vita <= 0.5)
            {
                CURA.SetActive(false);
                CREANEMICI.SetActive(true);
            }
        }
    }
    public void Attacco_2()
    {
        Attacca(Hermann, programmazione_2);
        Updatevita();
        if (programmazione.Vita > 0.5)
        {
            Attacca(programmazione, Hermann);
            Updatevita();
        }

        if (programmazione_2.Vita > 0.5)
        {
            Attacca(programmazione_2, Hermann);
            Updatevita();
        }
        else
        {
            ATTACCO2.SetActive(false);

            if (programmazione.Vita <= 0.5)
            {
                CURA.SetActive(false);
                CREANEMICI.SetActive(true);
            }
        }

    }


    public void Curami(Giocatore hermann)
    {
        if (hermann.Vita > 0)
        {
            hermann.Vita = hermann.Vita + 80 * mod;
        }
    }
    public void Cura()
    {
        if (npozioni > 0)
        {
            Curami(Hermann);
            Updatevita();

            if (programmazione.Vita > 0.5)
            {
                Attacca(programmazione, Hermann);
                Updatevita();
            }
            if (programmazione_2.Vita > 0.5)
            {
                Attacca(programmazione_2, Hermann);
                Updatevita();
            }

            npozioni = npozioni - 1;
            numeroPozioni.text = npozioni.ToString();
        }
    }

}


