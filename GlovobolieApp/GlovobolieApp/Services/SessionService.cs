using GlovobolieApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlovobolieApp.Services
{
    public class SessionService
    {
        private static SessionService instance;
        private static readonly object padlock = new object();

        public UserSessionData data { get; private set; }

        private SessionService() { }

        public void UpdateSession(int id, string username)
        {
            if (username == null) return;
            data = new UserSessionData(id, username, new List<Product>());
        }

        public void DisposeSession()
        {
            if (data != null)
            {
                data = null;
            }
        }

        public static SessionService Instance
        { 
            get 
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new SessionService();
                    }
                    return instance;
                }
            }
        }
    }
}
