using NHibernate;

namespace DammitBot.Data.Library
{
    public interface ISessionFactoryBuilder
    {
        ISessionFactory Build();
    }
}