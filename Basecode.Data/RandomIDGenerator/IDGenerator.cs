using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.RandomIDGenerator
{
    public class IDGenerator
    {
        public string? GenerateRandomApplicantId()
        {
            const string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(Enumerable.Repeat(allowedChars, 15)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return result;
        }
    }
}
