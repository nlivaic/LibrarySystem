using LibrarySystem.Core.Entities;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Interfaces
{
    public interface IScannerService
    {
        Task<User> ScanAsync(byte[] identityCardImage);
    }
}
