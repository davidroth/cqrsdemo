namespace CqrsDemo.ClientApp.App.Controllers
{
    public abstract class Controller<TViewModel> : Controller
        where TViewModel : class
    {
        protected Controller(IView view, ControllerContext context)
            : base(view, context)
        { }

        public new TViewModel ViewModel
        {
            get { return (TViewModel)View.DataContext; }
            set { View.DataContext = value; }
        }
    }

    public abstract class Controller : IController, IDisposable
    {
        private TaskCompletionSource<object?>? closeCompletionSource;
        private bool controllerClosed;

        public Controller(IView view, ControllerContext context)
        {
            View = view;
            Context = context;
        }

        public IView View { get; }

        public ControllerContext Context { get; }

        public AppController AppController => Context.AppController;

        public object ViewModel
        {
            get { return View.DataContext; }
            set { View.DataContext = value; }
        }

        public void Dismiss()
        {
            AppController.Dismiss(this);
            controllerClosed = true;
            closeCompletionSource?.SetResult(null);
        }

        public Task Dismissed
        {
            get
            {
                if (closeCompletionSource == null)
                {
                    closeCompletionSource = new TaskCompletionSource<object?>();
                    if (controllerClosed)
                    {
                        closeCompletionSource.SetResult(null);
                    }
                }
                return closeCompletionSource.Task;
            }
        }
        public virtual void Dispose()
        {
        }
    }
}