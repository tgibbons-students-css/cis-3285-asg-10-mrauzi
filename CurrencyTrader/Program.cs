using System;
using System.Reflection;
using CurrencyTrader.AdoNet;

namespace CurrencyTrader
{
    class Program
    {
        static void Main(string[] args)
        {
            var tradeStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SingleResponsibilityPrinciple.trades.txt");
            String tradeUrl = "http://faculty.css.edu/tgibbons/trades4.txt";

            var logger = new ConsoleLogger();

            //var tradeDataProvider = new StreamTradeDataProvider(tradeStream);
            var tradeDataProvider = new UrlTradeDataProvider(tradeUrl);

            var tradeValidator = new SimpleTradeValidator(logger);
            var tradeMapper = new SimpleTradeMapper();
            var tradeParser = new SimpleTradeParser(tradeValidator, tradeMapper);

            var tradeStorage = new AsynchTradeStorage(logger);

            var tradeProcessor = new TradeProcessor(tradeDataProvider, tradeParser, tradeStorage);
            tradeProcessor.ProcessTrades();

            Console.ReadKey();
        }
    }
}
