using anskus.Domain.Models.Authentication;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace anskus.Domain.Models
{
    public class CuestionarioActivo
    {
       
        public Guid Idcuestionario { get; set; }
        
        public int Codigo { get; set; }

        [NotMapped]
        public Cuestionario Cuestionario { get; set; }=new Cuestionario();
        public Guid IdUsuario { get; set; } 
    
        [InverseProperty("CuestionarioActivo")]
        public virtual ApplicationUser IdUsuarioNavigation { get; set; } = null!;

       
        public virtual ICollection< SalaParticipante>SalaParticipantes { get; set; } =new List< SalaParticipante>();
    }
}
