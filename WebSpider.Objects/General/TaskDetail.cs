using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSpider.Objects.General
{
	public class TaskDetail
	{
		public Int64 TaskID { get; set; }
		public Int64 TaskHeaderID { get; set; }
		/// <summary>
		/// Task Display Name
		/// </summary>
		public String TaskNameText { get; set; }

		/// <summary>
		/// Task Value - Category Value or BrandValue
		/// </summary>
		public String TaskNameValue { get; set; }

		/// <summary>
		/// Task Status Text for display
		/// </summary>
		public String TaskStatusText { get; set; }

		/// <summary>
		/// Task Status for searching
		/// </summary>
		public int TaskStatus { get; set; }

		/// <summary>
		/// Download Images - yes/no 
		/// </summary>
		public Boolean DownloadImages { get; set; }

		/// <summary>
		/// Incognito Mode - yes/no
		/// </summary>
		public Boolean IncognitoMode { get; set; }

		/// <summary>
		/// Task Type -- Category/brads/sale
		/// </summary>
		public String TaskType { get; set; }

		/// <summary>
		/// Task Mode - CRAWL, UPDATE
		/// </summary>
		public String TaskMode { get; set; }

		/// <summary>
		/// WebSite - Adiglobal
		/// </summary>
		public String TaskSite  { get; set; }

		/// <summary>
		/// Created Date
		/// </summary>
		public DateTime CreatedOn { get; set; }

		/// <summary>
		/// Updated Date
		/// </summary>
		public DateTime? UpdatedOn { get; set; }
	}
}
