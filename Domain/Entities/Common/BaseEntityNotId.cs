using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Common
{
    public class BaseEntityNotId
    {
        public DateTime InsertDateTime { get; set; } = DateTime.Now;
        public DateTime? UpdateDateTime { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime? RemoveDateTime { get; set; }
    }
}
