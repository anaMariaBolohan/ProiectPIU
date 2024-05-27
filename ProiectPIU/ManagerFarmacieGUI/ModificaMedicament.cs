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
using System.IO;

namespace ManagerFarmacieGUI
{
    public partial class ModificaMedicament : Form
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

        //label pt validare
        private Label lblNumeNou;
        private Label lblDescriereNou;
        private Label lblPretNou;
        private Label lblStocNou;
        private Label lblNumeMedicamentNou;

        private GroupBox groupBox1;
        private GroupBox groupBox2;

        Enumerari.CaracteristiciMedicament selectedEnum = Enumerari.CaracteristiciMedicament.None;
        List<Enumerari.CategorieMedicament> selectedEnums = new List<Enumerari.CategorieMedicament>();

        private Button btnAdauga;
        private FormStartPosition manual;
        private const int LATIME_DEFAULT = 80;




        public ModificaMedicament(string numeMedicament, string categoriiMedicament, string caracteristicaMedicament, string pretMedicament, string stocMedicament, string index)
        {
            InitializeComponent();

            string numeFisier = ConfigurationManager.AppSettings["NumeFisier"];
            string locatieFisierSolutie = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string caleCompletaFisier = locatieFisierSolutie + "\\" + numeFisier;

            //setare proprietati
            this.Size = new Size(LATIME_DEFAULT * 4 - 10, 550);
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
            txtNumeInput.Text = numeMedicament;
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
            groupBox1.Top = lblDescriereInput.Top -5;
            groupBox1.Left = lblNumeInput.Left + LATIME_DEFAULT + 20;
            groupBox1.Height = 50 + 25 * 6;

            


            var enumValues = Enum.GetValues(typeof(Enumerari.CategorieMedicament)).Cast<Enumerari.CategorieMedicament>();
            int yPos = 20;
            foreach (var value in enumValues)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Text = value.ToString();
                checkBox.Location = new System.Drawing.Point(10, yPos);
                checkBox.Tag = value; // Stochez valoarea enumerarilor in Tag 
                Size textSize = TextRenderer.MeasureText(checkBox.Text, checkBox.Font);
                checkBox.Width = groupBox1.Width - 50;

                yPos += 30;
                groupBox1.Controls.Add(checkBox);
            }
            var values = categoriiMedicament.Split(',');
            foreach (CheckBox checkBox in groupBox1.Controls.OfType<CheckBox>())
            {

                //verific dacă eticheta checkbox-ului se potrivește cu oricare din valorile enumerate
                if (values.Contains(checkBox.Tag.ToString()))
                {
                    // Check the checkbox
                    checkBox.Checked = true;
                }
            }

            // Adaug GroupBox1 la Form
            this.Controls.Add(groupBox1);//GroupBox1 pt Recomandari

            groupBox2 = new GroupBox();
            groupBox2.Top = groupBox1.Top + 10 + groupBox1.Height;
            groupBox2.Left = lblNumeInput.Left + LATIME_DEFAULT + 20;
            groupBox2.Height = 40 + 25 * 4;

