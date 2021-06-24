using LibrarySystem.Common.Exceptions;
using System;

namespace LibrarySystem.Common.Guards
{
    public class Guards
    {
        public static void NonEmpty(string target, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(target))
            {
                throw new BusinessException($"{parameterName} cannot be empty.");
            }
        }

        public static void NonDefault<T>(T target, string parameterName) where T : class
        {
            if (target == default(T))
            {
                throw new BusinessException($"{parameterName} cannot be default value.");
            }
        }

        public static void NonDefault(Guid target, string parameterName)
        {
            if (target == default)
            {
                throw new BusinessException($"{parameterName} cannot be default value.");
            }
        }

        public static void NonDefault(DateTime target, string parameterName)
        {
            if (target == default)
            {
                throw new BusinessException($"{parameterName} cannot be default value.");
            }
        }
    }
}
