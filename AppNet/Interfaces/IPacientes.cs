using AppNet.Models;

namespace AppNet.Interfaces;

public interface IPacientes
{
    public Task<List<Paciente2>> GetAll();
    public Task<Paciente2> GetById(int id);
    public Task<int> InsertPaciente(Paciente2 A);
    public Task<int> DeletePaciente(Paciente2 A);
    public Task<int> UpdatePaciente(Paciente2 A);

}

