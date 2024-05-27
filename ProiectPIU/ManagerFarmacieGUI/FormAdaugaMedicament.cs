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
using Clase;

namespace ManagerFarmacieGUI
{
    public partial class FormAdaugaMedicament : Form
    {
        AdministrareMedicamente_FisierText adminMedicament;

        private int ultimulId = 0;

        public object ColorScheme { get; private set; }
        public object Office2010Theme { get; private set; }
        


        private Label lblAdaugareMedicament;

        private Label lblNumeInput;
        private Label lblDescriereInput;
        private Label lblPretInput;
        private Label lblStocDisponibilInput;
        private Label lblVarstaInput;

        private TextBox txtNumeInput;
        private TextBox txtPretInput;
        private TextBox txtStocDisponibilInput;

        private RadioButton cbVarstaInput;
        private GroupBox groupBox1;
        private GroupBox groupBox2;

        //label pt validare
        private Label lblNumeNou;
        private Label lblDescriereNou;
        private Label lblpretnou;
        private Label lblStocNou;
        private Label lblNumeMedicamentNou;

        private Button btnAdauga;
        private FormStartPosition manual;
        private const int LATIME_DEFAULT = 80;


        Enumerari.CaracteristiciMedicament selectedEnum = Enumerari.CaracteristiciMedicament.None;
        List<Enumerari.CategorieMedicament> selectedEnums = new List<Enumerari.CategorieMedicament>();


