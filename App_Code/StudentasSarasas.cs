using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StudentasSarasas
/// </summary>
public sealed class StudentasSarasas
{
    /// <summary>
    /// vieno elemento klasė
    /// </summary>
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
    public double Fondas { get; private set; } // Stipendijos fondas
    public double Reikalavimai { get; private set; }  // Minimalus Pažimys stipendijai

    /// <summary>
    /// Įveda į studentų sąrašą // Stipendijos fondą ir Minimalų Pažimį stipendijai
    /// </summary>
    /// <param name="fondas"> Stipendijos fondas</param>
    /// <param name="reikalavimai"> Minimalus Pažimys stipendijai</param>
    public void PirmaEilute(double fondas, double reikalavimai)
    {
        Fondas = fondas;
        Reikalavimai = reikalavimai;
    }
    /// <summary>
    /// konstruktorius
    /// </summary>
    public StudentasSarasas()
    {
        pr = null;
        pb = null;
        d = null;
    }
    /// <summary>
    /// Įdeda duomenis atvirkštine tvarka
    /// </summary>
    /// <param name="naujas"> studento duomenys</param>
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
    /// <summary>
    /// rikiavimas pagal stipendijos dydį ir vardą pavardę
    /// </summary>
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
    /// <summary>
    /// Šalina studentus kurie negaus stipendijos
    /// </summary>
    public void SalintiStudentus()
    {
        for (Mazgas d1 = pr; d1 != null; /*d1 = d1.Kitas*/)
        {
            d1.Duom.StipendijosDydis(PinigaiTaskui);
            if (d1.Kitas != null)
                if (d1.Kitas.Duom.ArSkola|| !d1.Kitas.Duom.ArStipendija)
                {
                    d1.Kitas = d1.Kitas.Kitas;
                }
                else
                    d1 = d1.Kitas;
            else
                d1 = d1.Kitas;
        }
        if (pr != null)
            if (pr.Duom.ArSkola || !pr.Duom.ArStipendija)
                pr = pr.Kitas;
    }

    public double PinigaiTaskui { get; private set; } // 10% stipendijos
    /// <summary>
    /// Suskaičiuoja kiek yra 10% stipendijos
    /// </summary>
    public void SkaiciuotiStipendijosTaskus()
    {
        int Taskai = 0;
        for (Mazgas d1 = pr; d1 != null; d1 = d1.Kitas)
        {
            if (d1.Duom.ArGausStipendija(Reikalavimai))
            {
                Taskai = Taskai + 10;
                if (d1.Duom.ArPirmunas)
                    Taskai++;
            }
        }
        PinigaiTaskui = Fondas / Taskai;
    }
}