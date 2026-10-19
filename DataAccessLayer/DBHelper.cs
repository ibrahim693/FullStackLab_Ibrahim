using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace DataAccessLayer
{
    public class DBHelper
    {
        public static NpgsqlConnection GetConnection() {
            return new NpgsqlConnection("Host=aws-0-ap-northeast-1.pooler.supabase.com;Port=5432;Database=postgres;Username=postgres.mefpigulipyaeapwjnke;Password=Muhammadd@12345#;SSL Mode=Require;Trust Server Certificate=true");
        }
    }
}
