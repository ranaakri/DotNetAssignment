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
    public class LogRepo : ILogRepo
    {
        private readonly AppDbContext _appDbContext;

        public LogRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

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
