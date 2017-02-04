using System.Collections.Generic;
using TeamCitySharper.DomainEntities;

namespace TeamCitySharper.ActionTypes
{
    public interface IServerInformation
    {
        Server ServerInfo();
        List<Plugin> AllPlugins();
        string TriggerServerInstanceBackup(BackupOptions backupOptions);
        string GetBackupStatus();
    }
}