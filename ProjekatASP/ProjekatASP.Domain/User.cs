using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Domain
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsBanned { get; set; }
        public int Role { get; set; }

        public virtual Info Info { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual ICollection<UserUseCase> UserUseCases { get; set; } = new List<UserUseCase>();
        public virtual ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();

        //public virtual ICollection<AuditLog> AuditLog { get; set; }=new List<AuditLog>();

    }
}
