﻿using System.Data;
using DammitBot.Abstract;
using DammitBot.Data.Library;
using DammitBot.Data.NHibernate.Library;
using NHibernate;
using StructureMap;

// ReSharper disable once CheckNamespace
namespace DammitBot.Ioc
{
    public class NHibernatePluginContainerConfiguration : PluginContainerConfigurationBase
    {
        public override void Configure(ConfigurationExpression e)
        {
            e.For(typeof(ISessionFactoryBuilder)).Use(typeof(SessionFactoryBuilder));
            e.For(typeof(ISessionFactory)).Use(ctx => ctx.GetInstance<ISessionFactoryBuilder>().Build()).Singleton();
            e.For<IUnitOfWork>().Use<UnitOfWork>();
            e.For<IDataCommandHelper>().Use<DataCommandHelper>();
            e.For<IDbConnection>().Use(ctx => ctx.GetInstance<ISessionFactory>().OpenSession().Connection);
        }
    }
}
