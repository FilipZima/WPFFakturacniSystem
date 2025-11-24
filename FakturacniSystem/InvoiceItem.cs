namespace FakturacniSystem
{
    public class InvoiceItem
    {
        public InvoiceItem(string key, int amount, float taxRate, float cost, Currency currency)
        {
            this.key = key;
            this.amount = amount;
            this.taxRate = taxRate;
            this.cost = cost;
            this.currency = currency;
        }

        public int Id { get; set; }   // Primární klíč

        public string key { get; set; } = string.Empty;

        public int amount { get; set; }

        public float taxRate { get; set; } = 0.21f;

        public float cost { get; set; }

        public Currency currency { get; set; } = Currency.CZK;

        // Vypočítaná vlastnost – EF ji neuloží, ale můžeš ji používat v kódu
        public float FinalCost => cost * amount * (1 + taxRate);

        public override string ToString() =>
            $"{key} ({amount}x) | {FormatCost(cost, currency)} | {(taxRate * 100)}% | {FormatCost(FinalCost, currency)}";

        private static string FormatCost(float cost, Currency currency)
        {
            string costString = cost.ToString();

            int separatorIndex = costString.IndexOf(',');
            if (separatorIndex == -1) separatorIndex = costString.IndexOf('.');

            if (separatorIndex == -1) return $"{currency} {costString},-";

            return $"{currency} {costString[0..separatorIndex]}.{costString.Substring(separatorIndex + 1, Math.Min(2, costString.Length - separatorIndex - 1))},-";
        }
    }

}
