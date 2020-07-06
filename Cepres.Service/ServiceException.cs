using System;

namespace FileManager.Service.API
{
    public class ServiceException : Exception
    {
        public ServiceException(string ex) : base(ex) { }
    }
}
