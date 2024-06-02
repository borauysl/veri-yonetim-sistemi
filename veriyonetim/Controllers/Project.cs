using System;
using System.ComponentModel.DataAnnotations;
using MySql.Data.MySqlClient;

public class Project
{
    [Required]
    public string ProjeAdi { get; set; }

    [Required]
    public string ProjeYapimcisi { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime ProjeTarihi { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Süre 1 günden fazla olmalıdır.")]
    public int ProjeSure { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Bütçe negatif olamaz.")]
    public int ProjeButcesi { get; set; }

  
}
