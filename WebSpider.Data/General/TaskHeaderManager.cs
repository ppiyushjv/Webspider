using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSpider.Data.DatabaseManager;
using WebSpider.Objects.General;

namespace WebSpider.Data.General
{
    public class TaskHeaderManager :DataManager
    {
        #region [ Constructor ]
        public TaskHeaderManager (String ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }
        #endregion

        #region [ Get Data ]
        public List<TaskHeader> GetData()
        {
            String Query = "SELECT * FROM TaskHeader WITH (NOLOCK)";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return DataParser.ToList<TaskHeader>(oDm.GetTable());
        }

        public List<TaskHeader> GetData(Int64 ScheduleID)
        {
            String Query = "SELECT * FROM TaskHeader WITH (NOLOCK) WHERE ScheduleID = @ScheduleID";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddIntegerBigPara("ScheduleID", ScheduleID);
            return DataParser.ToList<TaskHeader>(oDm.GetTable());
        }
        #endregion

        #region [ Insert ]
        public int Insert(Int64 ScheduleID, String TaskName, String TaskDescription, String Site, DateTime ScheduleFrom
            , Boolean TaskRepeat, Int32 TaskRepeatInterval, String TaskRepeatUnit, Boolean Enabled)
        {
            String Query = "INSERT INTO [TaskHeader] ([TaskName], [TaskDescription], [Site], [ScheduleFrom], [TaskRepeat], [TaskRepeatInterval], [TaskRepeatUnit], [Enabled], [CreatedDate]) "
                + "VALUES (@TaskName, @TaskDescription, @Site, @ScheduleFrom, @TaskRepeat, @TaskRepeatInterval, @TaskRepeatUnit, @Enabled, @CreatedDate);";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            //oDm.AddIntegerBigPara("ScheduleID", ScheduleID);
            oDm.AddVarcharPara("TaskName", 4000, TaskName);
            oDm.AddVarcharPara("TaskDescription", 4000, TaskDescription);
            oDm.AddVarcharPara("Site", 4000, Site);
            oDm.AddDateTimePara("ScheduleFrom", ScheduleFrom);
            oDm.AddBoolPara("TaskRepeat", TaskRepeat);
            oDm.AddIntegerPara("TaskRepeatInterval", TaskRepeatInterval);
            oDm.AddVarcharPara("TaskRepeatUnit", 4000, TaskRepeatUnit);
            oDm.AddBoolPara("Enabled", Enabled);
            oDm.AddDateTimePara("CreatedDate", DateTime.Now);
            return oDm.RunActionQuery();
        }
        #endregion

        #region [ Update ]
        public int Update(Int64 ScheduleID, String TaskName, String TaskDescription, String Site, DateTime ScheduleFrom
            , Boolean TaskRepeat, Int32 TaskRepeatInterval, String TaskRepeatUnit, Boolean Enabled)
        {
            String Query = "UPDATE [TaskHeader] SET [TaskName] = @TaskName, [TaskDescription] = @TaskDescription, [Site] = @Site, " 
                + "[ScheduleFrom] = @ScheduleFrom, [TaskRepeat] = @TaskRepeat, [TaskRepeatInterval] = @TaskRepeatInterval, "
                + "[TaskRepeatUnit] = @TaskRepeatUnit, [Enabled] = @Enabled " 
                + "WHERE [ScheduleID] = @ScheduleID";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddIntegerBigPara("ScheduleID", ScheduleID);
            oDm.AddVarcharPara("TaskName", 4000, TaskName);
            oDm.AddVarcharPara("TaskDescription", 4000, TaskDescription);
            oDm.AddVarcharPara("Site", 4000, Site);
            oDm.AddDateTimePara("ScheduleFrom", ScheduleFrom);
            oDm.AddBoolPara("TaskRepeat", TaskRepeat);
            oDm.AddIntegerPara("TaskRepeatInterval", TaskRepeatInterval);
            oDm.AddVarcharPara("TaskRepeatUnit", 4000, TaskRepeatUnit);
            oDm.AddBoolPara("Enabled", Enabled);
            return oDm.RunActionQuery();
        }
        #endregion

