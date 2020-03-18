using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeSlotMachine.Core.Entities
{
    /// <summary>
    /// Bestellung verwaltet das bestellte Produkt, die eingeworfenen Münzen und
    /// die Münzen die zurückgegeben werden.
    /// </summary>
    public class Order : EntityObject {
        /// <summary>
        /// Datum und Uhrzeit der Bestellung
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Werte der eingeworfenen Münzen als Text. Die einzelnen 
        /// Münzwerte sind durch ; getrennt (z.B. "10;20;10;50")
        /// </summary>
        public String ThrownInCoinValues { get; set; }

        /// <summary>
        /// Zurückgegebene Münzwerte mit ; getrennt
        /// </summary>
        public String ReturnCoinValues { get; set; }

        /// <summary>
        /// Summe der eingeworfenen Cents.
        /// </summary>
        public int ThrownInCents { get; set; }

        /// <summary>
        /// Summe der Cents die zurückgegeben werden
        /// </summary>
        public int ReturnCents { get; set; }


        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        /// <summary>
        /// Kann der Automat mangels Kleingeld nicht
        /// mehr herausgeben, wird der Rest als Spende verbucht
        /// </summary>
        public int DonationCents { get; set; }

        /// <summary>
        /// Münze wird eingenommen.
        /// </summary>
        /// <param name="coinValue"></param>
        /// <returns>isFinished ist true, wenn der Produktpreis zumindest erreicht wurde</returns>
        public bool InsertCoin(int coinValue)
        {
            int productPrice = Product.PriceInCents;
            ThrownInCents += coinValue;
            ReturnCents = ThrownInCents - productPrice;
            int sum = ReturnCents;
            while(sum > 0)
            {
                if (sum - 200 >= 0)
                {
                    sum -= 200;
                    ReturnCoinValues += "200;";
                }
                else if (sum - 100 >= 0)
                {
                    sum -= 100;
                    ReturnCoinValues += "100;";
                }
                else if (sum - 50 >= 0)
                {
                    sum -= 50;
                    ReturnCoinValues += "50;";
                }
                else if (sum - 20 >= 0)
                {
                    sum -= 20;
                    ReturnCoinValues += "20;";
                }
                else if (sum - 10 >= 0)
                {
                    sum -= 10;
                    ReturnCoinValues += "10;";
                }
                else if (sum - 5 >= 0)
                {
                    sum -= 5;
                    ReturnCoinValues += "5;";
                }
                else if (sum - 2 >= 0)
                {
                    sum -= 2;
                    ReturnCoinValues += "2;";
                }
                else if (sum - 1 >= 0)
                {
                    sum -= 1;
                    ReturnCoinValues += "1;";
                }
            }
            ReturnCoinValues = ReturnCoinValues.Remove(ReturnCoinValues.Length - 1, 1);
            
            return coinValue >= Product.PriceInCents;
        }

        /// <summary>
        /// Übernahme des Einwurfs in das Münzdepot.
        /// Rückgabe des Retourgeldes aus der Kasse. Staffelung des Retourgeldes
        /// hängt vom Inhalt der Kasse ab.
        /// </summary>
        /// <param name="coins">Aktueller Zustand des Münzdepots</param>
        public void FinishPayment(IEnumerable<Coin> coins)
        {
            throw new NotImplementedException();
        }
    }
}
