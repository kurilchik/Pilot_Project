using System;
using System.Collections.Generic;
using System.Text;

namespace Pizza_Bot.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    class MinLenghtAttribute : Attribute
    {
        public int MinLenght { get; set; }

        public MinLenghtAttribute()
        {

        }
        public MinLenghtAttribute(int minLenght)
        {
            MinLenght = minLenght;
        }
    }
}
