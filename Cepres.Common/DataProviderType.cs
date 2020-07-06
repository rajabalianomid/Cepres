using System.Runtime.Serialization;

namespace Cepres.Common
{
    public enum DataProviderType
    {
        /// <summary>
        /// Unknown
        /// </summary>
        [EnumMember(Value = "")]
        Unknown,

        /// <summary>
        /// MS SQL Server
        /// </summary>
        [EnumMember(Value = "sqlserver")]
        SqlServer
    }
}
