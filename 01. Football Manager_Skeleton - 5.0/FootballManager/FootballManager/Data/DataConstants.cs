using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Data
{
    public class DataConstants
    {
        public const int DefaultMinLength = 5;
        public const int DefaulMaxtLength = 20;

        public const int EmailMinLength = 10;
        public const int EmailMaxLength = 60;


        public const string UserEmailRegularExpression = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        public const int FullNameMaxLength = 80;

        public const int MinValue = 0;
        public const int MaxValue = 10;

        public const int DescriptionMaxLength = 200;
    }
}
