using anskus.Domain.Models.Authentication;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace anskus.Domain.Models
{
    public class CuestionarioActivo
    {
        [Key]
        public Guid Idcuestionario { get; set; }
        public int Codigo { get; set; }

        [NotMapped]
        public Cuestionario Cuestionario { get; set; }=new Cuestionario();
        public string IdUsuario { get; set; } = "";
    
        [InverseProperty("CuestionarioActivo")]
        public virtual ApplicationUser IdUsuarioNavigation { get; set; } = null!;
    }
}
