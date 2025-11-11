using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakturacniSystem
{
    public class Invoice
    {
        private Dictionary<string, InvoiceItem> _items = new();

        public Invoice() 
        {
        }

        public void AddItem(InvoiceItem item)
        {
            // Key already found
            if (_items.ContainsKey(item.key))
            {
                InvoiceItem similarItem = _items[item.key];

                // Merge items
                if (similarItem.cost == item.cost
                    && similarItem.taxRate == item.taxRate
                    && similarItem.currency == item.currency)
                {
                    SetItem(
                        item.key,
                        MergeItems(similarItem, item)
                        );
                    return;
                }
            }

            SetItem(item.key, item);
        }

        private void SetItem(string key, InvoiceItem item)
        { 
            _items[item.key] = item;
        }

        private InvoiceItem MergeItems(InvoiceItem a, InvoiceItem b)
        {
            return new InvoiceItem(a.key, a.amount + b.amount, a.cost, a.taxRate, a.currency);
        }

        public override string ToString()
        {
            StringBuilder page = new();
            foreach (KeyValuePair<string, InvoiceItem> pair in _items)
            {
                page.AppendLine(pair.Value.ToString());
            }
            return page.ToString();
        }
    }
}
