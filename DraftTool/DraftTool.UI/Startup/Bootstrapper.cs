using Autofac;
using DraftTool.UI.ViewModel;
using DraftTool.UI.ViewModel.Interfaces;
using Prism.Events;

namespace DraftTool.UI.Startup
{
    class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();

            builder.RegisterType<DraftMenuVM>().As<IDraftMenuVM>();
            builder.RegisterType<DraftVM>().As<IDraftVM>();
            builder.RegisterType<CardListVM>().As<ICardListVM>();

            return builder.Build();
        }
    }
}
