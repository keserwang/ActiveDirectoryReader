﻿using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Net;
using System.Security.Principal;

namespace LdapAndAdLibrary
{
    /// <summary>
    /// A LDAP client utility.
    /// </summary>
    public class LdapHelper : IDisposable
    {
        private LdapConnection _connection;


        /// <summary>
        /// Login to LDAP Server. 可以使用此方法驗證使用者的帳號和密碼是否正確。
        /// </summary>
        /// <param name="serverUrl"></param>
        /// <param name="adminAccount"></param>
        /// <param name="password"></param>
        /// <param name="domain"></param>
        public LdapHelper(string serverUrl, string adminAccount, string password, string domain = null)
        {
            _connection = new LdapConnection(serverUrl);
            _connection.Credential = new NetworkCredential(adminAccount, password, domain);
            _connection.Bind();
        }

        public ResultCode Delete(string distinguishedName)
        {
            try
            {
                var request = new DeleteRequest(distinguishedName);
                DeleteResponse response = (DeleteResponse)_connection.SendRequest(request);
                return response.ResultCode;
            }
            catch (DirectoryOperationException ex)
            {
                return ex.Response.ResultCode;
            }
        }


        /// <summary>
        /// Performs a search in the LDAP server. This method is generic in its return value to show the power
        /// of searches. A less generic search method could be implemented to only search for users, for instance.
        /// </summary>
        /// <param name="baseDn">The distinguished name of the base node at which to start the search</param>
        /// <param name="ldapFilter">An LDAP filter as defined by RFC4515</param>
        /// <returns>A flat list of dictionaries which in turn include attributes and the distinguished name (DN)</returns>
        public List<string> Search(string distinguishedName, string ldapFilter = null, SearchScope searchScope = SearchScope.Subtree, params string[] attributeList)
        {
            var request = new SearchRequest(distinguishedName, ldapFilter, searchScope, attributeList);
            var response = (SearchResponse)_connection.SendRequest(request);

            //var result = new List<Dictionary<string, string>>();
            var result = new List<string>();

            foreach (SearchResultEntry entry in response.Entries)
            {
                //var dic = new Dictionary<string, string>();
                //dic["DN"] = entry.DistinguishedName;

                List<string> attributeNameList = new List<string>(entry.Attributes.AttributeNames.Count);
                foreach (string name in entry.Attributes.AttributeNames)
                {
                    attributeNameList.Add(name);
                }
                attributeNameList = attributeNameList.Distinct().OrderBy(x => x).ToList();

                foreach (string attributeName in attributeNameList)
                {
                    for (int i = 0; i < entry.Attributes[attributeName].Count; i++)
                    {
                        //dic[attrName] = entry.Attributes[attrName][i].ToString();

                        object attributeValue = entry.Attributes[attributeName][i];
                        string valueString = ExtractAttributValue(attributeName, attributeValue);
                        result.Add($"{attributeName}={valueString}");

                        //string[] attributeValues = entry.Attributes[attrName][i].ToString().Split(',');
                        //for (int a = 0; a < attributeValues.Length; a++)
                        //{
                        //    string[] value = attributeValues[i].Split('=');
                        //    //if (value[1].Equals("Organizational-Unit"))
                        //    //{
                        //    //    //做事
                        //    //}
                        //}
                    }
                }

                result.Add(Environment.NewLine);
            }

            return result;
        }

        ///// <summary>
        ///// Adds a user to the LDAP server database. This method is intentionally less generic than the search one to
        ///// make it easier to add meaningful information to the database.
        ///// </summary>
        ///// <param name="user">The user to add</param>
        //public void addUser(UserModel user)
        //{
        //    var sha1 = new SHA1Managed();
        //    var digest = Convert.ToBase64String(sha1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(user.UserPassword)));

        //    var request = new AddRequest(user.DN, new DirectoryAttribute[] {
        //        new DirectoryAttribute("uid", user.UID),
        //        new DirectoryAttribute("ou", user.OU),
        //        new DirectoryAttribute("userPassword", "{SHA}" + digest),
        //        new DirectoryAttribute("objectClass", new string[] { "top", "account", "simpleSecurityObject" })
        //    });

        //    _connection.SendRequest(request);
        //}

        /// <summary>
        /// This method shows how to modify an attribute.
        /// </summary>
        /// <param name="oldUid">Old user UID</param>
        /// <param name="newUid">New user UID</param>
        public void changeUserUid(string oldUid, string newUid)
        {
            var oldDn = string.Format("uid={0},ou=users,dc=example,dc=com", oldUid);
            var newDn = string.Format("uid={0},ou=users,dc=example,dc=com", newUid);

            DirectoryRequest request = new ModifyDNRequest(oldDn, "ou=users,dc=example,dc=com", "uid=" + newUid);
            _connection.SendRequest(request);

            request = new ModifyRequest(newDn, DirectoryAttributeOperation.Replace, "uid", new string[] { newUid });
            _connection.SendRequest(request);
        }

        public ResultCode Compare(string distinguishedName, string attributeName, string attributeValue)
        {
            var request = new CompareRequest(distinguishedName, attributeName, attributeValue);
            CompareResponse response = (CompareResponse)_connection.SendRequest(request);
            return response.ResultCode;
        }

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

        #region Implement IDisposable.
        // Track whether Dispose has been called.
        private bool disposed = false;

        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be disposed.
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                // Free any managed objects here.
                _connection.Dispose();
            }

            // Free any unmanaged objects here.

            // Note disposing has been done.
            disposed = true;
        }

        // Use C# destructor syntax for finalization code.
        // This destructor will run only if the Dispose method
        // does not get called.
        // It gives your base class the opportunity to finalize.
        // Do not provide destructors in types derived from this class.
        ~LdapHelper()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }
        #endregion
    }
}