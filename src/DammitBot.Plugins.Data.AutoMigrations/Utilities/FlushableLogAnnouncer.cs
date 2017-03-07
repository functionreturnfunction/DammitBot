using System;
using System.Text;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using log4net;

namespace DammitBot.Utilities
{
    public class FlushableLogAnnouncer : IFlushableLogAnnouncer
    {
        #region Private Members

        protected readonly ILog _log;
        protected StringBuilder _sb;
        protected readonly IAnnouncer _innerAnnouncer;

        #endregion

        #region Constructors

        public FlushableLogAnnouncer(ILog log)
        {
            _log = log;
            _sb = new StringBuilder();
            _innerAnnouncer = new TextWriterAnnouncer(s => _sb.Append(s));
        }

        #endregion

        #region Exposed Methods

        public void Heading(string message)
        {
            _innerAnnouncer.Heading(message);
        }

        public void Say(string message)
        {
            _innerAnnouncer.Say(message);
        }

        public void Emphasize(string message)
        {
            _innerAnnouncer.Emphasize(message);
        }

        public void Sql(string sql)
        {
            _innerAnnouncer.Sql(sql);
        }

        public void ElapsedTime(TimeSpan timeSpan)
        {
            _innerAnnouncer.ElapsedTime(timeSpan);
        }

        public void Error(string message)
        {
            _innerAnnouncer.Error(message);
        }

        public void Error(Exception exception)
        {
            _innerAnnouncer.Error(exception);
        }

        public void Write(string message, bool escaped)
        {
            _innerAnnouncer.Write(message, escaped);
        }

        public void Flush()
        {
            if (_sb.Length > 0)
            {
                _log.Info(Environment.NewLine + _sb);
            }
            _sb.Clear();
        }

        #endregion
    }
}