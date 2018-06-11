using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ICarColorService
    {
        Resp_Binary Add(CarColor model);

        Resp_Binary Modify(CarColor model);

        Resp_Binary Del(Base_SingleDel model);

        Resp_Query<CarColor> Query(RMG_Query request);

        Resp_Index<CarColor> Index(Req_Index request);
    }
}
