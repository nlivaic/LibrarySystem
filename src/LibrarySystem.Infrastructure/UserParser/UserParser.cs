using LibrarySystem.Core.Entities;
using LibrarySystem.Infrastructure.Scanner;
using System;

namespace LibrarySystem.Infrastructure.UserParser
{
    public class UserParser : IUserParser
    {
        public User Parse(MrzIdResponse response)
        {
            var rawUser = response.Result.MrzData.RawMrzString;
            var mrzMiddleRow = rawUser.Split("\n")[1];
            var rawDob = mrzMiddleRow[..6];
            var dob = new DateTime(
                Int32.Parse(rawDob[0..2]),
                Int32.Parse(rawDob[2..4]),
                Int32.Parse(rawDob[4..6]));
            var mrzLastRowTokenized = rawUser.Split("\n")[2].Split("<<");
            return new User(mrzLastRowTokenized[0], mrzLastRowTokenized[1], dob);
        }
    }
}
