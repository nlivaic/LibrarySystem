namespace LibrarySystem.Infrastructure.Scanner
{
    public interface ICheckDigitService
    {
        bool Validate(string rawMrz);
    }
}
