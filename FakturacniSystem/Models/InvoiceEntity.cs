using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace FakturacniSystem.Models
{
    [PrimaryKey(nameof(Id))]
    public class InvoiceEntity : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public Invoice _invoice {  get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public InvoiceEntity(Invoice invoice)
        {
            _invoice = invoice;
        }

        public Invoice Invoice
        {
            get => _invoice;
            set
            {
                _invoice = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(value)));
            }
        }
    }
}
