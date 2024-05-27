using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagerFarmacieGUI
{
    public partial class MainForm : Form
    {
        private bool isAddOpen;
        private bool isViewOpen;
        private FormAdaugaMedicament AdaugareMedicamentForm;
        private VizualizareStoc VizualizareStoc;

        private Button VizualizareMedicament;
        private Button AdaugareMedicament;
        private Button InfoAutor;
        private Button ExitButton;
        public MainForm()
        {
           

            InitializeComponent();
            
        }

        private void LoadForm(object sender, EventArgs e)
        {
            LoadContent();
        }

        private void LoadContent() {
            this.Width = 400;
            this.Height = 600;

            VizualizareMedicament = new Button()
            {
                Text = "Vizualizare medicamente",
                Top = 20,
                Left = 20,
                Width = this.Width - 60
            };
            AdaugareMedicament = new Button()
            {
                Text = "Adaugare medicamente",
                Top = 60,
                Left = 20,
                Width = this.Width - 60

            };
            InfoAutor = new Button()
            {
                Text = "Informatii autor",
                Top = 100,
                Left = 20,
                Width = this.Width - 60
            };

            ExitButton = new Button()
            {
                Text = "Iesire",
                Top = this.Height - 40 * 2,
                Left = 20,
                Width = this.Width - 60
            };

            AdaugareMedicament.Click += AdaugareMedicamentClick;
            VizualizareMedicament.Click += VizualizareMedicamentClick;
            InfoAutor.Click += InfoAutor_Click;
            ExitButton.Click += ExitButton_Click;
            this.Controls.Add(VizualizareMedicament);
            this.Controls.Add(AdaugareMedicament);
            this.Controls.Add(InfoAutor);
            this.Controls.Add(ExitButton);
        }

        private void InfoAutor_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Autor: Bolohan Ana-Maria\nGrupa: 3121A", "Info autor");

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("Sigur vrei sa iesi?","Confirmare iesire", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Result == DialogResult.Yes)
            {
                Application.Exit();
            }
            
        }

        private void AdaugareMedicamentClick(object sender, EventArgs e)
        {
            if (isAddOpen)
            {
                
                CloseAdauagareMedicament();
                
            }
            else
            {
                OpenAdaugareMedicamente();
            }
            Refresh();
        }

        private void VizualizareMedicamentClick(object sender, EventArgs e)
        {
            if (isViewOpen)
            {
                CloseVizualizareMedicament();
            }
            else
            {
                OpenVizualizareMedicamente();
            }
            Refresh();
        }

        private void OpenAdaugareMedicamente()
        {
            if(AdaugareMedicamentForm == null)
            {

                AdaugareMedicamentForm = new FormAdaugaMedicament();
                AdaugareMedicamentForm.TopLevel = false;  // Makes the extra form a non-top-level form, which will allow us to add its controls to the main form.
                AdaugareMedicamentForm.FormBorderStyle = FormBorderStyle.None;  // Remove the border so it looks seamless.
                AdaugareMedicamentForm.Visible = true;

                // Adjust the location of the extra form if needed.

                AdaugareMedicamentForm.Top = 20;
                AdaugareMedicamentForm.Left = this.Width;
                this.Controls.Add(AdaugareMedicamentForm);
                this.Width += AdaugareMedicamentForm.Width + 40;
                this.Height = AdaugareMedicamentForm.Height + 60;
                isAddOpen = true;
            }
            
        }
        private void CloseAdauagareMedicament()
        {
            if (AdaugareMedicamentForm != null)
            {
                this.Controls.Remove(AdaugareMedicamentForm);
                this.Width -= AdaugareMedicamentForm.Width + 40;
                this.Height = 600;
                AdaugareMedicamentForm.Dispose();
                AdaugareMedicamentForm = null;
                isAddOpen = false;
            }
        }
        private void OpenVizualizareMedicamente()
        {
            if (VizualizareStoc == null)
            {
                VizualizareStoc = new VizualizareStoc();
                VizualizareStoc.TopLevel = false;  // Makes the extra form a non-top-level form, which will allow us to add its controls to the main form.
                VizualizareStoc.FormBorderStyle = FormBorderStyle.None;  // Remove the border so it looks seamless.
                VizualizareStoc.Visible = true;

                // Adjust the location of the extra form if needed.
                if (isAddOpen)
                {
                    VizualizareStoc.Left = this.Width;
                }
                else
                {
                    VizualizareStoc.Left = this.Width;
                }
                VizualizareStoc.Top = 20;

                VizualizareStoc.AfiseazaMedicamente();
                this.Controls.Add(VizualizareStoc);
                this.Width += VizualizareStoc.Width + 40;
                this.Height = VizualizareStoc.Height + 80;
                isViewOpen = true;
            }

        }
        private void CloseVizualizareMedicament()
        {
            if (VizualizareStoc != null)
            {
                this.Controls.Remove(VizualizareStoc);
                this.Width -= VizualizareStoc.Width + 40;
                this.Height = 600;
                VizualizareStoc.Dispose();
                VizualizareStoc = null;
                isViewOpen = false;
            }
        }


    }
}
