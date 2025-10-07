public class Program
{
    public static void Main(string[] args)
    {
        DocumentAbstract document = new Invoice(new PDFFormatter());
        document.Export();
    }

    #region Documentation
    //High Level Module
    public abstract class DocumentAbstract
    { 
        protected IFormatter _formatter;
        protected DocumentAbstract (IFormatter formatter)
        {
            _formatter = formatter;
        }

        public abstract void Export();
        
    }

    // High Level Module Concrete
    public class Invoice : DocumentAbstract
    {
        public Invoice(IFormatter formatter): base(formatter)
        {
        }

        public override void Export()
        {
            _formatter.format();
        }
    }
    public class Report : DocumentAbstract
    {
        public Report(IFormatter formatter) : base(formatter)
        {
        }

        public override void Export()
        {
            _formatter.format();
        }
    }
    public class Resume : DocumentAbstract
    {
        public Resume(IFormatter formatter) : base(formatter)
        {
        }

        public override void Export()
        {
            _formatter.format();
        }
    }

    //Low Level Module Abstract
    public interface IFormatter
    {
        public void format();
    }

    //Low Level Module Concrete
    public class PDFFormatter : IFormatter
    {
        public void format()
        {
            Console.WriteLine("Formatting PDF Document");
        }
    }
    public class WordFormatter : IFormatter
    {
        public void format()
        {
            Console.WriteLine("Formatting Word Document");
        }
    }
    public class ExcelFormatter : IFormatter
    {
        public void format()
        {
            Console.WriteLine("Formatting Excel Document");
        }
    }
    #endregion
}