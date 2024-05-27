using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clase;
using ManagerFisier;
using static Clase.Enumerari;

namespace ManagerFarmacieGUI
{
    public partial class VizualizareStoc : Form
    {
        AdministrareMedicamente_FisierText adminMedicament;

        private int ultimulId = 0;

        private ListView listView1;

        private Label lblCautaNume;
        private Label lblCautaCategorie;
        private Label lblCautaRecomandare;

        private TextBox txtCautaNume;
        private ComboBox cmbCautaCategorie;
        private ComboBox cmbCautaRecomandare;

        private Button btnModify;
        private Button btnBack;
        private Button btnSearch;

        private FormStartPosition manual;
        private const int LATIME_DEFAULT = 120;
        private const int windowWidth = LATIME_DEFAULT * 6 + 40;

        public VizualizareStoc()
        {
            InitializeComponent();

            this.Size = new Size(windowWidth, 550);
            this.StartPosition = manual;
            this.BackColor = Color.White;
            this.Location = new Point(100, 100);
            this.Font = new Font("Arial", 9, FontStyle.Bold);
            this.StartPosition = FormStartPosition.CenterScreen; //centreaza forma in cadrul ecranului
            this.ForeColor = Color.DarkCyan;
            this.Text = "Informatii medicamente";

            listView1 = new ListView();
            listView1.Width = this.Size.Width - 60;
            //listView1.Height = this.Size.Height-60;
            listView1.Top = 20;
            listView1.Left = 20;
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.MultiSelect = false;
            listView1.HideSelection = false;
            listView1.Scrollable = true;
            listView1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.Controls.Add(listView1);

            string numeFisier = ConfigurationManager.AppSettings["NumeFisier"];
            string locatieFisierSolutie = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string caleCompletaFisier = locatieFisierSolutie + "\\" + numeFisier;

            adminMedicament = new AdministrareMedicamente_FisierText(caleCompletaFisier);
            int nrMedicamente = 0;

            Medicament[] medicamente = adminMedicament.GetMedicamente(out nrMedicamente);
           


            lblCautaNume = new Label
            {
                Text = "Nume",
                TextAlign = ContentAlignment.MiddleCenter,
                Top = 440,
                Width = listView1.Width / 3 - 10,
                Left = listView1.Left
            };
            lblCautaCategorie = new Label
            {
                Text = "Categorie",
                TextAlign = ContentAlignment.MiddleCenter,
                Top = 440,
                Width = listView1.Width / 3 - 10,
                Left = lblCautaNume.Left + lblCautaNume.Width + 20
            };
            lblCautaRecomandare = new Label
            {
                Text = "Recomandare",
                TextAlign = ContentAlignment.MiddleCenter,
                Top = 440,
                Width = listView1.Width / 3 - 10,
                Left = lblCautaCategorie.Left + lblCautaCategorie.Width + 20
            };
            txtCautaNume = new TextBox
            {
                Top = 470,
                Width = listView1.Width / 3 - 10,
                Left = listView1.Left
            };
            cmbCautaCategorie = new ComboBox
            {
                Top = 470,
                Width = listView1.Width / 3 - 10,
                Left = lblCautaNume.Left + lblCautaNume.Width + 20,
                DataSource = Enum.GetValues(typeof(Enumerari.CategorieMedicament))
            };



            cmbCautaRecomandare = new ComboBox
            {
                Top = 470,
                Width = listView1.Width / 3 - 10,
                Left = lblCautaCategorie.Left + lblCautaCategorie.Width + 20,
                DataSource = Enum.GetValues(typeof(Enumerari.CaracteristiciMedicament))
            };

            btnBack = new Button();
            btnBack.Text = "Refresh";
            btnBack.Top = txtCautaNume.Top + txtCautaNume.Height + 20;
            btnBack.ForeColor = Color.Gray;
            btnBack.Left = listView1.Left;
            btnBack.Width = listView1.Width / 3 - 10;
            this.Controls.Add(btnBack);

            btnBack.Click += BtnBack_Click;

            btnSearch = new Button();
            btnSearch.Text = "Cauta medicament";
            btnSearch.Top = txtCautaNume.Top + txtCautaNume.Height + 20;
            btnSearch.ForeColor = Color.Gray;
            btnSearch.Left = btnBack.Left + btnBack.Width + 20;
            btnSearch.Width = listView1.Width / 3 - 10;
            btnSearch.Click += CautaMedicament;
            this.Controls.Add(btnSearch);


            btnModify = new Button();
            btnModify.Text = "Modifica Medicament ";
            btnModify.Top = btnSearch.Top;
            btnModify.ForeColor = Color.Gray;
            btnModify.Left = btnSearch.Left + btnSearch.Width + 20;
            btnModify.Width = listView1.Width / 3 - 10;
            btnModify.Click += openModificare;


            this.Controls.Add(btnModify);
            this.Controls.Add(lblCautaNume);
            this.Controls.Add(lblCautaCategorie);
            this.Controls.Add(lblCautaRecomandare);
            this.Controls.Add(txtCautaNume);
            this.Controls.Add(cmbCautaRecomandare);
            this.Controls.Add(cmbCautaCategorie);
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            AfiseazaMedicamente();
        }

