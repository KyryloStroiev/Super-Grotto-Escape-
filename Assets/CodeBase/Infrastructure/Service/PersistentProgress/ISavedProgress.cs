#nullable enable
using CodeBase.Data;

namespace CodeBase.Infrastructure.Service.PersistentProgress
{
    public interface ISavedProgress : ISavedProgressReader
    {
        void UpdateProgress(PlayerProgress progress);
    }
}