using System;

namespace FakturacniSystem
{
    public class Currency
    {
        public static readonly Currency CZK = new("CZK");
        public static readonly Currency USD = new("USD");

        public readonly string currencyCode;

        public Currency(string currencyCode)
        {
            this.currencyCode = currencyCode;
        }
    }
}
