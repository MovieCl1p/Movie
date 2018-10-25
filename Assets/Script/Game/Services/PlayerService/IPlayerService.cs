
namespace Game.Services.PlayerService
{
    public interface IPlayerService
    {
        void CreateUser();

        string UserName { get; }
    }
}
