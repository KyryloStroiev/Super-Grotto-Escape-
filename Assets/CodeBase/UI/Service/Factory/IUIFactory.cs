using System.Threading.Tasks;
using CodeBase.Infrastructure.State;

namespace CodeBase.UI.Service.Factory
{
    public interface IUIFactory
    {
        void CreateMenu();
        Task CreateUIRoot();
    }
}