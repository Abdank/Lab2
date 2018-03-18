using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Labaratorinis2 : System.Web.UI.Page
{
    //public const string stduoma = "App_Data/U14a.txt";
    //public const string stduomb = "App_Data/U14b.txt";
    public const string stduoma = "App_Data/U14aa.txt";
    public const string stduomb = "App_Data/U14ba.txt";
    protected void Page_Load(object sender, EventArgs e)
    {

        StudentasSarasas Sarasas = skaitymas();
        Sarasas.SkaiciuotiStipendijosTaskus();
        Sarasas.SalintiStudentus();
        Sarasas.Rikiuoti();
        Spausdinti(Sarasas,Table1);
        SpausdintiPradiniusDuomenis(Table4, Table3);
    }
    private StudentasSarasas skaitymas()
    {
        StudentasSarasas sarasas = new StudentasSarasas();
        string path = Server.MapPath(stduomb);
        using (StreamReader reader1 = new StreamReader(@path))
        {
            string[] duomenys = reader1.ReadLine().Split(';');
            sarasas.PirmaEilute(double.Parse(duomenys[0]), double.Parse(duomenys[1]));
            string kitaspath = Server.MapPath(stduoma);
            string[] eilutes = File.ReadAllLines(kitaspath);
            foreach (string eilute in eilutes)
            {
                string[] StudentoDuomenys = reader1.ReadLine().Split(';');
                string[] eiluteA = eilute.Split(';');
                int[] pazymiai = new int[int.Parse(StudentoDuomenys[2])];
                int j = 0;
                for (int i = 3; i < 3 + int.Parse(StudentoDuomenys[2]); i++)
                {
                    pazymiai[j++] = int.Parse(StudentoDuomenys[i]);
                }

                Studentas studentas = new Studentas(eiluteA[0], eiluteA[1], StudentoDuomenys[1], int.Parse(StudentoDuomenys[2]), pazymiai, sarasas.Reikalavimai);

                sarasas.DetiDuomenis(studentas);
            }
        }
        return sarasas;
    }
    static void Spausdinti(StudentasSarasas A, Table lentele)
    {
        // Sąrašo peržiūra, panaudojant sąsajos metodus
        LentelesAntraste(lentele);
        for (A.Pradžia(); A.Yra(); A.Kitas())
        {
            IterptiIrasa(A.ImtiDuomenis(), lentele);
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {


    }
    static void IterptiIrasa(Studentas a, Table lentele)
    {
        TableRow eilute = new TableRow();
        string[] Duomenys = a.ToString().Split(';');
        for (int i = 0; i < 5; i++)
        {
            eilute.Cells.Add(Elementas(Duomenys[i]));
        }
        lentele.Rows.Add(eilute);

    }
    static TableCell Elementas( string tekstas)
    {
        TableCell elementas = new TableCell();
        elementas.Text = tekstas;
        return elementas;
    }
    static void LentelesAntraste(Table lentele)
    {
        TableRow Eilute = new TableRow();
        Eilute.Cells.Add(Elementas("Pavardė Vardas"));
        Eilute.Cells.Add(Elementas("Telefono nr."));
        Eilute.Cells.Add(Elementas("Grupė"));
        Eilute.Cells.Add(Elementas("Pažymių vidurkis"));
        Eilute.Cells.Add(Elementas("Gaunama stipendija"));
        lentele.Rows.Add(Eilute);
    }
    void SpausdintiPradiniusDuomenis(Table table1, Table table2)
    {
        string path = Server.MapPath(stduomb);
        using (StreamReader reader = new StreamReader(path))
        {
            string[] eilute = reader.ReadLine().Split(';');
            TableRow Eilute=new TableRow();
            Eilute.Cells.Add(Elementas(String.Format("{0}{1}", "Stipendijos fondas - ", eilute[0])));
            Eilute.Cells.Add(Elementas(String.Format("{0}{1}", "Minimalus vidurkis stipendijai gauti - ", eilute[1])));
            AntrosPradinesLentelesAntraste(table1);
            string line = reader.ReadLine();
            while (line != null)
            {
                string[] duomenys = line.Split(';');
                TableRow LentelesEilute = new TableRow();
                for (int i = 0; i < 3; i++)
                {
                    LentelesEilute.Cells.Add(Elementas(duomenys[i]));
                }
                string Pazymiai = line.Substring(1+line.IndexOf(String.Format(";{0};", duomenys[3])));
                LentelesEilute.Cells.Add(Elementas(Pazymiai));
                table1.Rows.Add(LentelesEilute);
                line = reader.ReadLine();
            }
        }
        string kitaspath = Server.MapPath(stduoma);
        string[] lines = File.ReadAllLines(kitaspath);
        PirmosPradinesLentelesAntraste(table2);
        foreach(string line in lines)
        {
            TableRow KitaLentelesEilute = new TableRow();
            string[] KitiDuomenys = line.Split(';');
            KitaLentelesEilute.Cells.Add(Elementas(KitiDuomenys[0]));
            KitaLentelesEilute.Cells.Add(Elementas(KitiDuomenys[1]));
            table2.Rows.Add(KitaLentelesEilute);
        }


    }
    static void AntrosPradinesLentelesAntraste(Table lentele)
    {
        TableRow Eilute = new TableRow();
        Eilute.Cells.Add(Elementas("Eilės numeris"));
        Eilute.Cells.Add(Elementas("Grupė"));
        Eilute.Cells.Add(Elementas("Pažymių kiekis"));
        Eilute.Cells.Add(Elementas("Pažymiai"));
        lentele.Rows.Add(Eilute);
    }
    static void PirmosPradinesLentelesAntraste(Table lentele)
    {
        TableRow Eilute = new TableRow();
        Eilute.Cells.Add(Elementas("Pavardė Vardas"));
        Eilute.Cells.Add(Elementas("Telefono nr."));
        lentele.Rows.Add(Eilute);
    }
}