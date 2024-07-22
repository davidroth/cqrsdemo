namespace CqrsDemo.ClientApp.App.Controllers
{
    public interface IMainView : IView
    {
        void ShowMessageBox(string message);
    }
}