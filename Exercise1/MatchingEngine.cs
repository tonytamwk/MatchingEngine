using System;

namespace Exercise1
{
    internal class MatchingEngine
    {
        private Ticker[] askTicker;
        private Ticker[] bidTicker;

        public MatchingEngine(Ticker[] bidTicker, Ticker[] askTicker)
        {
            this.bidTicker = bidTicker;
            this.askTicker = askTicker;
        }

        internal OrderStatus enter(Order order)
        {
            int remainingOrderQuantity = order.Quantity;

            switch (order.Side)
            {
                case Side.Buy:
                    if (order.Price <= bidTicker[0].Price)
                        return OrderStatus.Queue;
                    
                    for (int i = 0; i < askTicker.Length; i++)
                    {
                        if (order.Price >= askTicker[i].Price)
                        {
                            if (askTicker[i].Quantity >= order.Quantity)
                            {
                                return OrderStatus.Filled;
                            }
                            else
                            {
                                remainingOrderQuantity = remainingOrderQuantity - askTicker[i].Quantity;
                            }
                        }
                    }

                    if (remainingOrderQuantity == 0)
                        return OrderStatus.Filled;
                    else
                        return OrderStatus.PartiallyFilled;

                case Side.Sell:
                    if (order.Price >= askTicker[0].Price)
                        return OrderStatus.Queue;

                    for (int i = 0; i < bidTicker.Length; i++)
                    {
                        if (order.Price <= bidTicker[i].Price)
                        {
                            if (bidTicker[i].Quantity >= order.Quantity)
                            {
                                return OrderStatus.Filled;
                            }
                            else
                            {
                                remainingOrderQuantity = remainingOrderQuantity - bidTicker[i].Quantity;
                            }
                        }
                    }

                    if (remainingOrderQuantity == 0)
                        return OrderStatus.Filled;
                    else
                        return OrderStatus.PartiallyFilled;
            }
            throw new NotImplementedException();
        }
    }
}