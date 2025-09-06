using static AdapterPattern.Program;

namespace AdapterPattern {
    class Program {
        static void Main(string[] args) {
            //LoggerClient loggerClient = new LoggerClient(new NloggerAdaptee());
            //loggerClient.LogMessage("This is a  message.");
            //loggerClient.LogError("This is an  message.");

            //loggerClient = new LoggerClient(new SeriloggerAdapter());
            //loggerClient.LogMessage("This is a  message.");
            //loggerClient.LogError("This is an Nerror message.");

            PaymentClient payment = new PaymentClient(PaymentTypeFactory.GetPaymentService("paypal"));
            payment.PayAmount(100, "USD");

            payment = new PaymentClient(PaymentTypeFactory.GetPaymentService("stripe"));
            payment.PayAmount(100, "USD");
        }

        #region Logger Example

        //Target interface
        public interface ILogger {
            void Log(string message);
            void Error(string message);
        }
        //Adapter class
        public class SeriloggerAdapter : ILogger
        {
            public void Log(string message)
            {
                //NLog logging logic
                Console.WriteLine($"SeriLog: {message}");
            }
            public void Error(string message)
            {
                //NLog logging logic
                Console.WriteLine($"SeriError: {message}");
            }
        }
        //Adaptee class
        public class NloggerAdaptee : ILogger { 
            public void Log(string message) {
                //NLog logging logic
                Console.WriteLine($"NLog: {message}");
            }
            public void Error(string message)
            {
                //NLog logging logic
                Console.WriteLine($"NError: {message}");
            }
        }
        //Client class
        public class LoggerClient {
            private readonly ILogger _logger;
            public LoggerClient(ILogger logger) {
                _logger = logger;
            }

            public void LogMessage(string message) {
                _logger.Log(message);
            }

            public void LogError(string message)
            {
                _logger.Error(message);
            }
        }
        #endregion

        #region Payment Gateway Example

        public class PaymentTypeFactory {
            public static IPaymentService GetPaymentService (string paymentType)
            {
                switch (paymentType.ToLower()) {
                    case "paypal":
                        return new PayPalAdapter(new PayPalAdaptee());
                    case "stripe":
                        return new StripAdaptor(new StripeAdaptee());
                    default:
                        throw new NotSupportedException("Payment type not supported");
                }
            }
        }


        //Target interface
        public interface IPaymentService
        {
            bool Pay(decimal amount, string currency);
        }

        // client class
        public class PaymentClient {
             private readonly IPaymentService _paymentService;
            public PaymentClient(IPaymentService paymentService) { 
                _paymentService = paymentService;
            }

            public bool PayAmount(decimal amount, string currency) {
                return _paymentService.Pay(amount, currency);
            }
        }

        // Stripe Adaptee class
        public class StripeAdaptee
        {
           public bool ProcessPayment(decimal amount, string currency)
            {
                Console.WriteLine($"Processing payment of {amount} {currency} using Stripe.");
                return true;
            }
        }

        //Paypal Adaptee class
        public class PayPalAdaptee
        {
            public bool MakePayment(decimal amount, string currency)
            {
                Console.WriteLine($"Processing payment of {amount} {currency} using PayPal.");
                return true;
            }
        }

        //Paypal Adapter class
        public class PayPalAdapter: IPaymentService
        {
            private readonly PayPalAdaptee _payPalAdaptee;

            public PayPalAdapter(PayPalAdaptee payPalAdaptee)
            {
                _payPalAdaptee = payPalAdaptee;
            }

            public bool Pay(decimal amount, string currency)
            {
                return _payPalAdaptee.MakePayment(amount, currency);
            }
        }

        public class PayPalAdapter_Inheritance : PayPalAdaptee, IPaymentService
        {
            public PayPalAdapter_Inheritance()
            {
            }

            public bool Pay(decimal amount, string currency)
            {
                return base.MakePayment(amount, currency);
            }
        }

        // Stripe Adapter class
        public class  StripAdaptor: IPaymentService
        {
            private readonly StripeAdaptee _stripeAdaptee;
            public StripAdaptor(StripeAdaptee stripeAdaptee)
            {
                _stripeAdaptee = stripeAdaptee;
            }

            public bool Pay(decimal amount, string currency)
            {
                return _stripeAdaptee.ProcessPayment(amount, currency);
            }
        }

        #endregion
    }
}
