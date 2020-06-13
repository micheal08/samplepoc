using leave_management.Contracts;
using leave_management.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveRequestRepository(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public bool Create(LeaveRequest entity)
        {
            _db.LeaveRequests.Add(entity);
            return Save();
        }

        public bool Delete(LeaveRequest entity)
        {
            _db.LeaveRequests.Remove(entity);
            return Save();
        }

        public ICollection<LeaveRequest> FindAll()
        {
            return _db.LeaveRequests
                .Include(x => x.RequestingEmployee)
                .Include(x => x.LeaveType)
                .Include(x => x.ApprovedBy)
                .ToList();
        }

        public LeaveRequest FindById(int id)
        {
            return _db.LeaveRequests
                .Include(x => x.RequestingEmployee)
                .Include(x => x.LeaveType)
                .Include(x => x.ApprovedBy)
                .FirstOrDefault(x => x.Id == id);
        }

        public ICollection<LeaveRequest> GetLeaveRequestByEmployee(string employeeid)
        {
            return FindAll()
                .Where(x => x.RequestingEmployeeId == employeeid)
                .ToList();
        }

        public bool isExists(int id)
        {
            return _db.LeaveRequests.Any(x => x.Id == id);
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }

        public bool Update(LeaveRequest entity)
        {
            _db.LeaveRequests.Update(entity);
            return Save();
        }
    }
}
