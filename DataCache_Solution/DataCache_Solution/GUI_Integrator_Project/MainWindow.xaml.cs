using Common_Project.Classes;
using FileControler_Project.Enums;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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
using UI_Project.Classes;

namespace GUI_Integrator_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UI ui = new UI();
        OpenFileDialog dialog = new OpenFileDialog();
        public MainWindow()
        {
            InitializeComponent();
            geoComboBox.ItemsSource = ui.GetGeographicEntities();
            if (ui.DBOnline)
            {
                statusLabel.Foreground = Brushes.Green;
                statusLabel.Content = "Database online";
            }
            else
            {
                statusLabel.Foreground = Brushes.Red;
                statusLabel.Content = "Database offline";
            }
        }

        private void btnShowAudit_Click(object sender, RoutedEventArgs e)
        {
            AuditWindow audit = new AuditWindow();
            audit.ShowDialog();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = sender as TextBox;
            txt.Text = "";
        }

        private void TextBox_GotFocus_1(object sender, RoutedEventArgs e)
        {
            TextBox txt = sender as TextBox;
            txt.Text = "";
        }

        private void browseButton_Click(object sender, RoutedEventArgs e)
        {
            if (dialog.ShowDialog().Value)
                fileLabel.Text = dialog.SafeFileName;
            else
            {
                fileLabel.Text = "Chosen file";
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EFileLoadStatus status = ui.InitFileLoad(dialog.FileName, ELoadDataType.Consumption);
                bool check = true;
                switch (status)
                {
                    case EFileLoadStatus.Success:
                        check = true;
                        break;
                    case EFileLoadStatus.Failed:
                        check = false;
                        break;
                }
                if (check == true)
                    MessageBox.Show("Load successful!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                {
                    MessageBox.Show("Load failed!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (ServerTooBusyException)
            {
                MessageBox.Show("Server is too busy, please wait...", "Exception", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Unknown error occured, please contact the administrator", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnReconnect_Click(object sender, RoutedEventArgs e)
        {
            bool provera = ui.DBReconnect();
            if (provera == true)
            {
                statusLabel.Foreground = Brushes.Green;
                statusLabel.Content = "Database online";
            }
            else
            {
                statusLabel.Foreground = Brushes.Red;
                statusLabel.Content = "Database offline";
            }
        }

        private void btnAddGeo_Click(object sender, RoutedEventArgs e)
        {
            
            GeoRecord g = new GeoRecord();
            if(name.Text.Trim().Equals("Name") || id.Text.Trim().Equals("ID") || name.Text.Trim().Equals(String.Empty) || id.Text.Trim().Equals(String.Empty))
            {
                MessageBox.Show("Fill out all the fields", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                g.GName = name.Text;
                g.GID = id.Text;
                ui.PostGeoEntitiy(g);
                MessageBox.Show("Successfully added a field!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
        }

        private void btnGeoSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (geoComboBox.SelectedItem == null || fromDate.SelectedDate.Value == DateTime.MinValue || toDate.SelectedDate.Value == DateTime.MinValue)
                {
                    return;
                }
                DSpanGeoReq geo = new DSpanGeoReq(geoComboBox.SelectedItem.ToString(), fromDate.SelectedDate.Value.ToString("yyyy-MM-dd-HH"),
                                                                                       toDate.SelectedDate.Value.ToString("yyyy-MM-dd-HH"));
                var retVal = ui.InitConsumptionRead(geo);

                if (retVal.Item1 == CacheControler_Project.Enums.EConcumptionReadStatus.DBReadSuccess)
                {
                    List<ConsumptionRecord> list = ui.InitConsumptionRead(geo).Item2;
                    dataGrid.ItemsSource = list;
                }
                if (retVal.Item1 == CacheControler_Project.Enums.EConcumptionReadStatus.DBReadFailed)
                {
                    MessageBox.Show("Database is not accessible!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch(ServerTooBusyException)
            {
                MessageBox.Show("Server is too busy, please wait...", "Exception", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch(Exception)
            {
                MessageBox.Show("Unknown error occured, please contact the administrator", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
