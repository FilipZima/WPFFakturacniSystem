using Microsoft.EntityFrameworkCore;
using System;
using System.Text;

namespace FakturacniSystem
{
    [PrimaryKey(nameof(Id))]
    public class Invoice
    {
        public int Id { get; set; }

        public string title { get; set; } = "Unknown Invoice";
        public string sourceCIN { get; set; } = "Invalid CIN";
        public string destinationCIN { get; set; } = "Invalid CIN";
        public DateTime date { get; set; }
        public string sourceCompanyName { get; set; } = "Invalid Name";
        public string destCompanyName { get; set; } = "Invalid Name";

        // Pokud chceš ukládat položky faktury, musí to být kolekce entit
        public List<InvoiceItem> Items { get; set; } = new();

        public void AddItem(InvoiceItem item)
        {
            // Najdi existující položku
            var similarItem = Items.Find(i => i.key == item.key);

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
            foreach (var item in Items)
            {
                page.AppendLine(item.ToString());
            }
            return page.ToString();
        }

        public override string ToString() => title;
    }
}
