using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics.Eventing.Reader;
using ManagerFisier;
using System.Configuration;


namespace ManagerFarmacieGUI
{
    public partial class Form1 : Form
    {
        AdministrareMedicamente_FisierText adminMedicament;

        private int ultimulId = 0;

        public object ColorScheme { get; private set; }
        public object Office2010Theme { get; private set; }

        private Label lblsID;
        private Label lblsNume;
        private Label lblsDescriere;
        private Label lblsPret;
        private Label lblsStocDisponibil;

        private Label[] lblID;
        private Label[] lblNume;
        private Label[] lblDescriere;
        private Label[] lblPret;
        private Label[] lblStocDisponibil;
        private Label lblAdaugareMedicament;

        private Label lblNumeInput;
        private Label lblDescriereInput;
        private Label lblPretInput;
        private Label lblStocDisponibilInput;

        private TextBox txtNumeInput;
        private TextBox txtDescriereInput;
        private TextBox txtPretInput;
        private TextBox txtStocDisponibilInput;

        //label pt validare
        private Label lblNumeNou;
        private Label lblDescriereNou;
        private Label lblPretNou;
        private Label lblStocNou;
        private Label lblNumeMedicamentNou;

        private Button btnAdauga;
        private FormStartPosition manual;
        private const int LATIME_DEFAULT = 80;