        private void VizualizareStoc_Load(object sender, EventArgs e)
        {
            AfiseazaMedicamente();

        }

        private void openModificare(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                // Retrieve the selected item from the ListView
                ListViewItem selectedItem = listView1.SelectedItems[0];
                string numeMedicament = selectedItem.Text;
                string categoriiMedicament = selectedItem.SubItems[3].Text;
                string caracteristicaMedicament = selectedItem.SubItems[4].Text;
                string pretMedicament = selectedItem.SubItems[2].Text;
                string stocMedicament = selectedItem.SubItems[1].Text;
                ModificaMedicament modificaMedicament = new ModificaMedicament(numeMedicament, categoriiMedicament, caracteristicaMedicament, pretMedicament, stocMedicament, listView1.SelectedIndices[0].ToString());
               
                modificaMedicament.FormClosed += ModificaMedicament_FormClosed;
                modificaMedicament.ShowDialog();
            }
        }

        private void ModificaMedicament_FormClosed(object sender, FormClosedEventArgs e)
        {
            AfiseazaMedicamente();
        }

        private void CautaMedicament(object sender, EventArgs e)
        {
            string cautaNume = txtCautaNume.Text;
            Enumerari.CategorieMedicament cautaCategorie = (Enumerari.CategorieMedicament)cmbCautaCategorie.SelectedItem;
            Enumerari.CaracteristiciMedicament cautaVarsta = (Enumerari.CaracteristiciMedicament)cmbCautaRecomandare.SelectedItem;

            Medicament[] medicamente = adminMedicament.GetMedicamente(out int nrMedicamente);
            var medicamenteFiltrate = medicamente.Where(d =>
         // Filter by name if provided
             (string.IsNullOrEmpty(cautaNume) || d.Nume.Equals(cautaNume, StringComparison.OrdinalIgnoreCase)) &&
         // Filter by category if provided and not None
            (cautaCategorie == Enumerari.CategorieMedicament.None || d.Categorie.Contains(cautaCategorie)) &&
         // Filter by characteristics if provided and not None
            (cautaVarsta == Enumerari.CaracteristiciMedicament.None || d.Caracteristici == cautaVarsta)
            ).ToList();

            // Example: displaying the filtered results in a ListBox or other control
            listView1.Items.Clear();

            foreach (var medicament in medicamenteFiltrate)
            {
                var item = new ListViewItem(medicament.Nume);
                item.SubItems.Add(medicament.Pret.ToString());
                item.SubItems.Add(medicament.StocDisponibil.ToString());
                item.SubItems.Add(string.Join(",", medicament.Categorie));
                item.SubItems.Add(medicament.Caracteristici.ToString());
                listView1.Items.Add(item);
            }
        }

        public void AfiseazaMedicamente()
        {
            Medicament[] medicamente = adminMedicament.GetMedicamente(out int nrMedicamente);
            ultimulId = medicamente.Length;

            // Clear existing items before adding new ones
            listView1.Items.Clear();
            listView1.Columns.Clear();

            // Set ListView to Details view
            listView1.View = View.Details;

            // Add columns to the ListView for each attribute
            listView1.Columns.Add("Nume", listView1.Width / 5);
            listView1.Columns.Add("Pret", listView1.Width / 5);
            listView1.Columns.Add("Stoc Disponibil", listView1.Width / 5);
            listView1.Columns.Add("Categorie", listView1.Width / 5);
            listView1.Columns.Add("Recomandare", listView1.Width / 5);

            // Populate the ListView with data from medicamente array
            foreach (Medicament medicament in medicamente)
            {
                ListViewItem item = new ListViewItem();
                item.Text = medicament.Nume;
                item.SubItems.Add(medicament.Pret.ToString());
                item.SubItems.Add(medicament.StocDisponibil.ToString());

                // Combine categories into a single string separated by commas
                string categories = string.Join(",", medicament.Categorie);
                item.SubItems.Add(categories);

                item.SubItems.Add(medicament.Caracteristici.ToString());

                listView1.Items.Add(item);
            }

            // Set the height to display 10 items with a vertical scrollbar if necessary
            int itemHeight = listView1.GetItemRect(0).Height; // Assuming all items have the same height
            listView1.Height = itemHeight * 20 + SystemInformation.HorizontalScrollBarHeight;
            listView1.Scrollable = true;
        }

    }
}
