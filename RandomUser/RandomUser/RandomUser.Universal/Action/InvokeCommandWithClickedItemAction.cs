using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Xaml.Interactivity;

namespace RandomUser.Universal.Action
{
    public class InvokeCommandWithClickedItemAction : DependencyObject, IAction
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(InvokeCommandWithClickedItemAction), new PropertyMetadata(null));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public object Execute(object sender, object parameter)
        {
            if (Command != null && parameter is ItemClickEventArgs)
                Command.Execute(((ItemClickEventArgs)parameter).ClickedItem);

            return null;
        }
    }
}

