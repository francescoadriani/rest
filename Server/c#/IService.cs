using AudioLibraryServerRESTful.Discography;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AudioLibraryServerRESTful
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
             ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare,
             UriTemplate = "/genres/")]
        [System.ComponentModel.Description("Fornisce la lista dei generi musicali")]
        [return: MessageParameter(Name = "genres")]
        List<Genre> ReadGenres();
    }

}
