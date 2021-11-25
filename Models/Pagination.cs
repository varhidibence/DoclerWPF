using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
  public class Pagination
  {
    public int total { get; set; }
    public int count { get; set; }
    public int perPage { get; set; }
    public int currentPage { get; set; }
    public int totalPages { get; set; }
  }
}
