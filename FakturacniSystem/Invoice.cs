using Microsoft.EntityFrameworkCore;
using System;
using System.Text;

namespace FakturacniSystem
{
    [PrimaryKey(nameof(Id))]
    public class Invoice
    {
        public Invoice() 
        { 
        }
        public Invoice(string title, string sourceCIN, string destinationCIN, DateTime date, string sourceCompanyName, string destCompanyName)
        {
            this.title = title;
            this.sourceCIN = sourceCIN;
            this.destinationCIN = destinationCIN;
            this.date = date;
            this.sourceCompanyName = sourceCompanyName;
            this.destCompanyName = destCompanyName;
        }

        public int Id { get; set; }

        public string title { get; set; } = "Unknown Invoice";
        public string sourceCIN { get; set; } = "Invalid CIN";
        public string destinationCIN { get; set; } = "Invalid CIN";
        public DateTime date { get; set; } = DateTime.Now;
        public string sourceCompanyName { get; set; } = "Invalid Name";
        public string destCompanyName { get; set; } = "Invalid Name";

        public List<InvoiceItem> Items { get; set; } = new();

        public void AddItem(InvoiceItem item)
        {
            InvoiceItem? similarItem = Items.Find(i => i.key == item.key);

            if (similarItem != null)
            {
                if (similarItem.cost == item.cost
                    && similarItem.taxRate == item.taxRate
                    && similarItem.currency == item.currency)
                {
                    similarItem.amount += item.amount;
                    return;
                }
            }

            Items.Add(item);
        }

        public string ToPage()
        {
            StringBuilder page = new();

            float finCost = 0;
            foreach (var item in Items)
            {
                if (item.currency is Currency.USD) finCost += item.FinalCost * 22.0f;
                else finCost += item.FinalCost;
            }

            page.AppendLine(title);

            page.AppendLine();
            page.AppendLine(" --- -- - Informace - -- ---");
            page.AppendLine("Od: " + sourceCompanyName);
            page.AppendLine("IČO: " + sourceCIN);
            page.AppendLine();
            page.AppendLine("Komu: " + destCompanyName);
            page.AppendLine("IČO: " + destinationCIN);
            page.AppendLine();
            page.AppendLine("Datum: " + date.ToShortDateString());
            page.AppendLine();
            page.AppendLine(" --- -- - Položky - -- ---");
            page.AppendLine();
            page.AppendLine("[Název] ([množství]x) | [Cena za ks/h] | [Daň %] | [Cena + daň]");
            page.AppendLine();

            foreach (var item in Items)
            {
                page.AppendLine(item.ToString());
            }

            page.AppendLine();
            page.AppendLine($"Celková cena: CZK {finCost}");

            return page.ToString();
        }

        public override string ToString() => title;
    }
}
