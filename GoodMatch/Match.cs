using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Match
    {
        private string male;
        private string female;
        private int percentage;

        public Match(string male, string female, int percentage) {

            this.male = male;
            this.female = female;
            this.percentage = percentage;
        }

        public String getMale() {
            return this.male;
        }

        public String getFemale() {
            return this.female;
        }

        public int getPercentage() {

            return this.percentage;
        }

        public override string ToString()
        {
            if (this.percentage >= 80)
            {
                return this.male + " matches " + this.female + " "+ this.percentage + "%, great match";
            }
            else {
                return this.male + " matches " + this.female + " " + this.percentage + "%";
            }
        }

    }
}
