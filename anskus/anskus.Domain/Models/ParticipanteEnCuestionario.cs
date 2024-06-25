namespace anskus.Domain.Models
{
    public class ParticipanteEnCuestionario
    {
        public Guid IdPeC { get;set; }
        public string Nombre { get; set; } = "";
        public int? PuntosActuales { get; set; } = 0;
        public int? PuntosAnteriores { get; set; } = 0;
        public int codigo { get; set; }
        public int? CantidadPacertadas { get; set; } = 0;

    }
}
