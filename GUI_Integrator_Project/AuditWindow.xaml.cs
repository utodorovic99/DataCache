using Common_Project.Classes;
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
using System.Windows.Shapes;
using UI_Project.Classes;

namespace GUI_Integrator_Project
{
    /// <summary>
    /// Interaction logic for AuditWindow.xaml
    /// </summary>
    public partial class AuditWindow : Window
    {
        UI ui = new UI();
        public AuditWindow()
        {
            InitializeComponent();
            dataGrid.ItemsSource = ui.GetAuditEntities();
        }
    }
}
