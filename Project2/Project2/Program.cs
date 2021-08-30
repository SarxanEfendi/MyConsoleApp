using Project2.Models;
using Project2.Utils;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Project2
{
    class Program
    {
        static void Main(string[] args)
        {


            List<Pharmacy> _pharmacies = new List<Pharmacy>();


        startApp:
            Console.WriteLine("Ozunuzu tanitin");
            Console.WriteLine();
        startIntroduction:
            string userName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(userName))
            {
                Helper.Print(ConsoleColor.Red, "Sadece Enter basmag ve ya boshlug goymag olmaz");
                goto startIntroduction;
            }
            if (userName.StartsWith(' '))
            {
                Helper.Print(ConsoleColor.Red, "Ad boshlugla bashlaya bilmez");
                goto startIntroduction;
            }
            Console.WriteLine($"Xosh qelmisiniz {userName}");
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("Ne etmek isteyirsiniz?");
            actionTypeChoose:
                Console.WriteLine(" 1 - Yeni aptek yaratmag. 2 - Derman elave etmek. 3 - Derman axtarishi etmek. 4 - Aptekde olan dermanlarin siyahisi. " +
                    "5 - Satish. 6 - Cixmag.");
                string resulOfChoise = Console.ReadLine();
                bool isChoise = int.TryParse(resulOfChoise, out int myChoise);
                if (isChoise && (myChoise >= 1 && myChoise <= 6))
                {
                    switch (myChoise)
                    {
                        case 1:

                            Console.WriteLine("Yeni aptekin adini daxil edin");

                            string namePharmacy = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(namePharmacy))
                            {
                                Helper.Print(ConsoleColor.Red, "Sadece Enter basmag ve ya boshlug goymag olmaz");
                                goto startApp;
                            }
                            if (namePharmacy.StartsWith(' '))
                            {
                                Helper.Print(ConsoleColor.Red, "Ad boshlugla bashlaya bilmez");
                                goto startApp;
                            }
                            Pharmacy pharmacy = new Pharmacy(namePharmacy);

                            _pharmacies.Add(pharmacy);
                            break;

                        case 2:
                            Helper.Print(ConsoleColor.Yellow, "Yeni dermanin adini daxil edin");

                            string drugName = Console.ReadLine();

                        selectPrice:
                            Helper.Print(ConsoleColor.Yellow, "Yeni dermanin qiymetidi daxil edin");

                            string price = Console.ReadLine();

                            bool isDoublePrice = double.TryParse(price, out double drugPrice);

                            if (!isDoublePrice)
                            {
                                Helper.Print(ConsoleColor.Red, "Eded daxil edin");

                                goto selectPrice;
                            }
                        selectCount:

                            Helper.Print(ConsoleColor.Yellow, "Yeni dermanin sayini daxil edin");

                            string count = Console.ReadLine();

                            bool isIntCount = int.TryParse(count, out int drugCount);

                            if (!isIntCount)
                            {
                                Helper.Print(ConsoleColor.Red, "Eded daxil edin");

                                goto selectCount;
                            }

                            Helper.Print(ConsoleColor.Yellow, "Dermanin hansi tipde oldugunu daxil edin");

                            string typeName = Console.ReadLine();

                            DrugType drugType = new DrugType(typeName);

                            Drug drug = new Drug(drugName, drugPrice, drugCount, drugType);
                            choosePharmacy:
                            Console.WriteLine("Dermani hansi apteke elave edek");
                            foreach (var item in _pharmacies)
                            {
                                Console.WriteLine(item);
                            }
                            string choosePharmacy = Console.ReadLine();
                            bool isChooseInt = int.TryParse(choosePharmacy, out int chooseId);
                            if (!isChooseInt && !(chooseId >=0))
                            {
                                Console.WriteLine("Secimi dogru edin");
                                goto choosePharmacy;
                            }
                            Pharmacy existGroup = _pharmacies.Find(x => x.Id == chooseId);
                            existGroup.AddDrug(drug);
                            
                            Helper.Print(ConsoleColor.Green, $"Dermanlar barede melumat yenilendi");
                            Console.WriteLine();

                            break;

                        case 3:
                            chooseListPharmacy:
                            Console.WriteLine("Hansi aptekden derman axtarirsiz");
                            foreach (var item in _pharmacies)
                            {
                                Console.WriteLine(item);
                            }
                            string choosePharmacyItem = Console.ReadLine();
                            bool isMyChooseInt = int.TryParse(choosePharmacyItem, out int myChooseId);
                            if (!isMyChooseInt && !(myChooseId >= 0))
                            {
                                Console.WriteLine("Secimi dogru edin");
                                goto chooseListPharmacy;
                            }
                            Pharmacy existChooseGroup = _pharmacies.Find(x => x.Id == myChooseId);
                            //existChooseGroup.ShowDrugItems();

                            writeName:
                            Helper.Print(ConsoleColor.Yellow, "Axtardiginiz dermanin adini yazin");
                            string nameDrug = Console.ReadLine();

                            if (nameDrug!=null)
                            {
                                bool resultA = existChooseGroup.InfoDrug(nameDrug);
                                if (resultA == false)
                                {
                                    Helper.Print(ConsoleColor.Red, "Bele bir derman sistemde tapilmadi");
                                    Console.WriteLine();
                                    Helper.Print(ConsoleColor.Green, "1 - Yeniden yazmag.");
                                    Helper.Print(ConsoleColor.DarkRed, "2 - Cixmag ");
                                    string choise = Console.ReadLine();
                                    bool choiseCheck = int.TryParse(choise, out int action);
                                    if (action == 1)
                                    {
                                        goto writeName;
                                    }
                                    goto actionTypeChoose;
                                }
                            }

                            break;
                        case 4:
                            chooseCorrectPharmacyList:
                            Console.WriteLine("Hansi aptekin derman siyahisini gormek isteyirsiniz");
                            foreach (var item in _pharmacies)
                            {
                                Console.WriteLine(item);
                            }
                            string choosePharmacyList = Console.ReadLine();
                            bool isMyListInt = int.TryParse(choosePharmacyList, out int myChooseIdList);
                            if (!isMyListInt && !(myChooseIdList >= 0))
                            {
                                Console.WriteLine("Secimi dogru edin");
                                goto chooseCorrectPharmacyList;
                            }
                            Pharmacy existChoosedList = _pharmacies.Find(x => x.Id == myChooseIdList);
                            existChoosedList.ShowDrugItems();

                            break;

                        case 5:

                            Console.WriteLine("Hansi aptekden derman satishi etmek isteyirsiniz");
                            foreach (var item in _pharmacies)
                            {
                                Console.WriteLine(item);
                            }
                            string chooseForSale = Console.ReadLine();
                            bool isMySaleChooseInt = int.TryParse(chooseForSale, out int mySaleChooseId);
                            if (!isMySaleChooseInt && !(mySaleChooseId >= 0))
                            {
                                Console.WriteLine("Secimi dogru edin");
                                goto chooseCorrectPharmacyList;
                            }
                            Pharmacy existChoosedSaleList = _pharmacies.Find(x => x.Id == mySaleChooseId);
                        startSale:
                            Helper.Print(ConsoleColor.Yellow, "Almag istediyiniz dermanin adini daxil edin");
                            string nameBuy = Console.ReadLine();
                            if (nameBuy == null)
                            {
                                Helper.Print(ConsoleColor.Red, "Ad yazmaginiz xaish olunur");
                                goto startSale;
                            }
                        countBuy:
                            Helper.Print(ConsoleColor.Yellow, "Nece eded almag isteyirsiniz");
                            string countBuy = Console.ReadLine();
                            bool isIntCountBuy = int.TryParse(countBuy, out int countSale);
                            if (countSale <= 0)
                            {
                                Helper.Print(ConsoleColor.Red, "Dogru say yazin");
                                goto countBuy;
                            }
                        paymentBuy:
                            Helper.Print(ConsoleColor.Yellow, "Odenish meblegini yazin");
                            string paymentBuy = Console.ReadLine();
                            bool isDoublePaymentBuy = double.TryParse(paymentBuy, out double paymentSale);
                            if (paymentSale <= 0)
                            {
                                Helper.Print(ConsoleColor.Red, "Odenishi dogru yazin");
                                goto paymentBuy;
                            }
                            if (existChoosedSaleList.SaleDrug(nameBuy, countSale, paymentSale) == false)
                            {
                                Helper.Print(ConsoleColor.Green, "1 - Yeniden yazmag.");
                                Helper.Print(ConsoleColor.DarkRed, "2 - Cixmag ");
                                string choise = Console.ReadLine();
                                bool choiseCheck = int.TryParse(choise, out int action);
                                if (action == 1)
                                {
                                    goto case 5;
                                }
                                goto actionTypeChoose;
                            }
                            break;

                        case 6:
                            Console.Clear();
                            Console.WriteLine($"{userName}, Thanks for everything");
                            goto startApp;
                    }

                }
                Console.WriteLine($"{userName}, sizden verilmish ededlerden secmeyinizi xaish edirem");
            }
        }
    }
}