using anskus.Domain.Cuestionarios;
using anskus.Domain.Models;
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
        //ver tecnologias segundo plano, eliminar cuestionarios vacios 
        public CuestionarioRepository(IMongoDatabase database)
        {
            _cuestionarios = database.GetCollection<Cuestionario>("Cuestionario");
            var indexDefinition = Builders<Cuestionario>.IndexKeys.Ascending(x => x.IdCuestionario);
            _cuestionarios.Indexes.CreateOne(new CreateIndexModel<Cuestionario>(indexDefinition));
        }
        public async Task Add(Cuestionario cuestionario)
        {
            await _cuestionarios.InsertOneAsync(cuestionario);

        }
        public async Task<Cuestionario> GetbyId(Guid id)
        {
            var filter = Builders<Cuestionario>.Filter.Eq(c => c.IdCuestionario, id);
            return await _cuestionarios.Find(filter).FirstOrDefaultAsync();
        }
        public Task<List<Cuestionario>> GetbyUser(string email)
        {
            //Aplicar paginacion
            throw new NotImplementedException();
        }
        public async Task Update(Cuestionario cuestionario)
        {
           var filter = Builders<Cuestionario>
                .Filter
                .Eq(s=>s.IdCuestionario,cuestionario.IdCuestionario);
            await _cuestionarios.ReplaceOneAsync(filter, cuestionario);
        }
    }
}
