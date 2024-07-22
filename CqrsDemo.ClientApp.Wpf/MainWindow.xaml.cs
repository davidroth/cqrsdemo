using System.Windows;
using CqrsDemo.ClientApp.App.Controllers;

namespace CqrsDemo.ClientApp.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainView
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void ShowMessageBox(string message)
        {
            throw new NotImplementedException();
            
        }
    }
}