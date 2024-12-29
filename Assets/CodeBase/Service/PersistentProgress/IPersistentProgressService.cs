using CodeBase.Data;

namespace CodeBase.Infrastructure.Service
{
    public interface IPersistentProgressService
    {
        PlayerProgress Progress { get; set; }
    }
}