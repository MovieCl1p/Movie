
using System;

namespace Game.Services.Instance
{
    public interface IInstanceService
    {
        event Action OnFinishRound;

        void PlayerAnswer(string data);

        void StartGame();
    }
}
