using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet.Models.Kkk
{
  [Table("Department", Schema = "dbo")]
  public partial class Department
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DeptoId
    {
      get;
      set;
    }

    public ICollection<Department> Departments1 { get; set; }
    public Department Department1 { get; set; }
    [ConcurrencyCheck]
    public string Description
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string Supervisor
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string Extension1
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string Extension2
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string Extension3
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string Extension4
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string QtdStaff
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public int? StaffId
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public DateTime? LastUpdate
    {
      get;
      set;
    }
  }
}
