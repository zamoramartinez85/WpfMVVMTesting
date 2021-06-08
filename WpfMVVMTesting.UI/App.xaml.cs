using Autofac;
using System.Windows;
using WpfMVVMTesting.DataAccess;
using WpfMVVMTesting.UI.DataProvider;
using WpfMVVMTesting.UI.Start;
using WpfMVVMTesting.UI.ViewModel;
using WpfMVVMTesting.UI.Views;

namespace WpfMVVMTesting.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var bootStrapper = new BootStrapper();
            var container = bootStrapper.BootStrap();

            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }
    }
}
