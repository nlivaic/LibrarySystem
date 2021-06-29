using LibrarySystem.Core.Entities;
using LibrarySystem.Infrastructure.Scanner;

namespace LibrarySystem.Infrastructure.UserParser
{
    public interface IUserParser
    {
        User Parse(MrzIdResponse response);
    }
}
