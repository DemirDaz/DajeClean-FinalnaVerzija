using System;
using System.Printing;
using System.IO;
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
using MahApps.Metro.Controls;

namespace WPFCLEAN
{
    /// <summary>
    /// Interaction logic for Arhiva.xaml
    /// </summary>
    public partial class Arhiva : MetroWindow
    {
        ObservableCollection<ArhiviraniPosao> trlista = new ObservableCollection<ArhiviraniPosao>();
        ObservableCollection<ArhiviraniPosao> ListaArhiviranih = new ObservableCollection<ArhiviraniPosao>();

        bool ispisvega = false;

        List<string> tipovi = new List<string>();
        
        public Arhiva()
        {
            InitializeComponent();

            NapuniArhivirane();
            napuniTipove();

            comboTip.ItemsSource = tipovi;
            dgArhiva.ItemsSource = ListaArhiviranih;
        }
        private void NapuniArhivirane()
        {
            var trArhiva = EFDataProvider.GetArhiviraniPoslovi();
            foreach(var tr in trArhiva)
            {
                ListaArhiviranih.Add(tr);
            }
        }

        private void napuniTipove()
        {
            tipovi.Add("Sve");
            tipovi.Add("Pranje");
            tipovi.Add("Ciscenje");
            tipovi.Add("Kontejneri");
            
        }

        private void SkloniNepotrebne()
        {
            trlista.Clear();
            if (comboTip.SelectedValue == null || comboTip.SelectedValue.ToString() == "Sve")
                foreach (ArhiviraniPosao mp in ListaArhiviranih)
                    trlista.Add(mp);
            else if (comboTip.SelectedValue.ToString() == "Pranje")
            {
                foreach (ArhiviraniPosao mp in ListaArhiviranih)
                    if (mp.tip == "Pranje")
                        trlista.Add(mp);
            }
            else if (comboTip.SelectedValue.ToString() == "Ciscenje")
            {
                foreach (ArhiviraniPosao mp in ListaArhiviranih)
                    if (mp.tip == "Ciscenje")
                        trlista.Add(mp);
            }
            else if (comboTip.SelectedValue.ToString() == "Kontejneri")
            {
                foreach (ArhiviraniPosao mp in ListaArhiviranih)
                    if (mp.tip == "Kontejneri")
                        trlista.Add(mp);
            }
        }

        private void tip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int proveravremena = 0;
            //trlista.Clear();

            #region ProveraVremena

            if (PocetnoVreme.SelectedDate != null && KrajnjeVreme.SelectedDate != null)
                if (PocetnoVreme.SelectedDate > KrajnjeVreme.SelectedDate)
                    proveravremena = 5;
                else
                    proveravremena = 1;
            else if (PocetnoVreme.SelectedDate == null && KrajnjeVreme.SelectedDate != null)
                proveravremena = 2;
            else if (PocetnoVreme.SelectedDate != null && KrajnjeVreme.SelectedDate == null)
                proveravremena = 3;
            else if (PocetnoVreme.SelectedDate == null && KrajnjeVreme.SelectedDate == null)
                proveravremena = 4;
            else
                proveravremena = 5;

            #endregion

            if (proveravremena == 4)
                SkloniNepotrebne();
            else if (proveravremena == 1)
            {
                SkloniNepotrebne();
                for (int i = 0; i < trlista.Count; i++)
                    if (trlista[i].vreme < PocetnoVreme.SelectedDate || trlista[i].vreme > KrajnjeVreme.SelectedDate)
                    {
                        trlista.RemoveAt(i);
                        i -= 1;
                    }
            }
            else if (proveravremena == 2)
            {
                SkloniNepotrebne();
                for (int i = 0; i < trlista.Count; i++)
                    if (trlista[i].vreme > KrajnjeVreme.SelectedDate)
                    {
                        trlista.RemoveAt(i);
                        i -= 1;
                    }
            }
            else if (proveravremena == 3)
            {
                for (int i = 0; i < trlista.Count; i++)
                    if (trlista[i].vreme < PocetnoVreme.SelectedDate)
                    {
                        trlista.RemoveAt(i);
                        i -= 1;
                    }
            }
            else
                MessageBox.Show("Stavili ste nepostojece granice vremena.");
            dgArhiva.ItemsSource = trlista;
        }
        
