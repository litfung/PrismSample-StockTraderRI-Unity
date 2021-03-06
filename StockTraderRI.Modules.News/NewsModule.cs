﻿using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using StockTraderRI.Infrastructure;
using StockTraderRI.Infrastructure.Interfaces;
using StockTraderRI.Modules.News.Article;
using StockTraderRI.Modules.News.Controllers;
using StockTraderRI.Modules.News.Services;
using Unity;

namespace StockTraderRI.Modules.News
{
    public class NewsModule : IModule
    {
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;

        public NewsModule(IUnityContainer container, IRegionManager regionManager)
        {
            this.container = container;
            this.regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {

            this.regionManager.RegisterViewWithRegion(RegionNames.ResearchRegion,
                                                       () => this.container.Resolve<ArticleView>());

            this.regionManager.RegisterViewWithRegion(RegionNames.SecondaryRegion,
                                                       () => this.container.Resolve<NewsReaderView>());
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            this.container.RegisterInstance(typeof(INewsFeedService),
                                            container.Resolve<NewsFeedService>());

            this.container.RegisterInstance(typeof(ArticleViewModel),
                                            container.Resolve<ArticleViewModel>());

            this.container.RegisterInstance(typeof(NewsReaderViewModel),
                                            container.Resolve<NewsReaderViewModel>());

            this.container.RegisterInstance(typeof(INewsController),
                                            container.Resolve<NewsController>());

        }
    }
}
