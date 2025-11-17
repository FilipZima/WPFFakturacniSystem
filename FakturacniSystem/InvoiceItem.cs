namespace FakturacniSystem
{
    public class InvoiceItem
    {
        public float FinalCost => cost * amount * (1 + taxRate);

        public readonly int amount;
        public readonly string key;
        public readonly float taxRate = 0.21f;
        public readonly float cost;
        public readonly Currency currency = Currency.CZK;

        public InvoiceItem(string key, int amount, float cost, float taxRate, Currency currency)
        {
            if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount), amount, "Amount must be larger than 0");
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));

            this.amount = amount;
            this.key = key;
            this.taxRate = taxRate;
            this.cost = cost;
            this.currency = currency;
        }

        //                                     Name    Amount           Cost per item               Tax (ex. 21%)                   Total cost
        public override string ToString() => $"{key} ({amount}x) | {FormatCost(cost, currency)} | {(taxRate * 100)}% | {FormatCost(FinalCost, currency)}";

        private static string FormatCost(float cost, Currency currency)
        {
            string costString = cost.ToString();

            int separatorIndex = costString.IndexOf(',');
            if (separatorIndex == -1) separatorIndex = costString.IndexOf('.');

            // No decimal places
            if (separatorIndex == -1) return $"{currency.currencyCode} {costString},-";

            return $"{currency.currencyCode} {costString[0..separatorIndex]}.{costString.Substring(separatorIndex + 1, Math.Min(2, costString.Length - separatorIndex - 1))},-";
        }
    }
}