        private void PocetnoVreme_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            SkloniNepotrebne();
            if (!ispisvega)
            {
                if (KrajnjeVreme.SelectedDate == null)
                {
                    for (int i = 0; i < trlista.Count; i++)
                        if (trlista[i].vreme < PocetnoVreme.SelectedDate)
                        {
                            trlista.RemoveAt(i);
                            i -= 1;
                        }
                }
                else if (KrajnjeVreme.SelectedDate != null)
                    if (PocetnoVreme.SelectedDate <= KrajnjeVreme.SelectedDate)
                    {
                        for (int j = 0; j < trlista.Count; j++)
                            if (trlista[j].vreme < PocetnoVreme.SelectedDate || trlista[j].vreme > KrajnjeVreme.SelectedDate)
                            {
                                trlista.RemoveAt(j);
                                j -= 1;
                            }
                    }
                    else
                        MessageBox.Show("Stavili ste nepostojece granice vremena.");
                dgArhiva.ItemsSource = null;
                dgArhiva.ItemsSource = trlista;
            }
        }

        private void KrajnjeVreme_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            SkloniNepotrebne();
            if (!ispisvega)
            {
                if (PocetnoVreme.SelectedDate == null)
                {
                    for (int i = 0; i < trlista.Count; i++)
                        if (trlista[i].vreme > KrajnjeVreme.SelectedDate)
                        {
                            trlista.RemoveAt(i);
                            i -= 1;
                        }
                }
                else if (PocetnoVreme.SelectedDate != null)
                    if (PocetnoVreme.SelectedDate <= KrajnjeVreme.SelectedDate)
                    {
                        for (int i = 0; i < trlista.Count; i++)
                            if (trlista[i].vreme < PocetnoVreme.SelectedDate || trlista[i].vreme > KrajnjeVreme.SelectedDate)
                            {
                                trlista.RemoveAt(i);
                                i -= 1;
                            }
                    }
                    else
                        MessageBox.Show("Stavili ste nepostojece granice vremena.");
            }
            dgArhiva.ItemsSource = null;
            dgArhiva.ItemsSource = trlista;
            ispisvega = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            trlista.Clear();
            foreach (ArhiviraniPosao a in ListaArhiviranih)
            {
                trlista.Add(a);
            }
            ispisvega = true;
            PocetnoVreme.SelectedDate = null;
            KrajnjeVreme.SelectedDate = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDlg = new PrintDialog();
            // printDlg.PrintVisual(dgArhiva, "Grid Printing.");
            if (printDlg.ShowDialog() == true)
            {
                //get selected printer capabilities
                PrintCapabilities capabilities = printDlg.PrintQueue.GetPrintCapabilities(printDlg.PrintTicket);

                //get scale of the print wrt to screen of WPF visual
                double scale = Math.Min(capabilities.PageImageableArea.ExtentWidth / this.ActualWidth, capabilities.PageImageableArea.ExtentHeight /
                               this.ActualHeight);

                //Transform the Visual to scale
                dgArhiva.LayoutTransform = new ScaleTransform(scale, scale);

                //get the size of the printer page
                Size sz = new Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);

                //update the layout of the visual to the printer page size.
                dgArhiva.Measure(sz);
                dgArhiva.Arrange(new Rect(new Point(capabilities.PageImageableArea.OriginWidth, capabilities.PageImageableArea.OriginHeight), sz));

                //now print the visual to printer to fit on the one page.
                printDlg.PrintVisual(dgArhiva, "First Fit to Page WPF Print");
            }
        }

        private void dgArhiva_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
