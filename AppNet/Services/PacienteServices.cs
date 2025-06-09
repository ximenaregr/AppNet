using AppNet.Interfaces;
using AppNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNet.Services;

public class PacientesServices : IPacientes
{
    private readonly SQLLiteHelper<Paciente2> db;
    public PacientesServices()
    => db = new();

    public Task<int> DeleteAlumno(Paciente2 A)
     => Task.FromResult(db.Delete(A));

    public Task<List<Paciente2>> GetAll()
     => Task.FromResult(db.GetAllData());

    public Task<Paciente2> GetById(int id)
    => Task.FromResult(db.Get(id));

    public Task<int> InsertAlumno(Paciente2 A)
   => Task.FromResult(db.Add(A));


    public Task<int> UpdateAlumno(Paciente2 A)
    => Task.FromResult(db.Update(A));

}
