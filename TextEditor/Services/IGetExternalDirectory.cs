using System.Collections.Generic;
using System.IO;

namespace TextEditor.Services
{
    public interface IGetExternalDirectory
    {
        string GetCreateDirPath();
        string GetEditedDirPath();
        List<string> GetFiles(string path);
    }
}
