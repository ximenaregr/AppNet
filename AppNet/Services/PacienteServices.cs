using AppNet.Interfaces;
using AppNet.Models;

namespace AppNet.Services;

public class PacienteServices : IPacientes
{
    private readonly SQLLiteHelper<Paciente2> db;

    public PacienteServices()
        => db = new();

    public Task<int> DeletePaciente(Paciente2 paciente)
        => Task.FromResult(db.Delete(paciente));

    public Task<List<Paciente2>> GetAll()
        => Task.FromResult(db.GetAllData());

    public Task<Paciente2> GetById(int id)
        => Task.FromResult(db.Get(id));

    public Task<int> InsertPaciente(Paciente2 paciente)
        => Task.FromResult(db.Add(paciente));

    public Task<int> UpdatePaciente(Paciente2 paciente)
        => Task.FromResult(db.Update(paciente));
}