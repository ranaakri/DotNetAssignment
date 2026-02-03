using InventoryManagementSystem.DataAccess.Context;
using InventoryManagementSystem.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSystem.DataAccess.Repository
{
    public interface ILogRepo
    {
        public Task<IEnumerable<LogTable>> GetLogs();
        public Task InsertLog(LogTable log);
       
    }
    internal class LogRepo : ILogRepo
    {
        private AppDbContext _appDbContext;
        public async Task<IEnumerable<LogTable>> GetLogs()
        {
            return await _appDbContext.LogsTable.ToListAsync();
        }

        public async Task InsertLog(LogTable log)
        {
            await _appDbContext.LogsTable.AddAsync(log);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
