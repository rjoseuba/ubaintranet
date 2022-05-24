using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet.Models.Kkk
{
  [Table("Apps", Schema = "dbo")]
  public partial class App
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AppId
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string Name
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string Description
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string Ulr
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string ClassLogo
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string Category
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
