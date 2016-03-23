using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebSpider.Core
{
    public class CacheDb
    {
        #region [Constants]
        private const String _dsName = "UrlCache";
        private const String _dtName = "Cache";
        #endregion

        #region [Private Variables]
        private static String CacheDBPath;
        private static String CacheDBName;
        private static DataSet CacheDB;
        protected static int _cacheValidity = 3600; // in minutes
        private static object CacheLock = new object();
        #endregion

        #region [Constructor]
        static CacheDb()
        {
            //_cacheValidity = Convert.ToInt32(ConfigurationSettings.AppSettings["CacheTimeout"].ToString());
            if (Settings.GetValue("EnableCaching") == true)
                _cacheValidity = Settings.GetValue("CacheDuration");
            else
                _cacheValidity = 0;

            CacheDBPath = String.Format("{0}\\Cache", Application.StartupPath);
            CacheDBName = String.Format("{0}\\Cache.xml", CacheDBPath);
            CacheDB = new DataSet(_dsName);
            CacheDB.Tables.Add(new DataTable(_dtName));
            CacheDB.Tables[_dtName].Columns.Add("Url", typeof(String));
            CacheDB.Tables[_dtName].Columns.Add("FileName", typeof(String));
            CacheDB.Tables[_dtName].Columns.Add("LastUpdated", typeof(DateTime));
            CacheDB.Tables[_dtName].Columns.Add("ValidTill", typeof(DateTime));

            CacheDB.Tables[_dtName].Columns["LastUpdated"].DefaultValue = DateTime.Now;
            CacheDB.Tables[_dtName].Columns["ValidTill"].DefaultValue = DateTime.Now.AddMinutes(_cacheValidity);
            LoadCache();
        }
        #endregion

        #region [Load/Save Cache]
        /// <summary>
        /// Loads cache from disk
        /// </summary>
        private static void LoadCache()
        {
            try
            {
                lock (CacheLock)
                {
                    CacheDB.ReadXml(CacheDBName);
                    ValidateCache();

                    List<String> CacheFiles = CacheDB.Tables[_dtName].AsEnumerable().Select(x => GetFullFileName(x.Field<String>("FileName"))).ToList();
                    List<String> DirFiles = Directory.GetFiles(CacheDBPath).ToList();

                    DirFiles.Remove(GetFullFileName("Cache.xml"));
                    DirFiles.Remove(GetFullFileName("Readme.txt"));

                    foreach (String file in DirFiles.Except(CacheFiles))
                    {
                        new FileInfo(file).Delete();
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Saves cache to disk
        /// </summary>
        protected static void SaveCache()
        {
            try
            {
                lock (CacheLock)
                {
                    CacheDB.WriteXml(CacheDBName);
                }
            }
            catch
            {
                throw new Exception("Saving cache failed");
            }
        }
        #endregion

        #region [Generate new FileName]
        /// <summary>
        /// Generates a new filename for cache
        /// </summary>
        /// <returns></returns>
        private static String GenerateFileName()
        {
            List<String> filenames = CacheDB.Tables[_dtName].AsEnumerable().Select(x => x.Field<String>("FileName")).ToList();
            String FileName;
            do
            {
                FileName = Guid.NewGuid().ToString();
            } while (filenames.Contains(FileName));
            return FileName;
        }
        #endregion

        #region [Get Full Filename Including Path]
        /// <summary>
        /// GEt Full Filename including path
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        private static string GetFullFileName(String FileName)
        {
            return String.Format("{0}\\{1}", CacheDBPath, FileName);
        }
        #endregion

        #region [Validate Cache]
        protected static void ValidateCache()
        {
            //var rows = CacheDB.Tables[_dtName].AsEnumerable().Where(x => x.Field<DateTime>("ValidTill") <= DateTime.Now);
            // delete outdated files
            // delete invalid data from cache db
            bool CacheUpdated = false;
            try
            {
                if (!ReferenceEquals(CacheDB, null))
                {
                    lock (CacheDB)
                    {
                        for (int index = CacheDB.Tables[_dtName].Rows.Count - 1; index >= 0; index--)
                        {
                            DataRow dRow = CacheDB.Tables[_dtName].Rows[index];

                            if ((DateTime)dRow["ValidTill"] <= DateTime.Now)
                            {
                                File.Delete(GetFullFileName(dRow["FileName"].ToString()));
                                dRow.Delete();
                                CacheUpdated = true;
                            }
                            else if (!File.Exists(GetFullFileName(dRow["FileName"].ToString())))
                            {
                                dRow.Delete();
                                CacheUpdated = true;
                            }
                        }
                    }
                }
            }
            catch { }
            finally
            {
                if (CacheUpdated)
                    SaveCache();
            }
        }
        #endregion

        #region [Get Cached Url]
        /// <summary>
        /// Get Cached Url
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>

        public byte[] GetCachedUrl(String Url)
        {
            var CacheDBRow = CacheDB.Tables[_dtName].AsEnumerable().Where(x => x.Field<String>("Url") == Url);
            if (CacheDBRow.Count() == 1)
            {
                String FileName = CacheDBRow.Select(x => x.Field<String>("FileName")).FirstOrDefault();
                DateTime LastUpdated = CacheDBRow.Select(x => x.Field<DateTime>("LastUpdated")).FirstOrDefault();
                DateTime ValidTill = CacheDBRow.Select(x => x.Field<DateTime>("ValidTill")).FirstOrDefault();

                byte[] responseBytes = File.ReadAllBytes(GetFullFileName(FileName));
                return responseBytes;
            }
            return null;
        }
        #endregion

        #region [Save Cache]
        
        public static void SaveCache(String Url, byte[] urlData)
        {
            try
            {
                lock (CacheLock)
                {
                    String FileName = GenerateFileName();
                    File.WriteAllBytes(GetFullFileName(FileName), urlData);
                    DataRow dRow = CacheDB.Tables[_dtName].NewRow();
                    dRow["Url"] = Url;
                    dRow["FileName"] = FileName;
                    dRow["LastUpdated"] = DateTime.Now;
                    dRow["ValidTill"] = DateTime.Now.AddMinutes(_cacheValidity);
                    CacheDB.Tables[_dtName].Rows.Add(dRow);
                    CacheDB.WriteXml("Cache\\Cache.xml");
                }
            }
            catch { }
        }
        #endregion

        #region [Is Cached Url]
        /// <summary>
        /// Checks whether URL is cached
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public Boolean IsCachedUrl(String Url)
        {
            //return false;
            if (!ReferenceEquals(CacheDB, null) && !ReferenceEquals(CacheDB.Tables[_dtName], null) && CacheDB.Tables[_dtName].Rows.Count > 0)
            {
                int rowsFound = 0;
                //ValidateCache();

                var row = CacheDB.Tables[_dtName].AsEnumerable().Where(x => x.Field<String>("Url").Equals(Url)).FirstOrDefault();

                if (ReferenceEquals(row, null))
                    return false;
                else if (row.Field<DateTime>("ValidTill") < DateTime.Now || !File.Exists(row.Field<String>("FileName")))
                {
                    lock (CacheDB)
                    {
                        CacheDB.Tables[_dtName].Rows.Remove(row);
                        SaveCache();
                    }
                    return false;
                }
                else
                    return true;
                
                return rowsFound == 1 ? true : false;
            }
            return false;
        }
        #endregion

        #region [ Clear Cache ]
        public void Clear()
        {
            lock (CacheLock)
            {
                List<String> DirFiles = Directory.GetFiles(CacheDBPath).ToList();

                DirFiles.Remove(GetFullFileName("Cache.xml"));
                DirFiles.Remove(GetFullFileName("Readme.txt"));

                foreach (String file in DirFiles)
                {
                    new FileInfo(file).Delete();
                }
                ValidateCache();
            }
        }
        #endregion
    }
}
