using System;
using System.Collections.Generic;
using System.Text;

namespace IBS.Entities
{
    public class Nominee
    {
        public string NomineeName { get; set; }
        public string NomineeRelation { get; set; }
        public int NomineeAge { get; set; }
        public string NomineeGender { get; set; }
        public long NomineeMobileNumber { get; set; }
        public string NomineeAddress { get; set; }

       public Nominee()
        {

        }
        public Nominee(string name, string relation, int age, string gender, long mob, string add)
        {
            this.NomineeName = name;
            this.NomineeRelation = relation;
            this.NomineeAge = age;
            this.NomineeGender = gender;
            this.NomineeMobileNumber = mob;
            this.NomineeAddress = add;
        }
    }
}
