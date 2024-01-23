using CodeBase.Data;

namespace CodeBase.Infrastructure.Service.SaveLoad
{
    public interface ISaveLoadService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}