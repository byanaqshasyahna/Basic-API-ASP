using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Account
    {
		[Key, Required]
		public string NIK { get; set; }

		[Required]
		public string Password { get; set; }

		public int OTP { get; set; }
		public DateTime ExpiredOTP { get; set; }
		
		public Boolean IsUsed { get; set; }

		[JsonIgnore]
		public virtual ICollection<RoleAccount> RoleAccounts { get; set; }

		[JsonIgnore]
		public virtual Employee Employee { get; set; }

		[JsonIgnore]
		public virtual Profiling Profiling { get; set; }
	}
}
