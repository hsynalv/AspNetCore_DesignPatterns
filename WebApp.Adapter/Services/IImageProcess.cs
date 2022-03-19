using System.IO;

namespace WebApp.Adapter.Services
{
    public interface IImageProcess
    {
        void AddWatermark(string text, string fileNAme, Stream imageStream);
    }
}
