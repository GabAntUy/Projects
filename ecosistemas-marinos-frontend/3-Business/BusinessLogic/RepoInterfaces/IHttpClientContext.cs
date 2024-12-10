using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.RepoInterfaces
{
    public interface IHttpClientContext
    {
        public HttpResponseMessage Get(string uri);
        public HttpResponseMessage Post<T>(string uri,T obj);
        public HttpResponseMessage Put<T>(string uri,T obj);
        public HttpResponseMessage Delete(string uri);
    }
}
