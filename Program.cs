using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Цепочка_обязанностей_homework_29._03._2023
{
    interface IReceiver {
        IReceiver SetNext(IReceiver receiver);
        object Handle(object request);
    }

    abstract class PaymentHandler : IReceiver {
        private IReceiver next_handler;

        public IReceiver SetNext(IReceiver receiver) {
            next_handler = receiver;
            return receiver;
        }
        public virtual object Handle(object request) {
            if (this.next_handler != null) {
                return this.next_handler.Handle(request);
            }
            else {
                return null;
            }
        }
    }


    class MoneyPymentHandler : PaymentHandler { 
        public override object Handle(object request) { 
            if(request as string == "Money") {
                return "Money payment";
            }
            else {
                return base.Handle(request); ;
            }
        }
    }

    class PayPalPaymentHandler : PaymentHandler {
        public override object Handle(object request) {
            if(request as string == "PayPal") {
                return "PayPal payment";
            }
            else {
                return base.Handle(request);
            }
        }
    }

    class BankPaymentHandler : PaymentHandler { 
        public override object Handle(object request) { 
            if(request as string == "Bank") {
                return "Bank payment";
            }
            else {
                return base.Handle(request);
            }
        }
    }


    class Client { 
        public void ClientCode(PaymentHandler ph) {
            foreach (var payment in new List<string> { "Money", "Bank", "PayPal" }) {
                Console.WriteLine($"Client: Who wants a {payment}?");
                var result = ph.Handle(payment);
                if (result != null) {
                    Console.Write($"    {result}\n");
                }
                else { Console.WriteLine($"   {payment} was left untouched."); }
            }
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            var money = new MoneyPymentHandler();
            var paypal = new PayPalPaymentHandler();
            var bank = new BankPaymentHandler();

            money.SetNext(paypal).SetNext(bank);

            Client client = new Client();
            client.ClientCode(money);

        }
    }
}
