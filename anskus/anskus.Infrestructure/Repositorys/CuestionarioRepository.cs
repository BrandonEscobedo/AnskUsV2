using anskus.Domain.Cuestionarios;
using anskus.Domain.Models;
using anskus.Infrestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
namespace anskus.Infrestructure.Repositorys
{
    internal sealed class CuestionarioRepository : ICuestionarioRepository
    {
        private readonly IMongoCollection<Cuestionario> _cuestionarios;
        private readonly AnskusDbContext _context;
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
       
        public async Task<Cuestionario> GetbyId(Guid id, Guid IdUser)
        {
            var filter = Builders<Cuestionario>.Filter.Where(c => c.IdCuestionario== id && c.Iduser==IdUser);
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
            var cuest= await _cuestionarios.Find(filter).FirstOrDefaultAsync();

            if (cuestionario.Completo())
            {
                cuestionario.Estado = EstadoCuestionario.Guardado;
                cuestionario.SetEstado(new GuardadoState());
            }
            else
            {
                cuestionario.Estado = EstadoCuestionario.Borrador;
                cuestionario.SetEstado(new BorradorState());
            }
        
            return cuestionario;
        }
    }
}
