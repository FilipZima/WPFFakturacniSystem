using Microsoft.EntityFrameworkCore;

namespace FakturacniSystem.Database
{
    public class ContextMgr
    {
        private readonly DataContext _context;

        public ContextMgr(DataContext context)
        {
            _context = context;
        }

        public void Add(Invoice entity)
        {
            _context.InvoiceSet.Add(entity);
            _context.SaveChanges();
        }

        public void Remove(Invoice entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Invoice entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }

        public Invoice? GetByTitle(string title)
        {
            foreach (Invoice e in _context.InvoiceSet)
            {
                if (e.title == title) return e;
            }
            return null;
        }

        public Invoice? GetByID(int id) => _context.InvoiceSet.Find(id);

        public List<Invoice> GetAll() => _context.InvoiceSet.Include(i => i.Items).ToList();
    }
}
