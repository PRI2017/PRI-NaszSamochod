﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PRI_NaszSamochód.Models
{
    public class MembersModel
    {   [Key]
        public string Id { get; set; }
        public ApplicationUser User { get; set; }

        public MembersModel()
        {

        }

        public MembersModel(ApplicationUser user)
        {
            Guid guid = Guid.NewGuid();
            Id = guid.ToString();
            User = user;
        }
    }

    public class AdministratorModel
    {
        [Key]
        public string Id { get; set; }
        public ApplicationUser User { get; set; }

        public AdministratorModel()
        {

        }

        public AdministratorModel(ApplicationUser user)
        {
            Guid guid = Guid.NewGuid();
            Id = guid.ToString();
            User = user;
        }
    }
}