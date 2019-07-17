using PDF_conversion.src.logic;

namespace PDF_conversion.src.interfaces
{
    public interface IDataParser
    {
        DataFormatBase Parse(DataSource source);
    }
}
