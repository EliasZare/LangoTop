using System;
using LangoTop.Interfaces;

namespace LangoTop.Infrastructure.Repository
{
    public class GenerateShortKey : IGenerateShortKey
    {
        public string Generate(int length = 4)
        {
            var key = Guid.NewGuid().ToString().Replace("-", "").Substring(0, length);
            return key;
        }
    }
}
