using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Labaratorinis2 : System.Web.UI.Page
{
    //duomenų failai
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
        Spausdinti(Sarasas, Table1);
        SpausdintiPradiniusDuomenis(Table4, Table3, Table5);
        string AtrinktiGrupe = TextBox1.Text;
        SpausdintiAtrinkta(Sarasas, Table2, AtrinktiGrupe);
        SpausdintiRezultatus(Sarasas);
        SpausdintiDuomenis();
    }
    /// <summary>
    /// Skaitomo duomenis 
    /// </summary>
    /// <returns> Gražina studentų sąrašą</returns>
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
    /// <summary>
    /// Spausdina duomenis į lentelę
    /// </summary>
    /// <param name="A"> Perduodamas studentų sąrašas</param>
    /// <param name="lentele"> Į kurią lentelę vesti</param>
    static void Spausdinti(StudentasSarasas A, Table lentele)
    {
        // Sąrašo peržiūra, panaudojant sąsajos metodus
        LentelesAntraste(lentele);
        for (A.Pradžia(); A.Yra(); A.Kitas())
        {
            IterptiIrasa(A.ImtiDuomenis(), lentele);
        }

    }
    /// <summary>
    /// Spausdina nurodytls grupes studentus
    /// </summary>
    /// <param name="A"> Studentų sąrašus</param>
    /// <param name="lentele"> į kurią lentelę vesti</param>
    /// <param name="grupe"> grupės pavadinimas</param>
    static void SpausdintiAtrinkta(StudentasSarasas A, Table lentele, string grupe)
    {
        // Sąrašo peržiūra, panaudojant sąsajos metodus
        LentelesAntraste(lentele);
        for (A.Pradžia(); A.Yra(); A.Kitas())
        {
            if (A.ImtiDuomenis().Grupe == grupe)
            {
                IterptiIrasa(A.ImtiDuomenis(), lentele);
            }
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    /// <summary>
    /// Išveda studentą į lentelę
    /// </summary>
    /// <param name="a"> studentų sąrašas</param>
    /// <param name="lentele"> į kurią lentelę vesti</param>
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
    /// <summary>
    /// sukuriamas lenteles langelis
    /// </summary>
    /// <param name="tekstas"> langelio tekstas</param>
    /// <returns> grtažina langelį</returns>
    static TableCell Elementas(string tekstas)
    {
        TableCell elementas = new TableCell();
        elementas.Text = tekstas;
        return elementas;
    }
    /// <summary>
    /// Sukuriama rezultatų lentelės antraštė
    /// </summary>
    /// <param name="lentele"> į kurią lentelę vesti</param>
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
    /// <summary>
    /// Spausdina pradinius duomenis į lenteles
    /// </summary>
    /// <param name="table1"> į kurią lentelę vesti</param>
    /// <param name="table2"> į kurią lentelę vesti</param>
    void SpausdintiPradiniusDuomenis(Table table1, Table table2, Table table3)
    {
        string path = Server.MapPath(stduomb);
        using (StreamReader reader = new StreamReader(path))
        {
            string[] eilute = reader.ReadLine().Split(';');
            TableRow Eilute = new TableRow();
            Eilute.Cells.Add(Elementas(String.Format("{0}{1};", "Stipendijos fondas - ", eilute[0])));
            Eilute.Cells.Add(Elementas(String.Format("{0}{1}", "Minimalus vidurkis stipendijai gauti - ", eilute[1])));
            table3.Rows.Add(Eilute);
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
                string Pazymiai = line.Substring(1 + line.IndexOf(String.Format(";{0};", duomenys[3])));
                LentelesEilute.Cells.Add(Elementas(Pazymiai));
                table1.Rows.Add(LentelesEilute);
                line = reader.ReadLine();
            }
        }
        string kitaspath = Server.MapPath(stduoma);
        string[] lines = File.ReadAllLines(kitaspath);
        PirmosPradinesLentelesAntraste(table2);
        foreach (string line in lines)
        {
            TableRow KitaLentelesEilute = new TableRow();
            string[] KitiDuomenys = line.Split(';');
            KitaLentelesEilute.Cells.Add(Elementas(KitiDuomenys[0]));
            KitaLentelesEilute.Cells.Add(Elementas(KitiDuomenys[1]));
            table2.Rows.Add(KitaLentelesEilute);
        }


    }
    /// <summary>
    /// Antros pradinės lentelės antraštė
    /// </summary>
    /// <param name="lentele"> į kurią lentelę vesti</param>
    static void AntrosPradinesLentelesAntraste(Table lentele)
    {
        TableRow Eilute = new TableRow();
        Eilute.Cells.Add(Elementas("Eilės numeris"));
        Eilute.Cells.Add(Elementas("Grupė"));
        Eilute.Cells.Add(Elementas("Pažymių kiekis"));
        Eilute.Cells.Add(Elementas("Pažymiai"));
        lentele.Rows.Add(Eilute);
    }
    /// <summary>
    /// Pirmos pradinės lentelės antraštė
    /// </summary>
    /// <param name="lentele"> į kurią lentelę vesti</param>
    static void PirmosPradinesLentelesAntraste(Table lentele)
    {
        TableRow Eilute = new TableRow();
        Eilute.Cells.Add(Elementas("Pavardė Vardas"));
        Eilute.Cells.Add(Elementas("Telefono nr."));
        lentele.Rows.Add(Eilute);
    }
    /// <summary>
    /// Spausdina rezultatus
    /// </summary>
    /// <param name="A"> Studentų sąrašas</param>
    void SpausdintiRezultatus(StudentasSarasas A)
    {
        string path = Server.MapPath("App_Data/Rezultatai.txt");
        using (StreamWriter writer = new StreamWriter(path))
        {
            writer.WriteLine("----------------------------------------------------------------------------------------------------------");
            writer.WriteLine("{0,-35}{1,-16}{2,-15}{3,-20}{4,-30}", "Pavardė Vardas", "Telefono nr.", "Grupė", "Pažymių vidurkis", "Gaunama stipendija");
            writer.WriteLine("----------------------------------------------------------------------------------------------------------");
            for (A.Pradžia(); A.Yra(); A.Kitas())
            {
                string[] duomenys = A.ImtiDuomenis().ToString().Split(';');
                writer.WriteLine("{0,-35}{1,-16}{2,-15}{3,-20}{4,-30}", duomenys[0], duomenys[1], duomenys[2], duomenys[3], duomenys[4]);
            }
            writer.WriteLine("----------------------------------------------------------------------------------------------------------");
        }
    }
    /// <summary>
    /// spausdinami duomenys
    /// </summary>
    void SpausdintiDuomenis()
    {
        string path = Server.MapPath("App_Data/Duomenys.txt");
        using (StreamWriter writer = new StreamWriter(path))
        {
            string path1 = Server.MapPath(stduoma);
            string path2 = Server.MapPath(stduomb);
            using (StreamReader reader = new StreamReader(path2))
            {
                writer.WriteLine("U14a.txt :");
                writer.WriteLine();
                writer.WriteLine("--------------------------------------------------");
                writer.WriteLine("{0,-30}{1,20}", "Pavardė Vardas", "Telefono nr.");
                writer.WriteLine("--------------------------------------------------");
                string[] eilutes = File.ReadAllLines(path1);
                foreach (string eilute in eilutes)
                {
                    string[] duomenys = eilute.Split(';');
                    writer.WriteLine("{0,-30}{1,20}", duomenys[0], duomenys[1]);
                }
                writer.WriteLine("--------------------------------------------------");
                string Eilute = reader.ReadLine();
                string[] Duomenys = Eilute.Split(';');
                writer.WriteLine();
                writer.WriteLine("U14b.txt :");
                writer.WriteLine();
                writer.WriteLine("---------------------------------------------------------------------------------------------");
                writer.WriteLine("Fondas stipendijai: {0}  Reikalavimas Stipendijai: {1}", Duomenys[0], Duomenys[1]);
                writer.WriteLine("---------------------------------------------------------------------------------------------");
                writer.WriteLine("{0,-20}{1,-15}{2,-15}{3,-20}", "Eilės numeris", "Grupė", "Pažymių kiekis", "Pažymiai");
                writer.WriteLine("---------------------------------------------------------------------------------------------");
                Eilute = reader.ReadLine();
                while (Eilute != null)
                {
                    string[] duomenys = Eilute.Split(';');
                    string Pazymiai = Eilute.Substring(1 + Eilute.IndexOf(String.Format(";{0};", duomenys[3])));
                    writer.WriteLine("{0,-20}{1,-15}{2,-15}{3,-20}", duomenys[0], duomenys[1], duomenys[2], Pazymiai);

                    Eilute = reader.ReadLine();
                }
                writer.WriteLine("---------------------------------------------------------------------------------------------");

            }
        }
    }
}