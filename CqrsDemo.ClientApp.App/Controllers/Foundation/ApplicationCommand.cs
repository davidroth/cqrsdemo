using System.Windows.Input;

namespace CqrsDemo.ClientApp.App.Controllers
{
    public abstract class ApplicationCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private bool canExecute = false;

        protected List<Action> Behaviours { get; } = [];

        public object? Tag { get; set; }

        public void AddBehavior(Action action)
        {
            Behaviours.Add(action);
        }

        public bool CanExecute(object? parameter)
        {
            if (!raisingEvent)
            {
                CanExecuteCore(parameter).ContinueWith(x =>
                {
                    if (canExecute != x.Result)
                    {
                        canExecute = x.Result;
                        ProtectedRaiseCanExecuteChanged();
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            return canExecute;
        }


        public abstract Task<bool> CanExecuteCore(object? parameter);

        public void Execute(object? parameter)
        {
            foreach (var behaviour in Behaviours)
            {
                behaviour();
            }
            ExecuteCore(parameter);
        }

        public abstract Task ExecuteCore(object? parameter);


        bool raisingEvent;
        private void ProtectedRaiseCanExecuteChanged()
        {
            raisingEvent = true;
            RaiseCanExecuteChanged();
            raisingEvent = false;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}