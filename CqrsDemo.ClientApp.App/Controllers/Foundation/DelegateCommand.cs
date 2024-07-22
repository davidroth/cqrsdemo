namespace CqrsDemo.ClientApp.App.Controllers
{
    public class DelegateCommand : ApplicationCommand
    {
        readonly Func<object?, Task> execute;
        readonly Func<object?, Task<bool>>? canExecute;

        public DelegateCommand(Func<object?, Task> execute, Func<object?, Task<bool>>? canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute ?? (x => Task.FromResult(true));
        }

        public DelegateCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            this.execute = x =>
            {
                execute(x);
                return Task.CompletedTask;
            };
            this.canExecute = x => canExecute == null ? Task.FromResult(true) : Task.FromResult(canExecute(x));
        }

        public DelegateCommand(Action<object?> execute, Func<object?, Task<bool>> canExecute)
        {
            this.execute = x =>
            {
                execute(x);
                return Task.CompletedTask;
            };
            this.canExecute = canExecute ?? (x => Task.FromResult(true));
        }

        public override Task ExecuteCore(object? parameter)
        {
            return execute(parameter);
        }
        
        public override Task<bool> CanExecuteCore(object? parameter)
        {
            return canExecute == null 
                ? throw new InvalidOperationException("canExecute handler not set") 
                : canExecute(parameter);
        }
    }
}