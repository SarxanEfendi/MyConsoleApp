using MyProject.Models;
using MyProject.Utils;
using System;
using System.Text.RegularExpressions;

namespace MyProject
{



    class Program
    {
        static void Main(string[] args)
        {
        #region App Start

        startApp:

            Helper.Print(ConsoleColor.Yellow, $"Girmek istediyiniz apdekin adini yazin");
            Console.WriteLine();
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Helper.Print(ConsoleColor.Red, "Sadece Enter basmag ve ya boshlug goymag olmaz");
                goto startApp;
            }
            if (name.StartsWith(' '))
            {
                Helper.Print(ConsoleColor.Red, "Ad boshlugla bashlaya bilmez");
                goto startApp;
            }
            Pharmacy pharmacy = new Pharmacy(name);
            Console.WriteLine();
            Helper.Print(ConsoleColor.Green, $"Siz {name} aptekine daxil oldunuz");
            Console.WriteLine();

            #endregion

            while (true)
            {
                startAction:
                Helper.Print(ConsoleColor.Magenta, "Xaish olunur ozunuzu tanitin");
                Console.WriteLine();
                actionTypeChoise:
                Helper.Print(ConsoleColor.DarkGreen, "1 - Men saticiyam");
                Helper.Print(ConsoleColor.DarkGreen, "2 - Men aliciyam");
                Helper.Print(ConsoleColor.DarkGreen, "3 - Sef geldim cixiram");
                string resulOfChoise = Console.ReadLine();
                bool isChoise = int.TryParse(resulOfChoise, out int myChoise);
                if (isChoise && (myChoise >= 1 && myChoise <= 3))
                {
                    switch (myChoise)
                    {
                        #region Stuff 

                        case 1:
                            Helper.Print(ConsoleColor.Green, "Ne etmek isteyirsiz");
                            stuffMenu:
                            Helper.Print(ConsoleColor.DarkBlue, "1 - Yeni derman elave etmek. 2 - Butun dermanlarin siyyahisi. 3 - Cixis ");
                            string resultOfStuff = Console.ReadLine();
                            bool isStuffChoise = int.TryParse(resultOfStuff, out int menuForStuff);
                            if (isStuffChoise && (menuForStuff >= 1 && menuForStuff <= 3))
                            {
                                switch (menuForStuff)
                                {
                                    case 1:
                                        Helper.Print(ConsoleColor.Yellow,"Yeni dermanin adini daxil edin");

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

                                        pharmacy.AddDrug(drug);

                                        Helper.Print(ConsoleColor.Green, $"Dermanlar barede melumat yenilendi");
                                        Console.WriteLine();
                                        goto stuffMenu;

                                    case 2:

                                        Console.WriteLine($"{pharmacy.Name}-de olan dermanlarin siyahisi");
                                        pharmacy.ShowDrugItems();

                                        goto stuffMenu;

                                    case 3:
                                        Helper.Print(ConsoleColor.Cyan, "Gorduyunuz ishe qore minnetdaram");
                                        goto startAction;
                                }
                            }
                            Helper.Print(ConsoleColor.Red, "Movcud ededlerden secin");
                            goto stuffMenu;

                        #endregion

                        #region Customer

                        case 2:

                        customerMenu:
                            Helper.Print(ConsoleColor.Green, "Ne etmek isteyirsiz");
                            Helper.Print(ConsoleColor.DarkBlue, "1 - Derman axtarmag. 2 - Derman almag. 3 - Cixis ");
                            string resultOfCustomer = Console.ReadLine();
                            bool isCustomerChoise = int.TryParse(resultOfCustomer, out int menuForCustomer);
                            if (isCustomerChoise && (menuForCustomer >= 1 && menuForCustomer <= 3))
                            {
                                switch (menuForCustomer)
                                {
                                    case 1:
                                        Helper.Print(ConsoleColor.Yellow,"Axtardiginiz dermanin adini yazin");
                                        string nameDrug = Console.ReadLine();

                                        if (nameDrug != null)
                                        {
                                            bool resultA = pharmacy.InfoDrug(nameDrug);
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
                                                    goto case 1;
                                                }
                                                goto customerMenu;
                                            }
                                        }
                                        goto customerMenu;

                                    case 2:

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
                                        if (pharmacy.SaleDrug(nameBuy, countSale, paymentSale) == false)
                                        {
                                            Helper.Print(ConsoleColor.Green, "1 - Yeniden yazmag.");
                                            Helper.Print(ConsoleColor.DarkRed, "2 - Cixmag ");
                                            string choise = Console.ReadLine();
                                            bool choiseCheck = int.TryParse(choise, out int action);
                                            if (action == 1)
                                            {
                                                goto case 2;
                                            }
                                            goto startSale;
                                        }

                                        goto customerMenu;

                                    case 3:

                                        Helper.Print(ConsoleColor.Cyan, "Bizi secdiyinize gore minnetdaram");

                                        goto startAction;
                                        
                                }
                            }
                            Helper.Print(ConsoleColor.Red, "Movcud ededlerden secin");
                            goto customerMenu;

                        #endregion

                        case 3:
                            Console.Clear();
                            goto startApp;

                    }
                }
                Helper.Print(ConsoleColor.Red, "Movcud ededlerden secin");
                goto actionTypeChoise;

            }
        }

    }
}