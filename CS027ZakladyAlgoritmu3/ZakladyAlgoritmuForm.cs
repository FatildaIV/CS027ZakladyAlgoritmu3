using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS025ZakladyAlgoritmu
{
    public partial class ZakladyAlgoritmuForm : Form
    {
        public ZakladyAlgoritmuForm()
        {
            InitializeComponent();
        }
        
        public delegate string AlgoritmusFunkce(string vstup);
        //  definice struktury záznamu pro algoritmus
        public struct Algoritmus
        {
            // konstrutor pro vytvoření (Inicializaci) záznamu
            public Algoritmus(int cislo, string popis, string pokyny, AlgoritmusFunkce funkce)
            {
                Cislo = cislo;
                Popis = popis;
                Pokyny = pokyny;
                Funkce = funkce;                
            }

            // vlastnosti pro práci se záznamem
            public int Cislo { get; }
            public string Popis { get; }
            public string Pokyny { get; }
            public AlgoritmusFunkce Funkce { get; }           
        }
        // slovník pro uložení a rychlé vyhledání algoritmu podle jejich čísla (Číslo -> Algoritmus)
        static Dictionary<int, Algoritmus> algoritmy = new Dictionary<int, Algoritmus>();

        private void ZakladyAlgoritmuForm_Load(object sender, EventArgs e)
        {
            // Inicializace datového pole algoritmů
            
            

            // vytvoření prázdného seznamu algoritmů
            var seznamAlgoritmu = new List<Algoritmus>();
            seznamAlgoritmu.Add(
                new Algoritmus(
                    1,
                    "Nerovnost součet, jinak trojnásobek",
                    "Zadejte dvě celá čísla, oddělená čárkou",
                    Alg1NerovnostSoucetJinakSoucet3x));

            seznamAlgoritmu.Add(
                new Algoritmus(
                    2,
                    "Když n<51, potom 51 - n, jinak trojnásobek",
                    "Zadejte jedno celé číslo",
                    Alg2RozdilOd51NeboTrojnasobek));

            seznamAlgoritmu.Add(
                new Algoritmus(
                    3,
                    "Jedno z čísel je 30, nebo jejich součet je 30",
                    "Zadejte dvě celá čísla, oddělená čárkou",
                    Alg3JeCisloNeboSoucetTricet));

            seznamAlgoritmu.Add(
                new Algoritmus(
                    4,
                    "Číslo mezi 10 a 100, nebo číslo je 200",
                    "Zadejte jedno celé číslo",
                    Alg4CisloMezi10Az100Nebo200));
            
            seznamAlgoritmu.Add(
                new Algoritmus(
                    9,
                    "Poslední znak na začátek a na konec",
                    "Zadejte libovolný text",
                    Alg9PosledniZnakNaZacatekNaKonec));

            seznamAlgoritmu.Add(
                new Algoritmus(
                    10,
                    "Zadané číslo je dělitelné číslem 3 nebo 7",
                    "Zadejte celé číslo",
                    Alg10CisloNasobek3Nebo7));

            algoritmy = seznamAlgoritmu.ToDictionary(alg => alg.Cislo, alg => alg);            

            foreach (var alg in algoritmy)
            {
                algoritmyComboBox.Items.Add(string.Format("{0}. {1}", alg.Key, alg.Value.Popis));
            }


        }

        private void vstupTextBox_TextChanged(object sender, EventArgs e)
        {
            

            try
            {
                int cisloAlgoritmu = int.Parse (algoritmyComboBox.Text.Split('.')[0]);
                vystupLabel.Text = string.Format("= {0}", algoritmy[cisloAlgoritmu].Funkce(vstupTextBox.Text));
            }
            catch
            {
                vystupLabel.Text = string.Format("= {0}", "?");
            }
        }

        

        private void algoritmyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            pokynyTextBox.Text = algoritmy[int.Parse(algoritmyComboBox.Text.Split('.')[0])].Pokyny;
        }

        private void algoritmyComboBox_KeyDown(object sender, KeyEventArgs e)
        {            
            
            if (e.KeyCode == Keys.Enter)
            {
                try
                {                                      
                    for (int i = 0; i < algoritmyComboBox.Items.Count; i++)                   
                    {
                        if (int.Parse(algoritmyComboBox.Text) == int.Parse(algoritmyComboBox.Items[i].ToString().Split('.')[0]))
                        {
                            algoritmyComboBox.SelectedIndex = i;
                        }
                    }
                }
                catch
                {
                    algoritmyComboBox.Text += " Neexistuje";
                    
                }
                algoritmyComboBox.Select(0, algoritmyComboBox.Text.Length);
            }
        }

        static string Alg1NerovnostSoucetJinakSoucet3x(string vstup)
        {
            string[] data = vstup.Split(',');
            return NerovnostSoucetJinakSoucet3x(int.Parse(data[0]), int.Parse(data[1])).ToString();
        }
        static int NerovnostSoucetJinakSoucet3x(int a, int b)
        {
            return (a == b) ? (3 * (a + b)) : (a + b);
        }
        
        
        static string Alg2RozdilOd51NeboTrojnasobek(string vstup)
        {
            return RozdilOd51NeboTrojnasobek(int.Parse(vstup)).ToString();
        }
        static int RozdilOd51NeboTrojnasobek(int n)
        {
            return (n < 51) ? Math.Abs(51 - n) : (3 * Math.Abs(51 - n));
        }

        static string Alg3JeCisloNeboSoucetTricet(string vstup)
        {
            string[] data = vstup.Split(',');
            return JeCisloNeboSoucetTricet(int.Parse(data[0]), int.Parse(data[1])).ToString();
        }

        static bool JeCisloNeboSoucetTricet(int a, int b)
        {
            return (a == 30) || (b == 30) || ((a + b) == 30);
        }

        static string Alg4CisloMezi10Az100Nebo200(string vstup)
        {
            return CisloMezi10Az100Nebo200(int.Parse(vstup)).ToString();
        }

        static bool CisloMezi10Az100Nebo200(int n)
        {
            return (n >= 10) && (n <= 100) || (n == 200) ;
        }


        static string Alg9PosledniZnakNaZacatekNaKonec(string vstup)
        {
            var s = vstup.Substring(vstup.Length - 1);
            return s + vstup + s;
        }
        static string Alg10CisloNasobek3Nebo7(string vstup)
        {
            return CisloNasobek3Nebo7(int.Parse(vstup)).ToString();
        }
        static bool CisloNasobek3Nebo7(int n)
        {
            return (n % 3 == 0 || n % 7 == 0);
        }
    }
}
