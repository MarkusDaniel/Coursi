using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Data;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Tanfolyam_nyilvántartás
{
    public partial class Form1 : Form
    {
        // át kell írni  a connenction helyére hogy elérje az adatbázist.
        private const string constring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\AdatbazisT.mdf;Integrated Security=True;Connect Timeout=30";

        public Form1()
        {
            InitializeComponent();
            PopulateTanarComboBox();
            PopulateDiakComboBox();
            PopulateKurzusComboBox();
            
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            ModositButton.Visible = false;
            KatBox.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            DiagselectBox.Visible = false;
            DiagselectBox.Items.Add("Vonal");
            DiagselectBox.Items.Add("Kör");
            BevetMutatButton.Visible = false;
            BevetelDiag.Series.Clear();
            LetszamDiag.Series.Clear();
            BevetelDiag.Visible = false;
            LetszMutatoButton.Visible = false;
            KezdetLabel.Visible = false;
            VegeLabel.Visible = false;
            IntervalKezdet.Visible = false;
            IntervalVege.Visible = false;
            LetszamDiag.Visible = false;
            KuAdatButton.Visible = false;
            LezarasButton.Visible = false;
            DiakokBox.Visible = false;
            comboBox1.Visible = false;
            tanarbox.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            button1.Visible = false;
            KurzusList.Visible = false;
            BefizetesButton.Visible = false;
            FelvDiakButton.Visible = false;
            AktivalasButton.Visible = false;
            comboBox1.Items.Add("Tanfolyam");
            comboBox1.Items.Add("Tanár");
            comboBox1.Items.Add("Diák");
            KatBox.Items.Add("Tanfolyam");
            KatBox.Items.Add("Tanár");
            KatBox.Items.Add("Diák");
            Point originalTanarboxPosition = tanarbox.Location;
            tanarbox.Tag = originalTanarboxPosition;
            Point originalDiakokBoxPosition = textBox2.Location;
            DiakokBox.Tag = originalDiakokBoxPosition;
            KurzusBox.Visible = false;

        }

        // szélesség állítás a kurzus listnek attól függően milyen hosszú a text
        private void AdjustKurzusListWidth()
        {
            int maxWidth = 280;
            foreach (var item in KurzusList.Items)
            {
                int itemWidth = TextRenderer.MeasureText(item.ToString(), KurzusList.Font).Width;
                maxWidth = Math.Max(maxWidth, itemWidth);
            }
            maxWidth += 0;
            KurzusList.Width = maxWidth;
        }
        // tanár box feltöltése tanárokkal
        private void PopulateTanarComboBox()
        {
            try
            {
                string query = "SELECT Nev FROM [dbo].[Tanar]";

                using (SqlConnection connection = new SqlConnection(constring))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    tanarbox.Items.Clear();

                    int count = 0;
                    while (reader.Read())
                    {
                        string tanarNev = reader.GetString(0);
                        tanarbox.Items.Add(tanarNev);
                        count++;
                    }

                    reader.Close();


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        // diák box feltöltése diákokkal
        private void PopulateDiakComboBox()
        {
            try
            {
                string query = "SELECT Nev FROM [dbo].[Diak]";

                using (SqlConnection connection = new SqlConnection(constring))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    DiakokBox.Items.Clear();

                    int count = 0;
                    while (reader.Read())
                    {
                        string tanarNev = reader.GetString(0);
                        DiakokBox.Items.Add(tanarNev);
                        count++;
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        // bevitel gomb megnyomása, az adatok beinsertelésére
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {

                // dátum formátum hibakezelés
                if (!DateTime.TryParse(textBox2.Text, out DateTime kezdetDatuma))
                {
                    MessageBox.Show("Dátum formátuma helytelen.", "Hibás adat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Stop further execution
                }
                // hiányzó név hiba
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Kötelező nevet adni.", "Hiányzó információ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Stop further execution
                }
                // hiányzó költség
                if (string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("Kötelező költséget megadni.", "Hiányzó információ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Stop further execution
                }
                // egész számnak kell lennie a költségnek
                if (!int.TryParse(textBox3.Text, out int koltsegPerFo))
                {
                    MessageBox.Show("Egész számnak kell lennie a költségnek.", "Hibás adat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Stop further execution
                }
                // kötelező tanárt megadni
                if (tanarbox.SelectedItem == null)
                {
                    MessageBox.Show("A tanár megadása kötelező.", "Hiányzó információ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Stop further execution
                }
                int tanarId = -1; // Initialize to an invalid value
                if (tanarbox.SelectedItem != null)
                {
                    string tanarNev = tanarbox.SelectedItem.ToString(); // Assuming tanarbox.SelectedItem returns the Tanar's name
                    string queryGetTanarId = $"SELECT TanarId FROM Tanar WHERE Nev = '{tanarNev}'";

                    using (SqlConnection connection = new SqlConnection(constring))
                    {
                        SqlCommand command = new SqlCommand(queryGetTanarId, connection);
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            tanarId = Convert.ToInt32(result);
                        }
                    }

                }

                // Insert into Tanfolyam table with TanarID
                string query2 = $"INSERT INTO [dbo].[Tanfolyam](Nev, KezdetDatuma, KoltsegPerFo, TanarID) VALUES('{textBox1.Text}','{textBox2.Text}', '{textBox3.Text}', {tanarId})";
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    SqlCommand command = new SqlCommand(query2, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Close();
                }
                PopulateKurzusComboBox();
                //idáig
                textBox1.Text = string.Empty;
                textBox3.Text = string.Empty;
                textBox2.Text = string.Empty;
                tanarbox.SelectedItem = null;
            }
            if (comboBox1.SelectedIndex == 1)
            {
                label4.Visible = false;
                // hiányzó név hiba
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Kötelező nevet adni.", "Hiányzó információ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Stop further execution
                }
                //innen
                string query2 = $"INSERT INTO [dbo].[Tanar](Nev) VALUES('{textBox1.Text}')";
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    SqlCommand command = new SqlCommand(query2, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Close();
                }
                PopulateTanarComboBox();
                //idáig
                textBox1.Text = string.Empty;
                textBox3.Text = string.Empty;
                textBox2.Text = string.Empty;
            }
            if (comboBox1.SelectedIndex == 2)
            {
                label4.Visible = false;
                // hiányzó név hiba
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Kötelező nevet adni.", "Hiányzó információ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Stop further execution
                }
                //innen
                string query2 = $"INSERT INTO [dbo].[Diak](Nev, SzamlazasiNev, SzamlazasiCim) VALUES('{textBox1.Text}','{textBox2.Text}', '{textBox3.Text}')";

                using (SqlConnection connection = new SqlConnection(constring))
                {
                    SqlCommand command = new SqlCommand(query2, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Close();
                }
                PopulateDiakComboBox();
                //idáig
                textBox1.Text = string.Empty;
                textBox3.Text = string.Empty;
                textBox2.Text = string.Empty;
            }
        }
        // --OLDAL MENÜ-- //
        //adatfelvétel 
        private void button2_Click(object sender, EventArgs e)
        {
            ModositButton.Visible  = false;
            KatBox.Visible = false;
            if (comboBox1.SelectedIndex == 0)
            { label4.Visible = true; }
            label3.Visible = false;
            label2.Visible = false;
            label1.Visible = true;
            DiagselectBox.Visible = false;
            BevetMutatButton.Visible = false;
            BevetelDiag.Visible = false;
            LetszMutatoButton.Visible = false;
            KezdetLabel.Visible = false;
            VegeLabel.Visible = false;
            IntervalKezdet.Visible = false;
            IntervalVege.Visible = false;
            BefizetesButton.Visible = false;
            KuAdatButton.Visible = false;
            LezarasButton.Visible = false;
            DiakokBox.Visible = false;
            button2.Visible = true;
            comboBox1.Visible = true;
            textBox1.Visible = true;
            button1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            tanarbox.SelectedItem = null;
            KurzusList.Visible = false;
            comboBox1.SelectedIndex = 0;
            KurzusBox.Visible = false;
            KurzusBox.SelectedItem = null;
            DiakokBox.SelectedItem = null;
            FelvDiakButton.Visible = false;
            AktivalasButton.Visible = false;
            LetszamDiag.Visible = false;

            if (tanarbox.Tag is Point originalPosition)
            {
                tanarbox.Location = originalPosition;
            }

        }
        //modosítás menüpont
        private void AModButton_Click(object sender, EventArgs e)
        {
            ModositButton.Visible = true;
            DiagselectBox.Visible = false;
            BevetMutatButton.Visible=false;
            label1.Visible = true;
            KatBox.Visible = true;
            label4.Visible = false;
            label2.Visible = true;
            label3.Visible = false;
            KatBox.SelectedIndex = 0;
            BevetelDiag.Visible = false;
            LetszMutatoButton.Visible = false;
            KezdetLabel.Visible = false;
            VegeLabel.Visible = false;
            IntervalKezdet.Visible = false;
            IntervalVege.Visible = false;
            LetszamDiag.Visible = false;
            KuAdatButton.Visible = false;
            LezarasButton.Visible = false;
            DiakokBox.Visible = false;
            comboBox1.Visible = false;
            tanarbox.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = true;
            textBox3.Visible = true;
            button1.Visible = false;
            KurzusList.Visible = false;
            BefizetesButton.Visible = false;
            FelvDiakButton.Visible = false;
            AktivalasButton.Visible = false;

        }
        // tanáraink tanár menü
        private void button3_Click(object sender, EventArgs e)
        {
            ModositButton.Visible = false;
            label4.Visible = false;
            KatBox.Visible = false;
            label3.Visible = false;
            label3.Visible = false;
            label2.Visible = true;
            label2.Text = "Tanár";
            label2.Location = new Point(177, 67);
            label1.Visible = false;
            DiagselectBox.Visible = false;
            BevetMutatButton.Visible = false;
            BevetelDiag.Visible = false;
            LetszMutatoButton.Visible = false;
            KezdetLabel.Visible = false;
            VegeLabel.Visible = false;
            IntervalKezdet.Visible = false;
            IntervalVege.Visible = false;
            BefizetesButton.Visible = false;
            KuAdatButton.Visible = false;
            LezarasButton.Visible = false;
            DiakokBox.Visible = false;
            button2.Visible = true;
            comboBox1.Visible = false;
            textBox1.Visible = false;
            button1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            tanarbox.Visible = true;
            KurzusList.Visible = true;
            KurzusList.Items.Clear();
            tanarbox.SelectedItem = null;
            tanarbox.Location = textBox1.Location;
            KurzusBox.Visible = false;
            KurzusBox.SelectedItem = null;
            DiakokBox.SelectedItem = null;
            FelvDiakButton.Visible = false;
            AktivalasButton.Visible = false;
            LetszamDiag.Visible = false;
        }
        // diákok menüje, befizetés kezelés
        private void Diakok_Button_Click(object sender, EventArgs e)
        {
            tanarbox.SelectedItem = null;
            ModositButton.Visible = false;
            label4.Visible = false;
            KatBox.Visible = false;
            label3.Visible = false;
            label3.Visible = false;
            label2.Visible = true;
            label2.Text = "Diák";
            label2.Location = new Point(188, 67);
            label1.Visible = false;
            DiagselectBox.Visible = false;
            BevetMutatButton.Visible = false;
            BevetelDiag.Visible = false;
            LetszMutatoButton.Visible = false;
            KezdetLabel.Visible = false;
            VegeLabel.Visible = false;
            IntervalKezdet.Visible = false;
            IntervalVege.Visible = false;
            BefizetesButton.Visible = true;
            KuAdatButton.Visible = false;
            LezarasButton.Visible = false;
            button1.Visible = false;
            tanarbox.Visible = false;
            DiakokBox.SelectedItem = null;
            KurzusBox.SelectedItem = null;
            KurzusList.Visible = true;
            DiakokBox.Visible = true;
            DiakokBox.Location = textBox1.Location;
            comboBox1.Visible = false;
            button2.Visible = true;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            KurzusBox.Visible = false;
            DiakokBox.SelectedItem = null;
            tanarbox.SelectedItem = null;
            FelvDiakButton.Visible = false;
            AktivalasButton.Visible = false;
            KurzusList.Items.Clear();
            LetszamDiag.Visible = false;
        }
        //kurzus beállítás menüje
        private void KurzusButton_Click(object sender, EventArgs e)
        {
            ModositButton.Visible = false;
            label4.Visible = false;
            KatBox.Visible = false;
            label4.Visible = false;
            label3.Visible = false;
            label3.Visible = true;
            label2.Visible = true;
            label2.Text = "Tanfolyam";
            label2.Location = new Point(152, 67);
            label1.Visible = false;
            DiagselectBox.Visible = false;
            BevetMutatButton.Visible = false;
            BevetelDiag.Visible = false;
            LetszMutatoButton.Visible = false;
            KezdetLabel.Visible = false;
            VegeLabel.Visible = false;
            IntervalKezdet.Visible = false;
            IntervalVege.Visible = false;
            BefizetesButton.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            tanarbox.Visible = false;
            KuAdatButton.Visible = true;
            LezarasButton.Visible = true;
            KurzusBox.Visible = true;
            comboBox1.Visible = false;
            button2.Visible = true;
            DiakokBox.SelectedItem = null;
            KurzusBox.Location = textBox1.Location;
            KurzusList.Visible = false;
            DiakokBox.Visible = true;
            DiakokBox.Location = textBox2.Location;
            FelvDiakButton.Visible = true;
            AktivalasButton.Visible = true;
            LetszamDiag.Visible = false;
        }
        //Létszám menüje
        private void LetszamButton_Click(object sender, EventArgs e)
        {
            
            ModositButton.Visible = false;
            label4.Visible = false;
            KatBox.Visible = false;
            label3.Visible = false;
            label3.Visible = false;
            label2.Visible = true;
            label2.Location = new Point(177, 67);
            label2.Text = "Tanár";
            label1.Visible = false;
            DiagselectBox.Visible = false;
            BevetMutatButton.Visible = false;
            BevetelDiag.Visible = false;
            LetszamDiag.Series.Clear();
            LetszMutatoButton.Visible = true;
            KezdetLabel.Visible = true;
            VegeLabel.Visible = true;
            IntervalKezdet.Visible = true;
            IntervalVege.Visible = true;
            LetszamDiag.Visible = true;
            tanarbox.SelectedItem = null;
            tanarbox.Location = textBox1.Location;
            KurzusBox.Visible = false;
            KuAdatButton.Visible = false;
            LezarasButton.Visible = false;
            DiakokBox.Visible = false;
            comboBox1.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            button1.Visible = false;
            KurzusList.Visible = false;
            BefizetesButton.Visible = false;
            FelvDiakButton.Visible = false;
            AktivalasButton.Visible = false;
            tanarbox.Visible = true;
        }
        // Bevételek menüpont
        private void BevButton_Click(object sender, EventArgs e)
        {
            KatBox.Visible = false;
            ModositButton.Visible = false;
            label4.Visible = false;
            label3.Visible = false;
            label3.Visible = false;
            label1.Visible = false;
            label2.Visible = true;
            label2.Location = new Point(162, 67);
            label2.Text = "Diagram";
            DiagselectBox.Visible = true;
            BevetelDiag.Series.Clear();
            BevetMutatButton.Visible = true;
            BevetelDiag.Visible = true;
            LetszMutatoButton.Visible = false;
            KezdetLabel.Visible = true;
            VegeLabel.Visible = true;
            IntervalKezdet.Visible = true;
            IntervalVege.Visible = true;
            LetszamDiag.Visible = false;
            tanarbox.SelectedItem = null;
            KuAdatButton.Visible = false;
            LezarasButton.Visible = false;
            DiakokBox.Visible = false;
            KurzusBox.Visible = false;
            comboBox1.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            button1.Visible = false;
            KurzusList.Visible = false;
            BefizetesButton.Visible = false;
            FelvDiakButton.Visible = false;
            AktivalasButton.Visible = false;
            tanarbox.Visible = false;
        }
        // mit akarunk felvinni a rendszerbe? új tanár új diák új kurzus
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 2)
            {
                label4.Visible = false;
                DiakokBox.Visible = false;
                comboBox1.Visible = true;
                tanarbox.Visible = false;
                textBox1.Visible = true;
                button1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                KurzusList.Visible = false;
                textBox1.PlaceholderText = "Diák Neve";
                textBox2.PlaceholderText = "Számlázási Név";
                textBox3.PlaceholderText = "Számlázási Cím";
                KurzusBox.Visible = false;
            }

            if (comboBox1.SelectedIndex == 0)
            {
                label4.Visible = true;
                DiakokBox.Visible = false;
                comboBox1.Visible = true;
                tanarbox.Visible = true;
                button1.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                KurzusList.Visible = false;
                textBox1.PlaceholderText = "Tanfolyam neve";
                textBox2.PlaceholderText = "Kezdés dátuma(EEEEE-HH-NN)";
                textBox3.PlaceholderText = "Költség / Fő(kötelező)";
                KurzusBox.Visible = false;
            }

            if (comboBox1.SelectedIndex == 1)
            {
                label4.Visible = false;
                DiakokBox.Visible = false;
                comboBox1.Visible = true;
                tanarbox.Visible = false;
                button1.Visible = true;
                textBox1.Visible = true;
                textBox1.PlaceholderText = "Tanár neve";
                textBox2.Visible = false;
                textBox3.Visible = false;
                KurzusList.Visible = false;
                KurzusBox.Visible = false;
            }
        }
        // tanárbox elemének kiválasztása kurzushoz való hozzáadáshoz
        private void tanarbox_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (tanarbox.SelectedItem != null)
            {
                LetszamDiag.Series.Clear();
                string selectedTanarNev = tanarbox.SelectedItem.ToString(); // Assuming tanarbox.SelectedItem returns the Tanar's name
                string query = $"SELECT Tanfolyam.Nev, Tanfolyam.KezdetDatuma, Tanfolyam.VegzesDatuma, Tanfolyam.Aktiv " +
                               $"FROM Tanfolyam " +
                               $"INNER JOIN Tanar ON Tanfolyam.TanarID = Tanar.TanarId " +
                               $"WHERE Tanar.Nev = '{selectedTanarNev}'";

                using (SqlConnection connection = new SqlConnection(constring))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    // Clear any previous items in the listbox
                    KurzusList.Items.Clear();

                    while (reader.Read())
                    {
                        string tanfolyamNev = reader.GetString(0); // Tanfolyam's name
                        DateTime kezdetDatuma = reader.GetDateTime(1); // KezdetDatuma
                        DateTime? vegzesDatuma = reader.IsDBNull(2) ? null : (DateTime?)reader.GetDateTime(2); // VegzesDatuma (nullable)
                        bool aktiv = reader.GetBoolean(3); // Aktiv

                        // Determine status based on Aktiv and VegzesDatuma
                        string status = "";
                        if (!aktiv)
                        {
                            status = "Passzív";
                        }
                        else if (vegzesDatuma != null)
                        {
                            status = "Lezárt";
                        }
                        else
                        {
                            status = "Aktív";
                        }

                        string output;
                        if (vegzesDatuma != null)
                        {
                            output = $"{tanfolyamNev} - Vége: {vegzesDatuma.Value.ToShortDateString()}, {status}";
                        }
                        else
                        {
                            output = $"{tanfolyamNev} - Kezdés: {kezdetDatuma.ToShortDateString()}, {status}";
                        }

                        KurzusList.Items.Add(output);
                    }
                    reader.Close();

                    KurzusList.Height = Math.Min(KurzusList.PreferredHeight, 200);
                    AdjustKurzusListWidth();

                }
            }
        }

        private void PopulateKurzusComboBox()
        {
            try
            {
                string query = @"
            SELECT 
                t.Nev, 
                t.Aktiv, 
                t.VegzesDatuma, 
                COUNT(td.DiakId) AS DiakSzam 
            FROM 
                [dbo].[Tanfolyam] t
            LEFT JOIN 
                [dbo].[TanfolyamDiak] td ON t.TanfolyamId = td.TanfolyamId
            GROUP BY 
                t.TanfolyamId, t.Nev, t.Aktiv, t.VegzesDatuma";

                using (SqlConnection connection = new SqlConnection(constring))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    KurzusBox.Items.Clear();

                    while (reader.Read())
                    {
                        string kurzusNev = reader.GetString(0);
                        bool aktiv = reader.GetBoolean(1);
                        DateTime? vegzesDatuma = reader.IsDBNull(2) ? null : (DateTime?)reader.GetDateTime(2);
                        int diakSzam = reader.GetInt32(3);

                        string kurzusInfo = $"{kurzusNev} - ";
                        if (aktiv)
                        {
                            kurzusInfo += "Aktív";
                        }
                        else if (!aktiv && vegzesDatuma == null)
                        {
                            kurzusInfo += "Passzív";
                        }
                        else if (!aktiv && vegzesDatuma != null)
                        {
                            kurzusInfo += "Lezárt";
                        }

                        kurzusInfo += $" ({diakSzam} /8)";
                        KurzusBox.Items.Add(kurzusInfo);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void FelvDiakButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Ellenőrizzük, hogy van-e kiválasztott diák és kurzus
                if (DiakokBox.SelectedItem != null && KurzusBox.SelectedItem != null)
                {
                    // Kiválasztott diák és kurzus neve
                    string diakNev = DiakokBox.SelectedItem.ToString();
                    string kurzusNev = KurzusBox.SelectedItem.ToString();

                    // Kiválasztott kurzus nevéből kiszedjük a név részt
                    string[] parts = kurzusNev.Split(new[] { " - " }, StringSplitOptions.None);
                    string tanfolyamNev = parts[0];

                    // Megkeressük a tanfolyam azonosítóját a név alapján
                    int tanfolyamId = GetTanfolyamIdByName(tanfolyamNev);

                    if (tanfolyamId != -1)
                    {
                        // Ellenőrizzük, hogy a tanfolyamnak van-e végzés dátuma
                        if (!IsTanfolyamClosed(tanfolyamId))
                        {
                            // Megkeressük a diák azonosítóját a név alapján
                            int diakId = GetDiakIdByName(diakNev);

                            if (diakId != -1)
                            {
                                // Ellenőrizzük, hogy a diák már szerepel-e a kurzusnál
                                if (!IsDiakAlreadyEnrolled(tanfolyamId, diakId))
                                {
                                    // Ellenőrizzük, hogy van-e még hely a kurzuson
                                    if (!IsKurzusFull(tanfolyamId))
                                    {
                                        // Beszúrás a TanfolyamDiak táblába
                                        string insertQuery = "INSERT INTO [dbo].[TanfolyamDiak] (TanfolyamId, DiakId) VALUES (@TanfolyamId, @DiakId)";

                                        using (SqlConnection connection = new SqlConnection(constring))
                                        {
                                            SqlCommand command = new SqlCommand(insertQuery, connection);
                                            command.Parameters.AddWithValue("@TanfolyamId", tanfolyamId);
                                            command.Parameters.AddWithValue("@DiakId", diakId);

                                            connection.Open();
                                            int rowsAffected = command.ExecuteNonQuery();

                                            if (rowsAffected > 0)
                                            {
                                                MessageBox.Show("Diák sikeresen hozzáadva a kurzushoz.");
                                            }
                                            else
                                            {
                                                MessageBox.Show("Hiba történt a diák hozzáadása során.");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("A kurzus maximális létszáma elérte a limitet, új diák nem adható hozzá.");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("A megadott diák már hozzá van adva ehhez a kurzushoz.");
                                    DiakokBox.SelectedItem = null;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Nem található diák az adatbázisban a megadott név alapján.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Nem lehet diákot hozzáadni lezárt tanfolyamhoz.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nem található tanfolyam az adatbázisban a megadott név alapján.");
                    }
                }
                else
                {
                    MessageBox.Show("Kérjük, válasszon ki egy diákot és egy kurzust is a hozzáadáshoz.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            PopulateKurzusComboBox();
        }

        // Metódus, ami ellenőrzi, hogy van-e még hely a kurzuson
        private bool IsKurzusFull(int tanfolyamId)
        {
            int currentDiakCount = GetCurrentDiakCountForKurzus(tanfolyamId);
            return currentDiakCount >= 8; // Ha a jelenlegi diákok száma eléri vagy meghaladja a 8-at, akkor a kurzus tele van
        }

        // Metódus, ami visszaadja a jelenlegi diákok számát a kurzuson
        private int GetCurrentDiakCountForKurzus(int tanfolyamId)
        {
            int diakCount = 0;

            // Lekérjük a diákok számát a kurzuson
            string query = "SELECT COUNT(*) FROM [dbo].[TanfolyamDiak] WHERE TanfolyamId = @TanfolyamId";

            using (SqlConnection connection = new SqlConnection(constring))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TanfolyamId", tanfolyamId);

                connection.Open();
                diakCount = (int)command.ExecuteScalar();
            }

            return diakCount;
        }

        // Metódus a tanfolyam azonosítójának lekérésére név alapján
        private int GetTanfolyamIdByName(string tanfolyamNev)
        {
            int tanfolyamId = -1;
            string query = "SELECT TanfolyamId FROM [dbo].[Tanfolyam] WHERE Nev = @Nev";

            using (SqlConnection connection = new SqlConnection(constring))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nev", tanfolyamNev);

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    tanfolyamId = Convert.ToInt32(result);
                }
            }

            return tanfolyamId;
        }
        // Metódus a tanfolyam lezártságának ellenőrzésére
        private bool IsTanfolyamClosed(int tanfolyamId)
        {
            bool isClosed = false;
            string query = "SELECT VegzesDatuma FROM [dbo].[Tanfolyam] WHERE TanfolyamId = @TanfolyamId";

            using (SqlConnection connection = new SqlConnection(constring))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TanfolyamId", tanfolyamId);

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    isClosed = true;
                }
            }

            return isClosed;
        }

        // Metódus a diák azonosítójának lekérésére név alapján
        private int GetDiakIdByName(string diakNev)
        {
            int diakId = -1;
            string query = "SELECT DiakId FROM [dbo].[Diak] WHERE Nev = @Nev";

            using (SqlConnection connection = new SqlConnection(constring))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nev", diakNev);

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    diakId = Convert.ToInt32(result);
                }
            }

            return diakId;
        }
        // Metódus a diák jelenlétének ellenőrzésére a kurzusnál
        private bool IsDiakAlreadyEnrolled(int tanfolyamId, int diakId)
        {
            bool isEnrolled = false;
            string query = "SELECT COUNT(*) FROM [dbo].[TanfolyamDiak] WHERE TanfolyamId = @TanfolyamId AND DiakId = @DiakId";

            using (SqlConnection connection = new SqlConnection(constring))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TanfolyamId", tanfolyamId);
                command.Parameters.AddWithValue("@DiakId", diakId);

                connection.Open();
                int count = (int)command.ExecuteScalar();

                if (count > 0)
                {
                    isEnrolled = true;
                }
            }

            return isEnrolled;
        }

        private void KuAdatButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Ellenőrizzük, hogy van-e kiválasztott kurzus
                if (KurzusBox.SelectedItem != null)
                {
                    // Kiválasztott kurzus neve
                    string kurzusNev = KurzusBox.SelectedItem.ToString();

                    // Kiválasztott kurzus nevéből kiszedjük a név részt
                    string[] parts = kurzusNev.Split(new[] { " - " }, StringSplitOptions.None);
                    string tanfolyamNev = parts[0];

                    // Megkeressük a tanfolyam azonosítóját a név alapján
                    int tanfolyamId = GetTanfolyamIdByName(tanfolyamNev);

                    if (tanfolyamId != -1)
                    {
                        // Lekérjük a kurzus adatait és az oda járó diákok listáját
                        string kurzusInfo = GetKurzusInfoForExport(tanfolyamId);

                        // Exportáljuk az adatokat XML fájlba
                        ExportToXml(kurzusNev, kurzusInfo);
                    }
                    else
                    {
                        MessageBox.Show("Nem található tanfolyam az adatbázisban a megadott név alapján.");
                    }
                }
                else
                {
                    MessageBox.Show("Kérjük, válasszon ki egy kurzust az exportáláshoz.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        // A kurzus adatainak és az oda járó diákok listájának lekérésére
        private string GetKurzusInfoForExport(int tanfolyamId)
        {
            StringBuilder sb = new StringBuilder();

            // Lekérjük a kurzus adatait és az oda járó diákok listáját egy lekérdezéssel
            string query = @"
        SELECT t.Nev, t.KezdetDatuma, t.VegzesDatuma, t.KoltsegPerFo, t.Aktiv, d.Nev AS TanarNev, td.DiakId, diak.Nev AS DiakNev
        FROM [dbo].[Tanfolyam] t
        LEFT JOIN [dbo].[Tanar] d ON t.TanarID = d.TanarId
        LEFT JOIN [dbo].[TanfolyamDiak] td ON t.TanfolyamId = td.TanfolyamId
        LEFT JOIN [dbo].[Diak] diak ON td.DiakId = diak.DiakId
        WHERE t.TanfolyamId = @TanfolyamId";

            using (SqlConnection connection = new SqlConnection(constring))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TanfolyamId", tanfolyamId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                sb.AppendLine("<Kurzus>");

                if (reader.Read())
                {
                    string kurzusNev = reader.GetString(0);
                    DateTime kezdetDatuma = reader.GetDateTime(1);
                    DateTime? vegzesDatuma = reader.IsDBNull(2) ? null : (DateTime?)reader.GetDateTime(2);
                    decimal koltsegPerFo = reader.GetDecimal(3);
                    bool aktiv = reader.GetBoolean(4);
                    string tanarNev = reader.IsDBNull(5) ? "Nincs meghatározva" : reader.GetString(5);

                    sb.AppendLine($"  <Nev>{kurzusNev}</Nev>");
                    sb.AppendLine($"  <KezdetDatuma>{kezdetDatuma}</KezdetDatuma>");
                    sb.AppendLine($"  <VegzesDatuma>{vegzesDatuma}</VegzesDatuma>");
                    sb.AppendLine($"  <KoltsegPerFo>{koltsegPerFo}</KoltsegPerFo>");
                    sb.AppendLine($"  <Aktiv>{aktiv}</Aktiv>");
                    sb.AppendLine($"  <TanarNev>{tanarNev}</TanarNev>");
                    sb.AppendLine("  <Diakok>");

                    // Diákok neveinek hozzáadása
                    do
                    {
                        string diakNev = reader.GetString(7);
                        sb.AppendLine($"    <Diak>{diakNev}</Diak>");
                    } while (reader.Read());

                    sb.AppendLine("  </Diakok>");
                }

                sb.AppendLine("</Kurzus>");

                // Lezárjuk a DataReader-t
                reader.Close();
            }

            return sb.ToString();
        }
        //XML Fájlba való mentés
        private void ExportToXml(string kurzusNev, string kurzusInfo)
        {
            try
            {
                // Az alkalmazás futási helyének elérési útvonala
                string directory = Path.GetDirectoryName(Application.ExecutablePath);

                // Fájlnév létrehozása a kurzus nevével
                string fileName = $"{kurzusNev.Replace(" ", "_").Replace("/", "-").Replace("\\", "-").Replace(":", "-")}.xml";

                // Teljes fájl elérési útvonala
                string filePath = Path.Combine(directory, fileName);

                // XML fájl mentése
                File.WriteAllText(filePath, kurzusInfo);

                MessageBox.Show($"A(z) {kurzusNev} kurzus adatai sikeresen exportálva lettek a(z) {filePath} fájlba.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt az exportálás során: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void AktivalasButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Ellenőrizzük, hogy van-e kiválasztott tanfolyam
                if (KurzusBox.SelectedItem != null)
                {
                    // Kiválasztott tanfolyam adatainak kinyerése a névből
                    string kurzusInfo = KurzusBox.SelectedItem.ToString();
                    string[] parts = kurzusInfo.Split(new[] { " - " }, StringSplitOptions.None);
                    string tanfolyamNev = parts[0];

                    // Kiválasztott tanfolyam azonosítójának lekérése a név alapján
                    int tanfolyamId = GetTanfolyamIdByName(tanfolyamNev);

                    if (tanfolyamId != -1)
                    {
                        // Lekérjük a tanfolyamhoz tartozó diákok számát
                        int diakCount = GetDiakCountForTanfolyam(tanfolyamId);

                        // Ellenőrizzük, hogy van-e legalább 4 diák a tanfolyamon
                        if (diakCount >= 4)
                        {
                            // Tanfolyam állapotának frissítése az adatbázisban
                            UpdateTanfolyamAktivStatus(tanfolyamId, true);
                            MessageBox.Show("A tanfolyam sikeresen aktiválva lett.");
                        }
                        else
                        {
                            MessageBox.Show("A tanfolyam nem aktiválható, mert kevesebb mint 4 diák van rajta.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nem található tanfolyam az adatbázisban a megadott név alapján.");
                    }
                }
                else
                {
                    MessageBox.Show("Kérjük, válasszon ki egy tanfolyamot az aktiváláshoz.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            PopulateKurzusComboBox();
        }

        // Tanfolyam állapotának frissítése az adatbázisban
        private void UpdateTanfolyamAktivStatus(int tanfolyamId, bool aktiv)
        {
            string query = "UPDATE [dbo].[Tanfolyam] SET Aktiv = @Aktiv WHERE TanfolyamId = @TanfolyamId";

            using (SqlConnection connection = new SqlConnection(constring))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Aktiv", aktiv);
                command.Parameters.AddWithValue("@TanfolyamId", tanfolyamId);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
            }
        }

        // Diákok számának lekérése a tanfolyamon
        private int GetDiakCountForTanfolyam(int tanfolyamId)
        {
            int diakCount = 0;
            string query = "SELECT COUNT(*) FROM [dbo].[TanfolyamDiak] WHERE TanfolyamId = @TanfolyamId";

            using (SqlConnection connection = new SqlConnection(constring))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TanfolyamId", tanfolyamId);

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    diakCount = Convert.ToInt32(result);
                }
            }

            return diakCount;
        }

        private void LezarasButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Ellenőrizzük, hogy van-e kiválasztott kurzus
                if (KurzusBox.SelectedItem != null)
                {
                    // Kiválasztott diák és kurzus neve
                    string kurzusNev = KurzusBox.SelectedItem.ToString();

                    // Kiválasztott kurzus nevéből kiszedjük a név részt
                    string[] parts = kurzusNev.Split(new[] { " - " }, StringSplitOptions.None);
                    string tanfolyamNev = parts[0];

                    // Megkeressük a tanfolyam azonosítóját a név alapján
                    int tanfolyamId = GetTanfolyamIdByName(tanfolyamNev);

                    if (tanfolyamId != -1)
                    {
                        // Ellenőrizzük, hogy a kurzus aktív-e
                        if (IsKurzusActive(tanfolyamId))
                        {
                            // Új Form létrehozása a dátum kiválasztásához
                            using (var datePickerForm = new Form())
                            {
                                // Beállítjuk a Form tulajdonságait
                                datePickerForm.Text = "Végzés dátumának kiválasztása";

                                // Új DateTimePicker vezérlő létrehozása és hozzáadása a Form-hoz
                                DateTimePicker datePicker = new DateTimePicker();
                                datePicker.Format = DateTimePickerFormat.Short;
                                datePicker.ShowUpDown = false;
                                datePicker.Dock = DockStyle.Fill;

                                // A DateTimePicker hozzáadása a Form-hoz
                                datePickerForm.Controls.Add(datePicker);

                                // OK és Mégse gombok hozzáadása a Form-hoz
                                Button okButton = new Button();
                                okButton.Text = "OK";
                                okButton.DialogResult = DialogResult.OK;
                                okButton.Dock = DockStyle.Left;

                                Button cancelButton = new Button();
                                cancelButton.Text = "Mégse";
                                cancelButton.DialogResult = DialogResult.Cancel;
                                cancelButton.Dock = DockStyle.Right;

                                datePickerForm.Controls.Add(okButton);
                                datePickerForm.Controls.Add(cancelButton);

                                // Ha a felhasználó okézza a kiválasztott dátumot, akkor lezárjuk a kurzust
                                if (datePickerForm.ShowDialog() == DialogResult.OK)
                                {
                                    DateTime vegzesDatuma = datePicker.Value;

                                    // Lezárjuk a kurzust
                                    CloseKurzus(tanfolyamId, vegzesDatuma);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("A kiválasztott kurzus már le van zárva.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nem található tanfolyam az adatbázisban a megadott név alapján.");
                    }
                }
                else
                {
                    MessageBox.Show("Kérjük, válasszon ki egy kurzust a lezárásához.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            PopulateKurzusComboBox();
        }

        // Metódus, ami ellenőrzi, hogy a kurzus aktív-e
        private bool IsKurzusActive(int tanfolyamId)
        {
            bool isActive = false;

            // Lekérjük a kurzus aktív állapotát
            string query = "SELECT Aktiv FROM [dbo].[Tanfolyam] WHERE TanfolyamId = @TanfolyamId";

            using (SqlConnection connection = new SqlConnection(constring))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TanfolyamId", tanfolyamId);

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    isActive = (bool)result;
                }
            }

            return isActive;
        }

        // Metódus, ami lezárja a kurzust és beállítja a végzős dátumot
        private void CloseKurzus(int tanfolyamId, DateTime vegzesDatuma)
        {
            // Kurzus lezárása
            string updateQuery = "UPDATE [dbo].[Tanfolyam] SET Aktiv = 0, VegzesDatuma = @VegzesDatuma WHERE TanfolyamId = @TanfolyamId";

            using (SqlConnection connection = new SqlConnection(constring))
            {
                SqlCommand command = new SqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@TanfolyamId", tanfolyamId);
                command.Parameters.AddWithValue("@VegzesDatuma", vegzesDatuma);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("A kurzus sikeresen le lett zárva.");
                }
                else
                {
                    MessageBox.Show("Hiba történt a kurzus lezárása során.");
                }
            }
        }
        private void PopulateKurzusListBox(string diakNev)
        {
            try
            {
                // Lekérdezzük a diákhoz tartozó kurzusokat és az összesített költséget
                List<string> kurzusok = new List<string>();
                decimal osszeg = GetDiakKoltsegPerFo(diakNev, out kurzusok);

                // Ellenőrizzük, hogy a diák befizetett-e
                bool befizetve = IsDiakBefizetve(diakNev);

                KurzusList.Items.Clear();

                // Add kurzusokat a listához
                foreach (string kurzus in kurzusok)
                {
                    KurzusList.Items.Add(kurzus);
                }

                // Add a végösszeget és a fizetési státuszt
                KurzusList.Items.Add("__________________________________________________________");
                if (befizetve)
                {
                    KurzusList.Items.Add($"Befizetve: {osszeg} Ft");
                }
                else
                {
                    KurzusList.Items.Add($"Fizetendő összeg: {osszeg} Ft");
                }

                // Állítsuk be a ListBox magasságát a sorok számának megfelelően
                KurzusList.Height = KurzusList.ItemHeight * KurzusList.Items.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            AdjustKurzusListWidth();
        }

        private decimal GetDiakKoltsegPerFo(string diakNev, out List<string> kurzusok)
        {
            decimal osszeg = 0;
            kurzusok = new List<string>();

            try
            {
                string query = @"
            SELECT 
                t.Nev AS KurzusNev,
                t.KoltsegPerFo AS KoltsegPerFo
            FROM 
                [dbo].[Tanfolyam] t
            INNER JOIN 
                [dbo].[TanfolyamDiak] td ON t.TanfolyamId = td.TanfolyamId
            INNER JOIN 
                [dbo].[Diak] d ON td.DiakId = d.DiakId
            WHERE 
                d.Nev = @DiakNev";

                using (SqlConnection connection = new SqlConnection(constring))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@DiakNev", diakNev);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string kurzusNev = reader["KurzusNev"].ToString();
                        decimal koltsegPerFo = Convert.ToDecimal(reader["KoltsegPerFo"]);
                        osszeg += koltsegPerFo;
                        kurzusok.Add($"{kurzusNev}: {koltsegPerFo} Ft");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            return osszeg;
        }

        private bool IsDiakBefizetve(string diakNev)
        {
            bool befizetve = false;

            try
            {
                string query = "SELECT Befizetve FROM [dbo].[Diak] WHERE Nev = @DiakNev";

                using (SqlConnection connection = new SqlConnection(constring))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@DiakNev", diakNev);
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        befizetve = Convert.ToBoolean(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            return befizetve;
        }
        private void DiakokBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ellenőrizzük, hogy van-e kiválasztott diák
            if (DiakokBox.SelectedItem != null)
            {
                // Kiválasztott diák nevének lekérése
                string diakNev = DiakokBox.SelectedItem.ToString();

                // KurzusListBox frissítése a kiválasztott diákhoz tartozó kurzusokkal
                PopulateKurzusListBox(diakNev);
            }
        }

        private void BefizetesButton_Click(object sender, EventArgs e)
        {
            // Ellenőrizzük, hogy van-e kiválasztott diák
            if (DiakokBox.SelectedItem != null)
            {
                // Kiválasztott diák nevének lekérése
                string diakNev = DiakokBox.SelectedItem.ToString();

                // Diák befizetési attribútumának frissítése
                UpdateDiakBefizetve(diakNev, true);

                // Diákok újratöltése
                PopulateDiakComboBox();

                // KurzusListBox frissítése
                PopulateKurzusListBox(diakNev);
            }
        }

        private void UpdateDiakBefizetve(string diakNev, bool befizetve)
        {
            try
            {
                string query = "UPDATE [dbo].[Diak] SET Befizetve = @Befizetve WHERE Nev = @DiakNev";

                using (SqlConnection connection = new SqlConnection(constring))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Befizetve", befizetve);
                    command.Parameters.AddWithValue("@DiakNev", diakNev);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("A befizetés sikeresen frissítve lett.");
                    }
                    else
                    {
                        MessageBox.Show("Nem sikerült frissíteni a befizetést.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        //létszám gráf 
        private void LetszMutatoButton_Click(object sender, EventArgs e)
        {
            if (tanarbox.SelectedItem != null)
            {
                string selectedTanarNev = tanarbox.SelectedItem.ToString(); // Kiválasztott tanár neve
                DateTime intervalKezdet = IntervalKezdet.Value.Date;
                DateTime intervalVege = IntervalVege.Value.Date;

                PopulateLetszamDiag(selectedTanarNev, intervalKezdet, intervalVege);
            }
        }

        private void PopulateLetszamDiag(string selectedTanarNev, DateTime intervalKezdet, DateTime intervalVege)
        {
            LetszamDiag.Series.Clear();
            LetszamDiag.Series.Add("Diákok");

            try
            {
                string query = @"
            SELECT t.Nev AS TanfolyamNev, COUNT(td.DiakId) AS DiakSzam
            FROM [dbo].[Tanfolyam] t
            INNER JOIN [dbo].[TanfolyamDiak] td ON t.TanfolyamId = td.TanfolyamId
            INNER JOIN [dbo].[Tanar] tr ON t.TanarID = tr.TanarId
            WHERE tr.Nev = @TanarNev
            AND t.KezdetDatuma <= @IntervalVege
            AND (t.VegzesDatuma IS NULL OR t.VegzesDatuma >= @IntervalKezdet)
            GROUP BY t.Nev";

                using (SqlConnection connection = new SqlConnection(constring))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TanarNev", selectedTanarNev);
                    command.Parameters.AddWithValue("@IntervalKezdet", intervalKezdet);
                    command.Parameters.AddWithValue("@IntervalVege", intervalVege);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string tanfolyamNev = reader.GetString(0);
                        int diakSzam = reader.GetInt32(1);

                        // Összeállítjuk a tanfolyam nevet és a diákok számát
                        string label = $"{tanfolyamNev} ({diakSzam} diák)";

                        // Hozzáadjuk a pontot a diagramhoz
                        DataPoint point = new DataPoint();
                        point.Label = label; // A diagramon megjelenő címke
                        point.YValues = new double[] { diakSzam }; // A diákok létszáma

                        LetszamDiag.Series["Diákok"].Points.Add(point);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            LetszamDiag.Series["Diákok"].ChartType = SeriesChartType.Pie;
        }
        //Bevételek mutatása gráfon
        private void BevetMutatButton_Click(object sender, EventArgs e)
        {
            if (DiagselectBox.SelectedIndex == -1)
            {
                MessageBox.Show("Válassz diagram kategóriát!");
            }
            if (DiagselectBox.SelectedIndex == 0)
            {
                PopulateBevetelDiag();

            }
            if (DiagselectBox.SelectedIndex == 1)
            {
                PopulateBevetelDiagKor();

            }
        }

        private void PopulateBevetelDiag()
        {
            BevetelDiag.Series.Clear();
            BevetelDiag.Series.Add("Bevétel");

            try
            {
                DateTime kezdetDatum = IntervalKezdet.Value.Date;
                DateTime vegDatum = IntervalVege.Value.Date;

                string query = @"
            SELECT COALESCE(KezdetDatuma, @KezdetDatum) AS Datum, SUM(COALESCE(t.KoltsegPerFo * td.diakSzam, 0)) AS Bevetel
            FROM [dbo].[Tanfolyam] t
            LEFT JOIN (
                SELECT TanfolyamId, COUNT(DiakId) AS diakSzam
                FROM [dbo].[TanfolyamDiak]
                GROUP BY TanfolyamId
            ) td ON t.TanfolyamId = td.TanfolyamId
            WHERE COALESCE(KezdetDatuma, @VegDatum) <= @VegDatum AND COALESCE(VegzesDatuma, @KezdetDatum) >= @KezdetDatum
            GROUP BY COALESCE(KezdetDatuma, @KezdetDatum)
            ORDER BY COALESCE(KezdetDatuma, @KezdetDatum)";

                using (SqlConnection connection = new SqlConnection(constring))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@KezdetDatum", kezdetDatum);
                    command.Parameters.AddWithValue("@VegDatum", vegDatum);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        DateTime datum = reader.GetDateTime(0);
                        decimal bevétel = reader.GetDecimal(1);

                        // Hozzáadjuk a pontot a diagramhoz
                        DataPoint point = new DataPoint();
                        point.XValue = datum.ToOADate(); // Az x tengely értéke a dátum
                        point.YValues = new double[] { (double)bevétel }; // A bevétel

                        BevetelDiag.Series["Bevétel"].Points.Add(point);
                    }

                    reader.Close();
                }

                // Címkék hozzáadása a felhasználó által választott dátumok alapján
                BevetelDiag.ChartAreas[0].AxisX.CustomLabels.Add(
                    new CustomLabel(kezdetDatum.ToOADate(), kezdetDatum.ToOADate(), kezdetDatum.ToString("yyyy-MM-dd"), 0, LabelMarkStyle.None));
                BevetelDiag.ChartAreas[0].AxisX.CustomLabels.Add(
                    new CustomLabel(vegDatum.ToOADate(), vegDatum.ToOADate(), vegDatum.ToString("yyyy-MM-dd"), 0, LabelMarkStyle.None));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            // Diagram formázása
            BevetelDiag.BackColor = Color.Lavender;
            BevetelDiag.ChartAreas[0].AxisX.LabelStyle.Format = "yyyy-MM-dd"; // Dátum formátum beállítása
            BevetelDiag.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Days; // Napokra való bontás
            BevetelDiag.ChartAreas[0].AxisX.Interval = 1; // Minden nap megjelenítése
            BevetelDiag.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray; // Főrács színének beállítása
            BevetelDiag.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray; // Főrács színének beállítása
            BevetelDiag.Series["Bevétel"].ChartType = SeriesChartType.Line; // Vonaldiagram típus beállítása
            BevetelDiag.Series["Bevétel"].BorderWidth = 2; // Vonal vastagságának beállítása
            BevetelDiag.Series["Bevétel"].Color = Color.Blue; // Vonal színének beállítása
            BevetelDiag.Series["Bevétel"].MarkerStyle = MarkerStyle.Circle; // Jelölőstílus beállítása
            BevetelDiag.Series["Bevétel"].MarkerSize = 8; // Jelölő méretének beállítása
            BevetelDiag.Series["Bevétel"].MarkerColor = Color.Red; // Jelölő színének beállítása

            // A bal oldali tengely számformátumának beállítása Ft-ra
            BevetelDiag.ChartAreas[0].AxisY.LabelStyle.Format = "C0";
        }
        private void PopulateBevetelDiagKor()
        {
            BevetelDiag.Series.Clear();
            BevetelDiag.Series.Add("Bevétel");

            try
            {
                DateTime kezdetDatum = IntervalKezdet.Value;
                DateTime vegDatum = IntervalVege.Value;

                string query = $@"
            SELECT t.Nev AS TanfolyamNev, SUM(t.KoltsegPerFo * diakSzam) AS Bevetel
            FROM [dbo].[Tanfolyam] t
            INNER JOIN (
                SELECT TanfolyamId, COUNT(DiakId) AS diakSzam
                FROM [dbo].[TanfolyamDiak]
                GROUP BY TanfolyamId
            ) td ON t.TanfolyamId = td.TanfolyamId
            WHERE (t.KezdetDatuma < @VegDatum OR t.KezdetDatuma IS NULL) AND (t.VegzesDatuma > @KezdetDatum OR t.VegzesDatuma IS NULL)
            GROUP BY t.Nev";

                using (SqlConnection connection = new SqlConnection(constring))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@KezdetDatum", kezdetDatum);
                    command.Parameters.AddWithValue("@VegDatum", vegDatum);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string tanfolyamNev = reader.GetString(0);
                        decimal bevétel = reader.GetDecimal(1);

                        // Összeállítjuk a címkét, ami a tanfolyam nevét és az összeget tartalmazza
                        string label = $"{tanfolyamNev}";

                        // Hozzáadjuk a pontot a diagramhoz
                        DataPoint point = new DataPoint();
                        point.Label = label; // A diagramon megjelenő címke
                        point.LabelForeColor = Color.Black; // Címke színének beállítása
                        point.LabelBackColor = Color.Transparent; // Háttérszín átlátszó

                        point.YValues = new double[] { (double)bevétel }; // A bevétel

                        BevetelDiag.Series["Bevétel"].Points.Add(point);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            // Diagram formázása
            BevetelDiag.Series["Bevétel"].ChartType = SeriesChartType.Pie; // Kör diagram típus beállítása
            BevetelDiag.Series["Bevétel"].BackSecondaryColor = Color.FromArgb(49, 51, 64); // Háttérszín beállítása
            BevetelDiag.Series["Bevétel"].Font = new Font("Arial", 8, FontStyle.Bold); // Betűtípus beállítása
            BevetelDiag.Series["Bevétel"].LabelForeColor = Color.Black; // Címke színének beállítása

        }

        private void KatBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (KatBox.SelectedIndex == 0)
            {
                KurzusBox.Visible = true;
                DiakokBox.Visible = false;
                tanarbox.Visible = false;
                KurzusBox.SelectedItem = null;
                textBox3.Visible = true;
                label2.Text = "Tanfolyam";
                label2.Location = new Point(152, 67);
                KurzusBox.Location = textBox1.Location;
                textBox2.PlaceholderText = "Kezdés dátuma(EEEEE-HH-NN)";
                textBox3.PlaceholderText = "Költség / Fő";

            }
            if (KatBox.SelectedIndex == 1)
            {
                KurzusBox.Visible = false;
                DiakokBox.Visible = false;
                tanarbox.Visible = true;
                tanarbox.Location = textBox1.Location;
                textBox2.PlaceholderText = "Tanár új neve";
                textBox3.Visible = false;
                label2.Text = "Tanár";
                label2.Location = new Point(172, 67);
            }

            if (KatBox.SelectedIndex == 2)
            {
                KurzusBox.Visible = false;
                tanarbox.Visible = false;
                DiakokBox.Visible = true;
                textBox3.Visible = true;
                DiakokBox.Location = textBox1.Location;
                label2.Text = "Diák";
                label2.Location = new Point(182, 67);
                textBox2.PlaceholderText = "Számlázási Név";
                textBox3.PlaceholderText = "Számlázási Cím";
            }

        }
        //modosítás gombja
        private void ModositButton_Click(object sender, EventArgs e)
        {
            if (KatBox.SelectedIndex == 0)
            {
                if (KurzusBox.SelectedItem != null)
                {
                    // Ellenőrizzük, hogy van-e kiválasztott tanfolyam
                    try
                    {
                        string selectedKurzusInfo = KurzusBox.SelectedItem.ToString();
                        string kurzusNev = selectedKurzusInfo.Split('-')[0].Trim(); // kiválasztott tanfolyam neve
                        int tanfolyamId = GetTanfolyamIdByName(kurzusNev); // kiválasztott tanfolyam azonosítója
                        bool isClosed = IsTanfolyamClosed(tanfolyamId); // Ellenőrizzük, hogy a tanfolyam le van-e zárva

                        if (isClosed)
                        {
                            MessageBox.Show("A kiválasztott tanfolyam le van zárva. Nem lehet módosítani.");
                            return;
                        }
                        // Új kezdés dátumának ellenőrzése és konvertálása
                        DateTime? newKezdetDatum = null;
                        if (!string.IsNullOrEmpty(textBox2.Text))
                        {
                            if (DateTime.TryParse(textBox2.Text, out DateTime tempDate))
                            {
                                newKezdetDatum = tempDate;
                            }
                            else
                            {
                                MessageBox.Show("Hibás dátumformátum. Kérem, adjon meg egy dátumot az új kezdési dátumnak!");
                                return;
                            }
                        }

                        // Új költség per fő ellenőrzése és konvertálása
                        decimal? newKoltsegPerFo = null;
                        if (!string.IsNullOrEmpty(textBox3.Text))
                        {
                            if (decimal.TryParse(textBox3.Text, out decimal tempKoltsegPerFo))
                            {
                                newKoltsegPerFo = tempKoltsegPerFo;
                            }
                            else
                            {
                                MessageBox.Show("A költség per fő mező csak számot tartalmazhat. Kérem, adjon meg egy számot!");
                                return;
                            }
                        }

                        // SQL lekérdezés a tanfolyam adatainak frissítésére
                        string query = "UPDATE [dbo].[Tanfolyam] SET ";
                        bool hasSetClause = false;
                        if (newKezdetDatum != null)
                        {
                            query += "KezdetDatuma = @NewKezdetDatum";
                            hasSetClause = true;
                        }
                        if (newKoltsegPerFo != null)
                        {
                            if (hasSetClause) query += ",";
                            query += " KoltsegPerFo = @NewKoltsegPerFo";
                            hasSetClause = true;
                        }
                        query += " WHERE Nev = @KurzusNev";

                        using (SqlConnection connection = new SqlConnection(constring))
                        {
                            SqlCommand command = new SqlCommand(query, connection);
                            if (newKezdetDatum != null) command.Parameters.AddWithValue("@NewKezdetDatum", newKezdetDatum);
                            if (newKoltsegPerFo != null) command.Parameters.AddWithValue("@NewKoltsegPerFo", newKoltsegPerFo);
                            command.Parameters.AddWithValue("@KurzusNev", kurzusNev);
                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("A tanfolyam adatai sikeresen módosítva lettek.");
                                // Frissítjük a tanfolyamok listáját
                                PopulateKurzusComboBox();
                            }
                            else
                            {
                                MessageBox.Show("A tanfolyam adatainak módosítása sikertelen volt.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hiba történt: " + ex.Message);
                        Console.WriteLine(ex.StackTrace);
                    }
                }
                else
                {
                    MessageBox.Show("Kérem, válasszon ki egy tanfolyamot a módosításhoz!");
                }
                textBox3.Text = string.Empty;
                textBox2.Text = string.Empty;
            }

            if (KatBox.SelectedIndex == 1)
            {
                if (tanarbox.SelectedItem != null)
                {
                    // Új tanár név megadásának ellenőrzése
                    if (!string.IsNullOrEmpty(textBox2.Text))
                    {
                        try
                        {
                            string tanarNev = tanarbox.SelectedItem.ToString(); // Kiválasztott tanár neve
                            string ujNev = textBox2.Text; // Új tanár név

                            // SQL parancs a tanár nevének frissítésére
                            string query = "UPDATE [dbo].[Tanar] SET Nev = @UjNev WHERE Nev = @TanarNev";

                            using (SqlConnection connection = new SqlConnection(constring))
                            {
                                SqlCommand command = new SqlCommand(query, connection);
                                command.Parameters.AddWithValue("@UjNev", ujNev);
                                command.Parameters.AddWithValue("@TanarNev", tanarNev);
                                connection.Open();
                                int rowsAffected = command.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("A tanár neve sikeresen módosítva lett.");
                                    // Frissítjük a tanár kombinált mezőt
                                    PopulateTanarComboBox();
                                }
                                else
                                {
                                    MessageBox.Show("A tanár nevének módosítása sikertelen volt.");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Hiba történt: " + ex.Message);
                            Console.WriteLine(ex.StackTrace);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Kérem, adjon meg egy új nevet a tanárnak!");
                    }
                }
                else
                {
                    MessageBox.Show("Kérem, válasszon ki egy tanárt a módosításhoz!");
                }
                textBox2.Text = string.Empty;

            }
            if (KatBox.SelectedIndex == 2)
            {
                if (DiakokBox.SelectedItem != null)
                {
                    string szamlazasiNev = textBox2.Text.Trim();
                    string szamlazasiCim = textBox3.Text.Trim();

                    if (!string.IsNullOrEmpty(szamlazasiNev) || !string.IsNullOrEmpty(szamlazasiCim))
                    {
                        string diakNev = DiakokBox.SelectedItem.ToString();
                        UpdateDiakSzamlazasiAdatok(diakNev, szamlazasiNev, szamlazasiCim);
                    }
                    else
                    {
                        MessageBox.Show("Nincs adat a módosításhoz.");
                    }
                }
                else
                {
                    MessageBox.Show("Nincs kiválasztott diák.");
                }
                textBox3.Text = string.Empty;
                textBox2.Text = string.Empty;
            }
        }
        private void UpdateDiakSzamlazasiAdatok(string diakNev, string szamlazasiNev, string szamlazasiCim)
        {
            try
            {
                string query = @"
            UPDATE [dbo].[Diak] 
            SET SzamlazasiNev = @SzamlazasiNev, SzamlazasiCim = @SzamlazasiCim 
            WHERE Nev = @DiakNev";

                using (SqlConnection connection = new SqlConnection(constring))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@SzamlazasiNev", szamlazasiNev);
                    command.Parameters.AddWithValue("@SzamlazasiCim", szamlazasiCim);
                    command.Parameters.AddWithValue("@DiakNev", diakNev);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Diák adatok frissítve.");
                        PopulateDiakComboBox();
                    }
                    else
                    {
                        MessageBox.Show("Nem sikerült frissíteni a diák adatokat.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
