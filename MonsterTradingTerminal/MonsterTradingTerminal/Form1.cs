using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trady;
using Trady.Analysis.Indicator;
using Trady.Core.Infrastructure;
using Trady.Importer.Csv;
using Trady.Importer.Yahoo;
using Trady.Analysis.Backtest;
using Trady.Analysis;
using Trady.Analysis.Extension;
using Rule = Trady.Analysis.Rule;
using System.IO;
using Trady.Analysis.Infrastructure;

namespace MonsterTradingTerminal
{
    // Implement your pattern by creating a static class for extending IndexedCandle class
    public static class IndexedCandleExtension
    {
        public static bool IsOpenAboveSma(this IIndexedOhlcv ic, int periodCount = 50)
           => ic.Get<SimpleMovingAverage>(periodCount)[ic.Index].Tick.IsTrue(t => ic.Open > t);

        public static bool IsOpenBelowSma(this IIndexedOhlcv ic, int periodCount = 50)
            => ic.Get<SimpleMovingAverage>(periodCount)[ic.Index].Tick.IsTrue(t => ic.Open < t);

        public static bool IsSma10BullishCrossSma50(this IndexedCandle ic)
            => ic.Get<SimpleMovingAverage>(10, 50).ComputeNeighbour(ic.Index).IsTrue(
                (prev, current, next) => prev.Tick.IsNegative() && current.Tick.IsPositive());

        public static bool IsSma10BearishCrossSma50(this IndexedCandle ic)
            => ic.Get<SimpleMovingAverage>(10, 50).ComputeNeighbour(ic.Index).IsTrue(
                (prev, current, next) => prev.Tick.IsPositive() && current.Tick.IsNegative());

        public static bool IsSma10BullishCrossSma200(this IndexedCandle ic)
            => ic.Get<SimpleMovingAverage>(10, 200).ComputeNeighbour(ic.Index).IsTrue(
                (prev, current, next) => prev.Tick.IsPositive() && current.Tick.IsNegative());

        public static bool IsSma10BearishCrossSma200(this IndexedCandle ic)
            => ic.Get<SimpleMovingAverage>(10, 200).ComputeNeighbour(ic.Index).IsTrue(
                (prev, current, next) => prev.Tick.IsPositive() && current.Tick.IsNegative());

        public static bool IsSma50BullishCrossSma200(this IndexedCandle ic)
            => ic.Get<SimpleMovingAverage>(50, 200).ComputeNeighbour(ic.Index).IsTrue(
                (prev, current, next) => prev.Tick.IsPositive() && current.Tick.IsNegative());

        public static bool IsSma50BearishCrossSma200(this IndexedCandle ic)
            => ic.Get<SimpleMovingAverage>(50, 200).ComputeNeighbour(ic.Index).IsTrue(
                (prev, current, next) => prev.Tick.IsPositive() && current.Tick.IsNegative());

        public static bool IsRsiOverbought(this IIndexedOhlcv ic, int periodCount = 14)
          => ic.Get<RelativeStrengthIndex>(periodCount)[ic.Index].Tick.IsTrue(t => t >= 60);

        public static bool IsRsiOversold(this IIndexedOhlcv ic, int periodCount = 14)
            => ic.Get<RelativeStrengthIndex>(periodCount)[ic.Index].Tick.IsTrue(t => t <= 40);
    }

    public partial class frmMain : Form
    {
        private const string logPath = "backtest.txt";

        private Predicate<IIndexedOhlcv> buyRule = null;
        private Predicate<IIndexedOhlcv> sellRule = null;

        public frmMain()
        {
            InitializeComponent();
        }

        public async Task<IEnumerable<IOhlcv>> ImportIOhlcvDatasAsync(string script)
        {
            var csvImporter = new CsvImporter(@"Data/" + script + ".csv", CultureInfo.GetCultureInfo("en-US"));
            return await csvImporter.ImportAsync(script);
        }
        private void stockProStrategy()
        {        
            //buyRule = Rule.Create(c => IndexedCandleExtension.IsSma10BullishCrossSma50()).And(c => c.IsSma10BullishCrossSma50());
            //sellRule = Rule.Create(c => c.IsBelowSma(50));
        }

        private void sma50Strategy()
        {
            buyRule = Rule.Create(c => c.IsAboveSma(50));
            sellRule = Rule.Create(c => c.IsBelowSma(50));
        }

        private async Task<Result> ExecuteTrade(string script)
        {
            // Import your candles
            var fullIOhlcvDatas = await ImportIOhlcvDatasAsync(script);
            var candles = fullIOhlcvDatas.Where(c => c.DateTime > new DateTime(2017, 1, 1)).ToList();

            buyRule = null;
            sellRule = null;
            // Build buy rule & sell rule based on various patterns
            switch (lbStrategy.SelectedItem.ToString())
            {
                case ("SMA50 Strategy"):
                    {
                        sma50Strategy();
                        break;
                    }
                case ("Stock Pro Strategy"):
                    {
                        stockProStrategy();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }


            if (buyRule == null || sellRule == null)
            {
                return null;
            }

            // Create portfolio instance by using PortfolioBuilder
            var runner = new Builder()
            .Add(candles)
            .Buy(buyRule)
            .Sell(sellRule)
            .Build();

            // Start backtesting with the portfolio
            runner.OnBought += Backtest_OnBought;
            runner.OnSold += Backtest_Onsold;
            return await runner.RunAsync(10000);
        }

        private async void BtnBacktest_Click(object sender, EventArgs e)
        {
            Result result = null;
            var script = "";

            var scripts = this.lbScripts.SelectedItems;

            dgvResults.Rows.Clear();
            for (var i = 0; i < scripts.Count; i++)
            {
                script = scripts[i].ToString();
                result = await ExecuteTrade(script);

                if (result != null)
                {
                    // Get backtest result for the portfolio
                    dgvResults.Rows.Add();
                    dgvResults.Rows[i].Cells[0].Value = script;
                    dgvResults.Rows[i].Cells[1].Value = result.Transactions.Count().ToString();
                    dgvResults.Rows[i].Cells[6].Value = result.TotalCorrectedProfitLoss;
                }
                else
                {
                    MessageBox.Show("Fatal Error occured");
                    dgvResults.Rows.Clear();
                }

            }
        }

        private void Backtest_Onsold(IEnumerable<IOhlcv> candles, int index, DateTimeOffset dateTime, decimal sellPrice, decimal quantity, decimal absCashFlow, decimal currentCashAmount, decimal plRatio)
        {
            File.AppendAllLines(logPath, new[] { $"{index}({dateTime:yyyyMMdd}), Sell {candles.GetHashCode()}@{sellPrice} * {quantity}: {absCashFlow}, plRatio: {plRatio * 100:0.##}%, currentCashAmount: {currentCashAmount}" });
        }

        private void Backtest_OnBought(IEnumerable<IOhlcv> candles, int index, DateTimeOffset dateTime, decimal buyPrice, decimal quantity, decimal absCashFlow, decimal currentCashAmount)
        {
            File.AppendAllLines(logPath, new[] { $"{index}({dateTime:yyyyMMdd}), Buy {candles.GetHashCode()}@{buyPrice} * {quantity}: {absCashFlow}, currentCashAmount: {currentCashAmount}" });
        }
    }
}
