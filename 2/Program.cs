using System;
namespace BankSystem
{
    public class BadCashException: Exception
    {
        public BadCashException()
        {
            
        }
    }
    public delegate void BigMoneyHandler(object sender, BigMoneyArgs e);

    public class BigMoneyArgs
    {
        public string id { get; set; }
        public double amount { get; set; }

        public BigMoneyArgs(string id, double amount)
        {
            this.id = id;
            this.amount = amount;
        }
    }
    public class Account
    {
        private string _id;
        private string _password;
        private double _balance;

        public string id
        {
            get { return _id; }
        }
        public string password
        { 
            set { _password = value; }
        }
        public double balance
        { 
            get { return _balance; } 
        }
        public Account(string id, string password)
        {
            _id = id;
            _password = password;
            this.bigMoney += ShowAlert;
        }

        public bool CheckPassword(string password)
        { 
            return _password.Equals(password);
        }
        public void Deposit(double n)
        {
            if(n > 0)
            {
                _balance += n;
                Console.WriteLine("Deposit successful. Your balance is {0:C} now.", _balance);
            }
            else
            {
                Console.WriteLine("Deposit failed. Deposit amount must be a postive number.");
            }
        }
        public void Withdraw(double n)
        {
            if (n > 0)
            {
                if (_balance - n > 0) 
                { 
                    try
                    {
                        Random random = new Random();
                        int badCashRate = random.Next(0, 100);
                        if (badCashRate >= 70)
                        {
                            throw new BadCashException();
                        }
                        else
                        {

                            _balance -= n;
                            Console.WriteLine("Withdraw successful. Your balance is {0:C} now.", _balance);
                            if (n > 10000)
                            {
                                BigMoneyWithdrawed(n);
                            }
                        }
                    }
                    catch(BadCashException e) 
                    { 
                        Console.WriteLine("Bad cash, please contact the staff.");
                    }
                    
                }
                else
                {
                    Console.WriteLine("Withdraw failed. No enough balance.");
                }
            }
            else
            {
                Console.WriteLine("Withdraw failed. Withdraw amount must be a postive number.");
            }
        }
        public event BigMoneyHandler bigMoney;
        public void BigMoneyWithdrawed(double amount)
        {
            if (bigMoney != null)
            {
                bigMoney(this, new BigMoneyArgs(_id, amount));
            }
        }
        public void ShowAlert(object sender, BigMoneyArgs e)
        {
            Console.WriteLine("Warning: Account {0} has withdrawn {1:C}. Please confirm.", e.id, e.amount);
        }
    }

    public class Bank
    {
        private string _name;
        private Account[] _accounts;
        private int _accountNum;

        public string name
        {
            get { return _name; }
        }
        public Account[] accounts
        {
            get { return _accounts; }
        }
        public int accountNum
        {
            get { return _accountNum; }
        }

        public Bank(string name)
        {
            _name = name;
            _accounts = new Account[2];
            _accountNum = 0;
        }

        public Account this[string id]
        {
            get
            {
                for (int i = 0; i < _accountNum; i++)
                {
                    if (accounts[i].id == id)
                    {
                        return accounts[i];
                    }
                }
                return null;
            }
        }

        private void Recap(int n)
        {
            if(n > 0)
            {
                Account[] tmp = new Account[_accounts.Length * 2];
                for (int i = 0; i < _accounts.Length; i++)
                {
                    tmp[i] = _accounts[i];
                }
                _accounts = tmp;
            }
        }
        public void AddAccount(Account account)
        {
            if(_accountNum == _accounts.Length)
                Recap(_accounts.Length*2);
            _accounts[_accountNum] = account;
            _accountNum++;
        }
        public void RemoveAccount(string id)
        {
            for (int i = 0; i < _accountNum; i++)
            {
                if (accounts[i].id == id)
                {
                    for(int j = i; j < _accounts.Length; j++)
                        _accounts[j] = _accounts[j+1];
                    _accountNum--;
                    if (_accountNum > 10 && _accountNum < _accounts.Length / 4)
                        Recap(_accountNum / 2);
                    Console.WriteLine("Removed account {0} successfully.", id);
                    return;
                }
            }
            Console.WriteLine("Failed to remove account {0}: Account not exist.", id);
        }
    }

    public class ATM
    {
        private Bank _bank;
        private Account _account;

        public Bank bank
        { 
            get { return _bank; } 
        }
        public Account account
        {
            get { return _account; }
        }
        public ATM(Bank bank)
        {
            _bank = bank;
            _account = null;
        }
        public void Login(string id, string password)
        {
            Account LogInAcc = bank[id];
            if(LogInAcc != null)
            {
                if(LogInAcc.CheckPassword(password))
                {
                    _account = LogInAcc;
                    Console.WriteLine("Login sussessfully.");
                }
                else
                {
                    Console.WriteLine("Wrong password.");
                }
            }
            else
            {
                Console.WriteLine("Account {0} does not exist.", id);
            }
        }
        public void Exit()
        {
            _account = null;
            Console.WriteLine("Exit successfully.");
        }
        public void ShowBalance()
        {
            if(_account != null)
            {
                Console.WriteLine("Your balance is {0:C}", _account.balance);
            }
            else
            {
                Console.WriteLine("Please login.");
            }
        }
        public void Deopsit(double n)
        {
            if (_account != null)
            {
                _account.Deposit(n);
            }
            else
            {
                Console.WriteLine("Please login.");
            }
        }
        public void Withdraw(double n)
        {
            if(_account != null)
            {
                _account.Withdraw(n);
            }
            else
            {
                Console.WriteLine("Please login.");
            }
        }
    }
    public class CreditAccount : Account
    {
        private double _credits;

        public double credits
        {
            get { return _credits; }
            set { _credits = value; }
        }
        public CreditAccount(string id, string password) : base(id, password)
        {

        }

        public void SpendMoney(double n)
        {
            if (n > 0)
            {
                if (_credits - n < 0)
                {
                    Console.WriteLine("You don't have enough credits.");
                    return;
                }
                else
                {
                    _credits -= n;
                    Console.WriteLine("Spent {0:C} successfully. Your credit balance is {1:C} now.", n, _credits);
                }
            }
        }

        new public void Deposit(double n)
        { 
            if(n > 0)
            {
                if(_credits - n < 0)
                {
                    Console.WriteLine("You don't have enough credits.");
                    return;
                }
                else
                {
                    _credits -= n;
                    Console.WriteLine("Deposit successful.");
                }
            }
            else
            {
                Console.WriteLine("Withdraw failed. Withdraw amount must be a postive number.");
            }
        }
    }

    class Test
    {
        public static void Main()
        {
            Bank mybank = new("test_bank");
            Account acc1 = new("001", "111111");
            Account acc2 = new("002", "222222");
            mybank.AddAccount(acc1);
            mybank.AddAccount(acc2);
            ATM atm = new(mybank);

            atm.Login("001", "111111");
            atm.Deopsit(20000);
            atm.ShowBalance();
            atm.Withdraw(100);
            atm.Withdraw(15000);
            atm.Exit();

            atm.Login("002", "222222");
            atm.Login("003", "333333");

        }
    }
}
