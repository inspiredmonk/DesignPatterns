using Microsoft.VisualBasic;

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

            //Restaurant restaurant = new Restaurant();
            //restaurant.Order("AdrakChai", "Burger");

            FurnitureShop furnitureShop = new FurnitureShop();
            furnitureShop.PurchaseChairOnly("Victorian");
            furnitureShop.PurchaseCombo("Modern");
        }
    }

    #region AbstractFactory Example
    //Client Code
    public class FurnitureShop
    {
        private IFurnitureCombo _furnitureShop;
        public FurnitureShop()
        {
            
        }

        public void PurchaseChairOnly(string variantType)
        {
            _furnitureShop = FurnitureSimulator.Simulator(variantType);
            _furnitureShop.chairs().GetChair();
        }

        public void PurchaseSofaOnly(string variantType)
        {
            _furnitureShop = FurnitureSimulator.Simulator(variantType);
            _furnitureShop.sofas().GetSofa();
        }

        public void PurchaseCombo(string variantType)
        {
            _furnitureShop = FurnitureSimulator.Simulator(variantType);
            _furnitureShop.chairs().GetChair();
            _furnitureShop.sofas().GetSofa();
        }
    }

    public static class FurnitureSimulator
    {
        public static IFurnitureCombo Simulator(string variantType)
        {
            return variantType switch
            {
                "Modern" => new ModernFurniture(),
                "Victorian" => new VictorianFurniture(),
                "ArtDeco" => new ArtDecoFurniture(),
                _ => throw new NotSupportedException($"variantType type '{variantType}' is not supported.")
            };
        }
        //public static IChair ChairSimulator(string chairType)
        //{
        //    return chairType switch
        //    {
        //        "Modern" => new ModernChairFactory().CreateChair(),
        //        "Victorian" => new VictorianChairFactory().CreateChair(),
        //        "ArtDeco" => new ArtDecoChairFactory().CreateChair(),
        //        _ => throw new NotSupportedException($"chairType type '{chairType}' is not supported.")
        //    };
        //}
    }

    //Product
    public interface IChair
    {
        void GetChair();
    }
    public interface ISofa
    {
        void GetSofa();
    }
    public interface IFurnitureCombo
    {
        IChair chairs();
        ISofa sofas();
    }

    //Product Concret
    public class ModernFurniture: IFurnitureCombo
    {
        public IChair chairs()
        {
            return new ModernChair();
        }
        public ISofa sofas()
        {
            return new ModernSofa();
        }
    }
    public class VictorianFurniture : IFurnitureCombo
    {
        public IChair chairs()
        {
            return new VictorianChair();
        }
        public ISofa sofas()
        {
            return new VictorianSofa();
        }
    }
    public class ArtDecoFurniture : IFurnitureCombo
    {
        public IChair chairs()
        {
            return new ArtDecoChair();
        }
        public ISofa sofas()
        {
            return new ArtDecoSofa();
        }
    }

    public class ModernChair : IChair {
        public void GetChair() {
            Console.WriteLine("Modern chair is avaialble");
        }
    }
    public class VictorianChair : IChair
    {
        public void GetChair()
        {
            Console.WriteLine("Victorian chair is avaialble");
        }
    }
    public class ArtDecoChair : IChair
    {
        public void GetChair()
        {
            Console.WriteLine("ArtDeco chair is avaialble");
        }
    }
    public class ModernSofa : ISofa
    {
        public void GetSofa()
        {
            Console.WriteLine("Modern Sofa is avaialble");
        }
    }
    public class VictorianSofa : ISofa
    {
        public void GetSofa()
        {
            Console.WriteLine("Victorian Sofa is avaialble");
        }
    }
    public class ArtDecoSofa : ISofa
    {
        public void GetSofa()
        {
            Console.WriteLine("ArtDeco Sofa is avaialble");
        }
    }
    #endregion


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