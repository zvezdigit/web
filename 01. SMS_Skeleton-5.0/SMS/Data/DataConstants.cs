using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Data
{
    public class DataConstants
    {
        public const int UsernameMinLength = 5;
        public const int DefaultLength = 20;

        public const int PasswordMinLength = 6;

        public const int ProductMinLength = 4;

        public const string UserEmailRegularExpression = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        public const decimal ProductMinPrice = (decimal)0.05;

        public const decimal ProductMaxPrice = (decimal)1000;



    }
}
