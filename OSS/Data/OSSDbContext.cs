﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OSS.Models;
using AiursoftBase.Models.OSS;

namespace OSS.Data
{
    public class OSSDbContext : IdentityDbContext<OSSUser>
    {
        public DbSet<Bucket> Bucket { get; set; }
        public DbSet<OSSFile> OSSFile { get; set; }
        public DbSet<OSSApp> Apps { get; set; }
        public OSSDbContext(DbContextOptions<OSSDbContext> options) : base(options)
        {

        }
    }
}