        public Form1()
        {
            InitializeComponent();


            string numeFisier = ConfigurationManager.AppSettings["NumeFisier"];
            string locatieFisierSolutie = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string caleCompletaFisier = locatieFisierSolutie + "\\" + numeFisier;

            adminMedicament = new AdministrareMedicamente_FisierText(caleCompletaFisier);
            int nrMedicamente = 0;

            Medicament[] medicamente = adminMedicament.GetMedicamente(out nrMedicamente);

            //setare proprietati
            this.Size = new Size(850, 300);
            this.StartPosition = manual;
            this.BackColor = Color.White;
            //this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            //this.WindowState = FormWindowState.Maximized;
            this.Location = new Point(100, 100);
            this.Font = new Font("Arial", 9, FontStyle.Bold);
            this.StartPosition = FormStartPosition.CenterScreen; //centreaza forma in cadrul ecranului
            this.ForeColor = Color.DarkCyan;
            this.Text = "Informatii medicamente";
           
            //adaugare control de tip label pentru 'ID'
            lblsID = new Label();
            lblsID.Width = LATIME_DEFAULT - 40;
            lblsID.Top = 20;
            lblsID.Left = 460;
            lblsID.Text = "ID";
            lblsID.Font = new Font("Arial", 10, FontStyle.Bold);
            this.Controls.Add(lblsID);

            //adaugare control de tip label pentru 'Nume'
            lblsNume = new Label();
            lblsNume.Width = LATIME_DEFAULT + 50;
            lblsNume.Top = 20;
            lblsNume.Left = lblsID.Left + LATIME_DEFAULT -30 ;
            lblsNume.Text = "Nume";
            lblsNume.Font = new Font("Arial", 10, FontStyle.Bold);
            this.Controls.Add(lblsNume);

            //adaugare control de tip label pentru 'Descriere'
            lblsDescriere = new Label();
            lblsDescriere.Width = LATIME_DEFAULT + 60;
            lblsDescriere.Text = "Descriere";
            lblsDescriere.Top = 20;
            lblsDescriere.Left = lblsNume.Left + LATIME_DEFAULT + 60;
            lblsDescriere.Font = new Font("Arial", 10, FontStyle.Bold);
            this.Controls.Add(lblsDescriere);

            //adaugare control de tip label pentru 'Pret'
            lblsPret = new Label();
            lblsPret.Width = LATIME_DEFAULT - 20;
            lblsPret.Text = "Pret";
            lblsPret.Top = 20;
            lblsPret.Left = lblsDescriere.Left + LATIME_DEFAULT + 70;
            lblsPret.Font = new Font("Arial", 10, FontStyle.Bold);
            this.Controls.Add(lblsPret);

            //adaugare control de tip label pentru 'Stoc Disponibil'
            lblsStocDisponibil = new Label();
            lblsStocDisponibil.Width = LATIME_DEFAULT - 20;
            lblsStocDisponibil.Text = "Stoc";
            lblsStocDisponibil.Top = 20;
            lblsStocDisponibil.Left = lblsPret.Left + LATIME_DEFAULT - 15;
            lblsStocDisponibil.Font = new Font("Arial", 10, FontStyle.Bold);
            this.Controls.Add(lblsStocDisponibil);


            //adaugare control de tip label pentru 'Adaugare medicament'
            lblAdaugareMedicament = new Label();
            lblAdaugareMedicament.Width = 180;
            lblAdaugareMedicament.Text = "Adaugare medicament";
            lblAdaugareMedicament.Top = 20;
            lblAdaugareMedicament.Left = 20;
            lblAdaugareMedicament.Font = new Font("Arial", 10, FontStyle.Bold);
            lblAdaugareMedicament.ForeColor = Color.Gray;
            this.Controls.Add(lblAdaugareMedicament);

            //adaugare control de tip label pentru 'NumeInput'
            lblNumeInput = new Label();
            lblNumeInput.Width = LATIME_DEFAULT;
            lblNumeInput.Text = "Nume";
            lblNumeInput.Top = lblAdaugareMedicament.Top + 50;
            lblNumeInput.Left = 20;
            this.Controls.Add(lblNumeInput);

            //adaugare control de tip text pentru 'NumeInput'
            txtNumeInput = new TextBox();
            txtNumeInput.Width = LATIME_DEFAULT + 50;
            txtNumeInput.Top = lblAdaugareMedicament.Top + 50;
            txtNumeInput.Left = lblAdaugareMedicament.Left + LATIME_DEFAULT + 20;
            this.Controls.Add(txtNumeInput);

            // adaugare control de tip label pentru 'lblDescriereInput'
            lblDescriereInput = new Label();
            lblDescriereInput.Width = LATIME_DEFAULT;
            lblDescriereInput.Text = "Descriere medi";
            lblDescriereInput.Top = lblNumeInput.Top + 30;
            lblDescriereInput.Left = 20;
            this.Controls.Add(lblDescriereInput);

            //adaugare control de tip Text pentru 'txtDescriereInput'
            txtDescriereInput = new TextBox();
            txtDescriereInput.Width = LATIME_DEFAULT + 50;
            txtDescriereInput.Top = txtNumeInput.Top + 30;
            txtDescriereInput.Left = lblNumeInput.Left + LATIME_DEFAULT + 20;
            this.Controls.Add(txtDescriereInput);

            //adaugare control de tip label pentru 'lblPretInput'
            lblPretInput = new Label();
            lblPretInput.Width = LATIME_DEFAULT;
            lblPretInput.Text = "Pret";
            lblPretInput.Top = lblDescriereInput.Top + 30;
            lblPretInput.Left = 20;
            this.Controls.Add(lblPretInput);

            //adaugare control de tip text pentru 'PretInput'
            txtPretInput = new TextBox();
            txtPretInput.Width = LATIME_DEFAULT + 50;
            txtPretInput.Top = lblDescriereInput.Top + 30;
            txtPretInput.Left = lblDescriereInput.Left + LATIME_DEFAULT + 20;
            this.Controls.Add(txtPretInput);

            //adaugare control de tip label pentru 'lblStocDisponibilInput'
            lblStocDisponibilInput = new Label();
            lblStocDisponibilInput.Width = LATIME_DEFAULT;
            lblStocDisponibilInput.Text = "Stoc";
            lblStocDisponibilInput.Top = lblPretInput.Top + 30;
            lblStocDisponibilInput.Left = 20;
            this.Controls.Add(lblStocDisponibilInput);

            //adaugare control de tip Text pentru 'StocDisponibilInput'
            txtStocDisponibilInput = new TextBox();
            txtStocDisponibilInput.Width = LATIME_DEFAULT + 50;
            txtStocDisponibilInput.Top = lblPretInput.Top + 30;
            txtStocDisponibilInput.Left = lblPretInput.Left + LATIME_DEFAULT + 20;
            this.Controls.Add(txtStocDisponibilInput);

            //adaugare control de tip Button pentru 'Adauga medicament'
            btnAdauga = new Button();
            btnAdauga.Width = 230;
            btnAdauga.Text = "Adauga Medicament ";
            btnAdauga.Top = lblStocDisponibilInput.Top + 40;
            btnAdauga.ForeColor = Color.Gray;
            btnAdauga.Left = lblStocDisponibilInput.Left;

            //adaugare label pt mesaj validare pt Nume 
            lblNumeNou = new Label();
            lblNumeNou.Text = "*Introduceti doar caractere";
            lblNumeNou.TextAlign = ContentAlignment.MiddleLeft;
            lblNumeNou.Width = 200;
            lblNumeNou.Height = txtNumeInput.Height;
            lblNumeNou.Left = txtNumeInput.Left + txtNumeInput.Width + 10;
            lblNumeNou.Top = txtNumeInput.Top;
            lblNumeNou.ForeColor = Color.Red;

            //adaugare label pt mesaj validare pt Descriere 
            lblDescriereNou = new Label();
            lblDescriereNou.Text = "*Introduceti doar caractere";
            lblDescriereNou.TextAlign = ContentAlignment.MiddleLeft;
            lblDescriereNou.Width = 200;
            lblDescriereNou.Height = txtDescriereInput.Height;
            lblDescriereNou.Left = txtDescriereInput.Left + txtDescriereInput.Width + 10;
            lblDescriereNou.Top = txtDescriereInput.Top;
            lblDescriereNou.ForeColor = Color.Red;

            //adaugare label pt mesaj validare pt pret
            lblPretNou = new Label();
            lblPretNou.Text = "*Introduceti doar cifre";
            lblPretNou.TextAlign = ContentAlignment.MiddleLeft;
            lblPretNou.Width = 200;
            lblPretNou.Height = txtPretInput.Height;
            lblPretNou.Left = txtPretInput.Left + txtPretInput.Width + 10;
            lblPretNou.Top = txtPretInput.Top;
            lblPretNou.ForeColor = Color.Red;

            //adaugare label pt mesaj validare pt stoc disponibil
            lblStocNou = new Label();
            lblStocNou.Text = "*Introduceti doar cifre";
            lblStocNou.TextAlign = ContentAlignment.MiddleLeft;
            lblStocNou.Width = 200;
            lblStocNou.Height = txtStocDisponibilInput.Height;
            lblStocNou.Left = txtStocDisponibilInput.Left + txtStocDisponibilInput.Width + 10;
            lblStocNou.Top = txtStocDisponibilInput.Top;
            lblStocNou.ForeColor = Color.Red;

            //
            lblNumeMedicamentNou = new Label();
            lblNumeMedicamentNou.Text = "*Medicament deja introdus";
            lblNumeMedicamentNou.TextAlign = ContentAlignment.MiddleLeft;
            lblNumeMedicamentNou.Width = 210;
            lblNumeMedicamentNou.Height = lblNumeNou.Height;
            lblNumeMedicamentNou.Left = lblNumeNou.Left ;
            lblNumeMedicamentNou.Top = lblNumeNou.Top;
            lblNumeMedicamentNou.ForeColor = Color.Red;

            btnAdauga.Click += onButtonClicked;
            btnAdauga.Click += Form1_Load;

            this.Controls.Add(btnAdauga);


            txtNumeInput.GotFocus += new EventHandler(txtNumeInput_GetFocus);
            txtNumeInput.LostFocus += new EventHandler(txtNumeInput_LostFocus);

            txtDescriereInput.GotFocus += new EventHandler(txtDescriereInput_GetFocus);
            txtDescriereInput.LostFocus += new EventHandler(txtDescriereInput_LostFocus);

            txtPretInput.GotFocus += new EventHandler(txtPretInput_GetFocus);
            txtPretInput.LostFocus += new EventHandler(txtPretInput_LostFocus);

            txtStocDisponibilInput.GotFocus += new EventHandler(txtStocDisponibilInput_GetFocus);
            txtStocDisponibilInput.LostFocus += new EventHandler(txtStocDisponibilInput_LostFocus);

        }

