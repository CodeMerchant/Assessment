using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class User
    {
        private string name;
        private string gender;
        
        public User() { }

        public User(string name, string gender) {
            this.name = name;
            this.gender = gender;
        }

        public User(string name) {

            this.name = name;
        }

        public String getName()
        {
            return this.name;
        }

        public void setName(string name) {

            this.name = name;
        }

        public String getGender() {
            return this.gender;
        
        }

        public void setGender(string gender) {
            this.gender = gender;
        
        }

        public override string ToString()
        {
            return name + "," + gender;
        }
        
    }
}
