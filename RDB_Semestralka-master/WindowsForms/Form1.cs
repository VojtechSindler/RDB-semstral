/********************************************************************************************************************************
 *                                                  Aplikace na práci s databází
 *                                                  Předmět RDB
 *                                                  Autor Aplikace Robin Dvořák
 *                                                  Autor Databáze Vojtěch Šindler
 ********************************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;

namespace WindowsForms
{
    public partial class Form1 : Form
    {
        string sql_;
        DatabaseModel.DatabaseContext context;
        Database db;
        Paginator paginator;
        
        public Form1()
        {
            InitializeComponent();
            db = new Database();
            paginator = new Paginator();
            
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            //btnPrev.Enabled = false;
            context = new DatabaseModel.DatabaseContext();
            await LoadAll();
            //var querry = context.Multiple_data_select.SqlQuery("SELECT TOP 1000 * FROM Multiple_data_select");
            //this.multiple_data_selectBindingSource.DataSource = querry.ToList();
            //context.Multiple_data_select.Load();
            //this.multiple_data_selectBindingSource.DataSource = context.Multiple_data_select.Local.ToBindingList();
            
            toolStripStatusLabel2.Text = "Zobrazeno 50 záznamů z celkových " + db.countRows().ToString();
            multiple_data_selectBindingSource.DataSource = paginator.firstPage();
}
        /***********************************************************************************************************************
         *Načíst všechno
         ***********************************************************************************************************************/
        private void button8_Click(object sender, EventArgs e)
        {
            specificSearch();
        }
        /***********************************************************************************************************************
         *Vyhledávání podle skupiny
         ***********************************************************************************************************************/
        private void button1_Click(object sender, EventArgs e)
        {
            string searchText = this.textBox1.Text;
            if (searchText != "")
            {
                var data = db.searchGroupName(searchText);
                if (data != null)
                {
                    this.multiple_data_selectBindingSource.DataSource = data;
                    toolStripStatusLabel2.Text = "Filtrováno podle názvu skupiny. Vyhledaný termín: " + searchText;
                    this.sql_ = " SELECT * FROM Multiple_data_select WHERE descriptionGroup LIKE('%" + textBox1.Text + "%')";
                }
                else
                {
                    MessageBox.Show("Nic nenalezeno");
                }
                textBox1.Clear();
            }

        }
        /***********************************************************************************************************************
        * Hledání podle přístroje
        ***********************************************************************************************************************/
        private void button4_Click(object sender, EventArgs e)
        {
            string searchText = this.textBox2.Text;
            if (searchText != "")
            {
                var data = db.searchMachineID(searchText);
                if (data != null)
                {
                    this.multiple_data_selectBindingSource.DataSource = data;
                    toolStripStatusLabel2.Text = "Filtrováno podle ID přístroje. Vyhledaný termín: " + searchText;
                    this.sql_ = " SELECT * FROM Multiple_data_select  WHERE machineID LIKE ('%" + textBox1.Text + "%')";
                }
                else
                {
                    MessageBox.Show("Nic nenalezeno");
                }
                textBox2.Clear();
            }
        }
        /***********************************************************************************************************************
        * Hledání podle odchylky
        ***********************************************************************************************************************/
        private void button5_Click(object sender, EventArgs e)
        {
            string searchText = this.textBox3.Text;
            //float deviation = Single.Parse(this.textBox3.Text);

            if (searchText != "")
            {
                string sql = "SELECT * FROM Multiple_data_select WHERE ABS(value1-value2) <" + searchText;
                var data = db.search(sql);
                //var data = db.searchDeviation(deviation);
                if (data != null)
                {
                    this.multiple_data_selectBindingSource.DataSource = data;
                    toolStripStatusLabel2.Text = "Filtrováno podle odchylky. Vyhledaný termín: " + searchText;
                    this.sql_ = sql;
                }
                else
                {
                    MessageBox.Show("Nic nenalezeno");
                }
                textBox3.Clear();
            }
            else if(searchText.Equals("-1"))
            {
                string sql = "SELECT * FROM Multiple_data_select WHERE ABS(value1-value2) < PointAccuracy";
                string statusLabel = "Zobrazena filtrovaná data. Vyhledaný termín: " + searchText;
                db.search(sql);
                this.sql_ = sql;
            }
        }
        /***********************************************************************************************************************
        * Hledání podle souřadnic
        ***********************************************************************************************************************/
        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "" && textBox5.Text != "")
            {
                //podle X a Y
                //float x = Single.Parse(textBox4.Text);
                //float y = Single.Parse(textBox5.Text);
                string sql = "SELECT * FROM Multiple_data_select WHERE (X = " + textBox4.Text + " AND Y = " + textBox5.Text + ")";
                string statusLabel = "Vyhledána souřadnice X = " + textBox4.Text + " Y = " + textBox5.Text;
                var data = db.search(sql);
                //var data = db.searchCoordinates(x, y);
                if (data != null)
                {
                    this.multiple_data_selectBindingSource.DataSource = data;
                    toolStripStatusLabel2.Text = "Vyhledána souřadnice X = " + textBox4.Text + " Y = " + textBox5.Text;
                    this.sql_ = sql;
                }
                else
                {
                    MessageBox.Show("Nic nenalezeno");
                }

                /*
                string sql = "SELECT * FROM Multiple_data_select WHERE (X = " + textBox4.Text + " AND Y = " + textBox5.Text + ")";
                string statusLabel = "Vyhledána souřadnice X = " + x + " Y = " + y;
                db.search(sql);*/
                textBox4.Clear();
                textBox5.Clear();
            }
            if (textBox4.Text != "" && textBox5.Text == "")
            {
                //podle X
                string x = textBox4.Text;
                var data = db.search(x);
                if (data != null)
                {
                    this.multiple_data_selectBindingSource.DataSource = data;
                    toolStripStatusLabel2.Text = "Vyhledána souřadnice X = " + x;
                    this.sql_ = "SELECT * FROM Multiple_data_select WHERE X = " + textBox4.Text;
                }
                else
                {
                    MessageBox.Show("Nic nenalezeno");
                }                
                textBox4.Clear();
            }
            if (textBox4.Text == "" && textBox5.Text != "")
            {
                //podle Y
                string y = textBox5.Text;
                var data = db.search(y);
                if (data != null)
                {
                    this.multiple_data_selectBindingSource.DataSource = data;
                    toolStripStatusLabel2.Text = "Vyhledána souřadnice Y = " + y;
                    this.sql_ = "SELECT * FROM Multiple_data_select WHERE y = " + textBox5.Text;
                }
                else
                {
                    MessageBox.Show("Nic nenalezeno");
                }    
                textBox4.Clear();
            }

        }
        /***********************************************************************************************************************
        * Hledání podle datumu
        ***********************************************************************************************************************/
        private void button7_Click(object sender, EventArgs e)
        {
            DateTime date1 = dateTimePicker1.Value.Date;
            DateTime dt2 = dateTimePicker2.Value.Date;
            DateTime date2 = new DateTime(dt2.Year, dt2.Month, dt2.Day, 23, 59, 59);
            var data = db.searchByDate(date1, date2);
            if(data != null){
               this.multiple_data_selectBindingSource.DataSource = data;            
                toolStripStatusLabel2.Text = "Zobrazena měření z období " + date1.ToString("MM/dd/yyyy HH:mm:ss") + " - " + date2.ToString("MM/dd/yyyy HH:mm:ss");
                this.sql_ = "SELECT * FROM Multiple_data_select WHERE [time] BETWEEN '" + date1.ToString("MM/dd/yyyy HH:mm:ss") + "' AND '" + date2.ToString("MM/dd/yyyy HH:mm:ss") + "'"; 
            }
            else
            {
                MessageBox.Show("Nic nenalezeno");
            }
            
        }

        /***********************************************************************************************************************
        * Specificke hledani
       ***********************************************************************************************************************/
        private void specificSearch()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM Multiple_data_select WHERE");

            List<string> sql = new List<string>();

            if (textBox4.Text != "")
            {
                sql.Add(" X = " + textBox4.Text);

            }


            if (textBox5.Text != "")
            {
                sql.Add(" Y= " + textBox5.Text);

            }


            if (textBox3.Text != "")
            {
                sql.Add(" ABS(value1-value2) <" + textBox3.Text);

            }
            else if(textBox3.Text.Equals("-1"))
            {
                sql.Add(" ABS(value1-value2) < PointAccuracy");

            }


            if (textBox2.Text != "")
            {
                sql.Add(" machineID LIKE('%" + textBox2.Text + "%')");

            }


            if (textBox1.Text != "")
            {
                sql.Add(" descriptionGroup LIKE('%" + textBox1.Text + "%')");
            }

            for (int i = 0; i < sql.Count; i++)
            {
                sb.Append(sql[i]);
                if (i < sql.Count - 1)
                    sb.Append(" AND");
            }
            multiple_data_selectBindingSource.DataSource = db.search(sb.ToString());
            this.sql_ = sb.ToString();
        }

        /***********************************************************************************************************************
         * Ošetření vstupů
        ***********************************************************************************************************************/
        //Souřadnice X - jen čísla a des. čárka
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            NubersOnInput(sender, e);
        }

        //Souřadnice Y - jen čísla a des. čárka
        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            NubersOnInput(sender, e);
        }
        //Odchylka
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            NubersOnInput(sender, e);
        }
        private void NubersOnInput(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // jen jedna des. čárka
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        
        /***********************************************************************************************************************
         * Import souboru do databáze
         ***********************************************************************************************************************/
        private async void importovatCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Filter = "CSV soubory (*.csv)|*.csv|Všechny soubory (*.*)|*.*";
           if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    await UploadFileAndShowResults(openFileDialog1.FileName);
 
                    
                    sw.Stop();
                    TimeSpan ts = sw.Elapsed;
                    // Format and display the TimeSpan value.
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);
                    MessageBox.Show("Soubor byl úspěšně nahrán. Trvání operace " + elapsedTime);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Chyba při nahrávání souboru\n" + ex);
                }
            }

        }
        private async Task UploadFileAndShowResults(string path)
        {
            toolStripStatusLabel1.Text = "Nahrávám soubor...";
            bool result = await UploadFileAsync(path);
            toolStripStatusLabel1.Text = "";

        }
        private Task<bool> UploadFileAsync(string path)
        {
            return Task.Run(() =>
            {
                context.Database.CommandTimeout = 1800;
                context.savefile(path);
                GC.Collect();
                return true;
            });
        }
        /***********************************************************************************************************************
        * Export souboru z databáze
        ***********************************************************************************************************************/
        private void exportovatCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var querry = context.Multiple_data_select.SqlQuery(this.sql_.ToString()).ToList();
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog1.Title = "Export do CSV souboru";
            saveFileDialog1.Filter = "CSV soubory (*.csv)|*.csv|Všechny soubory (*.*)|*.*";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                using (StreamWriter w = new StreamWriter(saveFileDialog1.FileName))
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < querry.Count;i++)
                    {
                        sb.Append(Program.ToUnixTimestamp(querry[i].time));sb.Append(";");
                        sb.Append(querry[i].variableName);sb.Append(";");
                        sb.Append(querry[i].pointID);sb.Append(";");
                        sb.Append(querry[i].X);sb.Append(";");
                        sb.Append(querry[i].Y);sb.Append(";");
                        sb.Append(querry[i].description);sb.Append(";");
                        sb.Append(querry[i].value1);sb.Append(";");
                        sb.Append(querry[i].value2);sb.Append(";");
                        sb.Append(querry[i].PointAccuracy);sb.Append(";");
                        sb.Append(querry[i].machineID);sb.Append(";");
                        sb.Append(querry[i].PointAccuracy);sb.Append(";");
                        sb.Append(querry[i].descriptionMachine);sb.Append(";");
                        sb.Append(querry[i].groupID);sb.Append(";");
                        sb.Append(querry[i].descriptionGroup);sb.Append("\n");

                        WriteToCSV(sb.ToString(), w);
                        toolStripStatusLabel1.Text = "Úspěšně vyexportováno.";
                    }
                }
            }
            
        }
        
        private void WriteToCSV(string line, TextWriter w)
        {
            toolStripStatusLabel1.Text = "Zapisuji do souboru.";
            w.Write(line);
        }

        private void ukončitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }
        private async Task LoadAll()
        {
            toolStripStatusLabel1.Text = "Nahrávám kompletní data z DB...";
            bool result = await load();
            toolStripStatusLabel1.Text = "Nahráno vše.";

        }
        private Task<bool> load()
        {
            return Task.Run(() =>
            {
                context.Database.CommandTimeout = 1800;
                context.Multiple_data_select.Load();
                GC.Collect();
                return true;
            });
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            multiple_data_selectBindingSource.DataSource = paginator.previousPage();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            multiple_data_selectBindingSource.DataSource = paginator.nextPage();
        }


    }

}
