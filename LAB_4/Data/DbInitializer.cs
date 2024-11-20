using LAB_4.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Drawing;

namespace LAB_4.Data
{
    public static class DbInitializer
    {
        static DateOnly GetRandomDate(DateOnly start, DateOnly end)
        {
            Random random = new Random();
            int range = (end.DayNumber - start.DayNumber);
            int randomDayNumber = random.Next(0, range + 1);
            return start.AddDays(randomDayNumber);
        }
        public static void Initialize(SimilarProductsContext db)
        {
            db.Database.EnsureCreated();

            Random rand = new(0);
            // Если таблица пустая то ее заполнить
            if (!db.Products.Any())
            {
                InitializeProducts(db, productsNumber: 30, rand);
            }
            if (!db.Enterprises.Any())
            {
                InitializeEnterprises(db, enterprisesNumber: 30, rand);
            }
            if (!db.ProductionPlans.Any())
            {
                InitializeProductionPlans(db, productionPlansNumber: 30, rand);
            }

            //InitializeProductTypes(db, productTypesNumber: 400, rand);
            //InitializeSalesPlans(db, salesPlansNumber: 400, rand);
        }

        private static void InitializeProducts(SimilarProductsContext db, int productsNumber, Random rand)
        {
            string[] productName = ["Ноутбук Lenovo ThinkPad", "Смартфон Samsung Galaxy", "Холодильник LG",
                         "Кабель HDMI", "Пакет офисной бумаги", "Картридж для принтера HP",
                         "Молоко", "Мука", "Вода питьевая", "Бензин"];

            string[] productCharacteristics = ["Производительность для работы и учебы", "Большой экран и мощная камера",
                                    "Энергоэффективность и вместительность", "Подключение устройств",
                                    "Для офисных и учебных задач", "Расходный материал для печати",
                                    "Свежесть и качество", "Пекарное использование", "Чистота и минерализация",
                                    "Топливо для транспортных средств"];

            string[] productUnit = ["Штука", "Штука", "Штука", "Метр", "Пачка", "Штука",
                         "Литр", "Килограмм", "Литр", "Литр"];

            int countProductName = productName.GetLength(0);
            int countProductCharacteristics = productCharacteristics.GetLength(0);
            int countProductUnit = productUnit.GetLength(0);
            string imagesPath = "D:\\Study1\\BD\\LAB_4\\LAB_4\\wwwroot\\images";
            string[] exts = { ".jpg", ".png" };
            var images = Directory.GetFiles(imagesPath, "*.*").Where(file => exts.Contains(Path.GetExtension(file).ToLower())).ToArray();
            
            
            
            for (int i = 0; i < productsNumber; i++)
            {
                var imgBytes = ImageToByteArray(images[rand.Next(images.Length)]);
                db.Products.Add(new Product
                {
                    Name = productName[rand.Next(countProductName)],
                    Characteristics = productCharacteristics[rand.Next(countProductCharacteristics)],
                    Unit = productUnit[rand.Next(countProductUnit)],
                    Photo = imgBytes,
                });
            }
            db.SaveChanges();
        }

        private static void InitializeEnterprises(SimilarProductsContext db, int enterprisesNumber, Random rand)
        {
            string[] enterpriseName = {"АльфаСтрой", "ТехноСервис", "ИнноваПром", "ГлобалТрейд", "ЭкоФуд",
                                "МедФарм", "АгроЛэнд", "БизнесПроект", "ФинансыПлюс", "ЛогистикСеть",
                                "РемонтСервис", "ТехАвто", "СофтЭксперт", "ГоризонтТур", "ДизайнСтудия",
                                "КнигаМир", "МедиаЦентр", "КосметикПро", "СтройГарант", "Вкусноеда"
};

            string[] enterpriseDirectorName = {"Иванов Иван Иванович", "Петрова Мария Сергеевна", "Сидоров Петр Алексеевич", "Антонова Анна Викторовна",
                                "Смирнов Сергей Павлович", "Александрова Екатерина Николаевна", "Дмитриев Дмитрий Андреевич", "Ильина Ольга Романовна",
                                "Кузнецов Николай Васильевич", "Титова Светлана Ивановна", "Николаева Анастасия Владимировна", "Максимов Максим Степанович",
                                "Викторова Виктория Юрьевна", "Андреев Андрей Григорьевич", "Еленина Елена Арсеньевна", "Романов Роман Константинович",
                                "Ксенина Ксения Владиславовна", "Владимиров Владимир Петрович", "Дарьева Дарья Александровна", "Игорев Игорь Михайлович"
};
            string[] enterpriseActivityType = {"Строительство", "Торговля", "Производство", "Образование", "Медицинские услуги",
                                "Туризм", "Транспорт и логистика", "ИТ-услуги", "Консалтинг", "Финансовая деятельность",
                                "Ремонт и обслуживание техники", "Агропромышленность", "Пищевая промышленность", "Энергетика",
                                "Творческие услуги", "Рекламная деятельность", "Юридические услуги", "Недвижимость",
                                "Банковская деятельность", "Производство одежды"
};

            string[] enterpriseOwnershipForm = {"ООО", "ИП", "АО", "ПАО", "Государственное предприятие",
                                "Муниципальное предприятие", "Хозяйственное товарищество", "Некоммерческая организация"
};


            int countEnterpriseName = enterpriseName.GetLength(0);
            int countEnterpriseActivityType = enterpriseActivityType.GetLength(0);
            int countEnterpriseDirectorName = enterpriseDirectorName.GetLength(0);
            int countEnterpriseOwnershipForm = enterpriseOwnershipForm.GetLength(0);


            for (int i = 0; i < enterprisesNumber; i++)
            {
                db.Enterprises.Add(new Enterprise
                {
                    Name = enterpriseName[rand.Next(countEnterpriseName)],
                    DirectorName = enterpriseDirectorName[rand.Next(countEnterpriseDirectorName)],
                    ActivityType = enterpriseActivityType[rand.Next(countEnterpriseActivityType)],
                    OwnershipForm = enterpriseOwnershipForm[rand.Next(countEnterpriseOwnershipForm)],
                });
            }
            db.SaveChanges();
        }


