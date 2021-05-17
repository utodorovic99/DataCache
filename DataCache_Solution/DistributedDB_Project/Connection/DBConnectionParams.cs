using DistributedDB_Project.Exceptions.ExceptionAbstraction;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DistributedDB_Project.Connection
{
    internal class DBConnectionParams
    {
        private static string localDataSource = "";
        private static  string userID= "";
        private static  string password = "";
        private static string configurationFile = ""; 

        public static string LOCAL_DATA_SOURCE {get => localDataSource;}
        public static string USER_ID    {get => userID; }
        public static string PASSWORD   {get => password;}

        public static void LoadLoginParams()
        {
            configurationFile = ConfigurationManager.AppSettings.Get("DBParamsSrc");

            string loginParams = System.IO.File.ReadAllText(Path.GetFullPath(configurationFile));
            Regex sourceRX = new Regex(@"\s*LOCAL_DATA_SOURCE\s*=\s*(?<src>//[^\s^\r^\n^;]{12,})(\s|\r\n)*;",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

            Regex uidRX = new Regex(@"\s*USER_ID\s*=\s*(?<uid>[^\r\n]{8,})(\s|\r\n)*;",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

            Regex passwordRX = new Regex(@"\s*PASSWORD\s*=\s*(?<pass>[^\r\n]{3,})(\s|\r\n)*;",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

            MatchCollection matches;
            GroupCollection groups;


            //Current implementation: Only one DB & Connection supported
            if((matches=sourceRX.Matches(loginParams)).Count == 0 || matches.Count>1)   
            {
                throw new DBLoginFailed("DB login configuration file corrupted, call support", configurationFile, "SOURCE");
            }
            groups = matches[0].Groups;
            localDataSource = groups["src"].ToString();

            if ((matches = uidRX.Matches(loginParams)).Count == 0 || matches.Count > 1)
            {
                throw new DBLoginFailed("DB login configuration file corrupted, call support", configurationFile, "USER_ID");
            }
            groups = matches[0].Groups;
            userID = groups["uid"].ToString(); ;

            if ((matches = passwordRX.Matches(loginParams)).Count == 0 || matches.Count > 1)
            {
                throw new DBLoginFailed("DB login configuration file corrupted, call support", configurationFile, "PASSWORD");
            }
            groups = matches[0].Groups;
            password = groups["pass"].ToString();

        }
    }
}
