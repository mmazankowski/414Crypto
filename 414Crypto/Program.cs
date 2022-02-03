using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using EllipticCurve;  

namespace BoomCoin
{
    class Program
    {

        static void Main(string[] args)
        {
            PrivateKey key1 = new PrivateKey();
            PublicKey wallet1 = key1.publicKey();

            PrivateKey key2 = new PrivateKey();
            PublicKey wallet2 = key2.publicKey(); 

            Blockchain boomcoin = new Blockchain(2, 100);

            Console.WriteLine("Start the Miner!");
            boomcoin.MinePendingTransactions(wallet1);
            Console.WriteLine("\nBalance of wallet1 is $" + boomcoin.GetBalanceOfWallet(wallet1).ToString());

            Transaction tx1 = new Transaction(wallet1, wallet2, 10);
            tx1.SignTransaction(key1);
            boomcoin.addPendingTransaction(tx1);

            Console.WriteLine("Start the Miner!");
            boomcoin.MinePendingTransactions(wallet2);
            Console.WriteLine("\nBalance of wallet1 is $" + boomcoin.GetBalanceOfWallet(wallet1).ToString());
            Console.WriteLine("\nBalance of wallet2 is $" + boomcoin.GetBalanceOfWallet(wallet2).ToString());

            //boomcoin.AddBlock(new Block(1, DateTime.Now.ToString("yyyyMMddHHmmssffff"), "amount: 50"));
            //boomcoin.AddBlock(new Block(2, DateTime.Now.ToString("yyyyMMddHHmmssffff"), "amount: 200"));

            string blockJSON = JsonConvert.SerializeObject(boomcoin, Formatting.Indented);
            Console.WriteLine(blockJSON);

            //boomcoin.GetLatestBlock().PreviousHash = "12345"; 

            if (boomcoin.IsChainValid())
            {
                Console.WriteLine("Blockchain is valid!");
            }
            else
            {
                Console.WriteLine("Blockchain is NOT valid."); 
            }
        }

    }
}
