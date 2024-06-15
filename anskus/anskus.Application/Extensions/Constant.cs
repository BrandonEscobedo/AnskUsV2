using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Extensions
{
    public class Constant
    {
        public const string BrowserStorageKey = "x=key";
        public const string HttpClientName = "TestAnskusClient";
        public const string HttpClientHadersSchame = "Bearer";
        public const string CuestionarioRoute = "api/Cuestionarios";
        public const string CuestionarioActivoRoute = "api/CuestionarioActivo";
        public const string PreguntasRoute = "api/Preguntas";
        public const string RespuestasRoute = "api/Respuestas";
        public const string RegisterRoute = "api/identity/Register";
        public const string LoginRoute = "api/identity/Login";
        public const string RefreshTokenRoute = "api/identity/Refresh-token";
        public const string AuthenticationType = "JwtAuth";
    }
}
