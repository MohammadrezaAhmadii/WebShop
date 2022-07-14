using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.User
{
    public class RoleType : BaseEntity
    {
        public string Title { get; set; }
        public string State { get; set; }
        public string Code { get; set; }
        //public ICollection<UserRoleType> UserRoleTypeList { get; set; }
    }
}
