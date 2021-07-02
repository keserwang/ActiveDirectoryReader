using System;
using System.DirectoryServices;

namespace ActiveDirectoryLibrary
{
    public class ActiveDirectoryHelper
    {
        DirectoryEntry root;

        private readonly string _serverUrl;

        public ActiveDirectoryHelper(string serverUrl, string userAccount, string password)
        {
            _serverUrl = serverUrl;

            root = new DirectoryEntry(_serverUrl, userAccount, password);
        }

        public void Login(string userAccount, string password)
        {
            // 若之前有登入過，則先釋放資源。
            if (root != null)
            {
                root.Dispose();
            }

            root = new DirectoryEntry(_serverUrl, userAccount, password);
        }

        public SearchResult SearchFirstOne()
        {
            DirectorySearcher search = new DirectorySearcher(root);
            SearchResult result = search.FindOne();
            return result;
        }

        public SearchResultCollection SearchAll()
        {
            DirectorySearcher search = new DirectorySearcher(root);
            SearchResultCollection results = search.FindAll();
            return results;
        }

    }
}
