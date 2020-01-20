using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for Lista.xaml
    /// </summary>
    public partial class Lista : MetroWindow 
    {
        ArhiviraniPosao odradjeniposao = new ArhiviraniPosao();
        ObservableCollection<MoguciPosao> dnevniposlovi = new ObservableCollection<MoguciPosao>();
        int dodaniposlovi = 0;
        public Lista()
        {
            InitializeComponent();
            this.
            NapuniDnevno();
            if(MainWindow.privremeni == "Sve")
                dgDP.ItemsSource = dnevniposlovi;
            else if (MainWindow.privremeni == "Pranje")
            {
                for (int i = 0; i < dnevniposlovi.Count; i++)
                {
                    if (dnevniposlovi[i].tip != "Pranje")
                    {
                        dnevniposlovi.RemoveAt(i);
                        i -= 1;
                    }
                }
            }
            else if (MainWindow.privremeni == "Ciscenje")
            {
                for (int i = 0; i < dnevniposlovi.Count; i++)
                {
                    if (dnevniposlovi[i].tip != "Ciscenje")
                    {
                        dnevniposlovi.RemoveAt(i);
                        i -= 1;
                    }
                }
            }
            else 
            {
                for(int i = 0; i<dnevniposlovi.Count; i++)
                    if(dnevniposlovi[i].tip != "Kontejneri")
                    {
                        dnevniposlovi.RemoveAt(i);
                        i -= 1;
                    }
            }
            dgDP.ItemsSource = dnevniposlovi;
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
                        if (p.planp == "A" || p.planp == "E")
                            dnevniposlovi.Add(p);
                    }
                    break;
                case "Tuesday":
                    foreach (MoguciPosao p in trposlovi)
                    {
                        if (p.planp == "B")
                            dnevniposlovi.Add(p);
                    }
                    break;
                case "Wednesday":
                    string strdatum = DateTime.Now.ToString();
                    int datum = int.Parse(strdatum.ElementAt(3).ToString()) * 10 + int.Parse(strdatum.ElementAt(4).ToString());
                    if (datum >= 23)
                    {
                        foreach (MoguciPosao p in trposlovi)
                        {
                            if (p.planp == "F")
                                dnevniposlovi.Add(p);
                        }
                        break;
                    }
                    else
                    {
                        foreach (MoguciPosao p in trposlovi)
                        {
                            if (p.planp == "A")
                                dnevniposlovi.Add(p);
                        }
                        break;
                    }

                case "Thursday":
                    foreach (MoguciPosao p in trposlovi)
                    {
                        if (p.planp == "C")
                            dnevniposlovi.Add(p);
                    }
                    break;
                case "Friday":
                    foreach (MoguciPosao p in trposlovi)
                    {
                        if (p.planp == "D")
                            dnevniposlovi.Add(p);
                    }
                    break;

            }
        }

        private void dodaj_Click(object sender, RoutedEventArgs e)
        {
            switch (MainWindow.privremeni) {
                case "Sve":
                    dnevniposlovi.Add(new MoguciPosao(true));
                    break;
                case "Ciscenje":
                    dnevniposlovi.Add(new MoguciPosao(MainWindow.privremeni, true));
                    break;
                case "Pranje":
                    dnevniposlovi.Add(new MoguciPosao(MainWindow.privremeni, true));
                    break;
                case "Kontejneri":
                    dnevniposlovi.Add(new MoguciPosao(MainWindow.privremeni, true));
                    break;
            }
        }
        private void chkSelectAll_Checked(object sender, RoutedEventArgs e)
        {
            foreach (MoguciPosao p in dnevniposlovi)
            {
                p.Stiklirano = true;
            }
            dgDP.ItemsSource = null;
            dgDP.ItemsSource = dnevniposlovi;
        }

        private void chkSelectAll_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (MoguciPosao p in dnevniposlovi)
                p.Stiklirano = false;
            dgDP.ItemsSource = null;
            dgDP.ItemsSource = dnevniposlovi;
        }

        private void potvrdi_Click(object sender, RoutedEventArgs e)
        {
            List<MoguciPosao> brzalista = new List<MoguciPosao>();
            for(int j = 0; j < dnevniposlovi.Count; j++)
            {
                if (dnevniposlovi[j].Stiklirano == true)
                {
                    if (dnevniposlovi[j].ulica != null)
                    {
                        if (dnevniposlovi[j].tip == "Pranje" || dnevniposlovi[j].tip == "Ciscenje" || dnevniposlovi[j].tip == "Kontejneri")
                        {
                            odradjeniposao.ulicaIme = dnevniposlovi[j].ulica;
                            odradjeniposao.tip = dnevniposlovi[j].tip;
                            odradjeniposao.vreme = DateTime.Now;
                            EFDataProvider.DodajArhiviraniPosao(odradjeniposao);
                            if (j < (dnevniposlovi.Count - dodaniposlovi))
                                EFDataProvider.IzmeniMoguciPosao(dnevniposlovi[j]);
                        }
                        else if (dnevniposlovi[j].tip == "pranje" || dnevniposlovi[j].tip == "ciscenje" || dnevniposlovi[j].tip == "kontejneri")
                            MessageBox.Show("Pocetno slovo tipa datog posla mora biti veliko.");
                        else
                            MessageBox.Show("Uneli ste nepostojeći tip posla!");
                    }
                    else
                        MessageBox.Show("Niste uneli naziv ulice");
                }
            }
            if(dodaniposlovi > 0)
                for (int i = dnevniposlovi.Count - dodaniposlovi; i < dnevniposlovi.Count; i++)
                    brzalista.Add(dnevniposlovi[i]);
            dnevniposlovi.Clear();
            NapuniDnevno();
            if(dodaniposlovi > 0)
                foreach(MoguciPosao bp in brzalista)
                    dnevniposlovi.Add(bp);
        }
    }
}
