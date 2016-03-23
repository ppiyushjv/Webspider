using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSpider.Data.DatabaseManager;
using WebSpider.Objects.General;

namespace WebSpider.Data.General
{
    public class TaskDetailManager : DataManager
    {
        #region [ Constructor ]
        public TaskDetailManager(String ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }
        #endregion

        #region [ Get Data ]
        public List<TaskDetail> GetData()
        {
            String Query = "SELECT * FROM TaskDetail";
            SqlCeDataManager oDm = new SqlCeDataManager(ConnectionString, Query, true);
            return DataParser.ToList<TaskDetail>(oDm.GetTable());
        }

        public List<TaskDetail> GetDataByHeader(Int64 TaskHeaderID)
        {
            String Query = "SELECT * FROM TaskDetail WHERE TaskHeaderID = @TaskHeaderID";
            SqlCeDataManager oDm = new SqlCeDataManager(ConnectionString, Query, true);
            oDm.AddIntegerBigPara("TaskHeaderID", TaskHeaderID);
            return DataParser.ToList<TaskDetail>(oDm.GetTable());
        }

        public List<TaskDetail> GetDataById(Int64 TaskID)
        {
            String Query = "SELECT * FROM TaskDetail WHERE TaskID = @TaskID";
            SqlCeDataManager oDm = new SqlCeDataManager(ConnectionString, Query, true);
            oDm.AddIntegerBigPara("TaskID", TaskID);
            return DataParser.ToList<TaskDetail>(oDm.GetTable());
        }

        public List<TaskDetail> GetDataByHeaderId(Int64 TaskHeaderID)
        {
            String Query = "SELECT * FROM TaskDetail WHERE TaskHeaderID = @TaskHeaderID";
            SqlCeDataManager oDm = new SqlCeDataManager(ConnectionString, Query, true);
            oDm.AddIntegerBigPara("TaskHeaderID", TaskHeaderID);
            return DataParser.ToList<TaskDetail>(oDm.GetTable());
        }

        public List<TaskDetail> GetDataByStatus(Int64 TaskHeaderID, Int32 TaskStatus)
        {
            String Query = "SELECT * FROM TaskDetail WHERE TaskHeaderID = @TaskHeaderID AND TaskStatus = @TaskStatus";
            SqlCeDataManager oDm = new SqlCeDataManager(ConnectionString, Query, true);
            oDm.AddIntegerBigPara("TaskHeaderID", TaskHeaderID);
            oDm.AddIntegerPara("TaskStatus", TaskStatus);
            return DataParser.ToList<TaskDetail>(oDm.GetTable());
        }

        public List<TaskDetail> GetDataByStatus(Int64 TaskHeaderID, Int32[] TaskStatus)
        {
            String Query = "SELECT * FROM TaskDetail WHERE TaskHeaderID = " + TaskHeaderID.ToString() 
                + " AND TaskStatus IN (" + string.Join(",", TaskStatus.Select(x => x.ToString())) + ")";
            SqlCeDataManager oDm = new SqlCeDataManager(ConnectionString, Query, true);
            return DataParser.ToList<TaskDetail>(oDm.GetTable());
        }

        #region [ Get Data By Site and Status]
        public List<TaskDetail> GetDataBySite(Int64 TaskHeaderID, String TaskSite, Int32 TaskStatus)
        {
            String Query = "SELECT * FROM TaskDetail WHERE TaskSite = @TaskSite AND TaskStatus = @TaskStatus";
            SqlCeDataManager oDm = new SqlCeDataManager(ConnectionString, Query, true);
            oDm.AddVarcharPara("TaskSite", 4000, TaskSite);
            oDm.AddIntegerPara("TaskStatus", TaskStatus);
            return DataParser.ToList<TaskDetail>(oDm.GetTable());
        }

        public List<TaskDetail> GetDataBySite(Int64 TaskHeaderID, String TaskSite, Int32[] TaskStatus)
        {
            String Query = "SELECT * FROM TaskDetail WHERE TaskHeaderID = @TaskHeaderID AND TaskSite = @TaskSite AND TaskStatus IN ({0})";
            Query = String.Format(Query, String.Join(",", TaskStatus.Select(x => x.ToString()).ToArray()));
            SqlCeDataManager oDm = new SqlCeDataManager(ConnectionString, Query, true);
            oDm.AddIntegerBigPara("TaskHeaderID", TaskHeaderID);
            oDm.AddVarcharPara("TaskSite", 4000, TaskSite);
            return DataParser.ToList<TaskDetail>(oDm.GetTable());
        }
        #endregion

        public List<TaskDetail> GetDataByTaskDetail(Int64 TaskHeaderID, String TaskSite, String TaskMode, String TaskType, String TaskNameValue)
        {
            String Query = "SELECT * FROM TaskDetail WHERE TaskSite = @TaskSite AND TaskType = @TaskType AND TaskMode = @TaskMode AND TaskNameValue = @TaskNameValue";
            SqlCeDataManager oDm = new SqlCeDataManager(ConnectionString, Query, true);
            oDm.AddVarcharPara("TaskSite", 4000, TaskSite);
            oDm.AddVarcharPara("TaskType", 4000, TaskType);
            oDm.AddVarcharPara("TaskMode", 4000, TaskMode);
            oDm.AddVarcharPara("TaskNameValue", 4000, TaskNameValue);
            return DataParser.ToList<TaskDetail>(oDm.GetTable());
        }

