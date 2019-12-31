using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DermPOCMobile.Services
{
    public interface IPhotoDetector
    {
        Task<string> DetectAsync(Stream photo);
    }
}
