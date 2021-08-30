using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Models
{
    partial class Pharmacy
    {
        public override string ToString()
        {
            return $"{Id} - {Name}";
        }

        public bool AddDrug(Drug newDrug)
        {
            bool isFalse = false;
            foreach (var inPharmacyDrug in _drugs)
            {
                if (newDrug.Name == inPharmacyDrug.Name)
                {
                    inPharmacyDrug.Count += newDrug.Count;
                    isFalse = true;
                }
            }
            if (isFalse == false)
            {
                _drugs.Add(newDrug);
            }
            return false;
        }

        public bool InfoDrug(string name)
        {
            Drug existDrug = _drugs.Find(x => x.Name.ToLower() == name.ToLower());
            if (existDrug == null)
            {
                return false;
            }
            Console.WriteLine(existDrug);
            return true;
        }

        public bool ShowDrugItems()
        {
            if (_drugs.Count != 0)
            {
                foreach (Drug drug in _drugs)
                {
                    //drug.ToString();
                    Console.WriteLine(drug);

                }
                return true;
            }
            return false;
        }

        public bool SaleDrug(string name, int count, double payment)
        {

            Drug existDrug = _drugs.Find(x => x.Name.ToLower() == name.ToLower());
            if (existDrug == null)
            {
                Console.WriteLine("Bele bir derman tapilmadi. dogru derman yazin");
                return false;
            }
            else if (existDrug.Count < count)
            {
                Console.WriteLine($"{Name} aptekinde {existDrug.Name} dermani yeterli deyil");
                return false;
            }
            else if (payment < existDrug.Price * count)
            {
                Console.WriteLine("Odenilen mebleg yeterli deyil");
                return false;
            }
            else if (existDrug.Count - count == 0)
            {
                double leftOver = payment - existDrug.Price * count;
                _drugs.Remove(existDrug);
                Console.WriteLine($"Qaliq {leftOver} , Satish ugurludu");
                return true;
            }
            else if (existDrug.Count - count != 0)
            {
                _drugs.Find(x => x.Name.ToLower() == name.ToLower()).Count -= count;
                double leftOver = payment - existDrug.Price * count;
                Console.WriteLine($"{name} dermaninin satishi ugurla kecdi. galig mebleg {leftOver}");
            }
            return true;
        }
    }
}
