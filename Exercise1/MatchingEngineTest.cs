using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise1.UnitTesting
{
    class MatchingEngineTest
    {
        private MatchingEngine matchingEngine;
        private Ticker[] bidTickers;
        private Ticker[] askTickers;

        [SetUp]
        public void Setup()
        {
            bidTickers = new Ticker[5];
            bidTickers[0] = new Ticker { Price = 100, Quantity = 100 };
            bidTickers[1] = new Ticker { Price = 99.9, Quantity = 300 };
            bidTickers[2] = new Ticker { Price = 99.8, Quantity = 300 };
            bidTickers[3] = new Ticker { Price = 99.7, Quantity = 400 };
            bidTickers[4] = new Ticker { Price = 99.6, Quantity = 500 };

            askTickers = new Ticker[5];
            askTickers[0] = new Ticker { Price = 100.1, Quantity = 500 };
            askTickers[1] = new Ticker { Price = 100.2, Quantity = 1000 };
            askTickers[2] = new Ticker { Price = 100.3, Quantity = 2000 };
            askTickers[3] = new Ticker { Price = 100.4, Quantity = 2500 };
            askTickers[4] = new Ticker { Price = 100.5, Quantity = 3000 };

            matchingEngine = new MatchingEngine(bidTickers, askTickers);
        }

        [Test]
        public void WhenBuyOrderWithPrice100_1And500QtyThenFilled()
        {
            Order order = new Order(Side.Buy, 100.1, 500);
            Assert.AreEqual(OrderStatus.Filled, matchingEngine.enter(order));
        }

        [Test]
        public void WhenBuyOrderWithPrice100And500QtyThenQueue()
        {
            Order order = new Order(Side.Buy, 100, 500);
            Assert.AreEqual(OrderStatus.Queue, matchingEngine.enter(order));
        }

        [Test]
        public void WhenBuyOrderWithPrice100_2And2000QtyThenPartiallyFilled()
        {
            Order order = new Order(Side.Buy, 100.2, 2000);
            Assert.AreEqual(OrderStatus.PartiallyFilled, matchingEngine.enter(order));
        }

        [Test]
        public void WhenSellOrderWithPrice99_9And200QtyThenFilled()
        {
            Order order = new Order(Side.Sell, 99.9, 200);
            Assert.AreEqual(OrderStatus.Filled, matchingEngine.enter(order));
        }

        [Test]
        public void WhenSellOrderWithPrice100_6And500QtyThenQueue()
        {
            Order order = new Order(Side.Sell, 100.6, 500);
            Assert.AreEqual(OrderStatus.Queue, matchingEngine.enter(order));
        }

        [Test]
        public void WhenSellOrderWithPrice99_9And500QtyThenPartiallyFilled()
        {
            Order order = new Order(Side.Sell, 99.9, 500);
            Assert.AreEqual(OrderStatus.PartiallyFilled, matchingEngine.enter(order));
        }
    }
}
