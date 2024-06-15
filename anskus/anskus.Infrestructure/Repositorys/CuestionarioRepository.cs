using anskus.Domain.Cuestionarios;
using anskus.Domain.Models;
using anskus.Infrestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Infrestructure.Repositorys
{
    public class CuestionarioRepository : ICuestionarioRepository
    {
        private readonly IMongoCollection<Cuestionario> _cuestionarios;
        private readonly AnskusDbContext _context;
        //ver tecnologias segundo plano, eliminar cuestionarios vacios 
        public CuestionarioRepository(IMongoDatabase database, AnskusDbContext context)
        {
            _cuestionarios = database.GetCollection<Cuestionario>("Cuestionario");
            var indexDefinition = Builders<Cuestionario>.IndexKeys.Ascending(x => x.IdCuestionario);
            _cuestionarios.Indexes.CreateOne(new CreateIndexModel<Cuestionario>(indexDefinition));
            _context = context;
        }
        public async Task<Cuestionario> Add(Cuestionario cuestionario, string email)
        {
            var user =await _context.Users.FirstOrDefaultAsync(x=>x.Email == email);   
            if(user == null)
            {
                throw new Exception("No se encontro el usuario");
            }
            cuestionario.Iduser = user.Id;
            cuestionario.IdCuestionario = Guid.NewGuid();
            await _cuestionarios.InsertOneAsync(cuestionario);
            return cuestionario;
        }
        public async Task<Cuestionario> GetbyId(Guid id)
        {
            var filter = Builders<Cuestionario>.Filter.Eq(c => c.IdCuestionario, id);
            return await _cuestionarios.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<List<Cuestionario>> GetbyUser(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if(user == null)
            {
                throw new Exception("No se encontro el usuario");
            }
            var filter = Builders<Cuestionario>.Filter.Eq(x => x.Iduser, user.Id);
                return await _cuestionarios.Find(filter).ToListAsync();
            //Aplicar paginacion
        }
        public async Task<Cuestionario> Update(Cuestionario cuestionario)
        {
           var filter = Builders<Cuestionario>
                .Filter
                .Eq(s=>s.IdCuestionario,cuestionario.IdCuestionario);
            await _cuestionarios.ReplaceOneAsync(filter, cuestionario);
            return cuestionario;
        }
    }
}
