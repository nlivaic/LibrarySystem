using LibrarySystem.Common.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace LibrarySystem.Infrastructure.Scanner
{
    public class Td1CheckDigitService : ICheckDigitService
    {
        public bool Validate(string rawMrz)
        {
			var rows = rawMrz.Split("\n");
			DocumentNumberValidate(rows[0]);
			DateOfBirthValidate(rows[1]);
			DateOfExpiryValidate(rows[1]);
			OverallCheckDigitValidate(rows[0], rows[1]);
			return true;
		}

		private bool DocumentNumberValidate(string rawMrzFirstRow)
		{
			var mrzFirstRow = rawMrzFirstRow.Split("<")[0][5..];
			var checkDigitIdNumber = GetCheckDigit(mrzFirstRow[..9]);
			if (GetNumericValue(mrzFirstRow[9]) != checkDigitIdNumber)
			{
				throw new BusinessException("Failed Identity Card Document Number validation.");
			}
			return true;
		}

		private bool DateOfBirthValidate(string rawMrzSecondRow)
		{
			var rawDob = rawMrzSecondRow[..6];
			var checkDigitDob = GetCheckDigit(rawDob);
			if (GetNumericValue(rawMrzSecondRow[6]) != checkDigitDob)
			{
				throw new BusinessException("Failed Identity Card Date Of Birth validation.");
			}
			return true;
		}

		private bool DateOfExpiryValidate(string rawMrzSecondRow)
		{
			var rawExpirationDate = rawMrzSecondRow[8..14];
			var checkDigitExpirationDate = GetCheckDigit(rawExpirationDate);
			if (GetNumericValue(rawMrzSecondRow[14]) != checkDigitExpirationDate)
			{
				throw new BusinessException("Failed Identity Card Date Of Expiry validation.");
			}
			return true;
		}

		private bool OverallCheckDigitValidate(params string[] rawMrz)
		{
			//var compositeCheckDigit = GetCheckDigit(rawMrz[0][5..].Concat(rawMrz[1][0..7].Concat(rawMrz[1][8..15].Concat(rawMrz[1][18..29]))));
			var compositeCheckDigit = GetCheckDigit(rawMrz[0][5..] + rawMrz[1][0..7] + rawMrz[1][8..15] + rawMrz[1][18..29]);
			var idCardCompositeCheckDigit = rawMrz[1][^1];
			if (GetNumericValue(idCardCompositeCheckDigit) != compositeCheckDigit)
			{
				throw new BusinessException("Failed Identity Card Overall Check Digit validation.");
			}
			return true;
		}

		//private int GetSum(IEnumerable<char> raw)
		//{
		//	var weight = new int[] { 7, 3, 1 };
		//	return raw
		//		.Select((r, index) => GetNumericValue(r) * weight[index % 3])
		//		.Sum();
		//}

        private int GetSum(string raw)
		{
			var weight = new int[] { 7, 3, 1 };
			return raw
				.Select((r, index) => GetNumericValue(r) * weight[index % 3])
				.Sum();
		}

		private int GetCheckDigit(string raw) => GetSum(raw) % 10;

		//private int GetCheckDigit(IEnumerable<char> raw) => GetSum(raw) % 10;

		private int GetNumericValue(char c)
		{
            int value;
            if (char.IsDigit(c))
			{
				value = (int)char.GetNumericValue(c);
			}
			else if (char.IsLetter(c))
			{
				value = (int)c - 55;
			}
			else
			{
				value = 0;
			}
			return value;
		}
	}
}
