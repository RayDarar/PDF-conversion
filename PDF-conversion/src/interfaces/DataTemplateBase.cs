using PDF_conversion.src.logic;

namespace PDF_conversion.src.interfaces
{
    public abstract class DataTemplateBase
    {
        public abstract DataFormatBase Parse(DataSource source);
        protected DataFormatBase formatBase;
        public abstract string GetTemplateName();
    }
}
