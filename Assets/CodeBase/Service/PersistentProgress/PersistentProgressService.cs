using CodeBase.Data;

namespace CodeBase.Infrastructure.Service
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public PlayerProgress Progress { get; set; }
    }
}