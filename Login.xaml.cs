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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : MetroWindow
    {
        public static bool sef = true;
        private ObservableCollection<Nalog> osobe = new ObservableCollection<Nalog>();
        public Login()
        {
            InitializeComponent();

            Napuni();

            cb.ItemsSource = osobe;
            cb.DisplayMemberPath = "username";
        }

        private bool PraznoPolje()
        {
            return string.IsNullOrEmpty(txtSifra.Password);
        }

        private void OcistiLoz()
        {
            txtSifra.Password = string.Empty;
        }

        private void cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.DataContext = cb.SelectedItem;
        }

        private void btPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (PraznoPolje())
            {
                MessageBox.Show("Polje za sifru je prazno!");
            }
            else
            {
               
                bool provera = false;
                foreach (var korisnik in osobe)
                {
                    if (korisnik.password == txtSifra.Password && korisnik.username == cb.Text)
                    {
                        if (cb.Text == "Dino")
                            sef = true;
                        else
                            sef = false;

                        provera = true;
                    }
                }
                if (provera == true)
                {
                    MainWindow Glavniprozor = new MainWindow();
                    Glavniprozor.Owner = this;
                    Glavniprozor.ShowDialog();
                    this.Close();
                }
                else
                    MessageBox.Show("Uneli ste pogresnu sifru.");
            }
        }
        private void Napuni()
        {
            var trosobe = EFDataProvider.GetNalozi();
            foreach (Nalog trosoba in trosobe)
                osobe.Add(trosoba);
        }

        private void txtSifra_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if (PraznoPolje())
                {
                    MessageBox.Show("Polje za sifru je prazno!");
                }
                else
                {

                    bool provera = false;
                    foreach (var korisnik in osobe)
                    {
                        if (korisnik.password == txtSifra.Password && korisnik.username  == cb.Text)
                        {
                            if (cb.Text == "Dino")
                                sef = true;
                            else
                                sef = false;

                            provera = true;
                        }
                    }
                    if (provera == true)
                    {
                        MainWindow Glavniprozor = new MainWindow();
                        Glavniprozor.Visibility = Visibility.Visible;
                        this.Close();
                    }
                    else
                        MessageBox.Show("Uneli ste pogresnu sifru.");
                }
            }
        }
    }
}
