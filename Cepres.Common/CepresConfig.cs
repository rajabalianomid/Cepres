using Newtonsoft.Json;

namespace Cepres.Common
{
    public partial class CepresConfig
    {
        public string DataConnectionString { get; set; }
        public bool UserEF { get; set; }
    }
}
