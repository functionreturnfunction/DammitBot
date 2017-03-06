using DammitBot.Data.Models;
using FluentNHibernate.Mapping;

namespace DammitBot.Data.NHibernate.Mappings
{
    public class NickMap : ClassMap<Nick>
    {
        #region Constants

        public struct StringLengths
        {
            #region Constants

            public const int NICKNAME = 255;

            #endregion
        }

        #endregion

        #region Constructors

        public NickMap()
        {
            Id(x => x.Id);

            Map(x => x.Nickname).Not.Nullable().Length(StringLengths.NICKNAME);
            Map(x => x.CreatedAt).Not.Nullable();
            Map(x => x.UpdatedAt).Nullable();

            References(x => x.User);
        }

        #endregion
    }
}