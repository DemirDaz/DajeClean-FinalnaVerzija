using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Collections.ObjectModel;
using MahApps.Metro.Controls;
//using System.Windows.Forms;

namespace WPFCLEAN
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///  public void GetCords() { object some = this.WindowStartupLocation; }
  
    public partial class MainWindow : MetroWindow
    {
        public static string privremeni;
        static Random kordinataxy = new Random();
       

        ObservableCollection<MoguciPosao> dnevniposloviPranje = new ObservableCollection<MoguciPosao>();
        ObservableCollection<MoguciPosao> dnevniposloviCiscenje = new ObservableCollection<MoguciPosao>();
        ObservableCollection<MoguciPosao> dnevniposloviKontejneri = new ObservableCollection<MoguciPosao>();
       
        public MainWindow()
        {
            InitializeComponent();
            
            if (!Login.sef)
            {
                tablaBtn.Visibility = Visibility.Hidden;
                listaBtn.Visibility = Visibility.Hidden;
                
            }
            EFDataProvider.RefreshujPoslove();
            NapuniDnevno();
        }

        #region Logout
        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Visibility = Visibility.Visible;
            this.Close();
        }
        #endregion
        private void listaBtn_Click(object sender, RoutedEventArgs e)
        {
            Arhiva prozorArhive = new Arhiva();
            prozorArhive.Owner = this;
            prozorArhive.ShowDialog();
        }

     
        private void tablaBtn_Click(object sender, RoutedEventArgs e)
        {
            Account prozorAcc = new Account();
            prozorAcc.Owner = this;
            prozorAcc.ShowDialog();
        }
        

        private void PranjeBtn_Click(object sender, RoutedEventArgs e)
        {
            Mapa.Children.Clear();
            DrawingGroup zaprljane = new DrawingGroup();
            foreach (MoguciPosao r in dnevniposloviPranje)
            {
                ImageDrawing lokacija = new ImageDrawing();
                double x = kordinataxy.Next(50, 790);
                double y = kordinataxy.Next(50, 430);
                lokacija.Rect = new Rect(x, y, 35, 35);
                lokacija.ImageSource = new BitmapImage(
               new Uri("../../Resursi/pranjedirty.png", UriKind.Relative));
                zaprljane.Children.Add(lokacija);
            }
            DrawingImage imgSource = new DrawingImage(zaprljane);
            imgSource.Freeze();
            Image imageControl = new Image();
            imageControl.Stretch = Stretch.None;
            imageControl.Source = imgSource;
            Mapa.Children.Add(imageControl);
          
             

            MainWindow.privremeni = "Pranje";
            Lista ProzorListe = new Lista();
            ProzorListe.Top = this.Top;
            ProzorListe.Left = this.Left - 100;
            ProzorListe.Owner = this;
            ProzorListe.ShowDialog();
        }

        private void ciscenjeBtn_Click(object sender, RoutedEventArgs e)
        {
            Mapa.Children.Clear();
            DrawingGroup zaprljane = new DrawingGroup();
            foreach (MoguciPosao r in dnevniposloviCiscenje)
            {
                ImageDrawing lokacija = new ImageDrawing();
                double x = kordinataxy.Next(50, 790);
                double y = kordinataxy.Next(50, 430);
                lokacija.Rect = new Rect(x, y, 35, 35);
                lokacija.ImageSource = new BitmapImage(
               new Uri("../../Resursi/ciscenjedirty.png", UriKind.Relative));
                zaprljane.Children.Add(lokacija);
            }
            DrawingImage imgSource = new DrawingImage(zaprljane);
            imgSource.Freeze();
            Image imageControl = new Image();
            imageControl.Stretch = Stretch.None;
            imageControl.Source = imgSource;
            Mapa.Children.Add(imageControl);

            MainWindow.privremeni = "Ciscenje";
            Lista ProzorListe = new Lista();
            ProzorListe.Top = this.Top;
            ProzorListe.Left = this.Left - 100;
            ProzorListe.Owner = this;
            ProzorListe.ShowDialog();
        }

        private void KontejneriBtn_Click(object sender, RoutedEventArgs e)
        {
            Mapa.Children.Clear();
            DrawingGroup zaprljane = new DrawingGroup();
            foreach (MoguciPosao r in dnevniposloviKontejneri)
            {
                ImageDrawing lokacija = new ImageDrawing();
                double x = kordinataxy.Next(50, 790);
                double y = kordinataxy.Next(50, 430);
                lokacija.Rect = new Rect(x, y, 35, 35);
                lokacija.ImageSource = new BitmapImage(
               new Uri("../../Resursi/kontejnerdirty.png", UriKind.Relative));
                zaprljane.Children.Add(lokacija);
            }
            DrawingImage imgSource = new DrawingImage(zaprljane);
            imgSource.Freeze();
            Image imageControl = new Image();
            imageControl.Stretch = Stretch.None;
            imageControl.Source = imgSource;
            Mapa.Children.Add(imageControl);



            MainWindow.privremeni = "Kontejneri";
            Lista ProzorListe = new Lista();
            ProzorListe.Top = this.Top;
            ProzorListe.Left = this.Left - 100;
            ProzorListe.Owner = this;
            ProzorListe.ShowDialog();
        }
        private void NapuniDnevno()
        {
            var trposlovi = EFDataProvider.GetMoguciPoslovi();
            string danas = DateTime.Now.DayOfWeek.ToString();
            switch (danas)
            {
                case "Monday":
                    foreach (MoguciPosao p in trposlovi)
                    {
                        if ((p.planp == "A" && p.tip == "Pranje") || (p.planp == "E" && p.tip == "Pranje"))
                            dnevniposloviPranje.Add(p);
                        else if ((p.planp == "A" && p.tip == "Ciscenje") || (p.planp == "E" && p.tip == "Ciscenje"))
                            dnevniposloviCiscenje.Add(p);
                        else if ((p.planp == "A" && p.tip == "Kontejneri") || (p.planp == "E" && p.tip == "Kontejneri"))
                            dnevniposloviKontejneri.Add(p);
                    }
                    break;

                case "Tuesday":
                    foreach (MoguciPosao p in trposlovi)
                    {
                        if (p.planp == "B" && p.tip == "Pranje")
                            dnevniposloviPranje.Add(p);
                        else if (p.planp == "B" && p.tip == "Ciscenje")
                            dnevniposloviCiscenje.Add(p);
                        else if (p.planp == "B" && p.tip == "Kontejneri")
                            dnevniposloviKontejneri.Add(p);
                    }
                    break;
                case "Wednesday":
                    string strdatum = DateTime.Now.ToString();
                    int datum = int.Parse(strdatum.ElementAt(3).ToString()) * 10 + int.Parse(strdatum.ElementAt(4).ToString());
                    if (datum >= 23)
                    {
                        foreach (MoguciPosao p in trposlovi)
                        {
                            if (p.planp == "F" && p.tip == "Pranje")
                                dnevniposloviPranje.Add(p);
                           
                        }
                        break;
                    }
                    else
                    {
                        foreach (MoguciPosao p in trposlovi)
                        {
                            if (p.planp == "A" && p.tip == "Pranje")
                                dnevniposloviPranje.Add(p);
                            else if (p.planp == "A" && p.tip == "Ciscenje")
                                dnevniposloviCiscenje.Add(p);
                            else if (p.planp == "A" && p.tip == "Kontejneri")
                                dnevniposloviKontejneri.Add(p);
                        }
                        break;
                    }

                case "Thursday":
                    foreach (MoguciPosao p in trposlovi)
                    {
                        if (p.planp == "C" && p.tip == "Pranje")
                            dnevniposloviPranje.Add(p);
                        else if (p.planp == "C" && p.tip == "Ciscenje")
                            dnevniposloviCiscenje.Add(p);
                        else if (p.planp == "C" && p.tip == "Kontejneri")
                            dnevniposloviKontejneri.Add(p);

                    }
                    break;
                case "Friday":
                    foreach (MoguciPosao p in trposlovi)
                    {
                        if (p.planp == "D" && p.tip == "Pranje") 
                            dnevniposloviPranje.Add(p);
                        else if (p.planp == "D" && p.tip == "Ciscenje")
                            dnevniposloviCiscenje.Add(p);
                        else if (p.planp == "D" && p.tip == "Kontejneri")
                            dnevniposloviKontejneri.Add(p);
                    }
                    break;

            }
        }

        private void DodajBtn_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow.privremeni = "Sve";
            Lista ProzorListe = new Lista();
            ProzorListe.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ProzorListe.Owner = this;
            ProzorListe.ShowDialog();
        }
    }
}
