namespace PDF_conversion.src.interfaces
{
    public interface ITemplate
    {
        void ToExcel(string rawText);
        string GetTemplateName();
    }
}
