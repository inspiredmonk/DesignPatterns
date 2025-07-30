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

            //NotificationFactory notificationFactory = new EmailNotificationFactory();
            //var emailNotification = notificationFactory.NotificationCreator();


            //PDfGenerator pDfGenerator = new PDfGenerator(emailNotification);
            //pDfGenerator.Generate();

            Restaurant restaurant = new Restaurant();
            restaurant.Order("AdrakChai", "Burger");
        }
    }

    #region Restaurant
    //Client Code
    public class Restaurant
    {
        public Restaurant()
        {
        }

        public void Order(string teaType, string snackType)
        {
            IChai chai = Simulator.ChaiSimulator(teaType);
            chai.Banao();
            ISnacks snack = Simulator.SnacksSimulator(snackType);
            snack.CreateSnacks();
        }
    }

    public class Simulator {
        public static IChai ChaiSimulator(string teaType)
        {
            return teaType switch
            {
                "AdrakChai" => new AdrakChaiFactory().ChaiCreattor(),
                "EliachiChai" => new EliachiChaiFactory().ChaiCreattor(),
                _ => throw new NotSupportedException($"Tea type '{teaType}' is not supported.")
            };
        }

        public static ISnacks SnacksSimulator(string snackType)
        {
            return snackType switch
            {
                "Burger" => new BurgerSnacksFactory().CreateSnacks(),
                "Pizza" => new PizzaSnacksFactory().CreateSnacks(),
                _ => throw new NotSupportedException($"snack type '{snackType}' is not supported.")
            };
        }
    }

    //Products
    public interface IChai
    {
        void Banao();
    }
    public interface ISnacks
    {
        void CreateSnacks();
    }

    //Product Concret
    public class AdrakChai : IChai
    {
        public void Banao() {
            Console.WriteLine("Adrak Chai ban gayi hai");
        }
    }

    public class EliachiChai : IChai
    {
        public void Banao()
        {
            Console.WriteLine("Eliachi Chai ban gayi hai");
        }
    }
    public class BurgerSnacks: ISnacks{
        public void CreateSnacks()
        {
            Console.WriteLine("Burger is created");
        }
    }
    public class PizzaSnacks : ISnacks
    {
        public void CreateSnacks()
        {
            Console.WriteLine("Pizza is created");
        }
    }


    //Creator
    public abstract class SnacksFactory
    {
        public abstract ISnacks CreateSnacks();
    }

    public abstract class ChaiFactory
    {
        public abstract IChai ChaiCreattor();
    }

    //Creator Concrete
    public class BurgerSnacksFactory : SnacksFactory
    {
        public override ISnacks CreateSnacks()
        {
            return new BurgerSnacks();
        }
    }
    public class PizzaSnacksFactory : SnacksFactory
    {
        public override ISnacks CreateSnacks()
        {
            return new PizzaSnacks();
        }
    }

    public class AdrakChaiFactory : ChaiFactory {
        public override IChai ChaiCreattor() { 
            return new AdrakChai();
        }
    }

    public class EliachiChaiFactory : ChaiFactory
    {
        public override IChai ChaiCreattor()
        {
            return new EliachiChai();
        }
    }


    #endregion

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