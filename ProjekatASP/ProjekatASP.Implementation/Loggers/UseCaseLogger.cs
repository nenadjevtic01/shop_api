using ProjekatASP.Application.UseCases;
using ProjekatASP.Application.UseCases.DTO;
using ProjekatASP.DataAccess;
using ProjekatASP.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Implementation.Loggers
{
    public class UseCaseLogger : IUseCaseLogger
    {
        private ProjekatAspDbContext _context;

        public UseCaseLogger(ProjekatAspDbContext context)
        {
            _context = context;
        }

        public IEnumerable<UseCaseLog> GetLogs(UseCaseLogSearch search)
        {
            throw new NotImplementedException();
        }

        public void Log(UseCaseLog log)
        {
            var auditLog = new AuditLog
            {
                UserId= log.UserId,
                Name=log.User,
                UseCaseName=log.UseCaseName,
                ExecutionDate=log.ExecutionDateTime,
                Data=log.Data,
                IsAuthorized=log.IsAuthorized
            };

            _context.AuditLogs.Add(auditLog);
            _context.SaveChanges();
        }
    }
}
