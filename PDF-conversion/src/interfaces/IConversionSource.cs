using System.IO;

namespace PDF_conversion.src.interfaces
{
    /// <summary>
    /// Source for conversion from pdf to txt
    /// </summary>
    public interface IConversionSource
    {
        string FromPdfToTxt(FileInfo file);
    }
}
