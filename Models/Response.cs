using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
  public class Response
  {
    public bool Success { get; set; }

    public string Status { get; set; }

    public Data Data { get; set; }
  }
}
