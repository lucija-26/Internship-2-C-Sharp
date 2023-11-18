using System.Globalization;

namespace ConsoleApp4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            mainMenu();
            Console.WriteLine("Vas odabir: ");
            var articles = new Dictionary<string, (int quantity, double price, DateTime date)>()
         {
            {"Mlijeko", (1, 1.1f, new DateTime(2023,11,17)) }
         };
            var employes = new Dictionary<string, DateTime>()
        {
            {"Tea Bubic", new DateTime(2003, 01, 28)}
        };
            var choice = 0;
            int.TryParse(Console.ReadLine(), out choice);
            while (choice != 0)
            {
                switch (choice)
                {
                    case 1:
                        articlesMenu();
                        Console.WriteLine("Vas odabir: ");
                        _ = int.TryParse(Console.ReadLine(), out choice);
                        switch (choice)
                        {
                            case 1:
                                addAnArticle(articles);
                                break;
                            case 2:
                                deletionMenu();
                                var deletionChoice = Console.ReadLine();
                                switch (deletionChoice.ToLower())
                                {
                                    case "a":
                                        deleteByName(articles);
                                        break;
                                    case "b":
                                        deleteByExpirationDate(articles);
                                        break;
                                }
                                break;
                            case 3:
                                editArticlesMenu();
                                var editArticlesChoice = Console.ReadLine();
                                switch (editArticlesChoice.ToLower())
                                {
                                    case "a":
                                        editArticles(articles);
                                        break;
                                    case "b":
                                        editPrices(articles);
                                        break;
                                }
                                break;
                            case 4:
                                printMenu();
                                printArticlesChoice(articles);
                                break;
                        }
                        break;
                    case 2:
                        employesMenu();
                        int editEmployesChoice;
                        _ = int.TryParse(Console.ReadLine(), out editEmployesChoice);
                        switch (editEmployesChoice)
                        {
                            case 1:
                                inputEmployee(employes);
                                break;
                            case 2:
                                deleteEmployee(employes);
                                break;
                            case 3:
                                editEmployee(employes);
                                break;
                            case 4:
                                printEmployee(employes);
                                break;

                        }
                        break;

                };
                mainMenu();
                Console.WriteLine("Vas odabir: ");
                _ = int.TryParse(Console.ReadLine(), out choice);
            }
        }


        static void mainMenu()
        {
            Console.WriteLine("1 - Artikli");
            Console.WriteLine("2 - Radnici");
            Console.WriteLine("3 - Racuni");
            Console.WriteLine("4 - Statistika");
            Console.WriteLine("0 - Izlaz iz aplikacije");
        }

        static void articlesMenu()
        {
            Console.WriteLine("1 - Unos artikla");
            Console.WriteLine("2 - Brisanje artikla");
            Console.WriteLine("3 - Uredjivanje artikla");
            Console.WriteLine("4 - Ispis");
        }




        static void deletionMenu()
        {
            Console.WriteLine("a - Po imenu artikla");
            Console.WriteLine("b - Svi kojima je istekao rok trajanja");
        }

        static void editArticlesMenu()
        {
            Console.WriteLine("a - Uredjivanje proizvoda zasebno");
            Console.WriteLine("b - Popust/Poskupljenje na sve proizvode");
        }

        static void printMenu()
        {
            Console.WriteLine("a - svi artikli kako su spremljeni");
            Console.WriteLine("b - svi artikli sortirani po imenu");
            Console.WriteLine("c - svi artikli sortirani po datumu silazno");
            Console.WriteLine("d - svi artikli sortirani po datumu uzlazno");
            Console.WriteLine("e - svi artikli sortirani po kolicini");
            Console.WriteLine("f - najprodavaniji artikl");
            Console.WriteLine("g - najmanje prodavan artikl");
        }

        static string getString()
        {

            while (true)
            {
                string input = Console.ReadLine();
                if (checkIfString(input))
                    return input;
                else
                {
                    Console.WriteLine("Pogresno unesen string. Pokusajte ponovno.");
                }
            }
        }
        static bool checkIfString(string value)
        {
            foreach (char c in value)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                    return false;
            }

            return true;
        }
        static int getInt()
        {
            int input;
            do
            {
                if (int.TryParse(Console.ReadLine(), out input) == false || input <= 0)
                    Console.WriteLine("Incorrect input, please insert a number greater than 0! ");
            } while (input <= 0);
            return input;
        }

        static float getFloat()
        {
            float input;
            do
            {
                if (float.TryParse(Console.ReadLine(), out input) == false || input <= 0)
                    Console.WriteLine("Incorrect input, please insert a number greater than 0! ");
            } while (input <= 0);
            return input;
        }

        static DateTime GetDate()
        {
            DateTime userDate;

            while (true)
            {
                Console.Write("Unesite datum (yyyy-mm-dd): ");
                string userInput = Console.ReadLine();

                if (DateTime.TryParse(userInput, out userDate))
                    return userDate;
                else
                    Console.WriteLine("Invalid date format. Please try again.");
            }
        }

        static bool askUserToMakeAChange()
        {
            while (true)
            {
                Console.WriteLine("Zelite li napraviti promjenu? (y/n)");
                string askUserInput = Console.ReadLine();
                if (askUserInput.ToLower() == "y")
                    return true;
                else if (askUserInput.ToLower() == "n")
                    return false;
                else
                {
                    Console.WriteLine("Krivi unos.");
                }
            }
        }


        static void printEmployesByBirthday(Dictionary<string, DateTime> employes)
        {
            var currentMonth = DateTime.Now.Month;
            foreach(var employee in employes)
            {
                if(currentMonth == employee.Value.Month)
                    Console.WriteLine(employee.Key);
            }
        }

        static void printEmployes(Dictionary<string, DateTime> employes)
        {
            var currentTime = DateTime.Now;
            foreach(var employee in employes)
            {
                int age = currentTime.Year - employee.Value.Year;
                Console.WriteLine($"{employee.Key} - {age}");
            }
        }
        static void printEmployee(Dictionary<string, DateTime> employes)
        {
            Console.WriteLine("a - ispis svih radnika");
            Console.WriteLine("b - ispis radnika kojima je rodjendan u tekucem mjesecu");
            Console.WriteLine("c - Povratak");
            string choice = Console.ReadLine();
            var status = true;
            while(true)
            {
                switch (choice)
                {
                    case "a":
                        printEmployes(employes);
                        break;
                    case "b":
                        printEmployesByBirthday(employes);
                        break;
                    case "c":
                        status = false;
                        break;
                    default:
                        Console.WriteLine("Unesite nesto od ponudjenog");
                        break;
                }
            }
            
        }

        static void editEmployeeName(Dictionary<string, DateTime> employes)
        {
            Console.WriteLine("Unesite ime radnika: ");
            string name = getString();

            if (employes.ContainsKey(name))
            {
                Console.WriteLine("Upisite novo ime radnika: ");
                string newName = getString();
                employes[newName] = employes[name];
                employes.Remove(name);
                Console.WriteLine($"Ime {name} azurirano u {newName}");
            }
            else
            {
                Console.WriteLine("Ime nije azurirano");
            }
        }

        static void editEmployeeDate(Dictionary<string, DateTime> employes)
        {
            Console.WriteLine("Unesite ime radnika: ");
            string name = getString();
            if (employes.ContainsKey(name))
            {
                var newDate = GetDate();
                employes[name] = newDate;
                Console.WriteLine($"Datum rodjenja uspjesno promijenjen u {newDate}");
            }
            else
                Console.WriteLine("Datum rodjenja nije promijenjen. Radnik nije pronadjen. ");
        }
        static void editEmployee(Dictionary<string, DateTime> employes)
        {
            var status = true;
            while (status)
            {
                Console.WriteLine("Sto zelite promijeniti?");
                Console.WriteLine("a - Ime radnika");
                Console.WriteLine("b - Datum rodjenja");
                Console.WriteLine("c - povratak na prosli izbornik");
                string choice = Console.ReadLine();
                switch(choice)
                {
                    case "a":
                        editEmployeeName(employes);
                        break;
                    case "b":
                        editEmployeeDate(employes);
                        break;
                    case "c":
                        status = false;
                        break;
                }

            }
        }
        static void deleteEmployesOlderThan65(Dictionary<string, DateTime> employes)
        {
            DateTime currentDate = DateTime.Now;
            foreach (var employe in employes)
            {
                if ((currentDate - employe.Value).TotalDays > 65 * 365.25)
                {
                    employes.Remove(employe.Key);
                }
            }
        }

        static void deleteEmployesByName(Dictionary<string, DateTime> employes)
        {
            Console.WriteLine("Unesite ime radnika kojeg zelite izbrisati");
            string fullName = getString();
            if (askUserToMakeAChange())
            {
                employes.Remove(fullName);
                Console.WriteLine($"Osoba {fullName} je uspjesno izbrisana");
                return;
            }
            else
            {
                Console.WriteLine($"Osoba {fullName} nece biti izbrisana");
                return;
            }

        }
        static void deleteEmployee(Dictionary<string, DateTime> employes)
        {
            bool status = true;
            while (status)
            {
                employesDeletionMenu();
                string editChoice = Console.ReadLine();
                switch (editChoice.ToLower())
                {
                    case "a":
                        deleteEmployesByName(employes);
                        break;
                    case "b":
                        deleteEmployesOlderThan65(employes);
                        break;
                    case "c":
                        status = false;
                        break;
                }
            }

        }
        static void inputEmployee(Dictionary<string, DateTime> employes)
        {
            Console.WriteLine("Unesite ime i prezime radnika");
            var fullName = getString();
            var age = GetDate();
            if (askUserToMakeAChange())
            {
                employes.Add(fullName, age);
                Console.WriteLine("Radnik uspjesno dodan");
            }
            else
                Console.WriteLine("Radnik nece biti dodan");
        }

        static void printArticlesByQuantity(Dictionary<string, (int quantity, double price, DateTime date)> articles)
        {
            var sortedByQuantity = articles.OrderBy(kvp => kvp.Value.price);
            foreach (var article in sortedByQuantity)
            {
                var expiration = article.Value.date - DateTime.Now;
                int numberOfDays = expiration.Days;
                if (numberOfDays > 0)
                    Console.WriteLine($"{article.Key} ({article.Value.quantity}) - {article.Value.price} - broj dana do isteka roka ({numberOfDays})");
                else
                    Console.WriteLine($"{article.Key} ({article.Value.quantity}) - {article.Value.price} - broj dana do isteka roka ({Math.Abs(numberOfDays)})");
            }
        }
        static void printArticlesByDateAscending(Dictionary<string, (int quantity, double price, DateTime date)> articles)
        {
            var sortedByAscendingDate = articles.OrderBy(kvp => kvp.Value.date);
            foreach (var article in sortedByAscendingDate)
            {
                var expiration = article.Value.date - DateTime.Now;
                int numberOfDays = expiration.Days;
                if (numberOfDays > 0)
                    Console.WriteLine($"{article.Key} ({article.Value.quantity}) - {article.Value.price} - broj dana do isteka roka ({numberOfDays})");
                else
                    Console.WriteLine($"{article.Key} ({article.Value.quantity}) - {article.Value.price} - broj dana do isteka roka ({Math.Abs(numberOfDays)})");
            }
        }
        static void printArticlesByDateDescending(Dictionary<string, (int quantity, double price, DateTime date)> articles)
        {
            var sortedByDescendingDate = articles.OrderByDescending(kvp => kvp.Value.date);
            foreach (var article in sortedByDescendingDate)
            {
                var expiration = article.Value.date - DateTime.Now;
                int numberOfDays = expiration.Days;
                if (numberOfDays > 0)
                    Console.WriteLine($"{article.Key} ({article.Value.quantity}) - {article.Value.price} - broj dana do isteka roka ({numberOfDays})");
                else
                    Console.WriteLine($"{article.Key} ({article.Value.quantity}) - {article.Value.price} - broj dana do isteka roka ({Math.Abs(numberOfDays)})");
            }
        }
        static void printArticlesByName(Dictionary<string, (int quantity, double price, DateTime date)> articles)
        {
            var sortedArticles = articles.OrderBy(kvp => kvp.Key).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            foreach (var article in sortedArticles)
            {
                var expiration = article.Value.date - DateTime.Now;
                int numberOfDays = expiration.Days;
                if (numberOfDays > 0)
                    Console.WriteLine($"{article.Key} ({article.Value.quantity}) - {article.Value.price} - broj dana do isteka roka ({numberOfDays})");
                else
                    Console.WriteLine($"{article.Key} ({article.Value.quantity}) - {article.Value.price} - broj dana do isteka roka ({Math.Abs(numberOfDays)})");
            }
        }

        static void printArticle(Dictionary<string, (int quantity, double price, DateTime date)> articles)
        {
            foreach (var article in articles)
            {
                var expiration = article.Value.date - DateTime.Now;
                int numberOfDays = expiration.Days;
                if (numberOfDays > 0)
                    Console.WriteLine($"{article.Key} ({article.Value.quantity}) - {article.Value.price} - broj dana do isteka roka ({numberOfDays})");
                else
                    Console.WriteLine($"{article.Key} ({article.Value.quantity}) - {article.Value.price} - broj dana do isteka roka ({Math.Abs(numberOfDays)})");
            }
        }
        static void printArticlesChoice(Dictionary<string, (int quantity, double price, DateTime date)> articles)
        {
            var choice = Console.ReadLine();
            switch (choice.ToLower())
            {
                case "a":
                    printArticle(articles);
                    break;
                case "b":
                    printArticlesByName(articles);
                    break;
                case "c":
                    printArticlesByDateDescending(articles);
                    break;
                case "d":
                    printArticlesByDateAscending(articles);
                    break;
                case "e":
                    printArticlesByQuantity(articles);
                    break;
                case "f":
                    break;
                case "g":
                    break;
            }
        }

        static void employesMenu()
        {
            Console.WriteLine("1 - Unos radnika");
            Console.WriteLine("2 - Brisanje radnika");
            Console.WriteLine("3 - Uredjivanje radnika");
            Console.WriteLine("4 - Ispis");
        }

        static void employesDeletionMenu()
        {
            Console.WriteLine("a - po imenu");
            Console.WriteLine("b - svi koji imaju vise od 65 godina");
        }

        static void employesPrintMenu()
        {
            Console.WriteLine("a - svi radnici");
            Console.WriteLine("b - radnici kojima je rodjendan u tekucem mjesecu");
        }

        static void editPrices(Dictionary<string, (int quantity, double price, DateTime date)> articles)
        {
            Console.WriteLine("a - Poskupljenje svih proizvoda");
            Console.WriteLine("b - Popusti na sve proizvode");
            Console.WriteLine("c - Povratak na prethodni izbornik");
            var choice = Console.ReadLine();
            switch (choice.ToLower())
            {
                case "a":
                    priceIncrease(articles);
                    break;
                case "b":
                    priceDecrease(articles);
                    break;
                case "c":
                    break;
            }
        }

        static void priceIncrease(Dictionary<string, (int quantity, double price, DateTime date)> articles)
        {
            Console.WriteLine("Za koji iznos u eurima zelite da sve poskupi? ");
            float priceIncrement = getFloat();
            if (askUserToMakeAChange())
            {
                foreach (var key in articles.Keys.ToArray())
                {
                    articles[key] = (articles[key].quantity, articles[key].price + priceIncrement, articles[key].date);
                }
                Console.WriteLine("Uspjesno modificirano.");
            }
            else
                Console.WriteLine("Poskupljenje se nece primijeniti. ");
        }

        static void priceDecrease(Dictionary<string, (int quantity, double price, DateTime date)> articles)
        {
            Console.WriteLine("Za koji iznos u eurima zelite da sve pojeftini? ");
            float priceIncrement = getFloat();
            if (askUserToMakeAChange())
            {
                foreach (var key in articles.Keys.ToArray())
                {
                    articles[key] = (articles[key].quantity, articles[key].price - priceIncrement, articles[key].date);
                }
                Console.WriteLine("Uspjesno modificirano.");
            }
            else
                Console.WriteLine("Pojeftinjenje se nece primijeniti. ");
        }

        static void editArticles(Dictionary<string, (int quantity, double price, DateTime date)> articles)
        {
            Console.WriteLine("Unesite ime artikla kojeg zelite promijeniti: ");
            string nameOfArticle = getString();
            bool status = false;
            foreach (var item in articles)
            {
                if (nameOfArticle == item.Key)
                    status = true;
            }
            while (status)
            {
                Console.WriteLine("Sto zelite promijeniti? ");
                Console.WriteLine("a - Ime artikla");
                Console.WriteLine("b - kolicinu artikla");
                Console.WriteLine("c - cijenu artikla");
                Console.WriteLine("d - datum artikla");
                Console.WriteLine("e - povratak na prosli izbornik");

                var choice = Console.ReadLine();
                switch (choice.ToLower())
                {
                    case "a":
                        editArticleName(articles, nameOfArticle);
                        break;
                    case "b":
                        editArticleQuantity(articles, nameOfArticle);
                        break;
                    case "c":
                        editArticlePrice(articles, nameOfArticle);
                        break;
                    case "d":
                        editArticleExpirationDate(articles, nameOfArticle);
                        break;
                    case "e":
                        status = false;
                        break;
                }
            }
        }

        static void editArticleName(Dictionary<string, (int quantity, double price, DateTime date)> articles, string nameOfArticle)
        {
            Console.WriteLine("Novo ime artikla: ");
            string newArticleName = getString();

            if (askUserToMakeAChange())
            {
                articles.Add(newArticleName, (articles[nameOfArticle].quantity, articles[nameOfArticle].price, articles[nameOfArticle].date));
                articles.Remove(nameOfArticle);
                Console.WriteLine("Uspjesno promijenjeno ime! ");
            }
            else
            {
                Console.WriteLine("Ime nije promijenjeno");
            }
        }

        static void editArticleQuantity(Dictionary<string, (int quantity, double price, DateTime date)> articles, string nameOfArticle)
        {
            Console.WriteLine("Nova kolicina artikla: ");
            int newArticleQuantity = getInt();

            if (askUserToMakeAChange())
            {
                articles[nameOfArticle] = (newArticleQuantity, articles[nameOfArticle].price, articles[nameOfArticle].date);
                Console.WriteLine("Kolicina uspjesno promijenjena");
            }
            else
            {
                Console.WriteLine("Kolicina nije promijenjena");
            }
        }

        static void editArticlePrice(Dictionary<string, (int quantity, double price, DateTime date)> articles, string nameOfArticle)
        {
            Console.WriteLine("Nova cijena artikla: ");
            float newArticlePrice = getFloat();

            if (askUserToMakeAChange())
            {
                articles[nameOfArticle] = (articles[nameOfArticle].quantity, newArticlePrice, articles[nameOfArticle].date);
                Console.WriteLine("Cijena uspjesno promijenjena");
            }
            else
            {
                Console.WriteLine("Cijena nije promijenjena");
            }
        }

        static void editArticleExpirationDate(Dictionary<string, (int quantity, double price, DateTime date)> articles, string nameOfArticle)
        {
            Console.WriteLine("Novi rok trajanja: ");
            DateTime newExpirationDate = GetDate();

            if (askUserToMakeAChange())
            {
                articles[nameOfArticle] = (articles[nameOfArticle].quantity, articles[nameOfArticle].price, newExpirationDate);
                Console.WriteLine("Rok trajanja uspjesno promijenjen");
            }
            else
            {
                Console.WriteLine("Rok trajanja nije promijenjen");
            }
        }

        static void addAnArticle(Dictionary<string, (int quantity, double price, DateTime date)> articles)
        {
            Console.WriteLine("Unesite ime artikla: ");
            string nameOfArticle = getString();
            Console.WriteLine("Unesite kolicinu artikala: ");
            int quantityOfArticle = getInt();
            Console.WriteLine("Unesite cijenu artikla: ");
            float priceOfArticle = getFloat();
            Console.WriteLine("Unesite rok trajanja: ");
            DateTime dateOfArticle = GetDate();

            if (askUserToMakeAChange())
            {
                articles.Add(nameOfArticle, (quantityOfArticle, priceOfArticle, dateOfArticle));
                Console.WriteLine("Unos artikla je uspjesno obavljen!");
            }
            else
                Console.WriteLine("Unos artikla nije uspjesno obavljen!");
        }

        static void deleteByName(Dictionary<string, (int quantity, double price, DateTime date)> articles)
        {
            Console.WriteLine("Unesite ime artikla koji zelite izbrisati: ");
            var itemToDelete = Console.ReadLine();
            if (askUserToMakeAChange())
            {
                articles.Remove(itemToDelete);
                Console.WriteLine("Proizvod uspjesno izbrisan. ");
                return;
            }
            else
            {
                Console.WriteLine("Proizvod nije izbrisan. ");
                return;
            }
        }

        static void deleteByExpirationDate(Dictionary<string, (int quantity, double price, DateTime date)> articles)
        {
            if (askUserToMakeAChange())
            {
                foreach (var article in articles)
                {
                    if (article.Value.date < DateTime.Now)
                    {
                        articles.Remove(article.Key);
                    }

                }
                return;
            }
            else
            {
                Console.WriteLine("Proizvodi nece biti izbrisani. ");
                return;
            }

        }



    }
}