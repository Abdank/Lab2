using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StudentasSarasas
/// </summary>
public sealed class StudentasSarasas
{
    private sealed class Mazgas
    {
        public Studentas Duom { get; set; }
        public Mazgas Kitas { get; set; }
        public Mazgas(Studentas a, Mazgas adr)
        {
            Duom = a;
            Kitas = adr;
        }
    }
    private Mazgas pr; // sąrašo pradžią
    private Mazgas pb; // sąrašo pabaiga
    private Mazgas d; // sąrašo sąsaja
    public double Fondas { get; private set; }
    public double Reikalavimai { get; private set; }

    public void PirmaEilute(double fondas, double reikalavimai)
    {
        Fondas = fondas;
        Reikalavimai = reikalavimai;
    }
    public StudentasSarasas()
    {
        pr = null;
        pb = null;
        d = null;
    }
    public void DetiDuomenis(Studentas naujas)
    {
        pr = new Mazgas(naujas, pr);
    }
    /** Sąsajai priskiriama sąrašo pradžia */
    public void Pradžia()
    { d = pr; }
    /** Sąsajai priskiriamas tolesnis sąrašo elementas */
    public void Kitas()
    { d = d.Kitas; }
    /** Grąžina true, jeigu sąrašas netuščias */
    public bool Yra()
    { return d != null; }
    /** Grąžina sąrašo sąsajos elemento reikšmę */
    public Studentas ImtiDuomenis()
    { return d.Duom; }
    public void Rikiuoti()
    {
        for (Mazgas d1 = pr; d1 != null; d1 = d1.Kitas)
        {
            Mazgas minv = d1;
            for (Mazgas d2 = d1.Kitas; d2 != null; d2 = d2.Kitas)
                if (d2.Duom > minv.Duom)
                    minv = d2;
            // Informacinių dalių sukeitimas vietomis
            Studentas St = d1.Duom;
            d1.Duom = minv.Duom;
            minv.Duom = St;
        }
    }
    public void SalintiStudentus()
    {
        for (Mazgas d1 = pr; d1 != null; /*d1 = d1.Kitas*/)
        {
            d1.Duom.StipendijosDydis(PinigaiTaskui);
            if (d1.Kitas != null)
                if (d1.Kitas.Duom.HasDebts || !d1.Kitas.Duom.ArStipendija)
                {
                    d1.Kitas = d1.Kitas.Kitas;
                }
                else
                    d1 = d1.Kitas;
            else
                d1 = d1.Kitas;
        }
        if (pr != null)
            if (pr.Duom.HasDebts || !pr.Duom.ArStipendija)
                pr = pr.Kitas;
    }
    public double PinigaiTaskui { get; private set; }
    public void SkaiciuotiStipendijosTaskus()
    {
        int Taskai = 0;
        for (Mazgas d1 = pr; d1 != null; d1 = d1.Kitas)
        {
            if (d1.Duom.ArGausStipendija(Reikalavimai))
            {
                Taskai = Taskai + 10;
                if (d1.Duom.IsTopStudent)
                    Taskai++;
            }
        }
        PinigaiTaskui = Fondas / Taskai;
    }
}