using System.Data;
using System.Linq;
using DammitBot.Data.Library;
using DammitBot.Data.Models;
using Dapper;

namespace DammitBot.Data.Repositories
{
    public class ReminderRepository : RepositoryBase<Reminder>
    {
        public ReminderRepository(IDataCommandHelper helper) : base(helper) {}

        public override object Insert(Reminder entity)
        {
            entity.FromId = entity.From == null ? entity.FromId : entity.From.Id;
            entity.ToId = entity.To == null ? entity.ToId : entity.To.Id;
            return base.Insert(entity);
        }
    }
}