using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet.Models.Kkk
{
  [Table("Staff", Schema = "dbo")]
  public partial class Staff
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int StaffId
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string UnitCode
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string Country
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string Surname
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string FirstName
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string MiddleName
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string Email
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public DateTime? DateBirth
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public DateTime? CurrentYear
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public int? Age
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string JobGrade
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string GradeClassification
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string JobRole
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string SalesNonSales
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string Department
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string LocationBranch
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string CountryWork
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string CountryOrigin
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string EmploymentDate
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public DateTime? DateLastPromotion
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string LengthStay
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string SolId
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string Mobile1
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string Mobile2
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string Extension
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public DateTime? StartDate
    {
      get;
      set;
    }
  }
}
