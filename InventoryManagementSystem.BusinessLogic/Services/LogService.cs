using InventoryManagementSystem.DataAccess.Repository;
using InventoryManagementSystem.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSystem.BusinessLogic.Services
{
    public interface ILogservice
    {
        public Task<IEnumerable<LogTable>> GetLogsAsync();
    }
    public class LogService : ILogservice
    {
        private readonly ILogRepo _logservice;

        public LogService(ILogRepo logservice)
        {
            _logservice = logservice;
        }

        public async Task<IEnumerable<LogTable>> GetLogsAsync()
        {
            return await _logservice.GetLogs();
        }
    }
}
