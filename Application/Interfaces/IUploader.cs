using System.Threading.Tasks;

namespace WebApi.Application.Interfaces
{
    public interface IUploader
    {
        Task<string> UploadFile(string source, string prefix, string filename);
    }
}
