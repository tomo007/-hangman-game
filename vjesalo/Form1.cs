using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Vjesalo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string rec = "";

        List<Label> labels = new List<Label>();

        int kolicina = 0;

        enum DjeloviTijela
        {
            Glava,
            Lijevo_Oko,
            Desno_Oko,
            Usta,
            Desna_Ruka,
            Lijeva_Ruka,
            Tijelo,
            Desna_Noga,
            Lijeva_Noga
        }
        void NacrtajVjesalo()
        {
            Graphics g = panel1.CreateGraphics();
            Pen p = new Pen(Color.Brown, 10);
            g.DrawLine(p, new Point(210, 354), new Point(210, 5));
            g.DrawLine(p, new Point(215, 5), new Point(85, 5));
            g.DrawLine(p, new Point(80, 0), new Point(80, 50));       
        }

        void NacrtajDijeloveTijela(DjeloviTijela dt)
        {
            Graphics g = panel1.CreateGraphics();
            Pen p = new Pen(Color.Blue, 2);
            SolidBrush s = new SolidBrush(Color.DarkRed);
            switch (dt) {
                case DjeloviTijela.Glava:
                    g.DrawEllipse(p, 52, 50, 52, 52);
                    break;
                case DjeloviTijela.Desno_Oko:
                    g.FillEllipse(s, 83, 60, 5, 5);
                    break;
                case DjeloviTijela.Lijevo_Oko:
                    g.FillEllipse(s, 63, 60, 5, 5);
                    break;
                case DjeloviTijela.Usta:
                    g.DrawArc(p, 65, 65, 20, 20, 45, 90);
                    break;
                case DjeloviTijela.Tijelo:
                    g.DrawLine(p, new Point(80, 100), new Point(80, 170));
                    break;
                case DjeloviTijela.Lijeva_Ruka:
                    g.DrawLine(p, new Point(80, 120), new Point(30, 85));
                    break;
                case DjeloviTijela.Desna_Ruka:
                    g.DrawLine(p, new Point(80, 120), new Point(130, 85));
                    break;
                case DjeloviTijela.Lijeva_Noga:
                    g.DrawLine(p, new Point(80, 170), new Point(30, 250));
                    break;
                case DjeloviTijela.Desna_Noga:
                    g.DrawLine(p, new Point(80, 170), new Point(130, 250));
                    break;

            }
        }

        void NapraviLabels()
        {
            rec = UzmiNasumicnuRijec();
            char[] chars = rec.ToCharArray();
            int izmedju = 330 / chars.Length - 1;
            for (int i = 0; i < chars.Length; i++)
            {
                labels.Add(new Label());
                labels[i].Location = new Point((i * izmedju) + 10, 80);
                labels[i].Text = "_";
                labels[i].Parent = groupBox2;
                labels[i].BringToFront();
                labels[i].CreateControl();
            }
            label1.Text = "Duzina Rijeci: " + (chars.Length).ToString();
        }

        string UzmiNasumicnuRijec()
        {

            String[] text = System.IO.File.ReadAllLines(@"C:\Users\tomis\Desktop\projekti\Vjesalo\vjesalo\Rijeci.txt");
            Random r = new Random();
            return text[r.Next(0, text.Length-1)];
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            NacrtajVjesalo();
            NapraviLabels();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            char slovo = textBox1.Text.ToLower().ToCharArray()[0];
            textBox1.Clear();
            if (rec.Contains(slovo))
            {
                char[] slova = rec.ToCharArray();
                for (int i = 0; i < slova.Length; i++)
                {
                    if (slova[i] == slovo)
                        labels[i].Text = slovo.ToString();
                }
                foreach (Label l in labels)
                    if (l.Text == "_") return;
                MessageBox.Show("Pobjedili ste!", "Cestitamo");        
                Reset();
            }
            else
            {
                MessageBox.Show("Slovo koje ste probali nije u rijeci!");
                label2.Text += " " + slovo.ToString() + ",";
                NacrtajDijeloveTijela((DjeloviTijela)kolicina);
                kolicina++;
                if (kolicina == 9)
                {
                    MessageBox.Show("Izgubili ste! Rijec je bila " + rec);
                    Reset();
                }
            }
            
        }
        void Reset()
        {
            Graphics g = panel1.CreateGraphics();
            g.Clear(panel1.BackColor);
            UzmiNasumicnuRijec();
            NapraviLabels();
            NacrtajVjesalo();
            label2.Text = "Promasaji: ";
            textBox1.Text = "";
            kolicina = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == rec)
            {
                MessageBox.Show("Pobjedili ste!", "Cestitamo");
                textBox2.Clear();
                Reset();
            }
            else
            {
                MessageBox.Show("Rijec koju ste probali je pogresna!", "Greska!");
                NacrtajDijeloveTijela((DjeloviTijela)kolicina);
                kolicina++;
                if (kolicina == 9)
                {
                    MessageBox.Show("Izgubili ste! Rijec je bila " + rec);
                    Reset();
                }
            }
            textBox2.Clear();

        }

        
    }
}
