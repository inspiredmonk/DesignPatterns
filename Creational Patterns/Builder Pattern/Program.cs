using static BuilderPattern.Program;

namespace BuilderPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            //ReportBuilder builder = new ReportBuilder("Detailed");
            //var report = builder.WithTitle("Test")
            //                    .WithDate(DateTime.Now)
            //                    .WithCondition(true)
            //                    .WithCharts(true)
            //                    .Build();

            // report.CreateReport();
            // Console.WriteLine("Title = {0}, Date = {1}, Author = {2}, Export = {3}", report.title, report.date, report.author, report.includeExportOptions);

            var pizza = new PizzaBuilder()
                            .WithBase("Pan")
                            .WithSause("Tomato")
                            .WithCheese("Mozzarella")
                            .WithToppings(["Pepperoni", "Mushrooms"])
                            .WithExtraCheese(true)
                            .WithStuffedCrust(false)
                            .Build();
        }

        #region ReportBuilder
        public interface IReport
        {
            string title { get; set; }
            DateTime date { get; set; }
            string author { get; set; }
            bool includeExportOptions { get; set; }
            void CreateReport();
        }

        public interface IDetailedReport: IReport
        {
            bool isIncludeCharts { get; set; }
            bool isIncludeTables { get; set; }
        }

        public class SummaryReport: IReport
        {
            public string title { get; set; }
            public DateTime date { get; set; }
            public string author { get; set; }
            public bool includeExportOptions { get; set; }
            public void CreateReport()
            {
                Console.WriteLine("Summary report created.");
            }
        }
        public class DetailedReport : IDetailedReport
        {
            public string title { get; set; }
            public DateTime date { get; set; }
            public string author { get; set; }
            public bool includeExportOptions { get; set; }
            public bool isIncludeCharts { get; set; }
            public bool isIncludeTables { get; set; }
            public void CreateReport()
            {
                if (isIncludeCharts)
                {
                    Console.WriteLine("Detail report created with charts in it.");
                }
                if (isIncludeTables)
                {
                    Console.WriteLine("Detail report created with tables in it.");
                }
                if (!isIncludeCharts && isIncludeTables)
                {
                    Console.WriteLine("Detail report created");
                }
            }
        }

        public class CustomReport : IReport
        {
            public string title { get; set; }
            public DateTime date { get; set; }
            public string author { get; set; }
            public bool includeExportOptions { get; set; }
            public void CreateReport()
            {
                Console.WriteLine("Custom report created.");
            }
        }

        public class ReportFactory
        {
            public static IReport GetReport(string reportType)
            {
                switch (reportType)
                {
                    case "Summary":
                        return new SummaryReport();
                    case "Detailed":
                        return new DetailedReport();
                    case "Custom":
                        return new CustomReport();
                    default:
                        throw new NotImplementedException("Report type doesn't exist");
                }
            }
        }

        public interface IReportBuilder
        {
            IReportBuilder WithTitle(string title);
            IReportBuilder WithDate(DateTime date);
            IReportBuilder WithAuthor(string author);
            IReportBuilder WithCondition(bool includeExportOption);
            IReportBuilder WithCharts(bool includeCharts);
            IReportBuilder WithTables(bool includeTables);
            IReport Build();
        }

        //Builder
        public class ReportBuilder: IReportBuilder
        {
            IReport report;
            public ReportBuilder(string reportType) {
                report = ReportFactory.GetReport(reportType);
            }

            public IReportBuilder WithTitle(string title)
            {
                if (string.IsNullOrEmpty(title))
                {
                    throw new NotImplementedException("Title is required");
                }
                report.title = title;
                return this;
            }

            public IReportBuilder WithDate(DateTime date)
            {
                if (date > DateTime.Now)
                {
                    throw new NotImplementedException("Generated date shouldn't be greater than current date");
                }
                report.date = date;
                return this;
            }

            public IReportBuilder WithAuthor(string authorName)
            {
                report.author = authorName;
                return this;
            }

            public IReportBuilder WithCondition(bool includeExportOption)
            {
                report.includeExportOptions = includeExportOption;
                return this;
            }
            // These only apply to DetailedReport
            public IReportBuilder WithCharts(bool includeCharts)
            {
                if (report is IDetailedReport dr)
                    dr.isIncludeCharts = includeCharts;
                else
                    throw new InvalidOperationException("Charts are only available for Detailed Reports");

                return this;
            }

            public IReportBuilder WithTables(bool includeTables)
            {
                if (report is IDetailedReport dr)
                    dr.isIncludeTables = includeTables;
                else
                    throw new InvalidOperationException("Tables are only available for Detailed Reports");

                return this;
            }


            public IReport Build() {
                return report;
            }
        }

        #endregion

        #region PizzaBuilder
        //product
        public class Pizza
        {
            public Pizza() { }
            public string pizzaBase { get; set; }
            public string sauceType { get; set; }
            public string cheeseType { get; set; }
            public string[] toppings { get; set; }
            public bool isExtraCheese { get; set; }
            public bool isStuffedCrust { get; set; }
        }

        //Builder Abstract
        public interface IPizzaBuilder
        {
            IPizzaBuilder WithBase(string baseTye);
            IPizzaBuilder WithSause(string sauceTye);
            IPizzaBuilder WithCheese(string cheeseTye);
            IPizzaBuilder WithToppings(string[] toppings);
            IPizzaBuilder WithExtraCheese(bool isExtraCheese);
            IPizzaBuilder WithStuffedCrust(bool isStuffedCrust);
            Pizza Build();
        }

        public class PizzaBuilder: IPizzaBuilder
        {
            Pizza pizza =new Pizza();

            public IPizzaBuilder WithBase(string baseTye)
            {
                if (string.IsNullOrEmpty(baseTye))
                {
                    throw new ArgumentNullException("Base is required");
                }
                pizza.pizzaBase = baseTye;
                return this;
            }
            public IPizzaBuilder WithSause(string sauceTye)
            {
                pizza.sauceType = sauceTye;
                return this;
            }
            public IPizzaBuilder WithCheese(string cheeseTye)
            {
                pizza.cheeseType = cheeseTye;
                return this;
            }
            public IPizzaBuilder WithToppings(string[] toppings)
            {
                pizza.toppings = toppings;
                return this;
            }
            public IPizzaBuilder WithExtraCheese(bool isExtraCheese)
            {
                pizza.isExtraCheese = isExtraCheese;
                return this;
            }
            public IPizzaBuilder WithStuffedCrust(bool isStuffedCrust)
            {
                if (isStuffedCrust)
                {
                    if (!string.IsNullOrEmpty(pizza.pizzaBase) && pizza.pizzaBase == "Thin")
                    {
                        pizza.isStuffedCrust = isStuffedCrust;
                    }
                    else
                    {
                        throw new ArgumentNullException("StuffedCrust is not applicable in the given base type.");
                    }
                }

                return this;
            }
            public Pizza Build()
            {
                return pizza;
            }
        }

        #endregion
    }
}