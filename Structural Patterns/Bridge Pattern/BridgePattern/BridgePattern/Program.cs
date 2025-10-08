public class Program
{
    public static void Main(string[] args)
    {
        //DocumentAbstract document = new Invoice(new PDFFormatter());
        //document.Export();

        PaymentPlatformAbstraction payment = new WebPlatform(new CreditCardPayment(new USDProcessor()));
        payment.payAmount(100);
    }

    #region Documentation
    //High Level Module
    public abstract class DocumentAbstract
    {
        protected IFormatter _formatter;
        protected DocumentAbstract(IFormatter formatter)
        {
            _formatter = formatter;
        }

        public abstract void Export();

    }

    // High Level Module Concrete
    public class Invoice : DocumentAbstract
    {
        public Invoice(IFormatter formatter) : base(formatter)
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

    #region global payment processor
    //Platform High Level Module abstraction
    public abstract class PaymentPlatformAbstraction
    {
        protected PaymentAbstraction _payment;
        protected PaymentPlatformAbstraction(PaymentAbstraction payment)
        {
            _payment = payment;
        }

        public abstract void payAmount(decimal amount);
    }

    //Platform High Level Module concrete
    public class  WebPlatform: PaymentPlatformAbstraction
    {
        public WebPlatform(PaymentAbstraction payment) : base(payment)
        { }

        public override void payAmount(decimal amount)
        {
            Console.WriteLine("Web Platform");
            _payment.Pay(amount);
        }
    }
    public class MobilePlatform : PaymentPlatformAbstraction
    {
        public MobilePlatform(PaymentAbstraction payment) : base(payment)
        { }

        public override void payAmount(decimal amount)
        {
            Console.WriteLine("Mobile Platform");
            _payment.Pay(amount);
        }
    }



    //Payment High Level Module abstraction
    public abstract class PaymentAbstraction
    {
        protected IPaymentProcessor _paymentProcessor;

        protected PaymentAbstraction (IPaymentProcessor paymentProcessor)
        {
            _paymentProcessor = paymentProcessor;
        }

        public abstract void Pay(decimal amount);
    }

    //Payment High Level Module concrete
    public class CreditCardPayment: PaymentAbstraction
    {
        public CreditCardPayment(IPaymentProcessor paymentProcessor) : base(paymentProcessor)
        { }

        public override void Pay(decimal amount)
        {
           var convertedAmount = _paymentProcessor.ProcessPayment(amount);
            Console.WriteLine($"Processing Credit Card Payment of amount {convertedAmount}");
        }
    }
    public class PayPalPayment : PaymentAbstraction
    {
        public PayPalPayment(IPaymentProcessor paymentProcessor) : base(paymentProcessor)
        { }

        public override void Pay(decimal amount)
        {
            var convertedAmount = _paymentProcessor.ProcessPayment(amount);
            Console.WriteLine($"Processing PayPal Payment of amount {convertedAmount}");
        }
    }
    public class BankTransferPayment : PaymentAbstraction
    {
        public BankTransferPayment(IPaymentProcessor paymentProcessor) : base(paymentProcessor)
        { }

        public override void Pay(decimal amount)
        {
            var convertedAmount = _paymentProcessor.ProcessPayment(amount);
            Console.WriteLine($"Processing Bank Transfer Payment of amount {convertedAmount}");
        }
    }




    //Currency Low Level Module implementation interface
    public interface IPaymentProcessor
    {
        decimal ProcessPayment(decimal amount);
    }

    //Currency Low Level Module implementation concrete
    public class USDProcessor : IPaymentProcessor
    {
        public decimal ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Converting {amount} using USD Converter");
            return amount * 1;
        }
    }
    public class EURProcessor : IPaymentProcessor
    {
        public decimal ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Converting {amount} using EUR Converter");
            return amount * 2;
        }
    }
    public class INRProcessor : IPaymentProcessor
    {
        public decimal ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Converting {amount} using INR Converter");
            return amount * 3;
        }
    }
    #endregion
}