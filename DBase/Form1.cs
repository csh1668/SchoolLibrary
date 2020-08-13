using DBExtract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace DBase
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 책 리스트입니다. Form1 생성자에서 파일을 읽어옵니다.
        /// </summary>
        private List<Book> l = new List<Book>();

        public Form1()
        {
            InitializeComponent();
            try
            {
                FileStream fs = new FileStream("Database.dat", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                l = (List<Book>)formatter.Deserialize(fs);
                Console.WriteLine(l[0].name);
            }
            catch
            {
                MessageBox.Show("Database.dat 파일이 존재하지 않습니다.\n 데이터 파일이 실행파일과 같은 위치에 있나요?");
                Close();
            }
        }

        /// <summary>
        /// 입력한 검색어와 정보가 일치하는 책을 그리드로 출력합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string search = textBox1.Text;
            if (search == string.Empty)
            {
                MessageBox.Show("검색어를 입력해주세요.");
                return;
            }

            dataGridView1.Rows.Clear();

            for (int i = 0; i < l.Count; i++)
            {
                if (l[i].name.Contains(search) ||
                    l[i].vol.Contains(search) ||
                    l[i].reg_num.Contains(search) ||
                    l[i].symbol.Contains(search) ||
                    l[i].author.Contains(search) ||
                    l[i].publisher.Contains(search))
                {
                    dataGridView1.Rows.Add(l[i].reg_num, l[i].name, l[i].author, l[i].publisher, l[i].symbol, l[i].available);
                }
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }
    }
}