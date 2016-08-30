namespace CompanyStore.Data.Migrations
{
    using CompanyStore.Entity;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CompanyStore.Data.CompanyStoreContext>
    {
        private DateTime _dateFrom = DateTime.Now.AddYears(-15);
        private DateTime _dateTo = DateTime.Now;

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CompanyStore.Data.CompanyStoreContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            // Create Department
            context.Departments.AddOrUpdate(e => e.Name, MockDataInitializer.GenerateDepartments());
            // Create Employees
            context.Employees.AddOrUpdate(e => e.FirstName, GenerateEmployees());
            //  Create Genres
            context.Categories.AddOrUpdate(g => g.Name, GenerateCategories());
            // Create Devices
            context.Devices.AddOrUpdate(d => d.Name, GenerateDevices());
            // Create Stocks
            context.Stocks.AddOrUpdate(GenerateStocks());
            // Create Roles
            context.Roles.AddOrUpdate(r => r.Name, GenerateRoles());
            // Create Users
            context.Users.AddOrUpdate(u => u.Username, new User[]{
                new User()
                {
                    Email = "u510610433@gmail.com",
                    FirstName = "Wanasak",
                    LastName = "Suraintaranggoon",
                    Username = "smudger",
                    IsLocked = false,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    HashedPassword = "2O65mFzQWIxmfzbkPjeVnS3c8U0IN07oE8ymQWwgY5Y=",
                    Salt = "ljd/YZrfxnkEoB0l2rvjgA==",
                    Image = "twitter-profile.jpg"
                }
            });
            // Create user admin for smudger
            context.UserRoles.AddOrUpdate(new UserRole[]{
                new UserRole()
                {
                    UserID = 1,
                    RoleID = 1
                }
            });
        }

        private Category[] GenerateCategories()
        {
            Category[] categories = new Category[] {
                new Category() { Name = "Tablet" },
                new Category() { Name = "Laptop" },
                new Category() { Name = "Desktop" },
                new Category() { Name = "Mobile" },
                new Category() { Name = "Monitor" },
                new Category() { Name = "UPS" },
                new Category() { Name = "Credit Card Devices" },
                new Category() { Name = "Other" },
                new Category() { Name = "Network" },
                new Category() { Name = "Printer" },
            };

            return categories;
        }

        private Device[] GenerateDevices()
        {
            Device[] devices = new Device[] {
                new Device() {
                    Name = "DELL DESKTOP TW V3653 W260933TH",
                    Description = "��������������� ����ͧ㨼�����駴�ҹ��䫹���л���Է���Ҿ ����Ҿ�����س�Ҿ㹡����ҹ�����ҡ���� �ӧҹ��ú�ء�ѧ���� ��������м�ҹ�ſ�����ͧ�س��ŧ������ҧ������дش �ͺʹͧ�ء�����ҹ ���зء������ͧ��âͧ�س�����ҧŧ��� �Ѻ�ͧ��Ҷ١㨤������������͹",
                    Image = "W260933TH_V3650_BLACK_UBUNTU-1.jpg",
                    CategoryID = 3,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "ACER DESKTOP TW TC 710-614G1T00MGi/T002_Ci3",
                    Description = "�իջ���Է���Ҿ�٧ ������ٻ�ç��Ҵ��ҧ��䫹����º���� ʧ�ҧ���ѹ������¹͡�մ���ǹ ���ç �شѹ",
                    Image = "dt.b15st.002-1.jpg",
                    CategoryID = 3,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "HP DESKTOP TW 550-154l",
                    Description = "�իջ���Է���Ҿ�٧",
                    Image = "889894575241-01.jpg",
                    CategoryID = 3,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "Apple iMAC 27 with Retina 5K/3.2QC/8GB/1TB/M380-THA",
                    Description = "iMac �ش�ʹ���ʺ��ó�����ҹ���ʡ��ͻ��������ҡ��èѺ�����Ҿ���շ���ش��ҡѺ�������� ��ҿԡ ����ػ�ó�Ѵ�红����Ż���Է���Ҿ�٧������㹵������ͧ�ش�ҧ��º ������º��¹�����µ��",
                    Image = "MK462THA-a1.jpg",
                    CategoryID = 3,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "ASUS Smartphone ZENFONE 2 Deluxe-ZE551ML 32GB PURPLE (4G)",
                    Description = "ZENFONE 2 Deluxe-ZE551ML ���Ȩ������觧ҹ�����������ǡ����������ҹ�ѹ�����ҧ��������ͧૹ ����ʴ��ŷ���˹���дѺ���ҧ�����������ҡ�͹ ��ê��쨷���Ǵ���� ����ͧ��繷����Ѵ ��� UI �����ҹ�������ҧ���մ�ӡѴ �١�͡Ẻ���������͹���͹���շ���ش�ͧ�س",
                    Image = "4712900347722-a1.jpg",
                    CategoryID = 4,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "TP-Link USB Wireless Adapter TL-WN723N 150Mbps Mini Wireless N USB Adapter",
                    Description = "TP-link ����µ���Ѻ�ѭ�ҳ Wireless ����� USB 2.0 �Թ������ �բ�Ҵ�зѴ�Ѵ �������ء�� �Դ��駧��� ����������������·���ʹ��� ��������㹡���Ѻ�觢����Ŷ֧ 150Mbps",
                    Image = "6935364050559-e1.jpg",
                    CategoryID = 9,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "Linksys Router Wireless WRT54GL Wireless-G Broadband Router",
                    Description = "Linksys WRT54GL Wireless-G Router �ػ�ó� Wireless-G Broadband Router (802.11g) �������� 54 Mbps ������Ѻ����Թ�����絤��������٧������١���� ��ҹ�к��Ź����к��������",
                    Image = "745883568291-1.jpg",
                    CategoryID = 9,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "Dell Notebook 3459-W561077TH-BLACK-WIN10",
                    Description = "�굺��������� ����ջ���Է���Ҿ ��������س��ҹ����ҡ���� �ӧҹ�������ѹẺ������дش �ա���͡Ẻ��䫹���������� �ѹ���� �������ç ˹�Ҩ͢�Ҵ 14.0 ���� ˹�Ҩ͡��ҧ����Ѻ��÷ӧҹ�������� �����ö���ҡ��� ��駤��������´˹�Ҩ� 1366*768 �ԡ�� �����˹�Ҩ������ѹ��� ʴ�� ������ԧ",
                    Image = "W561077TH-3459-BLACK-WIN10-1.jpg",
                    CategoryID = 2,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "HP Notebook Pavilion 15-ab555TX",
                    Description = "����͡Ẻ���ʧ�ҧ�� ⴴ�� ��������Ч���� �����ͧ�����ҧ���س�������� ����ʴؤس�Ҿ�٧ �����§����§�� �ѧ���������ç�ա����",
                    Image = "889894987747-01.jpg",
                    CategoryID = 2,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "Apple iPad Pro 9.7 inch Wi-Fi + Cellular 32GB - Space Grey",
                    Description = "iPad Pro ��� 9.7 ���Ǿ����дǡ��������� ��������¡�ü����ҹ���ŧ��Ǣͧ����Է���Ҿ ��Ф����๡���ʧ���Ẻ���س����������ʷ���˹�ҡ�͹",
                    Image = "888462831345-1.jpg",
                    CategoryID = 1,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "Asus Tablet Z370CG-1B005A White",
                    Description = "ASUS ZenPad 7.0 (Z370CG) �������������ҧ���㨤س��ͧ���",
                    Image = "4712900063196-01.jpg",
                    CategoryID = 1,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "Logitech Keyboard USB K120",
                    Description = "��䫹��ͧ�ѹ�ͧ����ˡ��� �ͧ���Ǩ�����͡�ҡ������촢ͧ�س, �֧����ͧ�ѧ������ѹ�����¨ҡ�غѵ��˵��蹹��ˡ��� �ٻ��ҧ����ҧ��������ջ����ҧ����������ѹ�ٴպ���Тͧ�س 㹢�з��������ͧ͢�سʺ���ҡ��� �������㹷�ҷ��������",
                    Image = "9785506712-01.jpg",
                    CategoryID = 8,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "Rapoo Keyboard Wireless 2.4GHz - E1050",
                    Description = "Rapoo E1050, keyboard than ordinary keyboard is smaller, key bit rearrange, compact than ordinary keyboard, is also convenient to carry. Small keyboard sound, basic non-tone buttons",
                    Image = "6943518917665_1.jpg",
                    CategoryID = 8,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "Logitech Mouse M100R USB",
                    Description = "�ٻ��ҧ���������Ҩ���ͫ���������͢�� �س������֡ʺ��㹡����ҹ�ҹ�Ѻ������� ���¢�Ҵ�ҵðҹ ����͡Ẻ����������ͧ���",
                    Image = "97855089250_01.jpg",
                    CategoryID = 8,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "Microsoft Mouse L2 Basic Optical - Black",
                    Description = "Basic Optical Mouse �����Ẻ����� ��䫹��ЪѺ��� ��������Ẻ��¼�ҹ���� USB ���͡�Ẻ�����������ͫ��������͢�������������ҹ",
                    Image = "885370433845-1.jpg",
                    CategoryID = 8,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "CHUPHOTIC UPS MS1200 (1200VA/600W)",
                    Description = "�ӧҹ���������з���դ�������§�ҧ俿����ء�ٻẺ������������ͼ�ҹ����ͧ���ͧ俿�� CHUPHOTIC Mercury Smart MS1200 ����Ҿ����෤������ѹ�ѹ����",
                    Image = "8859255800925-1.jpg",
                    CategoryID = 6,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "SYNDOME UPS ICON (800/320W)",
                    Description = "����ͧ���ͧ� �س�Ҿ�ҵ�Ұҹ�ҡ� �ͧ�Ѻ�����ҹ�Ѻ�ػ�ó���ء������ ��䢻ѭ���к�俿�����ҧ����ó�Ẻ ��ҹ��������� Software �Ǻ��� �ٻẺ��硡зѴ�Ѵ �ѹ���� ������� SYNDOME ���Ť���������ͧ�س",
                    Image = "400000271705-a1.jpg",
                    CategoryID = 6,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "ACER MONITOR K242HLbd LED (24)",
                    Description = "����ó�Ẻ����Ѻ�ء�� ���Ҿ������Եõ������Ǵ���� �����٪�蹡���ʴ��ŷ������ö����Ңͧ���������Ѻ�����ҹ�ի�������ѹ ���ʹ͡����ҹ������º����������ͧ�š�Ҵ��¤س�Ҿ�ͧ�Ҿ",
                    Image = "UM.FW3SS.002_a1.jpg",
                    CategoryID = 5,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "SAMSUNG MONITOR S24D300HS LED (24)",
                    Description = "Samsung S24D300HS �����ʺ��ó����Ѻ������ʹ������ ����ʧ�շ����Ѵ����ԧ �������س�����Ѵ��ѧ�ҹ ���س���͡����������ͧ��� ����Ҿ�����Ѵ�����Ҥس�й�觴� �͹�� �����׹�͡���ѧ��¾������",
                    Image = "8806086099431_f1.jpg",
                    CategoryID = 5,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "LG MONITOR 20M38D-B.ATM (19.5)",
                    Description = "����ͧ�ǧ�Ңͧ�س��д�㹤����дǡʺ�� �ա����ѧ ��Ѻ�觾�鹷��ӧҹ�ͧ�س�ӧҹẺ multitasking ��������ᴧ�ŷ������ö�ͧ��繷���դ��������´��͹�ͧੴ��",
                    Image = "8806087596076_a1.jpg",
                    CategoryID = 5,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "PRINTER INKJET EPSON TANK L310 (P)",
                    Description = "����ͧ���鹷�������",
                    Image = "C11CE57501-01.jpg",
                    CategoryID = 10,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
            };
            return devices;
        }

        private Role[] GenerateRoles()
        {
            return new Role[] {
                new Role() {
                    Name = "Admin"
                }
            };
        }

        private Employee[] GenerateEmployees()
        {
            List<Employee> employees = new List<Employee>();

            for (int i = 0; i < 200; i++)
            {
                Employee emp = new Employee()
                {
                    FirstName = MockData.Person.FirstName(),
                    LastName = MockData.Person.Surname(),
                    Email = MockData.Internet.Email(),
                    IsActive = i % 9 == 0 ? false : true,
                    Gender = i % 7 == 0 ? "M" : "F",
                    UniqueKey = Guid.NewGuid(),
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    DepartmentID = MockData.RandomNumber.Next(1, 8)
                };
                employees.Add(emp);
            }

            return employees.ToArray();
        }

        private Stock[] GenerateStocks()
        {
            List<Stock> stocks = new List<Stock>();

            int devicesCount = GenerateDevices().Count();

            for (int i = 1; i <= devicesCount; i++)
            {
                // Three stocks for each movie
                for (int j = 0; j < MockData.RandomNumber.Next(1, 10); j++)
                {
                    Stock stock = new Stock()
                    {
                        DeviceID = i,
                        UniqueKey = Guid.NewGuid(),
                        IsAvailable = true
                    };
                    stocks.Add(stock);
                }
            }

            return stocks.ToArray();
        }

        //private Department[] GenerateDepartments()
        //{
            
        //    List<Department> departments = new List<Department>()
        //    {
        //        new Department()
        //        {
        //            Name = "Engineering",
        //            StartDate = MockData.Utils.RandomDate(_dateFrom, _dateTo)
        //        },
        //        new Department()
        //        {
        //            Name = "Economics",
        //            StartDate = MockData.Utils.RandomDate(_dateFrom, _dateTo)
        //        },
        //        new Department()
        //        {
        //            Name = "Mathematics",
        //            StartDate = MockData.Utils.RandomDate(_dateFrom, _dateTo)
        //        },
        //        new Department()
        //        {
        //            Name = "English",
        //            StartDate = MockData.Utils.RandomDate(_dateFrom, _dateTo)
        //        },
        //        new Department()
        //        {
        //            Name = "Nurse",
        //            StartDate = MockData.Utils.RandomDate(_dateFrom, _dateTo)
        //        },
        //        new Department()
        //        {
        //            Name = "Medicine",
        //            StartDate = MockData.Utils.RandomDate(_dateFrom, _dateTo)
        //        },
        //        new Department()
        //        {
        //            Name = "Human",
        //            StartDate = MockData.Utils.RandomDate(_dateFrom, _dateTo)
        //        }
        //    };
            
        //    return departments.ToArray();
        //}
    }
}
