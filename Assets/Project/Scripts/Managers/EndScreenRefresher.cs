using UnityEngine;

namespace CastL.Managers
{
    public class EndScreenRefresher : MonoBehaviour
    {
        private void OnEnable()
        {
            PlayerStatsManager.Instance?.LoadAndRefreshEndScreenStats();
        }
    }
}
