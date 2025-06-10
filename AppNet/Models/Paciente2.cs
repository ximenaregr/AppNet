using SQLite;
using System.ComponentModel.DataAnnotations;

namespace AppNet.Models;

[Table("pacientes")]
public class Paciente2 : BaseModels
{
    [SQLite.MaxLength(50)]
    [Required(ErrorMessage = "El nombre es requerido")]
    public string Nombre { get; set; } = string.Empty;

    [SQLite.MaxLength(50)]
    [Required(ErrorMessage = "El apellido es requerido")]
    public string Apellido { get; set; } = string.Empty;

    [Range(1, 120, ErrorMessage = "La edad debe estar entre 1 y 120 años")]
    public int Edad { get; set; }

    [Range(1, 500, ErrorMessage = "El peso debe estar entre 1 y 500 kg")]
    public double Peso { get; set; }

    [Range(50, 250, ErrorMessage = "La estatura debe estar entre 50 y 250 cm")]
    public double Estatura { get; set; }

    [Required(ErrorMessage = "El sexo es requerido")]
    [SQLite.MaxLength(1)]
    public string Sexo { get; set; } = string.Empty; // "M" o "F"

    [Range(1, 5, ErrorMessage = "El nivel de actividad debe estar entre 1 y 5")]
    public int NivelActividad { get; set; }

    // Propiedades calculadas (no se guardan en BD)
    [Ignore]
    public double IMC => Math.Round(Peso / Math.Pow(Estatura / 100, 2), 2);

    [Ignore]
    public string ClasificacionIMC
    {
        get
        {
            return IMC switch
            {
                < 18.5 => "Bajo peso",
                >= 18.5 and < 25 => "Peso normal",
                >= 25 and < 30 => "Pre-obesidad o Sobrepeso",
                >= 30 and < 35 => "Obesidad clase I",
                >= 35 and < 40 => "Obesidad clase II",
                >= 40 => "Obesidad clase III"
            };
        }
    }

    [Ignore]
    public double PorcentajeGrasa
    {
        get
        {
            int sexoMultiplier = Sexo.ToUpper() == "M" ? 1 : 0;
            return Math.Round(1.2 * IMC + 0.23 * Edad - 10.8 * sexoMultiplier - 5.4, 2);
        }
    }

    [Ignore]
    public string ClasificacionGrasa
    {
        get
        {
            if (Sexo.ToUpper() == "M")
            {
                return PorcentajeGrasa switch
                {
                    >= 2 and <= 5 => "Grasa esencial",
                    >= 6 and <= 13 => "Atletas",
                    >= 14 and <= 17 => "Fitness",
                    >= 18 and <= 24 => "Aceptable",
                    > 25 => "Obesidad",
                    _ => "Fuera de rango"
                };
            }
            else
            {
                return PorcentajeGrasa switch
                {
                    >= 10 and <= 13 => "Grasa esencial",
                    >= 14 and <= 20 => "Atletas",
                    >= 21 and <= 24 => "Fitness",
                    >= 25 and <= 31 => "Aceptable",
                    > 32 => "Obesidad",
                    _ => "Fuera de rango"
                };
            }
        }
    }

    [Ignore]
    public double PesoIdeal
    {
        get
        {
            if (Sexo.ToUpper() == "M")
            {
                return Math.Round(Estatura - 100 - ((Estatura - 150) / 4), 2);
            }
            else
            {
                return Math.Round(Estatura - 100 - ((Estatura - 150) / 2.5), 2);
            }
        }
    }

    [Ignore]
    public double BMR
    {
        get
        {
            if (Sexo.ToUpper() == "M")
            {
                return Math.Round((Estatura * 6.25) + (Peso * 9.99) - (Edad * 4.92) + 5, 2);
            }
            else
            {
                return Math.Round((Estatura * 6.25) + (Peso * 9.99) - (Edad * 4.92) - 161, 2);
            }
        }
    }

    [Ignore]
    public double TDEE
    {
        get
        {
            double multiplier = NivelActividad switch
            {
                1 => 1.2,   // Sedentario
                2 => 1.375, // Ligero (1-3 días)
                3 => 1.55,  // Moderado (3-5 días)
                4 => 1.725, // Activo (6-7 días)
                5 => 1.9,   // Muy activo (2 veces al día)
                _ => 1.2
            };
            return Math.Round(BMR * multiplier, 2);
        }
    }

    [Ignore]
    public string NombreCompleto => $"{Nombre} {Apellido}";

    [Ignore]
    public string DescripcionActividad
    {
        get
        {
            return NivelActividad switch
            {
                1 => "Sedentario (poco o nada de ejercicio)",
                2 => "Ligero (ejercicio ligero 1-3 días/semana)",
                3 => "Moderado (ejercicio moderado 3-5 días/semana)",
                4 => "Activo (ejercicio intenso 6-7 días/semana)",
                5 => "Muy activo (ejercicio muy intenso, trabajo físico)",
                _ => "No especificado"
            };
        }
    }

    public override string ToString() => $"Id:{Id} - {Nombre} {Apellido}";
}

public abstract class BaseModels
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
}