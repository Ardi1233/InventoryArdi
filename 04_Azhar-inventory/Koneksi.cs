using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Daftar_Pepustakaan
{
    class Koneksi
    {
        public MySqlConnection GetConn()
        {
            MySqlConnection Conn = new MySqlConnection();
            Conn.ConnectionString = "server=localhost;user=root;password=;database=dmarket";
            return Conn;
        }
    }
}
