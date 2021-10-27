using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices
{
    public interface IRemarkRepository
    {
        Task AddRemark(Remark remark);
        Task RemoveRemark(Remark remark);
        Task UpdateRemark(Remark remark);
        public Remark GetWhereIdRemark(int id);
        public IEnumerable<Remark> GetAllRemarks();
    }
}
