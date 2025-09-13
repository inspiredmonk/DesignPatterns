public class Program
{
    public static void Main()
    {
        //IStream stream = new BufferingStrem(new CompressionStrem(new EncryptionStrem(new FileStream())));
        //stream.Write("Hello World");

        //Console.WriteLine();
        //Console.WriteLine("******************");
        //IStream stream1 = new EncryptionStrem(new CompressionStrem(new BufferingStrem((new FileStream()))));
        //stream1.Read();
        //Console.ReadLine();

        IPaymentProcessor processor = new FraudCheckProcessor(
                                        new CurrencyConversionProcessor(
                                            new LoggingProcessor(
                                                new CreditCardProcessor())));
        processor.ProcessPayment(100);


        Console.ReadLine();
    }

    #region PaymentProcessorDecorator
    public interface IPaymentProcessor
    {
        void ProcessPayment(decimal amount);
    }

    public class CreditCardProcessor : IPaymentProcessor
    {
        public void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Processing credit card payment of {amount:C}");
        }
    }


    public class FraudCheckProcessor : IPaymentProcessor
    {
        private readonly IPaymentProcessor _processor;
        public FraudCheckProcessor(IPaymentProcessor paymentProcessor)
        {
            _processor = paymentProcessor;
        }
        public void ProcessPayment(decimal amount)
        {
            Console.WriteLine("Performing fraud check...");
            var isFraudulent = false;
            if (!isFraudulent)
            {
                _processor.ProcessPayment(amount);
            }
            else
            {
                throw new Exception("Payment flagged as fraudulent!");
            }
        }
    }

    public class CurrencyConversionProcessor : IPaymentProcessor
    {
        private readonly IPaymentProcessor _processor;
        public CurrencyConversionProcessor(IPaymentProcessor paymentProcessor)
        {
            _processor = paymentProcessor;
        }
        public void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Convert currency of {amount:C}");
            _processor.ProcessPayment(amount);
        }
    }
    public class LoggingProcessor : IPaymentProcessor
    {
        private readonly IPaymentProcessor _processor;
        public LoggingProcessor(IPaymentProcessor paymentProcessor)
        {
            _processor = paymentProcessor;
        }
        public void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Logging payment of {amount:C}");
            _processor.ProcessPayment(amount);

        }
    }


    #endregion

    #region StreamDecorator

    // The base component interface
    public interface IStream
    {
        void Read();
        void Write(string data);
    }

    // A concrete component
    public class FileStream : IStream
    {
        public void Read()
        {
            Console.WriteLine("Reading data from file.");
        }
        public void Write(string data)
        {
            Console.WriteLine($"Writing '{data}' to file.");
        }
    }

    public class CompressionStrem : IStream
    {
        private readonly IStream _stream;
        public CompressionStrem(IStream stream) 
        {
            _stream = stream;
        }
        public void Read()
        {
            Console.WriteLine($"Decompress data from a file.");
            _stream.Read();
        }
        public void Write(string data)
        {
            Console.WriteLine($"Compress data '{data}' in a file.");
            _stream.Write(data);
        }
    }

    public class EncryptionStrem : IStream
    {
        private readonly IStream _stream;
        public EncryptionStrem(IStream stream)
        {
            _stream = stream;
        }
        public void Read()
        {
            Console.WriteLine($"Decrypt data from a file.");
            _stream.Read();
        }
        public void Write(string data)
        {
            Console.WriteLine($"Encrypt data '{data}' in a file.");
            _stream.Write(data);
        }
    }

    public class BufferingStrem : IStream
    {
        private readonly IStream _stream;
        public BufferingStrem(IStream stream)
        {
            _stream = stream;
        }
        public void Read()
        {
            Console.WriteLine($"buffer data from a file.");
            _stream.Read();
        }
        public void Write(string data)
        {
            Console.WriteLine($"Buffer data '{data}' in a file.");
            _stream.Write(data);
        }
    }

    #endregion

    
}