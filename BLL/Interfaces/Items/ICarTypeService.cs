using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ICarTypeService
    {
        Resp_Binary Add(CarType model);

        Resp_Binary Modify(CarType model);

        Resp_Binary Del(Base_SingleDel model);

        Resp_Query<CarType> Query(RMG_Query request);

        Resp_Index<CarType> Index(Req_Index request);
    }
}
