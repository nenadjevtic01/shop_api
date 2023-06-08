using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Domain
{
    public class UseCase : Entity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<UserUseCase> UserUseCases { get; set; }=new List<UserUseCase>();


    }
}
