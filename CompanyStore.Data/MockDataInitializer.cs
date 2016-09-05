using CompanyStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Data
{
    public static class MockDataInitializer
    {
        public static List<Category> GenerateCategories()
        {
            List<Category> categories = new List<Category>() 
            {
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
        public static List<Department> GenerateDepartments()
        {
            DateTime fromDate = DateTime.Now.AddYears(-15);
            DateTime toDate = DateTime.Now;

            List<Department> departments = new List<Department>()
            {
                new Department()
                {
                    Name = "Engineering",
                    StartDate = MockData.Utils.RandomDate(fromDate, toDate)
                },
                new Department()
                {
                    Name = "Economics",
                    StartDate = MockData.Utils.RandomDate(fromDate, toDate)
                },
                new Department()
                {
                    Name = "Mathematics",
                    StartDate = MockData.Utils.RandomDate(fromDate, toDate)
                },
                new Department()
                {
                    Name = "English",
                    StartDate = MockData.Utils.RandomDate(fromDate, toDate)
                },
                new Department()
                {
                    Name = "Nurse",
                    StartDate = MockData.Utils.RandomDate(fromDate, toDate)
                },
                new Department()
                {
                    Name = "Medicine",
                    StartDate = MockData.Utils.RandomDate(fromDate, toDate)
                },
                new Department()
                {
                    Name = "Human",
                    StartDate = MockData.Utils.RandomDate(fromDate, toDate)
                }
            };

            return departments;
        }
        public static List<Device> GenerateDevices()
        {
            DateTime _dateFrom = DateTime.Now.AddYears(-15);
            DateTime _dateTo = DateTime.Now;
            List<Device> devices = new List<Device>()
            {
                new Device() {
                    Name = "DELL DESKTOP TW V3653 W260933TH",
                    Description = "คอมพิวเตอร์ตั้งโต๊ะ ที่ครองใจผู้ใช้ทั้งด้านดีไซน์และประสิทธิภาพ ที่มาพร้อมคุณภาพในการใช้งานที่หลากหลาย ทำงานได้ครบทุกฟังก์ชั่น ที่พร้อมจะผสานไลฟ์สไตล์ของคุณได้ลงตัวอย่างไม่มีสะดุด ตอบสนองทุกการใช้งาน เเละทุกความต้องการของคุณได้อย่างลงตัว รับรองว่าถูกใจคอเกมทั้งหลายแน่นอน",
                    Image = "W260933TH_V3650_BLACK_UBUNTU-1.jpg",
                    CategoryID = 3,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "ACER DESKTOP TW TC 710-614G1T00MGi/T002_Ci3",
                    Description = "พีซีประสิทธิภาพสูง พร้อมรูปทรงขนาดกลางดีไซน์เรียบง่าย สง่างามทันสมัยภายนอกสีดำล้วน แข็งแรง ดุดัน",
                    Image = "dt.b15st.002-1.jpg",
                    CategoryID = 3,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "HP DESKTOP TW 550-154l",
                    Description = "พีซีประสิทธิภาพสูง",
                    Image = "889894575241-01.jpg",
                    CategoryID = 3,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "Apple iMAC 27 with Retina 5K/3.2QC/8GB/1TB/M380-THA",
                    Description = "iMac สุดยอดประสบการณ์การใช้งานบนเดสก์ท็อปโดยเริ่มจากการจับคู่จอภาพที่ดีที่สุดเข้ากับโปรเซสเซอร์ กราฟิก และอุปกรณ์จัดเก็บข้อมูลประสิทธิภาพสูงทั้งหมดในตัวเครื่องสุดบางเฉียบ และเรียบเนียนไร้รอยต่อ",
                    Image = "MK462THA-a1.jpg",
                    CategoryID = 3,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "ASUS Smartphone ZENFONE 2 Deluxe-ZE551ML 32GB PURPLE (4G)",
                    Description = "ZENFONE 2 Deluxe-ZE551ML มหัศจรรย์แห่งงานฝีมือและวิศวกรรมที่ผสมผสานกันระหว่างความงามของเซน การแสดงผลที่เหนือระดับอย่างที่ไม่เคยมีมาก่อน การชาร์จที่รวดเร็ว การมองเห็นที่คมชัด และ UI ที่ใช้งานง่ายอย่างไร้ขีดจำกัด ถูกออกแบบให้เป็นเสมือนเพื่อนที่ดีที่สุดของคุณ",
                    Image = "4712900347722-a1.jpg",
                    CategoryID = 4,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "TP-Link USB Wireless Adapter TL-WN723N 150Mbps Mini Wireless N USB Adapter",
                    Description = "TP-link ปล่อยตัวรับสัญญาณ Wireless ซึ่งเป็น USB 2.0 อินเตอร์เฟส มีขนาดกะทัดรัด พกพาไปได้ทุกที ติดตั้งง่าย การเชื่อมต่อไร้สายที่ปลอดภัย ความเร็วในการรับส่งข้อมูลถึง 150Mbps",
                    Image = "6935364050559-e1.jpg",
                    CategoryID = 9,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "Linksys Router Wireless WRT54GL Wireless-G Broadband Router",
                    Description = "Linksys WRT54GL Wireless-G Router อุปกรณ์ Wireless-G Broadband Router (802.11g) ความเร็ว 54 Mbps ใช้สำหรับแชร์อินเทอร์เน็ตความเร็วสูงให้แก่ลูกข่าย ผ่านระบบแลนและระบบไวร์เลส",
                    Image = "745883568291-1.jpg",
                    CategoryID = 9,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "Dell Notebook 3459-W561077TH-BLACK-WIN10",
                    Description = "โน๊ตบุ้ครุ่นใหม่ ที่มีประสิทธิภาพ สามารให้คุณใช้งานได้หลากหลาย ทำงานได้พร้อมกันแบบไม่มีสะดุด มีการออกแบบดีไซน์ที่สวยเท่ห์ ทันสมัย คงทนแข็งแรง หน้าจอขนาด 14.0 นิ้ว หน้าจอกว้างสำหรับการทำงานที่เต็มตา เต็มอรรถรสมากขึ้น ทั้งความละเอียดหน้าจอ 1366*768 พิกเซล ทำให้หน้าจอมีสีสันสวย สดใส ดูสมจริง",
                    Image = "W561077TH-3459-BLACK-WIN10-1.jpg",
                    CategoryID = 2,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "HP Notebook Pavilion 15-ab555TX",
                    Description = "การออกแบบที่สง่างาม โดดเด่น หรูหราและงดงาม ในมุมมองที่กว้างจนคุณสัมผัสได้ ใช้วัสดุคุณภาพสูง ไม่เพียงแต่สวยงาม ยังคงความแข็งแรงอีกด้วย",
                    Image = "889894987747-01.jpg",
                    CategoryID = 2,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "Apple iPad Pro 9.7 inch Wi-Fi + Cellular 32GB - Space Grey",
                    Description = "iPad Pro รุ่น 9.7 นิ้วพกพาสะดวกเหลือเชื่อ พร้อมด้วยการผสมผสานที่ลงตัวของประสิทธิภาพ และความอเนกประสงค์ในแบบที่คุณไม่เคยสัมผัสที่ไหนมาก่อน",
                    Image = "888462831345-1.jpg",
                    CategoryID = 1,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "Asus Tablet Z370CG-1B005A White",
                    Description = "ASUS ZenPad 7.0 (Z370CG) ความหรูหราอย่างที่ใจคุณต้องการ",
                    Image = "4712900063196-01.jpg",
                    CategoryID = 1,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "Logitech Keyboard USB K120",
                    Description = "ดีไซน์ป้องกันของเหลวหกใส่ ของเหลวจะไหลออกจากคีย์บอร์ดของคุณ, จึงไม่ต้องกังวลว่ามันจะเสียจากอุบัติเหตุเช่นน้ำหกใส่ รูปร่างผอมบางคีย์บอร์ดมีปุ่มบางที่ช่วยให้มันดูดีบนโต๊ะของคุณ ในขณะที่ทำให้มือของคุณสบายมากขึ้น และอยู่ในท่าที่ไม่เกร็ง",
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
                    Description = "รูปร่างที่ดีไม่ว่าจะมือซ้ายหรือมือขวา คุณจะรู้สึกสบายในการใช้งานนานนับชั่วโมง ด้วยขนาดมาตรฐาน และออกแบบให้ใช้ได้ทั้งสองมือ",
                    Image = "97855089250_01.jpg",
                    CategoryID = 8,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "Microsoft Mouse L2 Basic Optical - Black",
                    Description = "Basic Optical Mouse เมาส์แบบมีสาย ดีไซน์กระชับมือ เชื่อมต่อแบบสายผ่านพอร์ต USB โดยออกแแบบให้ใช้ได้ทั้งมือซ้ายและมือขวาเหมาะแก่การใช้งาน",
                    Image = "885370433845-1.jpg",
                    CategoryID = 8,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "CHUPHOTIC UPS MS1200 (1200VA/600W)",
                    Description = "ทำงานภายใต้สภาวะที่มีความเสี่ยงทางไฟฟ้าได้ทุกรูปแบบเมื่อเชื่อมต่อผ่านเครื่องสำรองไฟฟ้า CHUPHOTIC Mercury Smart MS1200 ที่มาพร้อมเทคโนโลยีอันทันสมัย",
                    Image = "8859255800925-1.jpg",
                    CategoryID = 6,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "SYNDOME UPS ICON (800/320W)",
                    Description = "เครื่องสำรองไฟ คุณภาพมาตราฐานสากล รองรับการใช้งานกับอุปกรณ์ได้ทุกประเภท แก้ไขปัญหาระบบไฟฟ้าอย่างสมบูรณ์แบบ ใช้งานง่ายและมี Software ควบคุม รูปแบบเล็กกะทัดรัด ทันสมัย ไว้ใจให้ SYNDOME ดูแลคอมพิวเตอร์ของคุณ",
                    Image = "400000271705-a1.jpg",
                    CategoryID = 6,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "ACER MONITOR K242HLbd LED (24)",
                    Description = "สมบูรณ์แบบสำหรับทุกคน จอภาพที่เป็นมิตรต่อสิ่งแวดล้อม คือโซลูชั่นการแสดงผลที่สามารถเป็นเจ้าของได้ง่ายสำหรับการใช้งานพีซีในแต่ละวัน นำเสนอการใช้งานที่เรียบง่ายโดยไม่ต้องแลกมาด้วยคุณภาพของภาพ",
                    Image = "UM.FW3SS.002_a1.jpg",
                    CategoryID = 5,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "SAMSUNG MONITOR S24D300HS LED (24)",
                    Description = "Samsung S24D300HS ให้ประสบการณ์การรับชมที่ยอดเยี่ยม ให้แสงสีที่คมชัดสมจริง ช่วยให้คุณประหยัดพลังงาน ให้คุณเลือกได้ตามความต้องการ ให้ภาพที่คมชัดไม่ว่าคุณจะนั่งดู นอนดู หรือยืนออกกำลังกายพร้อมดู",
                    Image = "8806086099431_f1.jpg",
                    CategoryID = 5,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "LG MONITOR 20M38D-B.ATM (19.5)",
                    Description = "ปกป้องดวงตาของคุณและดูในความสะดวกสบาย อีกทั้งยัง ปรับแต่งพื้นที่ทำงานของคุณทำงานแบบ multitasking พร้อมการแดงผลที่สามารถมองเห็นที่มีความระเอียดอ่อนของเฉดสี",
                    Image = "8806087596076_a1.jpg",
                    CategoryID = 5,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
                new Device() {
                    Name = "PRINTER INKJET EPSON TANK L310 (P)",
                    Description = "เครื่องปริ้นท์หัวเข็ม",
                    Image = "C11CE57501-01.jpg",
                    CategoryID = 10,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    Price = MockData.RandomNumber.Next(50, 100000)
                },
            };
            return devices;
        }
        public static List<Stock> GenerateStocks()
        {
            List<Stock> stocks = new List<Stock>();
            int devicesCount = MockDataInitializer.GenerateDevices().Count();
            for (int i = 1; i <= devicesCount; i++)
            {
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
            return stocks;
        }
        //public static List<Rental> GenerateRentals()
        //{
        //    DateTime _dateFrom = DateTime.Now.AddYears(-10);
        //    DateTime _dateTo = DateTime.Now;
            
        //    List<Rental> rentals = new List<Rental>(); 
        //    for (int i = 1; i <= 21; i++)
        //    {
        //        Rental rental = new Rental()
        //        {
        //            EmployeeID = MockData.RandomNumber.Next(1, 10),
        //            StockID = i,
        //            RentalDate = MockData.Utils.RandomDate(_dateFrom, _dateTo.AddYears(-1)),
        //            ReturnedDate = MockData.Utils.RandomDate(_dateTo.AddYears(-1), _dateTo),
        //            Status = "Returned"
        //        };
        //        rentals.Add(rental);
        //    }
        //    return rentals;
        //}
        public static List<Role> GenerateRoles()
        {
            List<Role> roles = new List<Role>()
            {
                new Role() {
                    Name = "Admin"
                }
            };
            return roles;
        }
        public static List<Employee> GenerateEmployees()
        {
            DateTime _dateFrom = DateTime.Now.AddYears(-10);
            DateTime _dateTo = DateTime.Now;
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
            return employees;
        }
    }
}
