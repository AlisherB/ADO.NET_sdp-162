using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public static class CustomerIdGenerator
    {
        public static string GenerateUniqueId(string companyName, string contactName)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 2; i++)
            {
                sb.Append(companyName[i]);
            }
            for (int i = 0; i < 2; i++)
            {
                sb.Append(contactName[i]);
            }

            sb.Append(RandomGenerator.GetRandomInteger());
            return sb.ToString().ToUpper();
        }
    }
}
