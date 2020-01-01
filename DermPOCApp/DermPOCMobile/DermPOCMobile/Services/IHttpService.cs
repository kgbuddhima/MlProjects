using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DermPOCMobile.Services
{
    public interface IHttpService
    {
        Task<string> PredictImageAsync(byte[] image, string filepath);
    }
}
