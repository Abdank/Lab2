using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Studentas
/// </summary>
public class Studentas
{
    public Studentas(string name, string phone, string group, int gradecount, int[] grades, double reikalavimas)
    {
        Name = name;
        Phone = phone;
        Group = group;
        GradesCount = gradecount;
        int[] empty = new int[GradesCount];
        Grades = empty;
        Grades = grades;
        IsTopStudent = IsTop();
        HasDebts = HasDebt();
        ArGausStipendija(reikalavimas);

    }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Group { get; set; }
    public int GradesCount { get; set; }
    public int[] Grades { get; set; }
    public bool IsTopStudent { get; set; }
    public bool HasDebts { get; set; }
    public double Stipendija { get; set; }
    public bool ArStipendija { get; set; }
    public double Vidurkis { get; set; }
    public bool ArGausStipendija(double reikalavimas)
    {
        double vidurkis = 0;
        for (int i = 0; i < GradesCount; i++)
            vidurkis = vidurkis + Grades[i];
        vidurkis = vidurkis / (GradesCount);
        Vidurkis = Math.Round(vidurkis, 2);
        if (vidurkis > reikalavimas)
        {
            ArStipendija = true;
            return true;
        }
        else
        {
            ArStipendija = false;
            return false;
        }
    }
    private bool IsTop()
    {
        bool top = true;
        for (int i = 0; i < GradesCount; i++)
        {
            if (Grades[i] < 9)
                top = false;
        }
        return top;
    }
    private bool HasDebt()
    {
        for (int i = 0; i < GradesCount; i++)
        {
            if (Grades[i] < 5)
            {
                return true;
            }
        }
        return false;
    }
    static public bool operator >(Studentas pirmas, Studentas antras)
    {
        if (pirmas.Stipendija > antras.Stipendija)
            return true;
        else if (antras.Stipendija > pirmas.Stipendija)
            return false;
        else
        {
            int a = String.Compare(pirmas.Name, antras.Name, StringComparison.CurrentCulture);
            return a < 0;
        }
    }
    static public bool operator <(Studentas pirmas,
    Studentas antras)
    {
        return !(pirmas > antras);
    }
    public override string ToString()
    {
        string eilute = string.Format("{0,-30};{1,-12};{2,-15};{3,-10};{4, 20}", Name, Phone, Group, Vidurkis, Stipendija);
        return eilute;
    }
    public void StipendijosDydis(double PinigaiTaskui)
    {
        int taskai = 0;
        if (ArStipendija)
        {
            taskai = 10;
            if (IsTopStudent)
                taskai = 11;
        }
        Stipendija = Math.Floor(PinigaiTaskui * taskai * 100) / 100;
    }

}