        private void onButtonClicked(object sender, EventArgs e)
        {
            if (ValidareDate())
            {
                ultimulId++;
                Medicament medicament = new Medicament(ultimulId, txtNumeInput.Text, txtDescriereInput.Text, Convert.ToDecimal(txtPretInput.Text), Convert.ToInt32(txtStocDisponibilInput.Text));
                adminMedicament.AddMedicament(medicament);
               

            }
            
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
            AfiseazaMedicamente();
            //ultimulId = medicamente
        }

        private void txtNumeInput_GetFocus(object sender, EventArgs e)
        {
            txtNumeInput.BackColor = Color.LightBlue;
        }

        private void txtNumeInput_LostFocus(object sender, EventArgs e)
        {
            txtNumeInput.BackColor = SystemColors.Window;
        }

        private void txtDescriereInput_GetFocus(object sender, EventArgs e)
        {
            txtDescriereInput.BackColor = Color.LightBlue;

        }

        private void txtDescriereInput_LostFocus(object sender, EventArgs e)
        {
            txtDescriereInput.BackColor = SystemColors.Window;

        }

        private void txtPretInput_GetFocus(object sender, EventArgs e)
        {
            txtPretInput.BackColor = Color.LightBlue;
        }

        private void txtPretInput_LostFocus(object sender, EventArgs e)
        {
            txtPretInput.BackColor = SystemColors.Window;

        }

