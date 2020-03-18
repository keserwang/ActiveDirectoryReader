using System;
using System.Security.Principal;

namespace DirectoryLibrary
{
    public class DirectoryUtility
    {
        /// <summary>
        /// 特別解析 objectguid 和 objectsid 的值，轉為人類可閱讀的字串，其它皆轉為字串。
        /// </summary>
        /// <param name="name">Attribut Name</param>
        /// <param name="value">Attribut Value</param>
        /// <returns></returns>
        public static string ExtractAttributValue(string name, object value)
        {
            string valueString;
            if (name.Contains("objectguid") && value is byte[] && ((byte[])value).Length == 16)
            {
                Guid guid = new Guid(value as byte[]);
                valueString = guid.ToString();
            }
            else if (name == "objectsid" && value is byte[])
            {
                // A security identifier (SID) is used to uniquely identify a security principal or security group.
                // Security principals can represent any entity that can be authenticated by the operating system, such as a user account, a computer account, or a thread or process that runs in the security context of a user or computer account.
                // https://docs.microsoft.com/en-us/windows/security/identity-protection/access-control/security-identifiers
                // https://www.lijyyh.com/2015/08/sid-deep-dive.html
                SecurityIdentifier sid = new SecurityIdentifier((byte[])value, 0);
                valueString = sid.Value;
            }
            else
            {
                valueString = value.ToString();
            }

            return valueString;
        }
    }
}
