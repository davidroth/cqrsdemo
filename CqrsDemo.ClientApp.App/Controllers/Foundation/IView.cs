namespace CqrsDemo.ClientApp.App.Controllers
{
    public interface IView
    {
        object DataContext { get; set; }

        void Show();

        void Close();

        bool? ShowDialog();
    }
}