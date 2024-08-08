using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobo.Application.Dtos;
    public class AccountDto
    {
        public int Id {  get; set; }
        public string NumCuenta {  get; set; }
        public decimal Balance { get; set; }
        public Guid IdUsuario { get; set; }
    }
