using System.ComponentModel;

namespace CqrsDemo.ClientApp.App.Controllers
{
    public class ObjectBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Raises the property changed event for the given propertyName.
        /// </summary>
        /// <param name="propertyName">The property name used in the property changed event args.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Raises the property changed event with the given <see cref="PropertyChangedEventArgs"/>.
        /// </summary>
        /// <param name="args">The PropertyChangedEventArgs used to identify the change.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }
    }
}