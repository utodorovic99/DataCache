using Common_Project.Classes;
using FileControler_Project.Enums;
using Microsoft.Win32;
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
            ui.InitFileLoad(dialog.FileName, ELoadDataType.Consumption);
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
            g.GName = name.Text;
            g.GID = id.Text;
            ui.PostGeoEntitiy(g);
        }
    }
}
