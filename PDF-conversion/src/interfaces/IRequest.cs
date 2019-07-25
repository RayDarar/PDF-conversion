using System.IO;

namespace PDF_conversion.src.interfaces
{
    public interface IRequest
    {
        void SetConversionSource(IConversionSource source);
        void SetTemplate(ITemplate template);
        void SetFiles(FileInfo[] files);
        bool Convert();
    }
}