        public FormAdaugaMedicament()
        {
            InitializeComponent();


            string numeFisier = ConfigurationManager.AppSettings["NumeFisier"];
            string locatieFisierSolutie = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string caleCompletaFisier = locatieFisierSolutie + "\\" + numeFisier;

            adminMedicament = new AdministrareMedicamente_FisierText(caleCompletaFisier);
            int nrMedicamente = 0;

            Medicament[] medicamente = adminMedicament.GetMedicamente(out nrMedicamente);

            //setare proprietati
            this.Size = new Size(450, 550);
            this.StartPosition = manual;
            this.BackColor = Color.White;
            this.Location = new Point(100, 100);
            this.Font = new Font("Arial", 9, FontStyle.Bold);
            this.StartPosition = FormStartPosition.CenterScreen; //centreaza forma in cadrul ecranului
            this.ForeColor = Color.DarkCyan;
            this.Text = "Informatii medicamente";
           
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
            txtNumeInput.Width = LATIME_DEFAULT + 120;
            txtNumeInput.Top = lblAdaugareMedicament.Top + 50;
            txtNumeInput.Left = lblAdaugareMedicament.Left + LATIME_DEFAULT + 20;
            this.Controls.Add(txtNumeInput);

            // adaugare control de tip label pentru 'lblDescriereInput'
            lblDescriereInput = new Label();
            lblDescriereInput.Width = LATIME_DEFAULT;
            lblDescriereInput.Text = "Categorie";
            lblDescriereInput.Top = lblNumeInput.Top + 30;
            lblDescriereInput.Left = 20;
            this.Controls.Add(lblDescriereInput);

            // adaugare control de tip label pentru 'lblVarstaInput'
            lblVarstaInput = new Label();
            lblVarstaInput.Width = LATIME_DEFAULT + 10;
            lblVarstaInput.Text = "Recomandari";
            lblVarstaInput.Top = lblDescriereInput.Top + LATIME_DEFAULT + 130;
            lblVarstaInput.Left = 20;
            this.Controls.Add(lblVarstaInput);


            groupBox1 = new GroupBox();
            groupBox1.Top = lblDescriereInput.Top;
            groupBox1.Left = lblNumeInput.Left + LATIME_DEFAULT + 20;
            groupBox1.Height = 30 + 25 * 6;

            var enumValues = Enum.GetValues(typeof(Enumerari.CategorieMedicament)).Cast<Enumerari.CategorieMedicament>();
            int yPos = 20;
            foreach (var value in enumValues)
            {
                if (value != Enumerari.CategorieMedicament.None)
                {
                    CheckBox checkBox = new CheckBox();
                    checkBox.Text = value.ToString();
                    checkBox.Location = new System.Drawing.Point(10, yPos);
                    checkBox.Tag = value; // Store the enum value in the Tag property
                    Size textSize = TextRenderer.MeasureText(checkBox.Text, checkBox.Font);
                    checkBox.Width = groupBox1.Width - 50;

                    yPos += 30;
                    groupBox1.Controls.Add(checkBox);
                }
                    
            }

            // Add GroupBox to the form
            this.Controls.Add(groupBox1);

            groupBox2 = new GroupBox();
            groupBox2.Top = groupBox1.Top + 30 + groupBox1.Height;
            groupBox2.Left = lblNumeInput.Left + LATIME_DEFAULT + 20;
            groupBox2.Height = 30 + 25 * 4;

            var enumValues2 = Enum.GetValues(typeof(Enumerari.CaracteristiciMedicament)).Cast<Enumerari.CaracteristiciMedicament>();
            int yPoss = 20;
            foreach (var value in enumValues2)
            {
                if(value != Enumerari.CaracteristiciMedicament.None)
                {
                    RadioButton radioButtonn = new RadioButton();
                    radioButtonn.Text = value.ToString();
                    radioButtonn.Location = new System.Drawing.Point(10, yPoss);
                    radioButtonn.Tag = value; // Store the enum value in the Tag property
                    Size textSize = TextRenderer.MeasureText(radioButtonn.Text, radioButtonn.Font);
                    radioButtonn.Width = groupBox2.Width - 20;

                    yPoss += 30;
                    groupBox2.Controls.Add(radioButtonn);
                }
                
            }

            //Adauga GroupBox2
            this.Controls.Add(groupBox2);

            //adaugare control de tip ComboBox pentru 'cbVarstaInput'
            cbVarstaInput = new RadioButton();
            cbVarstaInput.Width = LATIME_DEFAULT + 50;
            cbVarstaInput.Top = 15;
            cbVarstaInput.Left = 15;
            groupBox2.Controls.Add(cbVarstaInput);

            ////adaugare control de tip label pentru 'lblPretInput'
            lblPretInput = new Label();
            lblPretInput.Width = LATIME_DEFAULT;
            lblPretInput.Text = "Pret";
            lblPretInput.Top = groupBox2.Height+groupBox2.Top+20;
            lblPretInput.Left = 20;
            this.Controls.Add(lblPretInput);

            ////adaugare control de tip text pentru 'PretInput'
            txtPretInput = new TextBox();
            txtPretInput.Width = LATIME_DEFAULT + 120;
            txtPretInput.Top = lblDescriereInput.Top + 360;
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



            //adaugare label pt mesaj validare pt pret
            lblpretnou = new Label();
            lblpretnou.Text = "*introduceti doar cifre";
            lblpretnou.TextAlign = ContentAlignment.MiddleLeft;
            lblpretnou.Width = 200;
            lblpretnou.Height = txtPretInput.Height;
            lblpretnou.Left = txtPretInput.Left + txtPretInput.Width + 10;
            lblpretnou.Top = txtPretInput.Top;
            lblpretnou.ForeColor = Color.Red;

            //adaugare label pt mesaj validare pt stoc disponibil
            lblStocNou = new Label();
            lblStocNou.Text = "*Introduceti doar cifre";
            lblStocNou.TextAlign = ContentAlignment.MiddleLeft;
            lblStocNou.Width = 200;
            lblStocNou.Height = txtStocDisponibilInput.Height;
            lblStocNou.Left = txtStocDisponibilInput.Left + txtStocDisponibilInput.Width + 10;
            lblStocNou.Top = txtStocDisponibilInput.Top;
            lblStocNou.ForeColor = Color.Red;


            //adaugare control de tip Button pentru 'Adauga medicament'
            btnAdauga = new Button();
            btnAdauga.Width = 230;
            btnAdauga.Text = "Adauga Medicament ";
            btnAdauga.Top = txtStocDisponibilInput.Top + 40;
            btnAdauga.ForeColor = Color.Gray;
            btnAdauga.Left = lblPretInput.Left;
            btnAdauga.Width = lblPretInput.Width + groupBox2.Width + 20;

            //adaugare label pt mesaj validare pt Nume 
            lblNumeNou = new Label();
            lblNumeNou.Text = "*Introduceti doar caractere";
            lblNumeNou.TextAlign = ContentAlignment.MiddleLeft;
            lblNumeNou.Width = 200;
            lblNumeNou.Height = txtNumeInput.Height;
            lblNumeNou.Left = txtNumeInput.Left + txtNumeInput.Width + 10;
            lblNumeNou.Top = txtNumeInput.Top;
            lblNumeNou.ForeColor = Color.Red;


            //
            lblNumeMedicamentNou = new Label();
            lblNumeMedicamentNou.Text = "*Medicament deja introdus";
            lblNumeMedicamentNou.TextAlign = ContentAlignment.MiddleLeft;
            lblNumeMedicamentNou.Width = 210;
            lblNumeMedicamentNou.Height = lblNumeNou.Height;
            lblNumeMedicamentNou.Left = lblNumeNou.Left;
            lblNumeMedicamentNou.Top = lblNumeNou.Top;
            lblNumeMedicamentNou.ForeColor = Color.Red;


            btnAdauga.Click += onButtonClicked;
            btnAdauga.Click += Form1_Load;

            this.Controls.Add(btnAdauga);
            this.Controls.Add(groupBox1);
            this.Controls.Add(groupBox2);


            txtNumeInput.GotFocus += new EventHandler(txtNumeInput_GetFocus);
            txtNumeInput.LostFocus += new EventHandler(txtNumeInput_LostFocus);


            txtPretInput.GotFocus += new EventHandler(txtPretInput_GetFocus);
            txtPretInput.LostFocus += new EventHandler(txtPretInput_LostFocus);

            txtStocDisponibilInput.GotFocus += new EventHandler(txtStocDisponibilInput_GetFocus);
            txtStocDisponibilInput.LostFocus += new EventHandler(txtStocDisponibilInput_LostFocus);

        }