        private void txtStocDisponibilInput_GetFocus(object sender, EventArgs e)
        {
            txtStocDisponibilInput.BackColor = Color.LightBlue;

        }

        private void txtStocDisponibilInput_LostFocus(object sender, EventArgs e)
        {
            txtStocDisponibilInput.BackColor = SystemColors.Window;

        }

        private bool ValidareDate()
        {
            //string numeMedicamentNou = txtNumeInput.Text;
            bool dateValide = true;
            //VAlidare nume
            if (string.IsNullOrEmpty(txtNumeInput.Text) || !VerificaCaractere(txtNumeInput.Text))
            {
                dateValide = false;
                lblNumeInput.ForeColor = Color.Red;
                Controls.Add(lblNumeNou);
            }
            else
            {
                lblNumeInput.ForeColor = Color.DarkCyan;
                Controls.Remove(lblNumeNou);

                string numeMedicamentNou = txtNumeInput.Text;
                bool medicamentExistent = false;  //verificare dacă există deja un medicament cu același nume
                foreach (Medicament medicament in adminMedicament.GetMedicamente(out _))
                {
                    if (medicament.Nume == numeMedicamentNou)
                    {
                        medicamentExistent = true;
                        break;
                    }
                
                }
                if (medicamentExistent)
                {
                    dateValide = false;
                    lblNumeInput.ForeColor = Color.Red;
                    Controls.Add(lblNumeMedicamentNou);
                }
                else
                {
                    lblNumeInput.ForeColor = Color.DarkCyan;
                    Controls.Remove(lblNumeMedicamentNou);
                }

            }
            //this.Refresh();
            // Validare Descriere
            
            if (string.IsNullOrEmpty(txtDescriereInput.Text) || !VerificaCaractere(txtDescriereInput.Text))
            {
                dateValide = false;
                lblDescriereInput.ForeColor = Color.Red;
                this.Controls.Add(lblDescriereNou);
            }
            else
            {
                lblDescriereInput.ForeColor = Color.DarkCyan;
                Controls.Remove(lblDescriereNou);

            }
            //this.Refresh();

            // Validare Pret
            decimal pret;
            
            if (!decimal.TryParse(txtPretInput.Text, out pret) || pret <= 0)
            {
                dateValide = false;
                lblPretInput.ForeColor = Color.Red;    
                Controls.Add(lblPretNou);
            }
            else
            {
                lblPretInput.ForeColor = Color.DarkCyan;
                Controls.Remove(lblPretNou);
            }
            //this.Refresh();

            // Validare Stoc Disponibil
            int stoc;
            
            if (!int.TryParse(txtStocDisponibilInput.Text, out stoc)  || stoc < 0 || !VerificaNumar(txtStocDisponibilInput.Text))
            {
                dateValide = false;
                lblStocDisponibilInput.ForeColor = Color.Red;
                Controls.Add(lblStocNou);
            }
            else
            {
                 lblStocDisponibilInput.ForeColor = Color.DarkCyan;
                 Controls.Remove(lblStocNou);
            }
           
            //this.Refresh();
            return dateValide;
        }