        public List<TaskDetail> GetDataBySite(Int64 TaskHeaderID, String TaskSite, String TaskMode, Int32 TaskStatus)
        {
            String Query = "SELECT * FROM TaskDetail WHERE TaskSite = @TaskSite AND TaskStatus = @TaskStatus AND TaskMode = @TaskMode";
            SqlCeDataManager oDm = new SqlCeDataManager(ConnectionString, Query, true);
            oDm.AddVarcharPara("TaskSite", 4000, TaskSite);
            oDm.AddIntegerPara("TaskStatus", TaskStatus);
            oDm.AddVarcharPara("TaskMode", 4000, TaskMode);
            return DataParser.ToList<TaskDetail>(oDm.GetTable());
        }

        public List<TaskDetail> GetDataBySite(Int64 TaskHeaderID, String TaskSite, String TaskMode, Int32[] TaskStatus)
        {
            String Query = "SELECT * FROM TaskDetail WHERE TaskSite = @TaskSite AND TaskStatus = @TaskStatus AND TaskMode IN ({0})";
            Query = String.Format(Query, String.Join(",", TaskStatus.Select(x => x.ToString()).ToArray()));
            SqlCeDataManager oDm = new SqlCeDataManager(ConnectionString, Query, true);
            oDm.AddVarcharPara("TaskSite", 4000, TaskSite);
            oDm.AddVarcharPara("TaskMode", 4000, TaskMode);
            return DataParser.ToList<TaskDetail>(oDm.GetTable());
        }

        #endregion

        #region [ Get Schedule Tasks Pending ]
        public List<TaskDetail> GetDataScheduledPendingTasks()
        {
            String Query = "SELECT	TD.* FROM	TaskHeader TH WITH (NOLOCK) "
                    + "JOIN TaskDetail TD WITH (NOLOCK) ON TH.ScheduleID = TD.TaskHeaderID "
                    + "WHERE TH.NextRun IS NOT NULL AND TH.NextRun <= GETDATE()";
            SqlCeDataManager oDm = new SqlCeDataManager(ConnectionString, Query, true);
            return DataParser.ToList<TaskDetail>(oDm.GetTable());
        }

        public List<TaskDetail> GetDataScheduledPendingTasks(String TaskSite)
        {
            String Query = "SELECT	TD.* FROM	TaskHeader TH WITH (NOLOCK) "
                    + "JOIN TaskDetail TD WITH (NOLOCK) ON TH.ScheduleID = TD.TaskHeaderID "
                    + "WHERE TH.NextRun IS NOT NULL AND TH.NextRun <= GETDATE() AND TaskSite = @TaskSite";
            SqlCeDataManager oDm = new SqlCeDataManager(ConnectionString, Query, true);
            oDm.AddVarcharPara("TaskSite", 4000, TaskSite);
            return DataParser.ToList<TaskDetail>(oDm.GetTable());
        }
        #endregion

        #region [ Insert ]
        public int Insert(Int64 TaskID, Int64 TaskHeaderID, String TaskNameText, String TaskNameValue, String TaskStatusText, int TaskStatus, Boolean DownloadImages, Boolean IgnitoMode, String TaskType, String TaskMode, String TaskSite)
        {
            String Query = "INSERT INTO TaskDetail (TaskHeaderID, TaskNameText ,TaskNameValue ,TaskStatusText ,TaskStatus, DownloadImages, IgnitoMode, TaskType, TaskMode ,TaskSite ,CreatedOn, UpdatedOn) "
                + "VALUES (@TaskHeaderID, @TaskNameText ,@TaskNameValue ,@TaskStatusText ,@TaskStatus, @DownloadImages, @IgnitoMode ,@TaskType, @TaskMode ,@TaskSite ,@CreatedOn, @UpdatedOn)";
            SqlCeDataManager oDm = new SqlCeDataManager(ConnectionString, Query, true);
            oDm.AddIntegerBigPara("TaskHeaderID", TaskHeaderID);
            oDm.AddVarcharPara("TaskNameText", 4000, TaskNameText);
            oDm.AddVarcharPara("TaskNameValue", 4000, TaskNameValue);
            oDm.AddVarcharPara("TaskStatusText", 4000, TaskStatusText);
            oDm.AddIntegerPara("TaskStatus", TaskStatus);
            oDm.AddBoolPara("DownloadImages", DownloadImages);
            oDm.AddBoolPara("IgnitoMode", IgnitoMode);
            oDm.AddVarcharPara("TaskType", 4000, TaskType);
            oDm.AddVarcharPara("TaskMode", 4000, TaskMode);
            oDm.AddVarcharPara("TaskSite", 4000, TaskSite);
            oDm.AddDateTimePara("CreatedOn", DateTime.Now);
            oDm.AddDateTimePara("UpdatedOn", null);
            return oDm.RunActionQuery();
        }
        #endregion
        
