using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobo.Application.Dtos;
    public class AccountDto
    {
        public int id {  get; set; }
        public string numCuenta {  get; set; }
        public decimal balance { get; set; }
        public Guid IdUsuario { get; set; }
    }
