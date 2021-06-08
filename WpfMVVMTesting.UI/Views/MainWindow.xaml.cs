using System.Windows;
using WpfMVVMTesting.UI.ViewModel;

namespace WpfMVVMTesting.UI.Views
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel mainViewModel;

        public MainWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;

            this.mainViewModel = mainViewModel;
            DataContext = this.mainViewModel;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.mainViewModel.Load();
        }
    }
}