        #region [ Update ] 
        public int Update(Int64 TaskID, Int64 TaskHeaderID, string TaskNameText, string TaskNameValue, string TaskStatusText, int TaskStatus, bool DownloadImages, bool IgnitoMode, string TaskType, String TaskMode, string TaskSite)
        {
            String Query = "UPDATE TaskDetail SET TaskHeaderID = @TaskHeaderID, TaskNameText = @TaskNameText, TaskNameValue = @TaskNameValue, TaskStatusText = @TaskStatusText, TaskStatus = @TaskStatus, "
                + "DownloadImages = @DownloadImages, IgnitoMode = @IgnitoMode, TaskType = @TaskType, TaskMode = @TaskMode, TaskSite = @TaskSite, UpdatedOn = @UpdatedOn WHERE TaskID = @TaskID";
            SqlCeDataManager oDm = new SqlCeDataManager(ConnectionString, Query, true);
            oDm.AddIntegerBigPara("TaskID", TaskID);
            oDm.AddIntegerBigPara("TaskHeaderID", TaskHeaderID);
            oDm.AddVarcharPara("TaskNameText", 4000, TaskNameText);
            oDm.AddVarcharPara("TaskNameValue", 4000, TaskNameValue);
            oDm.AddVarcharPara("TaskStatusText", 4000, TaskStatusText);
            oDm.AddIntegerPara("TaskStatus", TaskStatus);
            oDm.AddBoolPara("DownloadImages", DownloadImages);
            oDm.AddBoolPara("IgnitoMode", IgnitoMode);
            oDm.AddVarcharPara("TaskType", 4000, TaskType);
            oDm.AddVarcharPara("TaskMode", 4000, TaskMode);
            oDm.AddVarcharPara("TaskSite", 4000, TaskSite);
            oDm.AddDateTimePara("UpdatedOn", DateTime.Now);
            return oDm.RunActionQuery();
        }

        public int UpdateDownloadImage(Int64 TaskID, Boolean DownloadImage)
        {
            String Query = "UPDATE TaskDetail SET DownloadImage = @DownloadImage, UpdatedOn = @UpdatedOn) WHERE TaskID = @TaskID";
            SqlCeDataManager oDm = new SqlCeDataManager(ConnectionString, Query, true);
            oDm.AddIntegerBigPara("TaskID", TaskID);
            oDm.AddBoolPara("DownloadImage", DownloadImage);
            oDm.AddDateTimePara("UpdatedOn", DateTime.Now);
            return oDm.RunActionQuery();
        }

        public int UpdateIgnitoMode(Int64 TaskID, Boolean IgnitoMode)
        {
            String Query = "UPDATE TaskDetail SET IgnitoMode = @IgnitoMode, UpdatedOn = @UpdatedOn) WHERE TaskID = @TaskID";
            SqlCeDataManager oDm = new SqlCeDataManager(ConnectionString, Query, true);
            oDm.AddIntegerBigPara("TaskID", TaskID);
            oDm.AddBoolPara("IgnitoMode", IgnitoMode);
            oDm.AddDateTimePara("UpdatedOn", DateTime.Now);
            return oDm.RunActionQuery();
        }

        public int UpdateStatus(Int64 TaskID, String TaskStatusText, int TaskStatus)
        {
            String Query = "UPDATE TaskDetail SET TaskStatusText = @TaskStatusText, TaskStatus = @TaskStatus, UpdatedOn = @UpdatedOn WHERE TaskID = @TaskID";
            SqlCeDataManager oDm = new SqlCeDataManager(ConnectionString, Query, true);
            oDm.AddIntegerBigPara("TaskID", TaskID);
            oDm.AddVarcharPara("TaskStatusText", 4000, TaskStatusText);
            oDm.AddIntegerPara("TaskStatus", TaskStatus);
            oDm.AddDateTimePara("UpdatedOn", DateTime.Now);
            return oDm.RunActionQuery();
        }
        #endregion

        #region [ Delete ]
        public int DeleteAll()
        {
            String Query = "DELETE FROM TaskDetail";
            SqlCeDataManager oDm = new SqlCeDataManager(ConnectionString, Query, true);
            return oDm.RunActionQuery();
        }

        public int DeleteById(Int64 TaskID)
        {
            String Query = "DELETE FROM TaskDetail WHERE TaskID = @TaskID";
            SqlCeDataManager oDm = new SqlCeDataManager(ConnectionString, Query, true);
            oDm.AddIntegerBigPara("TaskID", TaskID);
            return oDm.RunActionQuery();
        }

        public int DeleteByHeaderId(Int64 TaskHeaderID)
        {
            String Query = "DELETE FROM TaskDetail WHERE TaskHeaderID = @TaskHeaderID";
            SqlCeDataManager oDm = new SqlCeDataManager(ConnectionString, Query, true);
            oDm.AddIntegerBigPara("TaskHeaderID", TaskHeaderID);
            return oDm.RunActionQuery();
        }
        #endregion



        
    }
}