        private bool VerificaCaractere(string input)
        {
            foreach(char c in input)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                {
                    return false;
                }
            }
            return true;
        }

        private bool VerificaNumar(string input)
        {
            foreach(char c in input)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        private void AfiseazaMedicamente()
        {
            Medicament[] medicamente = adminMedicament.GetMedicamente(out int nrMedicamente);
            ultimulId = medicamente.Length;
            lblID = new Label[nrMedicamente];
            lblNume = new Label[nrMedicamente];
            lblPret = new Label[nrMedicamente];
            lblStocDisponibil = new Label[nrMedicamente];
            lblDescriere = new Label[nrMedicamente];

            int i = 0;
            foreach (Medicament medicament in medicamente)
            {
                //adaugare control de tip label pentru 'ID'

                lblID[i] = new Label();
                lblID[i].Width = LATIME_DEFAULT - 40;
                lblID[i].Top = lblsID.Top + 30*(i + 1);
                lblID[i].Left = 460;
                //lblID[i].BackColor = Color.Gray;
                lblID[i].Text = Convert.ToString(medicament.Id);
                this.Controls.Add(lblID[i]);

                //adaugare control de tip label pentru 'Nume'
                lblNume[i] = new Label();
                lblNume[i].Width = LATIME_DEFAULT + 50;
                lblNume[i].Top = lblsNume.Top+30 * (i + 1);
                lblNume[i].Left = lblID[i].Left + LATIME_DEFAULT - 30;
                lblNume[i].Text = medicament.Nume;
                //lblNume[i].BackColor = Color.Gray;
                this.Controls.Add(lblNume[i]);

                //adaugare control de tip label pentru 'Descriere'
                lblDescriere[i] = new Label();
                lblDescriere[i].Width = LATIME_DEFAULT + 60;
                lblDescriere[i].Top = lblsDescriere.Top + 30 * (i + 1);
                lblDescriere[i].Left = lblNume[i].Left + LATIME_DEFAULT + 60;
                //lblDescriere[i].BackColor = Color.Gray;
                lblDescriere[i].Text = medicament.Descriere;
                this.Controls.Add(lblDescriere[i]);

                //adaugare control de tip label pentru 'Pret'
                lblPret[i] = new Label();
                lblPret[i].Width = LATIME_DEFAULT - 20;
                lblPret[i].Top = lblsPret.Top + 30 * (i + 1);
                lblPret[i].Left = lblDescriere[i].Left + LATIME_DEFAULT + 70;
                //lblPret[i].BackColor = Color.Gray;
                lblPret[i].Text = Convert.ToString(medicament.Pret);
                this.Controls.Add(lblPret[i]);

                //adaugare control de tip label pentru 'Stoc Disponibil'
                lblStocDisponibil[i] = new Label();
                lblStocDisponibil[i].Width = LATIME_DEFAULT;
                lblStocDisponibil[i].Top = lblsStocDisponibil.Top+30 * (i + 1);
                lblStocDisponibil[i].Left = lblPret[i].Left + LATIME_DEFAULT - 15;
               //lblStocDisponibil[i].BackColor = Color.Gray;
                lblStocDisponibil[i].Text =Convert.ToString(medicament.StocDisponibil);
                this.Controls.Add(lblStocDisponibil[i]);
                i++;
            }


        }
    }
}
