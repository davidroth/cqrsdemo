using System.Windows;
using CqrsDemo.ClientApp.App.Controllers;

namespace CqrsDemo
{
    /// <summary>
    /// Interaction logic for OrderForm.xaml
    /// </summary>
    public partial class OrderForm : Window, IOrderFormView
    {
        public OrderForm()
        {
            InitializeComponent();
        }
    }
}