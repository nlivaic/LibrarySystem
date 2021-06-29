using LibrarySystem.Core.Entities;
using System;

namespace LibrarySystem.Infrastructure.Scanner
{
    public class UserParser : IUserParser
    {
        public User Parse(MrzIdResponse response)
        {
            var rawUser = response.Result.MrzData.RawMrzString;
            var mrzMiddleRow = rawUser.Split("\n")[1];
            var rawDob = mrzMiddleRow[..6];
            var dob = new DateTime(
                1900 + int.Parse(rawDob[0..2]),
                int.Parse(rawDob[2..4]),
                int.Parse(rawDob[4..6]));
            var mrzLastRowTokenized = rawUser.Split("\n")[2].Split("<<");
            return new User(string.Join(" ", mrzLastRowTokenized[1].Split('<')), mrzLastRowTokenized[0], dob);
        }
    }
}
