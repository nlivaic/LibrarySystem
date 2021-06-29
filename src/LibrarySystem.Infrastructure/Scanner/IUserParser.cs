using LibrarySystem.Core.Entities;

namespace LibrarySystem.Infrastructure.Scanner
{
    public interface IUserParser
    {
        User Parse(MrzIdResponse response);
    }
}
