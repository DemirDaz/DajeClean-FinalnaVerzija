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
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class Account : MetroWindow
    {
        ObservableCollection<Nalog> listaNaloga = new ObservableCollection<Nalog>();
        
        public Account()
        {
            InitializeComponent();
            NapuniNaloge();
            listaNaloga.RemoveAt(0);
            dgAcc.ItemsSource = listaNaloga;
            
        }
        private void NapuniNaloge()
        {
            var trosobe = EFDataProvider.GetNalozi();
            foreach (Nalog trosoba in trosobe)
                listaNaloga.Add(trosoba);
        }

        private void izmeniacc_Click(object sender, RoutedEventArgs e)
        {
            if (dgAcc.SelectedItem != null)
            {
                Registracija prozorRegistracije = new Registracija();
                prozorRegistracije.DataContext = dgAcc.SelectedItem;
                prozorRegistracije.Owner = this;
                prozorRegistracije.ShowDialog();
            }
            else
                MessageBox.Show("Morate selektovati nalog.");
        }

        private void izbrisiacc_Click(object sender, RoutedEventArgs e)
        {
            if (dgAcc.SelectedItem != null)
            {
                EFDataProvider.IzbrisiNalog(listaNaloga[dgAcc.SelectedIndex]);
                listaNaloga.RemoveAt(dgAcc.SelectedIndex);
            }
            else
                MessageBox.Show("Morate selektovati nalog.");
        }

        private void dgAcc_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount == 2)
            {
                Registracija prozorRegistracije = new Registracija();
                prozorRegistracije.DataContext = dgAcc.SelectedItem;
                prozorRegistracije.Owner = this;
                prozorRegistracije.ShowDialog();
            }
        }

        private void dodajacc_Click(object sender, RoutedEventArgs e)
        {
            Registracija prozorDodavanja = new Registracija();
            prozorDodavanja.DataContext = listaNaloga;
            prozorDodavanja.Owner = this;
            prozorDodavanja.Show();
            prozorDodavanja.txtusername.Clear();
            prozorDodavanja.txtsifra.Clear();
            prozorDodavanja.txtimeprezime.Clear();
        }
    }
}
