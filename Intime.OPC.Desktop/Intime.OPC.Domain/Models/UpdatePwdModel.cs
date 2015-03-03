using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPCAPP.Domain.Models
{
  public  class UpdatePwdModel
    {

      public int Id { get; set; }

      public string LogonName { get; set; }
      public string NewPassword { get; set; }
      public string RePassword { get; set; }

    }
}
