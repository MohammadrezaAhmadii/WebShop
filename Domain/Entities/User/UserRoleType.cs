using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.User
{
    public class UserRoleType : BaseEntity
    {
        public virtual User User { get; set; }
        public long UserId { get; set; }

        public virtual RoleType RoleType { get; set; }
        public long RoleTypeId { get; set; }
    }
}
