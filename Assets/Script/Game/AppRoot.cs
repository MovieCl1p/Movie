using Game.Config;
using UnityEngine;

namespace Game
{
    public class AppRoot : MonoBehaviour
    {
        private void Awake()
        {
            AppConfig config = new AppConfig();
        }
        
    }
}
