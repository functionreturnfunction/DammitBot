using System;

namespace DammitBot
{
    public interface IBot : IDisposable
    {
        #region Abstract Properties

        bool Running { get; }

        #endregion

        #region Abstract Methods

        void Run();
        void SayToAll(string message);
        void Die();

        #endregion
    }
}