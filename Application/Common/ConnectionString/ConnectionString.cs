using Application.Services.EncryptAndDecrypt;
using Microsoft.Extensions.Configuration;

namespace Application.Common.ConnectionString
{
    public class ConnectionString
    {
        private readonly IConfiguration _configuration;
        private readonly string _Connection;
        public ConnectionString(IConfiguration config)
        {
            _configuration = config;
            _Connection = config["ConnectionStrings:AplicationDBContext"];
        }
        public string? AplicationDBContext { get; set; }

        public string DecryptConnection()
        {
            var _encryptAndDecryptService = new EncryptAndDecryptService(_configuration);
            return this.AplicationDBContext = _encryptAndDecryptService.Decrypt(_Connection);
        }
    }
}
