using System.Windows;

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

        public int InvoiceId { get; set; }      // foreign key

        public Invoice Invoice { get; set; } = null!; 

        public int Id { get; set; }

        public string key { get; set; } = string.Empty;

        public int amount { get; set; }

        public float taxRate { get; set; } = 0.21f;

        public float cost { get; set; }

        public Currency currency { get; set; } = Currency.CZK;

        public float FinalCost => cost * amount * (1 + taxRate);

        public override string ToString() =>
            $"{key} ({amount}x) | {FormatCost(cost, currency)} | {taxRate * 100}% | {FormatCost(FinalCost, currency)}";

        private static string FormatCost(float cost, Currency currency)
        {
            return $"{currency} {cost}";
        }
    }

}
