namespace DammitBot.Abstract
{
    public interface IPluginThingy
    {
        /// <summary>
        /// If set to true, the plugin will be initialized immediately rather than waiting for all other
        /// plugins to be gathered and instantiated first.  This is useful for things like migration
        /// runners which need to create state (set things up) which will be utilized by other plugins.
        /// </summary>
        bool Priority => false;
        
        void Initialize();
        void Cleanup();
    }
}