using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using WpfMVVMTesting.DataAccess;
using WpfMVVMTesting.UI.DataProvider;
using WpfMVVMTesting.UI.ViewModel;
using WpfMVVMTesting.UI.ViewModelInterface;
using WpfMVVMTesting.UI.Views;

namespace WpfMVVMTesting.UI.Start
{
    public class BootStrapper
    {
        public IContainer BootStrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();

            builder.RegisterType<NavigationViewModel>()
                .As<INavigationViewModel>();

            builder.RegisterType<NavigationDataProvider>()
                .As<INavigationDataProvider>();

            builder.RegisterType<FileDataService>()
                .As<IDataService>();

            return builder.Build();
        }
    }
}
