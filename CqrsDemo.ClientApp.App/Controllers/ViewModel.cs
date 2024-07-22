namespace CqrsDemo.ClientApp.App.Controllers
{
    public class ViewModel : ObjectBase
    {
        public void BindCommand(ApplicationCommand command, string property)
        {
            PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == property)
                {
                    command.RaiseCanExecuteChanged();
                }
            };
        }
    }
}