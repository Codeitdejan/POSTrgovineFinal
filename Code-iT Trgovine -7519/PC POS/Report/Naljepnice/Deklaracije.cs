using System;
using GenCode128;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace PCPOS.Report.Naljepnice
{
    public partial class Deklaracije : Form
    {
        public Deklaracije()
        {
            InitializeComponent();
        }

        private void btnPartner_Click(object sender, EventArgs e)
        {
            frmRobaTrazi roba_trazi = new frmRobaTrazi();
            roba_trazi.ShowDialog();
            string propertis_sifra = Properties.Settings.Default.id_roba.Trim();
            if (propertis_sifra != "")
            {
                string sql = "SELECT * FROM roba WHERE sifra='" + propertis_sifra + "'";

                DataTable DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra.Text = DTRoba.Rows[0]["sifra"].ToString();
                    txtNaslov.Text = DTRoba.Rows[0]["naziv"].ToString();
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoji artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Deklaracije_Load(object sender, EventArgs e)
        {
            try
            {
                /* 
                if (File.Exists("Naljepnice"))
                {
                    string[] podaci = File.ReadAllLines("Naljepnice");
                    txtSifra.Text = podaci[0];
                    txtNaslov.Text = podaci[1];
                    txtBrojNaljepnica.Text = podaci[5];
                    txtZapocniOdBroja.Text = podaci[6];
                }*/
            }
            catch (Exception ex)
            {
            }
            this.reportViewer1.RefreshReport();
        }

        private void btnKreiraj_Click(object sender, EventArgs e)
        {
            try
            {
                int brojZadanihNaljepnica;
                int.TryParse(txtBrojNaljepnica.Text, out brojZadanihNaljepnica);

                int zapocniOdBroja = 0;
                int.TryParse(txtZapocniOdBroja.Text, out zapocniOdBroja);

                int brojGeneriranih = 0;

                DataTable DTlista = dSDeklaracije.DTRoba;

                DTlista.Clear();

                for (int i = 0; i < (brojZadanihNaljepnica + zapocniOdBroja); i = i + 3)
                {
                    bool _1 = false, _2 = false, _3 = false;

                    if ((i + 1) >= zapocniOdBroja && brojGeneriranih < brojZadanihNaljepnica)
                    {
                        _1 = true;
                        brojGeneriranih++;
                    }
                    if ((i + 2) >= zapocniOdBroja && brojGeneriranih < brojZadanihNaljepnica)
                    {
                        _2 = true;
                        brojGeneriranih++;
                    }
                    if ((i + 3) >= zapocniOdBroja && brojGeneriranih < brojZadanihNaljepnica)
                    {
                        _3 = true;
                        brojGeneriranih++;
                    }


                    DataRow r = DTlista.NewRow();
                    r["sifra"] = _1 ? txtSifra.Text : "";
                    r["naziv_robe"] = _1 ? txtNaslov.Text : "";
                    r["proizvodac"] = _1 ? txtProizvodac.Text : "";
                    r["izvoznik"] = _1 ? txtIzvoznik.Text : "";
                    r["uvoznik"] = _1 ? txtUvoznik.Text : "";
                    r["zemlja_podrijetla"] = _1 ? txtZemlja.Text : "";
                    r["ostalo"] = _1 ? txtOstalo.Text : "";
                    
                    r["sifra1"] = _2 ? txtSifra.Text : "";
                    r["naziv_robe1"] = _2 ? txtNaslov.Text : "";
                    r["proizvodac1"] = _2 ? txtProizvodac.Text : "";
                    r["izvoznik1"] = _2 ? txtIzvoznik.Text : "";
                    r["uvoznik1"] = _2 ? txtUvoznik.Text : "";
                    r["zamlja_podrijetla1"] = _2 ? txtZemlja.Text : "";
                    r["ostalo1"] = _2 ? txtOstalo.Text : "";

                    r["sifra2"] = _3 ? txtSifra.Text : "";
                    r["naziv_robe2"] = _3 ? txtNaslov.Text : "";
                    r["proizvodac2"] = _3 ? txtProizvodac.Text : "";
                    r["izvoznik2"] = _3 ? txtIzvoznik.Text : "";
                    r["uvoznik2"] = _3 ? txtUvoznik.Text : "";
                    r["zamlja_podrijetla2"] = _3 ? txtZemlja.Text : "";
                    r["ostalo2"] = _3 ? txtOstalo.Text : "";

                    DTlista.Rows.Add(r);
                }

                this.reportViewer1.RefreshReport();

            }
            catch (Exception ex)
            {
            }
        }
    }
}
