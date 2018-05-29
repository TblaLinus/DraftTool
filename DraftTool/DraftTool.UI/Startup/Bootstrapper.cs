using Autofac;
using DraftTool.DataAccess;
using DraftTool.UI.Service;
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
            builder.RegisterType<DBService>().As<IDBService>().SingleInstance();
            builder.RegisterType<GameEngine>().As<IGameEngine>().SingleInstance();
            builder.RegisterType<Repo>().As<IRepo>().SingleInstance();

            builder.RegisterType<DBInitiate>().AsSelf();
            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();

            builder.RegisterType<CardMenuVM>().As<ICardMenuVM>();
            builder.RegisterType<CardListVM>().As<ICardListVM>();
            builder.RegisterType<CardVM>().As<ICardVM>();
            builder.RegisterType<GameVM>().As<IGameVM>();
            builder.RegisterType<DraftMenuVM>().As<IDraftMenuVM>();
            builder.RegisterType<DraftVM>().As<IDraftVM>();
            builder.RegisterType<PlayerReadyVM>().As<IPlayerReadyVM>();
            builder.RegisterType<ResultVM>().As<IResultVM>();

            return builder.Build();
        }
    }
}
