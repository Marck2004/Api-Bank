using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobo.Application.Dtos
{
    public class TransacctionDto
    {
        public int id {  get; set; }
        public string CuentaOrg { get; set; }
        public string CuentaDst { get; set; }
        public decimal cant {  get; set; }
    }
}
