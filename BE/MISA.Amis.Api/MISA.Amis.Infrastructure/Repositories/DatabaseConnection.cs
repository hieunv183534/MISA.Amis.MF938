﻿using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Infrastructure.Repositories
{
    public class DatabaseConnection
    {
        /// <summary>
        /// Khởi tạo đối tương connector
        /// </summary>
        public static IDbConnection DbConnection
        {
            get { return new MySqlConnection("Host=localhost;Port=3307 ;Database=misa.amis_accountant.mf938 ; User Id=hieunv; Password=12345678"); }
        }
    }
}
