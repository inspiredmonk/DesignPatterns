namespace FactoryPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            //LoggerFactory loggerFactory = new ConsoleLogger();
            //var logger = loggerFactory.CreateLogger();

            //// Actual client
            //ReportGenerator report = new ReportGenerator(logger);
            //report.Generate();

            NotificationFactory notificationFactory = new EmailNotificationFactory();
            var emailNotification = notificationFactory.NotificationCreator();


            PDfGenerator pDfGenerator = new PDfGenerator(emailNotification);
            pDfGenerator.Generate();
        }
    }

    #region Notification System

    //Product
    public interface INotification {
        void sendNotification(string message);
    }

    // Concrete Products
    public class EmailNotification : INotification {
        public void sendNotification(string message)
        {
            Console.WriteLine("[Email Notification]- {0}", message);
        }
    }

    public class SMSNotification : INotification
    {
        public void sendNotification(string message)
        {
            Console.WriteLine("[SMS Notification]- {0}", message);
        }
    }

    //Creator
    public abstract class NotificationFactory{
        public abstract INotification NotificationCreator();
    }

    // Concrete Creator
    public class EmailNotificationFactory : NotificationFactory { 
        public override INotification NotificationCreator()
        {
            return new EmailNotification();
        }
    }

    public class SMSNotificationFactory : NotificationFactory
    {
        public override INotification NotificationCreator()
        {
            return new SMSNotification();
        }
    }

    //ClientCode
    public class PDfGenerator {

        private readonly INotification _notification;
        public PDfGenerator(INotification notification) {
            _notification = notification;
        }

        public void Generate()
        {
            Console.WriteLine("PDF Generated");
            _notification.sendNotification("Notification sent.");
        }
    }


    #endregion

    #region Logging system
    // Product
    public interface ILogger
    {
        void Log(string message);
    }

    // Concrete Product
    public class Consoler : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }

    // Client code
    public class ReportGenerator
    {
        private readonly ILogger _logger;
        public ReportGenerator(ILogger logger) {
            _logger = logger;
        }

        public void Generate()
        {
            // Simulate report generation
            Console.WriteLine("Generating report...");
            _logger.Log("Report has been successfully generated.");
        }
    }

    //Creator
    public abstract class LoggerFactory
    {
        public abstract ILogger CreateLogger();
    }

    public class ConsoleLogger : LoggerFactory {
        public override ILogger CreateLogger()
        {
            return new Consoler();
        }
    }
    #endregion


}