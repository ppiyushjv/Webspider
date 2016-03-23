using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSpider.Data.General;
using WebSpider.Objects;
using WebSpider.Objects.General;

namespace WebSpider.Core
{
    public class TasksScheduler
    {
        TaskDetailManager detailManager;
        TaskHeaderManager thManager;

        public TasksScheduler()
        {
            detailManager = new TaskDetailManager(Constants.ConnectionString);
            thManager = new TaskHeaderManager(Constants.ConnectionString);
        }

        public List<TaskDetail> GetAllTasks()
        {
            return detailManager.GetData();
        }

        public List<TaskDetail> GetAllTasks(Int64 ScheduleID)
        {
            return detailManager.GetDataByHeader(ScheduleID);
        }

        public List<TaskDetail> GetPendingTasks()
        {
            int[] taskStatus = { TaskDetailStatus.Pending, TaskDetailStatus.Open };
            return detailManager.GetDataByStatus(-1, taskStatus);
        }


        public List<TaskDetail> GetAdiPendingTasks()
        {
            int []taskStatus = {TaskDetailStatus.Pending, TaskDetailStatus.Open};
            return detailManager.GetDataBySite(-1, Constants.SiteName.ADIGLOBAL, taskStatus);
        }

        #region [Get Task Detail By Task ID]
        public TaskDetail GetTaskDetailByID(long TaskID)
        {
            List<TaskDetail> tasks = detailManager.GetDataById(TaskID);
            return tasks.Count == 0 ? null : tasks[0];
        }
        #endregion


        #region [Save Task Detail]
        public int SaveTaskDetail(TaskDetail taskDetail)
        {
            return SaveTaskDetail(taskDetail.TaskID, taskDetail.TaskHeaderID, taskDetail.TaskNameText, taskDetail.TaskNameValue, taskDetail.TaskStatusText, taskDetail.TaskStatus, taskDetail.DownloadImages, taskDetail.IncognitoMode, taskDetail.TaskType, taskDetail.TaskMode, taskDetail.TaskSite);
        }

        public int SaveTaskDetail(Int64 TaskID, Int64 TaskHeaderID, String TaskNameText, String TaskNameValue, String TaskStatusText, int TaskStatus, Boolean DownloadImages, Boolean IgnitoMode, String TaskType, String TextMode, String TaskSite)
        {
            if (detailManager.GetDataById(TaskID).Count() == 1)
            {
                // Update
                return detailManager.Update(TaskID, TaskHeaderID, TaskNameText, TaskNameValue, TaskStatusText, TaskStatus, DownloadImages, IgnitoMode, TaskType, TextMode, TaskSite);
            }
            else
            {
                return detailManager.Insert(TaskID, TaskHeaderID, TaskNameText, TaskNameValue, TaskStatusText, TaskStatus, DownloadImages, IgnitoMode, TaskType, TextMode, TaskSite);
            }
        }
        #endregion

        #region [RemoveTasks]
        public int DeleteTaskDetail(TaskDetail taskDetail)
        {
            return DeleteTaskDetail(taskDetail.TaskID);
        }

        public int DeleteTaskDetail(Int64 TaskID)
        {
            return detailManager.DeleteById(TaskID);
        }
        #endregion


        #region [ Scheduler ] 
        public void GenerateSchedules()
        {
            thManager.GenerateScheduleNextRun();
        }
        public List<TaskDetail> GetPendingSchedules()
        {
            return detailManager.GetDataScheduledPendingTasks();
        }
        public List<TaskDetail> GetPendingSchedules(String TaskSite)
        {
            return detailManager.GetDataScheduledPendingTasks(TaskSite);
        }
        #endregion


        public void DeleteTaskHeader(TaskHeader th)
        {
            detailManager.DeleteByHeaderId(th.ScheduleID);
            thManager.DeleteById(th.ScheduleID);
            

        }
    }
}
