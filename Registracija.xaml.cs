using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace WPFCLEAN
{
    /// <summary>
    /// Interaction logic for Registracija.xaml
    /// </summary>
    public partial class Registracija : MetroWindow
    {
        public Registracija()
        {
            InitializeComponent();
        }

        private void Otkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {

            if (Provera_Polja())
            {
                MessageBox.Show("Jedno ili vise pola je prazno! Unesite sva polja da bi napravili nalog.");
            }
            else
            {
                if (DataContext is ObservableCollection<Nalog> Osobeplus)
                {
                    bool provera = false;
                    foreach (var osoba in Osobeplus)
                    {
                        if (osoba.username == txtusername.Text)
                        {
                            provera = true;
                        }
                    }

                    if (!provera)
                    {
                        Nalog novaosoba = new Nalog(txtusername.Text, txtsifra.Text, txtimeprezime.Text);

                        Osobeplus.Add(novaosoba);
                        EFDataProvider.DodajNalog(novaosoba);
                        MessageBox.Show("Uspesno ste dodali novi nalog!");
                        this.Close();
                    }
                    else
                        MessageBox.Show("Korisničko ime je zazeto!");
                }
                
                else if (DataContext is Nalog izmena)
                {
                    BindingOperations.GetBindingExpression(txtusername, TextBox.TextProperty).UpdateSource();
                    BindingOperations.GetBindingExpression(txtsifra, TextBox.TextProperty).UpdateSource();
                    BindingOperations.GetBindingExpression(txtimeprezime, TextBox.TextProperty).UpdateSource();

                    if (EFDataProvider.IzmeniNalog(izmena) != 0)
                        MessageBox.Show("Uspešno ste izmenili nalog.");
                    else
                        MessageBox.Show("Korisnicko ime je zauzeto.");

                    this.Close();
                }
            }
        }
        private bool Provera_Polja()
        {
            return string.IsNullOrEmpty(txtusername.Text) || string.IsNullOrEmpty(txtsifra.Text) || string.IsNullOrEmpty(txtimeprezime.Text);        
        }
    }
}
