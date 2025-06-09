namespace AppNet.Models;

[Table("pacientes")]
public class Paciente2 : BaseModels
{

    [MaxLength(30)]
    public string Nombre { get; set; } = "";

    [MaxLength(30)]
    public string Apellido { get; set; } = "";

    public override string ToString()=>
    $"Id:{Id} - {Nombre} {Apellido}";
}


public abstract class BaseModels
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

}