        #region [ Save ]
        public int Save(TaskHeader taskHeader)
        {
            return Save(taskHeader.ScheduleID, taskHeader.TaskName, taskHeader.TaskDescription, taskHeader.Site, taskHeader.ScheduleFrom
                , taskHeader.TaskRepeat, taskHeader.TaskRepeatInterval, taskHeader.TaskRepeatUnit, taskHeader.Enabled);
        }

        public int Save(Int64 ScheduleID, String TaskName, String TaskDescription, String Site, DateTime ScheduleFrom
            , Boolean TaskRepeat, Int32 TaskRepeatInterval, String TaskRepeatUnit, Boolean Enabled)
        {
            if (GetData(ScheduleID).Count > 0)
                return Update(ScheduleID, TaskName, TaskDescription, Site, ScheduleFrom, TaskRepeat, TaskRepeatInterval, TaskRepeatUnit, Enabled);
            else
                return Insert(ScheduleID, TaskName, TaskDescription, Site, ScheduleFrom, TaskRepeat, TaskRepeatInterval, TaskRepeatUnit, Enabled);
        }
        #endregion

        #region [ Generate Next Run ]
        public void GenerateScheduleNextRun()
        {
            String Query = String.Empty;
            SqlCeDataManager oDm;
            Query = "UPDATE	[TaskHeader] WITH (ROWLOCK) SET [NextRun] = DATEADD(yy, [TaskRepeatInterval], CASE WHEN [LastRun] IS NULL THEN [ScheduleFrom] ELSE [LastRun] END) WHERE	[TaskRepeatUnit] = 'YEAR' AND (ISNULL([NextRun]) = 1 OR [NextRun] < GETDATE())";
            oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.RunActionQuery();
            Query = "UPDATE	[TaskHeader] WITH (ROWLOCK) SET [NextRun] = DATEADD(q, [TaskRepeatInterval], CASE WHEN [LastRun] IS NULL THEN [ScheduleFrom] ELSE [LastRun] END) WHERE	[TaskRepeatUnit] = 'QUARTER' AND (ISNULL([NextRun]) = 1 OR [NextRun] < GETDATE())";
            oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.RunActionQuery();
            Query = "UPDATE	[TaskHeader] WITH (ROWLOCK) SET [NextRun] = DATEADD(m, [TaskRepeatInterval], CASE WHEN [LastRun] IS NULL THEN [ScheduleFrom] ELSE [LastRun] END) WHERE	[TaskRepeatUnit] = 'MONTH' AND (ISNULL([NextRun]) = 1 OR [NextRun] < GETDATE())";
            oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.RunActionQuery();
            Query = "UPDATE	[TaskHeader] WITH (ROWLOCK) SET [NextRun] = DATEADD(ww, [TaskRepeatInterval], CASE WHEN [LastRun] IS NULL THEN [ScheduleFrom] ELSE [LastRun] END) WHERE	[TaskRepeatUnit] = 'WEEK' AND (ISNULL([NextRun]) = 1 OR [NextRun] < GETDATE())";
            oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.RunActionQuery();
            Query = "UPDATE	[TaskHeader] WITH (ROWLOCK) SET [NextRun] = DATEADD(d, [TaskRepeatInterval], CASE WHEN [LastRun] IS NULL THEN [ScheduleFrom] ELSE [LastRun] END) WHERE	[TaskRepeatUnit] = 'DAY' AND (ISNULL([NextRun]) = 1 OR [NextRun] < GETDATE())";
            oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.RunActionQuery();
            Query = "UPDATE	[TaskHeader] WITH (ROWLOCK) SET [NextRun] = DATEADD(hh, [TaskRepeatInterval], CASE WHEN [LastRun] IS NULL THEN [ScheduleFrom] ELSE [LastRun] END) WHERE	[TaskRepeatUnit] = 'HOUR' AND (ISNULL([NextRun]) = 1 OR [NextRun] < GETDATE())";
            oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.RunActionQuery();
            Query = "UPDATE	[TaskHeader] WITH (ROWLOCK) SET [NextRun] = DATEADD(mi, [TaskRepeatInterval], CASE WHEN [LastRun] IS NULL THEN [ScheduleFrom] ELSE [LastRun] END) WHERE	[TaskRepeatUnit] = 'MINUTE' AND (ISNULL([NextRun]) = 1 OR [NextRun] < GETDATE())";
            oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.RunActionQuery();
        }

