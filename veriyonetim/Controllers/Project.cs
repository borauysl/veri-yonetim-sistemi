using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MySql.Data.MySqlClient;

public class Project
{
    [Required]
    [DataType(DataType.Date)]
    public DateTime ProjeOneriTarihi { get; set; }

    [Required]
    public string ProjeAdi { get; set; }

    [Required]
    public string ProjeTuru { get; set; }

    [Required]
    public string ProjeDanismani { get; set; }

    [Required]
    public string ProjeYurutucusu { get; set; }

    [Required]
    public string ProjeEkibi { get; set; }

    [Required]
    public string ProjeKonusu { get; set; }

    [Required]
    public string ProjeAmaci { get; set; }

    [Required]
    public string ProjeHedefKitlesi { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Süre 1 günden fazla olmalıdır.")]
    public int ProjeSure { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Bütçe negatif olamaz.")]
    public int ProjeButcesi { get; set; }

    [Required]
    public string ProjeEtkilikleri { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime ProjeBaslangicTarihi { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime ProjeBitisTarihi { get; set; }

    [Required]
    public string ProjeKurumKuruluslar { get; set; }

    [Required]
    public string ProjeMateryaller { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (ProjeBitisTarihi < ProjeBaslangicTarihi)
        {
            yield return new ValidationResult("Proje bitiş tarihi, başlangıç tarihinden küçük olamaz.", new[] { "ProjeBitisTarihi" });
        }
    }
  }
