using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DBExtract
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("=====데이터베이스 추출 프로그램=====");
            Console.WriteLine("제작: 2020 Hello World 동아리");
            Console.WriteLine("사용법은 readme.txt를 참고해주세요.");
            Console.WriteLine("숫자를 입력하고 엔터키를 누르세요.");
            Console.WriteLine("1. 데이터 추출");
            int n = int.Parse(Console.ReadLine());
            Program p = new Program();
            switch (n)
            {
                case 1:
                    p.Save();
                    break;

                default:
                    break;
            }
            Console.WriteLine("아무키나 눌러 프로그램을 종료하세요.");
            Console.ReadKey();
        }

        public void Save()
        {
            var l = new List<Book>();
            var book = new Book();

            string server, port, uid, pwd, database;
            Console.WriteLine("접속할 서버를 입력해주세요. (기본: localhost)");
            server = Console.ReadLine();
            Console.WriteLine("포트를 입력해주세요. (기본: 3306)");
            port = Console.ReadLine();
            Console.WriteLine("UID를 입력해주세요. (기본: root)");
            uid = Console.ReadLine();
            Console.WriteLine("비밀번호를 입력해주세요. (기본: autoset)");
            pwd = Console.ReadLine();
            Console.WriteLine("접근할 데이터베이스를 입력해주세요. (기본: db1)");
            database = Console.ReadLine();

            string strConn = string.Format("server={0};port={1};uid={2};pwd={3};database={4};charset=utf8 ;", server, port, uid, pwd, database);
            MySqlConnection conn = new MySqlConnection(strConn);
            conn.Open();
            string sql = "SELECT * FROM `books`";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader table = cmd.ExecuteReader();

            while (table.Read())
            {
                book.num = (int)table["num"];
                book.reg_num = (string)table["reg_num"];
                book.name = (string)table["name"];
                book.vol = (string)table["vol"];
                book.author = (string)table["author"];
                book.publisher = (string)table["publisher"];
                book.year = (int)table["year"];
                book.symbol = (string)table["symbol"];
                book.reg_day = (string)table["reg_day"];
                book.available = (string)table["available"];
                book.place = (string)table["place"];
                l.Add(book);
            }
            Console.WriteLine("파일 작성을 시작합니다...");
            var fs = new FileStream("Database.dat", FileMode.Create);

            var formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, l);
                Console.WriteLine("파일 작성을 완료했습니다.");
            }
            catch
            {
                Console.WriteLine("파일 작성 중 오류 발생!");
                throw;
            }
            finally
            {
                fs.Close();
            }
        }
    }
}