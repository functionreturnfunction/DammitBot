using System;

namespace DammitBot
{
    public interface IBot : IDisposable
    {
        #region Abstract Methods

        void Run();
        void SayInChannel(string message);
        void Die();

        #endregion
    }
}