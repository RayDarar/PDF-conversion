using PDF_conversion.src.logic;

namespace PDF_conversion.src.interfaces
{
    public abstract class DataParserBase
    {
        public DataParserBase()
        {

        }

        public abstract DataFormatBase Parse(DataSource source);
        protected DataFormatBase formatBase;
    }
}
