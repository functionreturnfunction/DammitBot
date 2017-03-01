using NHibernate;

namespace DammitBot.Data.NHibernate.Library
{
    public interface ISessionFactoryBuilder
    {
        ISessionFactory Build();
    }
}