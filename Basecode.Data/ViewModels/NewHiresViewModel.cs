using Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.ViewModels
{
	public class NewHiresViewModel
	{
		public List<CurrentHires>? CurrentHires { get; set; }
		public List<JobPostings>? jobPostings { get; set; }
	}
}
