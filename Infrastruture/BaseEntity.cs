using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruture
{
    public abstract class BaseEntity<TID>:IEntity
    {
        public virtual TID ID { get; set; }

        //public string GetID()
        //{
        //    return string.Empty;
        //}
    }
}