        public void GenerateScheduleNextRun(Int64 ScheduleID)
        {
            String Query = String.Empty;
            SqlCeDataManager oDm;
            Query = "UPDATE	[TaskHeader] WITH (ROWLOCK) SET [NextRun] = DATEADD(yy, [TaskRepeatInterval], CASE WHEN [LastRun] IS NULL THEN [ScheduleFrom] ELSE [LastRun] END) WHERE	[TaskRepeatUnit] = 'YEAR' AND (ISNULL([NextRun]) = 1 OR [NextRun] < GETDATE()) AND ScheduleID = @ScheduleID";
            oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddIntegerBigPara("ScheduleID", ScheduleID);
            oDm.RunActionQuery();
            
            Query = "UPDATE	[TaskHeader] WITH (ROWLOCK) SET [NextRun] = DATEADD(q, [TaskRepeatInterval], CASE WHEN [LastRun] IS NULL THEN [ScheduleFrom] ELSE [LastRun] END) WHERE	[TaskRepeatUnit] = 'QUARTER' AND (ISNULL([NextRun]) = 1 OR [NextRun] < GETDATE()) AND ScheduleID = @ScheduleID";
            oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddIntegerBigPara("ScheduleID", ScheduleID);
            oDm.RunActionQuery();

            Query = "UPDATE	[TaskHeader] WITH (ROWLOCK) SET [NextRun] = DATEADD(m, [TaskRepeatInterval], CASE WHEN [LastRun] IS NULL THEN [ScheduleFrom] ELSE [LastRun] END) WHERE	[TaskRepeatUnit] = 'MONTH' AND (ISNULL([NextRun]) = 1 OR [NextRun] < GETDATE()) AND ScheduleID = @ScheduleID";
            oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddIntegerBigPara("ScheduleID", ScheduleID);
            oDm.RunActionQuery();

            Query = "UPDATE	[TaskHeader] WITH (ROWLOCK) SET [NextRun] = DATEADD(ww, [TaskRepeatInterval], CASE WHEN [LastRun] IS NULL THEN [ScheduleFrom] ELSE [LastRun] END) WHERE	[TaskRepeatUnit] = 'WEEK' AND (ISNULL([NextRun]) = 1 OR [NextRun] < GETDATE()) AND ScheduleID = @ScheduleID";
            oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddIntegerBigPara("ScheduleID", ScheduleID);
            oDm.RunActionQuery();

            Query = "UPDATE	[TaskHeader] WITH (ROWLOCK) SET [NextRun] = DATEADD(d, [TaskRepeatInterval], CASE WHEN [LastRun] IS NULL THEN [ScheduleFrom] ELSE [LastRun] END) WHERE	[TaskRepeatUnit] = 'DAY' AND (ISNULL([NextRun]) = 1 OR [NextRun] < GETDATE()) AND ScheduleID = @ScheduleID";
            oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddIntegerBigPara("ScheduleID", ScheduleID);
            oDm.RunActionQuery();

            Query = "UPDATE	[TaskHeader] WITH (ROWLOCK) SET [NextRun] = DATEADD(hh, [TaskRepeatInterval], CASE WHEN [LastRun] IS NULL THEN [ScheduleFrom] ELSE [LastRun] END) WHERE	[TaskRepeatUnit] = 'HOUR' AND (ISNULL([NextRun]) = 1 OR [NextRun] < GETDATE()) AND ScheduleID = @ScheduleID";
            oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddIntegerBigPara("ScheduleID", ScheduleID);
            oDm.RunActionQuery();

            Query = "UPDATE	[TaskHeader] WITH (ROWLOCK) SET [NextRun] = DATEADD(mi, [TaskRepeatInterval], CASE WHEN [LastRun] IS NULL THEN [ScheduleFrom] ELSE [LastRun] END) WHERE	[TaskRepeatUnit] = 'MINUTE' AND (ISNULL([NextRun]) = 1 OR [NextRun] < GETDATE()) AND ScheduleID = @ScheduleID";
            oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddIntegerBigPara("ScheduleID", ScheduleID);
            oDm.RunActionQuery();
        }
        #endregion

        public int DeleteById(Int64 ScheduleID)
        {
            String Query = "DELETE FROM [TaskHeader] WHERE [ScheduleID] = @ScheduleID";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddIntegerBigPara("ScheduleID", ScheduleID);
            return oDm.RunActionQuery();
        }
    }
}