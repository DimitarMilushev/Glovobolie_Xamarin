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

        public UserSessionData Data { get; private set; }

        private SessionService() { }

        public void UpdateSession(int id, string username, PersonalData personalData)
        {
            if (username == null) return;
            Data = new UserSessionData(id, username, new List<Product>(), personalData);
        }

        public void DisposeSession()
        {
            if (Data != null)
            {
                Data = null;
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