            var enumValues2 = Enum.GetValues(typeof(Enumerari.CaracteristiciMedicament)).Cast<Enumerari.CaracteristiciMedicament>();
            int yPoss = 20;
            foreach (var value in enumValues2)
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
            var ageValues = caracteristicaMedicament;
            foreach (RadioButton radioButton in groupBox2.Controls.OfType<RadioButton>())
            {
                // Check if the checkbox's tag matches any of the enum values
                if (ageValues.Contains(radioButton.Tag.ToString()))
                {
                    // Check the checkbox
                    radioButton.Checked = true;
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
            lblPretInput.Top = groupBox2.Height + groupBox2.Top + 20;
            lblPretInput.Left = 20;
            this.Controls.Add(lblPretInput);

            ////adaugare control de tip text pentru 'PretInput'
            txtPretInput = new TextBox();
            txtPretInput.Text = pretMedicament;
            txtPretInput.Width = LATIME_DEFAULT + 120;
            txtPretInput.Top = lblDescriereInput.Top + 367;
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
            txtStocDisponibilInput.Text = stocMedicament;
            txtStocDisponibilInput.Width = LATIME_DEFAULT + 50;
            txtStocDisponibilInput.Top = lblPretInput.Top + 30;
            txtStocDisponibilInput.Left = lblPretInput.Left + LATIME_DEFAULT + 20;
            this.Controls.Add(txtStocDisponibilInput);

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

            ////adaugare label pt mesaj validare pt pret
            //lblpretnou = new Label();
            //lblpretnou.text = "*introduceti doar cifre";
            //lblpretnou.textalign = contentalignment.middleleft;
            //lblpretnou.width = 200;
            //lblpretnou.height = txtpretinput.height;
            //lblpretnou.left = txtpretinput.left + txtpretinput.width + 10;
            //lblpretnou.top = txtpretinput.top;
            //lblpretnou.forecolor = color.red;

            ////adaugare label pt mesaj validare pt stoc disponibil
            //lblStocNou = new Label();
            //lblStocNou.Text = "*Introduceti doar cifre";
            //lblStocNou.TextAlign = ContentAlignment.MiddleLeft;
            //lblStocNou.Width = 200;
            //lblStocNou.Height = txtStocDisponibilInput.Height;
            //lblStocNou.Left = txtStocDisponibilInput.Left + txtStocDisponibilInput.Width + 10;
            //lblStocNou.Top = txtStocDisponibilInput.Top;
            //lblStocNou.ForeColor = Color.Red;

            //
            lblNumeMedicamentNou = new Label();
            lblNumeMedicamentNou.Text = "*Medicament deja introdus";
            lblNumeMedicamentNou.TextAlign = ContentAlignment.MiddleLeft;
            lblNumeMedicamentNou.Width = 210;
            lblNumeMedicamentNou.Height = lblNumeNou.Height;
            lblNumeMedicamentNou.Left = lblNumeNou.Left;
            lblNumeMedicamentNou.Top = lblNumeNou.Top;
            lblNumeMedicamentNou.ForeColor = Color.Red;

            btnAdauga.Click += (s, ev) => onButtonClicked(s, ev, Convert.ToInt32(index), caleCompletaFisier);
            btnAdauga.Click += Form1_Load;

            this.Controls.Add(btnAdauga);
            this.Controls.Add(groupBox1);
            this.Controls.Add(groupBox2);


            txtNumeInput.GotFocus += new EventHandler(txtNumeInput_GetFocus);
            txtNumeInput.LostFocus += new EventHandler(txtNumeInput_LostFocus);

            //cbAnalgezic.GotFocus += new EventHandler(txtDescriereInput_GetFocus);
            //cbAnalgezic.LostFocus += new EventHandler(txtDescriereInput_LostFocus);

            txtPretInput.GotFocus += new EventHandler(txtPretInput_GetFocus);
            txtPretInput.LostFocus += new EventHandler(txtPretInput_LostFocus);

            //txtStocDisponibilInput.GotFocus += new EventHandler(txtStocDisponibilInput_GetFocus);
            //txtStocDisponibilInput.LostFocus += new EventHandler(txtStocDisponibilInput_LostFocus);

        }

        private void onButtonClicked(object sender, EventArgs e, int index, string caleCompletaFisier)
        {
            if (ValidareDate())
            {


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
                Medicament medicament = new Medicament(ultimulId, txtNumeInput.Text, Convert.ToDecimal(txtPretInput.Text), Convert.ToInt32(txtStocDisponibilInput.Text), selectedEnumArray, selectedEnum);

                string[] lines = File.ReadAllLines(caleCompletaFisier);
                lines[index] = medicament.ConversieLaSir_PentruFisier();
                File.WriteAllLines(caleCompletaFisier, lines);
                this.Refresh();
                this.Close();
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

        //private void txtDescriereInput_GetFocus(object sender, EventArgs e)
        //{
        //    cbAnalgezic.BackColor = Color.LightBlue;

        //}

        //private void txtDescriereInput_LostFocus(object sender, EventArgs e)
        //{
        //    cbAnalgezic.BackColor = SystemColors.Window;

        //}

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


            }
            //this.Refresh();
            // Validare Descriere

            //if (string.IsNullOrEmpty(cbAnalgezic.Text) || !VerificaCaractere(cbAnalgezic.Text))
            //{
            //    dateValide = false;
            //    lblDescriereInput.ForeColor = Color.Red;
            //    this.Controls.Add(lblDescriereNou);
            //}
            //else
            //{
            //    lblDescriereInput.ForeColor = Color.DarkCyan;
            //    Controls.Remove(lblDescriereNou);

            //}
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

            if (!int.TryParse(txtStocDisponibilInput.Text, out stoc) || stoc < 0 || !VerificaNumar(txtStocDisponibilInput.Text))
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
            foreach (char c in input)
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
            foreach (char c in input)
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