        private void onButtonClicked(object sender, EventArgs e)
        {
            if (ValidareDate())
            {
                selectedEnums.Clear();

                ultimulId++;
                

                foreach (Control control in groupBox1.Controls)
                {
                    if (control is CheckBox checkBox && checkBox.Checked)
                    {
                        if (checkBox.Tag is Enumerari.CategorieMedicament enumValue)
                        {
                            selectedEnums.Add(enumValue);
                            
                        }
                    }
                }
                

                foreach (Control control in groupBox2.Controls)
                {
                    if (control is RadioButton radioButton && radioButton.Checked)
                    {
                        if (radioButton.Tag is Enumerari.CaracteristiciMedicament enumValue)
                        {
                            selectedEnum = enumValue;
                            break;
                        }
                    }
                }
                
                Enumerari.CategorieMedicament[] selectedEnumArray = selectedEnums.ToArray();
                Console.WriteLine(selectedEnumArray);
                Medicament medicament = new Medicament(ultimulId, txtNumeInput.Text, Convert.ToDecimal(txtPretInput.Text), Convert.ToInt32(txtStocDisponibilInput.Text), selectedEnumArray, selectedEnum);
                adminMedicament.AddMedicament(medicament);
                Array.Clear(selectedEnumArray, 0, selectedEnumArray.Length);
                
            }
            txtNumeInput.Clear();
            txtPretInput.Clear();
            txtStocDisponibilInput.Clear();
            foreach (Control control in groupBox1.Controls)
            {
                if (control is CheckBox checkBox && checkBox.Checked)
                {
                    checkBox.Checked = false;
                }
            }
            foreach (Control control in groupBox2.Controls)
            {
                if (control is RadioButton radioButton && radioButton.Checked)
                {
                    radioButton.Checked = false;
                }
            }

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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

            // Validare Pret
            decimal pret;
            
            if (!decimal.TryParse(txtPretInput.Text, out pret) || pret <= 0)
            {
                dateValide = false;
                lblPretInput.ForeColor = Color.Red;    
                Controls.Add(lblpretnou);
            }
            else
            {
                lblPretInput.ForeColor = Color.DarkCyan;
                Controls.Remove(lblpretnou);
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
            foreach (Control control in groupBox1.Controls)
            {
                if (control is CheckBox checkBox && checkBox.Checked)
                {
                    if (checkBox.Tag is Enumerari.CategorieMedicament enumValue)
                    {
                        selectedEnums.Add(enumValue);
                    }
                }
            }
            if (selectedEnums.Count == 0)
            {
                lblDescriereInput.ForeColor = Color.Red;
            }


            foreach (Control control in groupBox2.Controls)
            {
                if (control is RadioButton radioButton && radioButton.Checked)
                {
                    if (radioButton.Tag is Enumerari.CaracteristiciMedicament enumValue)
                    {
                        selectedEnum = enumValue;
                        break;
                    }
                }
            }
            if (selectedEnum == Enumerari.CaracteristiciMedicament.None)
            {
                lblVarstaInput.ForeColor = Color.Red;
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

    }
}
