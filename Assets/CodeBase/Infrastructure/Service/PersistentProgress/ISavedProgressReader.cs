#nullable enable
using CodeBase.Data;

namespace CodeBase.Infrastructure.Service.PersistentProgress
{
    public interface ISavedProgressReader
    {
        void LoadProgress(PlayerProgress progress);
    }
}