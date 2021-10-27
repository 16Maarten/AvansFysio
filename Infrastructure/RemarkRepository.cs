using Domain;
using DomainServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class RemarkRepository : IRemarkRepository
    {
        private DbFysioContext _context { get; set; }

        public RemarkRepository(DbFysioContext dbFysioContext)
        {
            _context = dbFysioContext;
        }
        public async Task AddRemark(Remark remark)
        {
            _context.Add(remark);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Remark> GetAllRemarks()
        {
            return _context.Remarks.ToList();
        }

        public Remark GetWhereIdRemark(int id)
        {
            return _context.Remarks.Include(b => b.Student).Include(b => b.Physiotherapist).FirstOrDefault(entity => entity.Id == id);
        }

        public async Task RemoveRemark(Remark remark)
        {
            _context.Remove(remark);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRemark(Remark remark)
        {
            _context.Update(remark);
            await _context.SaveChangesAsync();
        }
    }
}