        private static void InitializeProductionPlans(SimilarProductsContext db, int productionPlansNumber, Random rand)
        {
            int[] productionPlanPlannedVolume = { 1200, 1700, 1400, 1500, 1800, 1600 };

            int[] productionPlanActualVolume = { 600, 1000, 800, 1653, 1986, 1822 };

            byte[] productionPlanQuarter = { 1, 2, 3, 4 };

            int[] productionPlanYear = { 1999, 2021, 2020, 2022, 2024, 2019, 2005, 2007, 2018 };

            int countProductionPlanPlannedVolume = productionPlanPlannedVolume.GetLength(0);
            int countProductionPlanActualVolume = productionPlanActualVolume.GetLength(0);
            int countproductionPlanQuarter = productionPlanQuarter.GetLength(0);
            int countProductionPlanYear = productionPlanYear.GetLength(0);


            for (int i = 0; i < productionPlansNumber; i++)
            {
                var enterprise = db.Enterprises.OrderBy(e => Guid.NewGuid()).FirstOrDefault();
                var product = db.Products.OrderBy(p => Guid.NewGuid()).FirstOrDefault();
                if (enterprise == null && product == null) continue;
                db.ProductionPlans.Add(new ProductionPlan
                {
                    PlannedVolume = productionPlanPlannedVolume[rand.Next(countProductionPlanPlannedVolume)],
                    ActualVolume = productionPlanActualVolume[rand.Next(countProductionPlanActualVolume)],
                    Year = productionPlanYear[rand.Next(countProductionPlanYear)],
                    ProductId = product.ProductId,
                    EnterpriseId = enterprise.EnterpriseId,
                    Quarter = productionPlanQuarter[rand.Next(countproductionPlanQuarter)]
                });
            }
            db.SaveChanges();
        }

        private static void InitializeProductTypes(SimilarProductsContext db, int productTypesNumber, Random rand)
        {
            string[] productTypeName = {"Электроника", "Бытовая техника", "Канцелярские товары", "Продукты питания", "Топливо"};

            int countproductTypeName = productTypeName.GetLength(0);

            for (int i = 0; i < productTypesNumber; i++)
            {
                db.ProductTypes.Add(new ProductType
                {
                    Name = productTypeName[rand.Next(countproductTypeName)],
                });
            }
            db.SaveChanges();
        }
        
        private static void InitializeSalesPlans(SimilarProductsContext db, int salesPlansNumber, Random rand)
        {
            int[] salesPlanPlannedSales = { 1200, 1700, 1400, 1500, 1800, 1600 };
            int[] salesPlanActualSales = { 600, 1000, 800, 1653, 1986, 1822 };
            byte[] salesPlanQuarter = { 1, 2, 3, 4 };
            int[] salesPlanYear = { 1999, 2021, 2020, 2022, 2024, 2019, 2005, 2007, 2018 };


            int countsalesPlanPlannedSales = salesPlanPlannedSales.GetLength(0);
            int countsalesPlanActualSales = salesPlanActualSales.GetLength(0);
            int countsalesPlanQuarter = salesPlanQuarter.GetLength(0);
            int countsalesPlanYear = salesPlanYear.GetLength(0);

            for (int i = 0; i < salesPlansNumber; i++)
            {
                db.SalesPlans.Add(new SalesPlan
                {
                    PlannedSales = salesPlanPlannedSales[rand.Next(countsalesPlanPlannedSales)],
                    ActualSales = salesPlanActualSales[rand.Next(countsalesPlanActualSales)],
                    Quarter = salesPlanQuarter[rand.Next(countsalesPlanQuarter)],
                    Year = salesPlanYear[rand.Next(countsalesPlanYear)],
                });
            }
            db.SaveChanges();
            db.SaveChanges();
        }

        public static byte[] ImageToByteArray(string imagePath)
        {
            // Проверяем существование файла
            if (!File.Exists(imagePath))
                throw new FileNotFoundException($"Файл {imagePath} не найден.");

            // Загружаем изображение из указанного пути
            using (Image image = Image.FromFile(imagePath))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    // Сохраняем изображение в поток памяти в формате PNG
                    image.Save(memoryStream, image.RawFormat);
                    return memoryStream.ToArray();
                }
            }
        }

        public static string GetRandomFilePath(string folderPath)
        {
            // Проверка существования папки
            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Папка {folderPath} не существует.");
                return null;
            }

            // Получаем все файлы в папке
            string[] files = Directory.GetFiles(folderPath);

            // Проверка на наличие файлов в папке
            if (files.Length == 0)
            {
                return null; // В папке нет файлов
            }

            // Генерация случайного индекса
            Random random = new Random();
            int randomIndex = random.Next(files.Length);

            // Возвращаем путь к случайному файлу
            return files[randomIndex];
        }
    }
}
